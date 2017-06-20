#region License
// ====================================================
// MaxiGuard Copyright(C) 2017 MaxiGame
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace MaxiGuard.Runtime.Networking.Securities {
    public enum SecurityFlags {
        None = 0,
        Handshake = 1,
        Blowfish = 2,
        SecurityBytes = 4,
    }
}
