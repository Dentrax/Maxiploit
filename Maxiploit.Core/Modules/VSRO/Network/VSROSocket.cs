#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Net.Sockets;
using System.Collections.Generic;
using Maxiploit.Runtime.Networking;
using Maxiploit.Runtime.Networking.Securities;
using Maxiploit.Modules.VSRO.API.SilkroadSecurity;
using System.Threading;

namespace Maxiploit.Modules.VSRO.Network {
    public sealed class VSROSocket : SocketContext, ISocketContext {


        private Security m_security;

        protected volatile int m_firedConnectionLost;

        protected bool m_sendingInProgress;

        protected bool m_disconnectAfterTransfer;


        public static EventHandler<Packet> OnPacketReceived;

        public static EventHandler OnLostConnection;

        public static EventHandler<Packet> OnPacketSent;

        public VSROSocket() : base() {

        }

        public VSROSocket(Socket client, SecurityFlags flags) : base() {
            SetSocket(client);
            SetSecurity(flags);
        }

        ~VSROSocket() {
            Dispose(false);
        }

        public override void SetContext(object context) {
            base.SetContext(context);
        }

        public override void SetOwner(TCPServer server) {
            base.SetOwner(server);
        }

        public override void SetSocket(Socket client) {
            m_firedConnectionLost = 0;
            m_disconnectAfterTransfer = false;
            m_sendingInProgress = false;
            base.SetSocket(client);
        }


        public override void Dispose() {
            base.Dispose();
        }

        protected override void Dispose(bool disposing) {
            if (!m_wasDisposed) {
                if (disposing) {
                   //
                }
                if (m_security != null) {
                    m_security.Dispose();
                    m_security = null;
                }
            }
            base.Dispose(disposing);
        }

        public override void Clean() {
            m_security.Dispose();
            m_security = null;
            base.Clean();
        }

        public override void Disconnect() {
            try {
                lock (m_locker) {
                    if (Socket != null) {
                        if (Socket.Connected) {
                            if (m_sendingInProgress) {
                                m_disconnectAfterTransfer = true;
                                return;
                            }
                        }
                        FireOnLostConnection();
                    }
                }
            } catch {
                //TODO: Logger'a hata ekle ?
            }
            base.Disconnect();
        }


        public void SetSecurity(SecurityFlags flags) {
            m_security = new Security();
            m_security.GenerateSecurity(flags.HasFlag(SecurityFlags.Blowfish), flags.HasFlag(SecurityFlags.SecurityBytes), flags.HasFlag(SecurityFlags.Handshake));
            m_security.ChangeIdentity("GatewayServer", 0);
        }

        public void Begin() {
            if (Socket == null || m_security == null) {
                throw new InvalidOperationException("[EasySSA.Core.Network::SocketContext::Begin()] You must set socket & security before calling Begin()");
            }

            SocketError ec;
            Socket.BeginReceive(this.m_buffer, 0, this.m_buffer.Length, SocketFlags.None, out ec, AsyncReceive, null);

            if (ec != SocketError.Success && ec != SocketError.IOPending) {
                Disconnect();
                FireOnLostConnection();
                return;
            }

            ProcessSendQueue();
        }

        //// async/await version
        //static async Task<int> Test1Async(Task<int> task) {
        //    if (task.IsCompleted)
        //        return task.Result;
        //    return await task;
        //}

        //// TPL version
        //static Task<int> Test2Async(Task<int> task) {
        //    if (task.IsCompleted)
        //        return Task.FromResult(task.Result);

        //    return task.ContinueWith(
        //        t => t.Result,
        //        CancellationToken.None,
        //        TaskContinuationOptions.ExecuteSynchronously,
        //        TaskScheduler.Default);
        //}

        public void Send(Packet p) {
            m_security.Send(p);
            ProcessSendQueue();
        }

        public void EnqueuePacket(Packet p) {
            m_security.Send(p);
        }

        public void ProcessSendQueue() {
            try {
                lock (this) {
                    if (m_sendingInProgress)
                        return;

                    if (m_security.HasPacketToSend()) {
                        m_sendingInProgress = true;

                        var packet = m_security.GetPacketToSend();

                        SocketError ec;
                        Socket.BeginSend(packet.Key.Buffer, 0, packet.Key.Size, SocketFlags.None, out ec, AsyncSend, packet);

                        if (ec != SocketError.Success && ec != SocketError.IOPending) {
                            Disconnect();
                            FireOnLostConnection();
                            return;
                        }
                    }
                }
            } catch (Exception ex) {
                //TODO: Get ex?
                Disconnect();
                FireOnLostConnection();
            }
        }


        private void AsyncReceive(IAsyncResult res) {
            try {
                if (!Socket.Connected) {
                    FireOnLostConnection();
                    return;
                }

                SocketError ec;

                int size = Socket.EndReceive(res, out ec);

                if (ec != SocketError.Success) {
                    FireOnLostConnection();
                    return;
                }

                if (size <= 0) {
                    FireOnLostConnection();
                    return;
                }


                m_security.Recv(m_buffer, 0, size);
                List<Packet> packets = m_security.TransferIncoming();
                foreach (var p in packets)
                    FireOnPacketReceived(p);

                Socket.BeginReceive(m_buffer, 0, m_buffer.Length, SocketFlags.None, out ec, AsyncReceive, null);

                if (ec != SocketError.Success && ec != SocketError.IOPending) {
                    Disconnect();
                    FireOnLostConnection();
                    return;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
                Disconnect();
                FireOnLostConnection();
            }
        }

        private void AsyncSend(IAsyncResult res) {
            try {

                lock (this) {

                    if (!Socket.Connected) {
                        FireOnLostConnection();
                        return;
                    }

                    SocketError ec;
                    int size = Socket.EndSend(res, out ec);

                    if (ec != SocketError.Success) {
                        FireOnLostConnection();
                        return;
                    }

                    if (size <= 0) {
                        FireOnLostConnection();
                        return;
                    }

                    KeyValuePair<TransferBuffer, Packet> transferedPacket = (KeyValuePair<TransferBuffer, Packet>)res.AsyncState;
                    FireOnPacketSent(transferedPacket.Value);

                    if (!m_security.HasPacketToSend()) {
                        m_sendingInProgress = false;
                        if (m_disconnectAfterTransfer) {
                            Disconnect();
                        }

                        return;
                    }

                    var packet = m_security.GetPacketToSend();
                    Socket.BeginSend(packet.Key.Buffer, 0, packet.Key.Size, SocketFlags.None, out ec, AsyncSend, packet);

                    if (ec != SocketError.Success && ec != SocketError.IOPending) {
                        Disconnect();
                        FireOnLostConnection();
                        return;
                    }
                }
            } catch (Exception) {
                Disconnect();
                FireOnLostConnection();
            }
        }

        private void FireOnLostConnection() {
            if (Interlocked.CompareExchange(ref m_firedConnectionLost, 1, 0) == 0) {
                if (OnLostConnection != null) {
                    OnLostConnection(this, null);
                }
                this.Clean();
                Owner.SocketContextPool.PutObject(this);
                Interlocked.Decrement(ref Owner.ConnectionCount);
            }
        }

        private void FireOnPacketReceived(Packet p) {
            if (p.Opcode == 0x5000 || p.Opcode == 0x9000) {
                ProcessSendQueue();
            } else {
                if (OnPacketReceived != null) {
                    OnPacketReceived(this, p);
                }
            }
        }

        private void FireOnPacketSent(Packet p) {
            if (OnPacketSent != null) {
                OnPacketSent(this, p);
            }
        }

    }
}
