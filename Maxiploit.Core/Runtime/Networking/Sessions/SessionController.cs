#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Net.Sockets;
using System.Collections.Generic;
using System;

namespace Maxiploit.Core.Runtime.Networking.Sessions {
    public abstract class SessionController {

        protected List<Session> m_sessions = new List<Session>();
        protected readonly object m_locker = new object();

        //TODO: interlocked al
        public int SessionCount {
            get {return m_sessions.Count; }


        }

        public SessionController() {

        }

        public abstract bool Start(Socket socket);


        public virtual bool Destroy(Session session, bool requireStop) {
            lock (this.m_locker) {
                if (!this.m_sessions.Contains(session)) {
                    return false;
                }
                this.m_sessions.Remove(session);
            }

            //DestroySession() called from session itself
            if (requireStop) {
                session.Stop();
            }

            return true;
        }

        public void FindAndDropInactiveSessions() {
            lock (this.m_locker) {
                List<Session> sessionsToDrop = new List<Session>();
                for (int i = 0; i < m_sessions.Count; i++) {
                    TimeSpan diff = DateTime.Now - m_sessions[i].LastActivity;
                    if (diff.TotalMilliseconds >= 5000) {
                        sessionsToDrop.Add(m_sessions[i]);
                    }

                }

                for (int i = 0; i < sessionsToDrop.Count; i++) {
                    this.Destroy(sessionsToDrop[i], true);
                }
            }
        }

    }
}
