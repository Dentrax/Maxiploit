#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Net.Sockets;

namespace Maxiploit.Core.Runtime.Networking {
    public interface ISocketContext {
        void SetContext(object context);
        void SetOwner(TCPServer server);
        void SetSocket(Socket client);
        void Clean();
        void Disconnect();
    }
}
