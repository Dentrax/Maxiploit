#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace Maxiploit.Runtime.Networking.Sessions {
    public interface ISession {
        bool IsRunning { get; }
        bool Start();
        bool Stop();
        bool Destroy(Session session, bool stopRequired);
        void ActivityTick();
    }
}
