#region License
// ====================================================
// MaxiGuard Copyright(C) 2017 MaxiGame
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Net.Sockets;

namespace MaxiGuard.Runtime.Networking {
    public interface ISocketContext {
        void SetContext(object context);
        void SetOwner(TCPServer server);
        void SetSocket(Socket client);
        void Clean();
        void Disconnect();
    }
}
