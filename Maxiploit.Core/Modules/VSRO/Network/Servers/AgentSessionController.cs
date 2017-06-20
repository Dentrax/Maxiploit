#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Net.Sockets;
using Maxiploit.Runtime.Networking.Sessions;

namespace Maxiploit.Modules.VSRO.Network.Servers {
    public sealed class AgentSessionController : VSROSessionController {

        private VSROServer.VSROServerDelegate m_onSocketDisconnect;

        public AgentSessionController(VSROServer.VSROServerDelegate onDisconnect) : base() {
            this.m_onSocketDisconnect = onDisconnect;
        }

        public override bool Create(Socket socket) {
            SessionBuffer buffer = new SessionBuffer(4096);

            Session agent = new AgentSession(socket, ref buffer);

            lock (m_locker) {
                m_sessions.Add(agent);
            }

            if (this.SessionCount >= Settings.MaxConnServer) {
                Destroy(agent, true);
                return false;
            }

            if (Utility.NetHelper.ConnectionsToLocalPortCount(Settings.BindPort, Utility.NetHelper.GetIpString(client)) > Settings.MaxConnPerIp) {
                this.Destroy(agent, true);
                return false;
            }


            return agent.Start();

        }

        public override bool Destroy(Session session, bool requireStop) {
            return base.Destroy(session, requireStop);
        }

    }
}
