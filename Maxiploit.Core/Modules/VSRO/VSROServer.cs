#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Net;
using Maxiploit.Runtime.Networking;
using Maxiploit.Modules.VSRO.Data;
using System.Net.Sockets;
using System.Threading;
using Maxiploit.Common.Log;
using Maxiploit.Modules.VSRO.Network.Servers;

namespace Maxiploit.Modules.VSRO {
    public sealed class VSROServer : TCPServer {

        public delegate void VSROServerDelegate(ref Socket ClientSocket, ServerType serverType);

        public event VSROServerDelegate OnClientConnect;
        public event VSROServerDelegate OnClientDisconnect;

        public ServerType ServerType { get; private set; }

        //public VSROSessionTaskController GatewaySessionController { get; private set; }
        //public VSROSessionTaskController AgentSessionController { get; private set; }

        //private GatewaySessionController m_gatewaySessionController;

        //private AgentSessionController m_agentSessionController;

        public volatile int ConnectionCount_AGENT;

        public volatile int ConnectionCount_GATEWAY;


        public VSROServer(string ip, int port, ServerType serverType) : base(ip, port) {
            this.ServerType = serverType;

            //TODO: threadCount çek, 
            //this.GatewaySessionController = new VSROSessionTaskController(4, ServerType.GATEWAY);
            //this.AgentSessionController = new VSROSessionTaskController(4, ServerType.AGENT);


            //this.ClientCount.AddOrUpdate(ServerType.AGENT, 0);

        }

        public override bool Start() {
            bool flag = false;

            if (this.m_listenerSocket != null) {
                throw new Exception("AsyncServer::Trying to start server on socket which is already in use");
            }


            this.m_listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //TODO: Backlog 'u settings 'den al 
            try {
                this.m_listenerSocket.Bind(new IPEndPoint(IPAddress.Parse(this.m_ip), this.m_port));
                this.m_listenerSocket.Listen(5);

                this.m_socketAccepterThread = new Thread(DOAccept);
                this.m_socketAccepterThread.Start();

                Console.WriteLine("AsyncServer::Start():: Success [{0}]", this.ServerType.ToString());

                flag = true;
            } catch (SocketException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::Start()::0xVS1D100] -> Could not bind/listen socket!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (Exception ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::Start()::0xVS1D101] -> Could not bind/listen socket!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            }

            return flag;
        }

        public override bool Stop() {
            throw new NotImplementedException();
        }



        private void DOAccept() {
            while (this.m_listenerSocket != null) {

                this.m_manualResetEvent.Reset();

                try {
                    this.m_listenerSocket.BeginAccept(new AsyncCallback(BeginAcceptCallback), null);
                } catch (ObjectDisposedException ex) {
                    Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D200] -> There was an error getting socket connection!");
                    if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
                } catch (NotSupportedException ex) {
                    Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D201] -> There was an error getting socket connection!");
                    if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
                } catch (InvalidOperationException ex) {
                    Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D202] -> There was an error getting socket connection!");
                    if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
                } catch (ArgumentOutOfRangeException ex) {
                    if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
                    Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D203] -> There was an error getting socket connection!");
                    if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
                } catch (SocketException ex) {
                    Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D204] -> There was an error getting socket connection!");
                    if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
                } catch (Exception ex) {
                    Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D205] -> There was an error getting socket connection!");
                    if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
                }

                this.m_manualResetEvent.WaitOne();

            }
        }

        private void BeginAcceptCallback(IAsyncResult iar) {
            Socket socket = null;

            this.m_manualResetEvent.Set();

            try {
                socket = this.m_listenerSocket.EndAccept(iar);

                //TODO: Debug build de
                //Console.WriteLine("Socket accepted: {0}", ((IPEndPoint)(ClientSocket.RemoteEndPoint)).Port);
            } catch (ArgumentNullException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D300] -> There was an error getting socket connection!");
                if(Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (ArgumentException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D301] -> There was an error getting socket connection!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (SocketException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D302] -> There was an error getting socket connection!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (ObjectDisposedException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D303] -> There was an error getting socket connection!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (InvalidOperationException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D304] -> There was an error getting socket connection!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (NotSupportedException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D305] -> There was an error getting socket connection!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (Exception ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D306] -> There was an error getting socket connection!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            }

            try {
                switch (this.ServerType) {
                    case ServerType.GATEWAY: {
                            new GatewaySessionController(new VSROServerDelegate(this.RaiseOnClientDisconnect)).Start(socket);
                           
                            Interlocked.Increment(ref ConnectionCount_GATEWAY);
                        }
                        break;
                    case ServerType.AGENT: {
                            new AgentSessionController(new VSROServerDelegate(this.RaiseOnClientDisconnect)).Start(socket);

                            Interlocked.Increment(ref ConnectionCount_AGENT);
                        }
                        break;
                    default: {
                            Logger.VSRO.Print(LogLevel.Warning, "[VSROServer::DOAccept()::0xVS1D310] -> Unknown server type handled : " + this.ServerType);
                        }
                        break;
                }
            } catch (SocketException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D307] -> Error while starting context!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (Exception ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::DOAccept()::0xVS1D308] -> Error while starting context!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            }

            this.RaiseOnSocketEvent();
        }

        private void RaiseOnClientConnected(ref Socket clientSocket, ServerType serverType) {
            if (clientSocket == null) return;
            //TODO: Boşmu galacaq
            this.RaiseOnSocketEvent();
        }

        private void RaiseOnClientDisconnect(ref Socket clientSocket, ServerType serverType) {
            if (clientSocket == null) return;

            switch (serverType) {
                case ServerType.GATEWAY: {
                        Interlocked.Decrement(ref ConnectionCount_GATEWAY);
                    }
                    break;
                case ServerType.AGENT: {
                        Interlocked.Decrement(ref ConnectionCount_AGENT);
                    }
                    break;
            }

            try {
                clientSocket.Close();
            } catch (SocketException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::RaiseOnClientConnected()::0xVS1D400] -> Error closing socket!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (ObjectDisposedException ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::RaiseOnClientConnected()::0xVS1D401] -> Error closing socket!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            } catch (Exception ex) {
                Logger.VSRO.Print(LogLevel.Error, "[VSROServer::RaiseOnClientConnected()::0xVS1D402] -> Error closing socket!");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
            }


            clientSocket = null;
            GC.Collect();

            this.RaiseOnSocketEvent();
        }


        //TODO: Update shits
        private void RaiseOnSocketEvent() {
            Console.Title = string.Format("Client count [GatewayServer: {0}] [AgentServer: {1}]", ConnectionCount_GATEWAY, ConnectionCount_AGENT);
        }

    }
}
