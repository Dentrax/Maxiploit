#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using Maxiploit.Modules.VSRO.Data;

namespace Maxiploit.Modules.VSRO.Packets {
    public struct PacketHandlerInfo {

        public PacketDirectionType Direction { get; private set; }

        public ushort Opcode { get; private set; }

        public Processor.PacketProcessor.PacketHandler Handler { get; private set; }

        public PacketHandlerInfo(PacketDirectionType direction, ushort opcode, Processor.PacketProcessor.PacketHandler handler) {
            this.Direction = direction;
            this.Opcode = opcode;
            this.Handler = handler;
        }

        public override bool Equals(object obj) {
            if (!(obj is PacketHandlerInfo)) {
                return false;
            }

            PacketHandlerInfo mys = (PacketHandlerInfo)obj;

            if (mys.Equals(default(PacketHandlerInfo)) || obj.Equals(default(PacketHandlerInfo))) {
                return false;
            }

            return mys.Direction == this.Direction && mys.Opcode == this.Opcode;

        }

        public override int GetHashCode() {
            unchecked {
                int h = 17;
                h = h * 23 + Direction.GetHashCode();
                h = h * 23 + Opcode.GetHashCode();
                return h;
            }
        }
    }
}
