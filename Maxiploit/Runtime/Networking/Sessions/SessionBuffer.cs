#region License
// ====================================================
// MaxiGuard Copyright(C) 2017 MaxiGame
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace MaxiGuard.Runtime.Networking.Sessions {
    public sealed class SessionBuffer {

        public readonly object Locker = new object();

        public byte[] ClientBuffer { get; private set; }

        public byte[] ModuleBuffer { get; private set; }

        public SessionBuffer(int size) {
            this.ClientBuffer = new byte[size];
            this.ModuleBuffer = new byte[size];
        }
    }
}
