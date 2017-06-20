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
using System.Collections.Generic;
using Maxiploit.Runtime.Networking.Sessions;
using Maxiploit.Modules.VSRO.Data;
using Maxiploit.Modules.VSRO.Network.Servers;

namespace Maxiploit.Modules.VSRO.Network {
    public sealed class VSROSessionTaskController : SessionTaskController {

        public ServerType ServerType { get; private set; }

        private GatewaySessionController m_gatewaySessionController;

        private AgentSessionController m_agentSessionController;

        private Random m_random;


        public VSROSessionTaskController(int threadCount, ServerType taskServerType) : base(threadCount) {
            this.ServerType = taskServerType;
            this.m_random = new Random();

            switch (this.ServerType) {
                case ServerType.GATEWAY:
                    this.m_gatewaySessionController = new GatewaySessionController();
                    break;
                case ServerType.AGENT:
                    this.m_agentSessionController = new AgentSessionController();
                    break;
                default:
                    Console.WriteLine("sadsadsadsa");
                    break;
            }
          
        }

        public override void Initialize() {
            base.Initialize();
        }

        protected override void SessionTaskWorker(int index) {
            this.m_tasksStarted[index].Set();

            List<SessionWorkItem> tmpWork = new List<SessionWorkItem>();

            while (!m_cancelToken.Token.WaitHandle.WaitOne(1)) {
                lock (m_locker) {

                    for (int i = 0; i < m_workToAssign.Count; i++) {
                        if (m_workToAssign[i].TaskIndex == index) {
                            tmpWork.Add(m_workToAssign[i]);
                        }
                    }

                    for (int i = 0; i < tmpWork.Count; i++) {
                        m_workToAssign.Remove(tmpWork[i]);

                        this.CreateSession(tmpWork[i].ClientSock);

                    }

                    tmpWork.Clear();
                }

            }

            m_cancelToken.Token.ThrowIfCancellationRequested();
        }

        public void StartVSROClient(Socket clientSock) {
            int randIndex = this.m_random.Next(0, this.m_sessionProcThreadCount);

            this.m_taskAcceptedWorkCount[randIndex]++;

            SessionWorkItem work = new SessionWorkItem(clientSock, randIndex);

            lock (this.m_locker) {
                this.m_workToAssign.Add(work);
            }
        }

        protected override void SessionPollWorker() {
            m_sessionPollStarted.Set();

            while (!m_cancelToken.Token.WaitHandle.WaitOne(1)) {
                this.FindAndDropInactiveSessions();
            }
        }

        private void CreateSession(Socket socket) {
            switch (this.ServerType) {
                case ServerType.GATEWAY:
                    this.m_gatewaySessionController.Create(socket);
                    break;
                case ServerType.AGENT:
                    this.m_agentSessionController.Create(socket);
                    break;
                default:

                    Console.WriteLine("AsyncServer::AcceptConnectionCallback()::Unknown server type");
                    break;
            }
        }

        private void FindAndDropInactiveSessions() {
            switch (this.ServerType) {
                case ServerType.GATEWAY:
                    this.m_gatewaySessionController.FindAndDropInactiveSessions();
                    break;
                case ServerType.AGENT:
                    this.m_agentSessionController.FindAndDropInactiveSessions();
                    break;
                default:

                    Console.WriteLine("AsyncServer::AcceptConnectionCallback()::Unknown server type");
                    break;
            }
        }
    }
}
