#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Collections.Generic;
using System.Net.Sockets;

namespace Maxiploit.Core.Runtime.Networking {
    public abstract class Client {

       
        public string CIPAddress { get; set; }
        public byte[] CPublicKey { get; set; }
        public byte[] CRNonce { get; set; }
        public byte[] CSessionKey { get; set; }
        public byte[] CSharedKey { get; set; }
        public byte[] CSNonce { get; set; }
        public int CState { get; set; }

        public List<byte> DataStream { get; set; }

        public SocketContext SocketContext { get; private set; }

        public readonly long SocketHandle;

        public Client(SocketContext socketContext) {
            SocketContext = socketContext;
            SocketHandle = socketContext.Socket.Handle.ToInt64();
            DataStream = new List<byte>();
            CState = 0;
        }

        public bool IsConnected() {
            try {
                return !((SocketContext.Socket.Poll(1000, SelectMode.SelectRead) && (SocketContext.Socket.Available == 0)) || !SocketContext.Socket.Connected);
            } catch {
                return false;
            }
        }

    }
}
