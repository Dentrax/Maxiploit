#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using Maxiploit.Modules.VSRO.Data;
using Maxiploit.Runtime.Networking.Sessions;
using Maxiploit.Modules.VSRO.API.SilkroadSecurity;

namespace Maxiploit.Modules.VSRO.Network {
    public interface IVSROSession : ISession {
        void SendPacket(Packet packet, PacketDirectionType direction);
    }
}
