#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using Maxiploit.Modules.VSRO.API.SilkroadSecurity;
using Maxiploit.Modules.VSRO.Data;
using Maxiploit.Runtime.Networking.Sessions;
using Maxiploit.Runtime.Networking;
using Maxiploit.Core.Utils;
using System.Collections.Generic;
using Maxiploit.Modules.VSRO.Packets;
using Maxiploit.Modules.VSRO.Exploit;

namespace Maxiploit.Modules.VSRO.Network.Servers {
    public sealed class AgentSession : VSROSession, IVSROSession {

        //TODO: Performans artışı için SocketPool - ObjectPoolSystem lazım !!!

        private VSROServer m_server;

        public int ActiveConnectionCount => Interlocked.Exchange(ref m_server.ConnectionCount_AGENT, m_server.ConnectionCount_AGENT);

        //Agent koruma kodları buraya
        public AgentSession(Socket clientSocket, ref SessionBuffer buffer) : base(clientSocket, buffer) {
            this.ServerType = Data.ServerType.AGENT;
            //this.m_socketContextPool = new ObjectPool<VSROSocket>(() => new VSROSocket(), () => m_socketContextPool.Count > 200);


            try {
                //m_moduleSocket.Connect(new IPEndPoint(IPAddress.Parse(Squirrel.cRedirAsHost), Squirrel.cRealAgentPort));
                

                //m_clientSecurity.GenerateSecurity(true, true, false);
                ////  m_LocalSecurity.GenerateSecurity(true, true, false);
                //m_TransferPoolThread = new Thread(TransferPool);
                //m_TransferPoolThread.Start();

                //DoRecvFromServer();
                //DoRecvFromClient();


            } catch (Exception ex) {
                //Console.WriteLine("Failed to connect to module [agent] ! Last packets dump [newest on buttom]");
                //DumpLastPackets();
            }
        }



        public override bool Start() {
            this.LastActivity = DateTime.Now;

            //this.m_moduleSocket = NetworkHelper.Connect(Settings.DestIp, Settings.DestPort, Settings.ModuleConnTimeout);

            if (m_moduleSocket == null) {
                Stop();

                //SessionManager.DestroySession(this, false);
                return false;
            }

            this.IsRunning = true;

            this.DoRecvFromClient();
            this.DoRecvFromModule();

            return true;

        }

        public override bool Stop() {
            bool flag1 = false;
            bool flag2 = false;

            try {
                this.m_clientSocket.Close();
                if (!this.m_clientSocket.Connected) {
                    flag1 = true;
                }
            } catch { }

            try {
                this.m_moduleSocket.Close();
                if (!this.m_moduleSocket.Connected) {
                    flag2 = true;
                }
            } catch { }

            if(flag1 && flag2) {
                this.IsRunning = false;
                return true;
            }

            return false;
        }
        public override bool Destroy(Session session, bool stopRequired) {
            //Destroy codes


            //if (Interlocked.CompareExchange(ref m_FiredConnectionLost, 1, 0) == 0) {
            //    if (OnLostConnection != null)
            //        OnLostConnection(this, null);
            //    this.Clean();
            //    m_Owner.SocketContextPool.PutObject(this);
            //    Interlocked.Decrement(ref m_Owner.m_ConnectionCount);
            //}



            return false;
        }

        public override void ActivityTick() {
            this.LastActivity = DateTime.Now;
        }


    }
}
