#region License
// ====================================================
// MaxiGuard Copyright(C) 2017 MaxiGame
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Net.Sockets;

namespace MaxiGuard.Runtime.Networking.Sessions {
    public sealed class SessionWorkItem {

        public Socket ClientSock { get; private set; }

        public int TaskIndex { get; private set; }

        private SessionWorkItem() {
            //TODO: Boş mu koyçan ? :(
        }

        public SessionWorkItem(Socket clientSock, int taskIndex) {
            this.ClientSock = clientSock;
            this.TaskIndex = taskIndex;
        }
    }
}
