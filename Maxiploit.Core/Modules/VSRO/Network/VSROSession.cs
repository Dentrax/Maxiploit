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
using Maxiploit.Runtime.Networking.Sessions;
using Maxiploit.Modules.VSRO.API.SilkroadSecurity;
using Maxiploit.Modules.VSRO.Data;
using Maxiploit.Core.Utils;
using System.Collections.Generic;

namespace Maxiploit.Modules.VSRO.Network {
    public abstract class VSROSession : Session, ISession {
        protected Socket m_localSocket;
        protected Socket m_remoteSocket;

        protected Security m_clientSecurity;
        protected Security m_moduleSecurity;

        protected TransferBuffer m_clientBuffer;
        protected TransferBuffer m_moduleBuffer;

        public ServerType ServerType { get; protected set; }

        protected ulong m_bytesPerSecondFromClient = 0;


        protected ObjectPool<VSROSocket> m_socketContextPool;

        public ObjectPool<VSROSocket> SocketContextPool => m_socketContextPool;


        protected readonly object m_Lock = new object();

        public VSROSession(Socket clientSocket, SessionBuffer buffer) : base() {
            this.ServerType = ServerType.NONE;
            this.m_localSocket = clientSocket;

            this.m_clientSecurity = new Security();
            this.m_moduleSecurity = new Security();

            this.m_clientBuffer = new TransferBuffer(0x10000, 0, 0);
            this.m_moduleBuffer = new TransferBuffer(0x10000, 0, 0);

            this.m_clientSecurity.GenerateSecurity(true, true, true);

            //this.State = new SessionState();
            //this.State.Set<string>("ip", Utility.NetHelper.GetIpString(client));


            this.m_remoteSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }


        public abstract bool Destroy(Session session, bool stopRequired);
        public abstract void ActivityTick();


        protected void DoRecvFromClient() {
            try {
                this.m_localSocket.BeginReceive(this.m_buffer.ClientBuffer, 0, this.m_buffer.ClientBuffer.Length, SocketFlags.None, (iar) => {
                    try {
                        int recvCount = 0;

                        lock (this.m_buffer.Locker) {
                            recvCount = this.m_localSocket.EndReceive(iar);
                        }

                        ActivityTick();

                        if (recvCount == 0) {
                            this.Stop();
                            this.Destroy(this, false);
                            //return from anonymous method, but not from DoRecvFromClient ;)
                            return;
                        }

                        //TODO: Add SSA handling here
                        this.m_clientSecurity.Recv(this.m_buffer.ClientBuffer, 0, recvCount);

                        List<Packet> packets = this.m_clientSecurity.TransferIncoming();
                        if (packets != null) {
                            //...

                            for (int i = 0; i < packets.Count; i++) {
                                Packet pck = packets[i];

                                //Add to last packets
                                CrashHandler.AddLast(pck);

                                if (pck.Opcode == 0x9000 || pck.Opcode == 0x5000 || pck.Opcode == 0x2001) {
                                    continue;
                                }

                                HandleTransferPacket(pck, PacketDirectionType.TO_MODULE);
                            }

                        }

                        this.TransferToModule();
                        this.DoRecvFromClient();
                    } catch {
                        this.Stop();
                        this.Destroy(this, false);
                    }
                }, null);
            } catch {
                this.Stop();
                this.Destroy(this, false);
            }
        }

        protected void DoRecvFromModule() {
            try {
                this.m_remoteSocket.BeginReceive(this.m_buffer.ModuleBuffer, 0, this.m_buffer.ModuleBuffer.Length, SocketFlags.None, (iar) => {
                    try {
                        int recvCount = 0;

                        lock (this.m_buffer.Locker) {
                            recvCount = this.m_remoteSocket.EndReceive(iar);
                        }

                        ActivityTick();

                        if (recvCount == 0) {
                            this.Stop();
                            this.Destroy(this, false);
                            //return from anonymous method, but not from DoRecvFromModule ;)
                            return;
                        }

                        lock (this.m_buffer.Locker) {
                            this.m_moduleSecurity.Recv(this.m_buffer.ModuleBuffer, 0, recvCount);
                        }

                        //Log.WriteCli(LogType.Notify, "ToClient recv {0}", recvCount);
                        List<Packet> packets = this.m_moduleSecurity.TransferIncoming();
                        if (packets != null) {
                            for (int i = 0; i < packets.Count; i++) {
                                Packet pck = packets[i];
                                if (pck.Opcode == 0x9000 || pck.Opcode == 0x5000) {
                                    continue;
                                }
                                HandleTransferPacket(pck, PacketDirectionType.TO_CLIENT);
                            }

                        }

                        //If any data in SSA
                        this.TransferToClient();
                        this.DoRecvFromModule();
                    } catch {
                        this.Stop();
                        this.Destroy(this, false);
                    }
                }, null);
            } catch {
                this.Stop();
                this.Destroy(this, false);
            }
        }

