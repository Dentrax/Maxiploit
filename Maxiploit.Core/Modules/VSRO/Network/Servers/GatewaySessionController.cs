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
    public sealed class GatewaySessionController : VSROSessionController {

        private VSROServer.VSROServerDelegate m_onSocketDisconnect;

        public GatewaySessionController(VSROServer.VSROServerDelegate onDisconnect) : base() {
            this.m_onSocketDisconnect = onDisconnect;
        }

        public override bool Start(Socket socket) {
            SessionBuffer buffer = new SessionBuffer(4096);

            Session gateway = new GatewaySession(socket, ref buffer);

            lock (m_locker) {
                m_sessions.Add(gateway);
            }

            if (this.SessionCount >= Settings.MaxConnServer) {
                Destroy(gateway, true);
                return false;
            }

            if (Utility.NetHelper.ConnectionsToLocalPortCount(Settings.BindPort, Utility.NetHelper.GetIpString(client)) > Settings.MaxConnPerIp) {
                this.Destroy(gateway, true);
                return false;
            }


            return gateway.Start();

        }

        public override bool Destroy(Session session, bool requireStop) {
            return base.Destroy(session, requireStop);
        }

    }
}
