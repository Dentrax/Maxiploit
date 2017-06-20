#region License
// ====================================================
// MaxiGuard Copyright(C) 2017 MaxiGame
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Net;
using System.Net.Sockets;
using System.Threading;
using MaxiGuard.Core.Utils;
using MaxiGuard.Runtime.Networking.Securities;

namespace MaxiGuard.Runtime.Networking {
    public abstract class AsyncServer {

        public bool WasConnectionAllowed { get; protected set; }

        protected Socket m_listenerSocket;

        protected SecurityFlags m_securityFlags;

        public volatile int ConnectionCount;

        protected ManualResetEvent m_manualResetEvent = new ManualResetEvent(false);

        protected Thread m_socketAccepterThread = null;

        protected string m_ip;
        protected int m_port;


        public abstract bool Start();

        public abstract bool Stop();

        //ANTI-CPU LOAD RELAX
        //        var delay = TimeSpan.FromMilliseconds(50);
        //while (true) {
        // await Task.Delay(delay);
        //        await SendMessageAsync(mySocket, someData);
        //        await ReceiveReplyAsync(mySocket);
        //    }


        //TODO
        //    Check if client is connected
        //    Poll the client
        //Check if data is available
        //Read the data
        //Go back to step 1

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/20f737aa-6c45-4e9f-b627-d3128d31de61/multi-threaded-server-socket-for-multiple-clients-threading-and-send-receive-issue?forum=csharpgeneral

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]

        public AsyncServer(string ip, int port) {
            this.m_ip = ip;
            this.m_port = port;
        }
    }
}
