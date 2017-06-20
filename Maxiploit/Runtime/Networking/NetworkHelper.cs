#region License
// ====================================================
// MaxiGuard Copyright(C) 2017 MaxiGame
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace MaxiGuard.Runtime.Networking {
    public static class NetworkHelper {

        public static Socket Connect(string hostname, int port, int timeout) {
            IPAddress ip = ParseToIPAdress(hostname);

            if (ip == null) {
                return null;
            }

            if (port <= 0 || port >= 65535) {
                return null;
            }

            if (timeout <= 0) {
                return null;
            }

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            DateTime beginTime = DateTime.Now;

            try {
                IAsyncResult iar = socket.BeginConnect(new IPEndPoint(ip, port), null, null);

                if (iar.AsyncWaitHandle.WaitOne(timeout)) {
                    socket.EndConnect(iar);
                    return socket;
                } else {
                    socket.Close();
                }
            } catch { }

            return null;
        }

        public static IPAddress ParseToIPAdress(string ipOrHostname) {

            IPAddress ip;
            bool isIp = IPAddress.TryParse(ipOrHostname, out ip);

            if (isIp) {
                return ip;
            }

            try {
                var hostEntry = Dns.GetHostEntry(ipOrHostname);
                ip = hostEntry.AddressList.First();

                if (!ip.ToString().Contains('.')) {
                    return null;
                }

                return ip;
            } catch { }

            return null;
        }

    }

}
