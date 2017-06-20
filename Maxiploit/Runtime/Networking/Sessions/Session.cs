#region License
// ====================================================
// MaxiGuard Copyright(C) 2017 MaxiGame
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace MaxiGuard.Runtime.Networking.Sessions {
    public abstract class Session {
        public bool IsRunning { get; protected set; }


        public DateTime LastActivity { get; protected set; }

        public DateTime StartTime { get; protected set; }

        //public SessionState State { get; protected set; }

        protected SessionBuffer m_buffer;

        public Session() {
            this.IsRunning = false;

            this.StartTime = DateTime.Now;
            this.LastActivity = DateTime.Now;
        }

        public abstract bool Start();
        public abstract bool Stop();
    }
}
