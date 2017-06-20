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
using Maxiploit.Modules.VSRO.API.SilkroadSecurity;
using Maxiploit.Modules.VSRO.Data;
using Maxiploit.Runtime.Networking.Sessions;

namespace Maxiploit.Modules.VSRO.Network.Servers {
    public sealed class GatewaySession : VSROSession {
        public GatewaySession(Socket clientSocket, ref SessionBuffer buffer) : base(clientSocket, buffer) {
        }

        //Gateway koruma kodları buraya
        public override void ActivityTick() {
            throw new NotImplementedException();
        }

        public override bool Destroy(Session session, bool stopRequired) {
            throw new NotImplementedException();
        }

        public override bool Start() {
            throw new NotImplementedException();
        }

        public override bool Stop() {
            throw new NotImplementedException();
        }

        protected override void DoRecvFromClient() {
            throw new NotImplementedException();
        }

        protected override void DoRecvFromModule() {
            throw new NotImplementedException();
        }

        protected override void HandleTransferPacket(Packet packet, PacketDirectionType direction) {
            throw new NotImplementedException();
        }
    }
}
