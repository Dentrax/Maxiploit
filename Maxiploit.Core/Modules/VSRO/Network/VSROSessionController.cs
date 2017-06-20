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
using System;

namespace Maxiploit.Modules.VSRO.Network {
    public abstract class VSROSessionController : SessionController {

        public VSROSessionController() : base() {

        }

        public override bool Start(Socket socket) {
            return false;
        }

        public override bool Destroy(Session session, bool requireStop) {
            return base.Destroy(session, requireStop);
        }
    }
}