        protected void HandleTransferPacket(Packet packet, PacketDirectionType direction) {
            PacketHandler handler = PacketManager.GetHandler(dir, packet.Opcode);
            PacketOperationResult res = handler(packet, this);

            Security context = (direction == PacketDirectionType.TO_CLIENT) ? this.m_clientSecurity : this.m_moduleSecurity;

            switch (res.ResultType) {
                case PacketOperationType.REPLACE:
                    res.Packets.ForEach(userPck => {
                        context.Send(userPck);
                    });
                    break;
                case PacketOperationType.INJECT:
                    context.Send(packet);
                    res.Packets.ForEach(userPck => {
                        context.Send(userPck);
                    });
                    break;
                case PacketOperationType.DISCONNECT:
                    this.Stop();
                    this.Destroy(this, false);
                    break;
                case PacketOperationType.IGNORE:
                    break;
                //TODO: EKLE
                case PacketOperationType.BLOCK_IP:
                    this.Stop();
                    this.Destroy(this, false);
                    break;
                case PacketOperationType.SPECIAL:
                    break;
                case PacketOperationType.NOTHING:
                    context.Send(packet);
                    break;
                case PacketOperationType.NONE:
                    break;
                default:
                    break;
            }

            switch (direction) {
                case PacketDirectionType.TO_CLIENT:
                    this.TransferToClient();
                    break;
                case PacketDirectionType.TO_MODULE:
                    this.TransferToModule();
                    break;
            }
        }

        protected void DoSendToClient(byte[] buffer, int len) {
            try {
                this.m_localSocket.BeginSend(buffer, 0, len, SocketFlags.None,
                    (iar) => {
                        try {
                            int sentCount = this.m_localSocket.EndSend(iar);
                            ActivityTick();
                        } catch {
                            this.Stop();
                            this.Destroy(this, false);
                        }
                    }, null);
            } catch {
                this.Stop();
                this.Destroy(this, false);
            }
        }

        protected void DoSendToModule(byte[] buffer, int len) {
            try {
                this.m_remoteSocket.BeginSend(buffer, 0, len, SocketFlags.None,
                    (iar) => {
                        try {
                            int sentCount = this.m_remoteSocket.EndSend(iar);
                            ActivityTick();
                        } catch {
                            this.Stop();
                            this.Destroy(this, false);
                        }
                    }, null);
            } catch {
                this.Stop();
                this.Destroy(this, false);
            }
        }

        protected void TransferToClient() {
            try {
                var kvp = this.m_clientSecurity.TransferOutgoing();
                if (kvp != null) {
                    kvp.ForEach(item => {
                        DoSendToClient(item.Key.Buffer, item.Key.Buffer.Length);
                    });
                }
            } catch {
                this.Stop();
                this.Destroy(this, false);
            }
        }


        protected void TransferToModule() {
            try {
                var kvp = this.m_moduleSecurity.TransferOutgoing();
                if (kvp != null) {
                    kvp.ForEach(item => {
                        DoSendToModule(item.Key.Buffer, item.Key.Buffer.Length);
                    });
                }
            } catch {
                this.Stop();
                this.Destroy(this, false);
            }
        }

        public void SendPacket(Packet packet, PacketDirectionType direction) {
            if (direction == PacketDirectionType.TO_CLIENT) {
                this.m_clientSecurity.Send(packet);
                this.TransferToClient();
            }

            if (direction == PacketDirectionType.TO_MODULE) {
                this.m_moduleSecurity.Send(packet);
                this.TransferToModule();
            }
        }

    }
}
