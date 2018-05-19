#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Sockets;

//Reference -> http://dotnetpattern.com/threading-manualresetevent

namespace Maxiploit.Core.Runtime.Networking.Sessions {
    public abstract class SessionTaskController {

        private List<Task> m_tasks;
        protected List<ManualResetEvent> m_tasksStarted;
        protected CancellationTokenSource m_cancelToken;
        protected List<uint> m_taskAcceptedWorkCount;
        protected List<SessionWorkItem> m_workToAssign;
        private Task m_sessionPoll;
        protected ManualResetEvent m_sessionPollStarted;

        protected int m_sessionProcThreadCount;

        protected readonly object m_locker = new object();

        public SessionTaskController(int threadCount) {
            this.m_sessionProcThreadCount = threadCount;
        }

        public virtual void Initialize() {
            m_tasks = new List<Task>();
            m_tasksStarted = new List<ManualResetEvent>(this.m_sessionProcThreadCount);
            m_cancelToken = new CancellationTokenSource();
            m_taskAcceptedWorkCount = new List<uint>(this.m_sessionProcThreadCount);
            m_workToAssign = new List<SessionWorkItem>();
            m_sessionPollStarted = new ManualResetEvent(false);


            //test
            for (int index = 0; index < this.m_sessionProcThreadCount; index++) {
                m_tasksStarted.Add(new ManualResetEvent(false));
                m_taskAcceptedWorkCount.Add(0);

                Task task = new Task(() => SessionTaskWorker(index), m_cancelToken.Token, TaskCreationOptions.LongRunning);

                task.Start();

                m_tasksStarted[index].WaitOne();

                m_tasks.Add(task);
            }

            //Start session activity poller task
            m_sessionPoll = new Task(() => SessionPollWorker(), m_cancelToken.Token, TaskCreationOptions.LongRunning);
            m_sessionPoll.Start();
            m_sessionPollStarted.WaitOne();
        }

        protected abstract void SessionTaskWorker(int index);

        protected abstract void SessionPollWorker();

    }
}
