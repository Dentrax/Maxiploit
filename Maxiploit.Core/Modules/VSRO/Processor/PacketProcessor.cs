#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Linq;
using System.Collections.Generic;
using Maxiploit.Common.Log;
using Maxiploit.Modules.VSRO.API.SilkroadSecurity;
using Maxiploit.Modules.VSRO.Data;
using Maxiploit.Modules.VSRO.Exploit;
using Maxiploit.Modules.VSRO.Network;
using Maxiploit.Modules.VSRO.Packets;

namespace Maxiploit.Modules.VSRO.Processor {
    public sealed class PacketProcessor {

        public delegate PacketOperationResult PacketHandler(Packet packet, VSROSession session);

        private List<PacketHandlerInfo> m_packetHandlers;

        private PacketHandler m_clientHandler;
        private PacketHandler m_moduleHandler;

        public PacketProcessor() {
            this.m_packetHandlers = new List<PacketHandlerInfo>();
            this.m_clientHandler = DefaultClientHandler;
            this.m_moduleHandler = DefaultModuleHandler;
        }

        private PacketOperationResult DefaultClientHandler(Packet packet, VSROSession session) {
            return new PacketOperationResult(PacketOperationType.NOTHING);
        }

        private PacketOperationResult DefaultModuleHandler(Packet packet, VSROSession session) {
            return new PacketOperationResult(PacketOperationType.NOTHING);
        }

        public void SetHandler(PacketDirectionType direction, ushort opcode, PacketHandler handler) {
            PacketHandlerInfo info = new PacketHandlerInfo(direction, opcode, handler);

            bool replaced = false;

            try {
                for (int i = 0; i < this.m_packetHandlers.Count; i++) {
                    var item = this.m_packetHandlers[i];
                    if (item.Equals(info)) {
                        Logger.VSRO.Print(LogLevel.Warning, "[PacketProcessor::SetHandler()] -> Packet opcode condition changed. OLD : " + this.m_packetHandlers[i].Opcode + " - NEW : " + info.Opcode);
                        this.m_packetHandlers[i] = info;
                        replaced = true;
                        break;
                    }
                }
            } catch (Exception ex) {
                Logger.VSRO.Print(LogLevel.Error, "[PacketProcessor::SetHandler()::0xPP1A100] -> Packet filter handler error while analyzing the coditions");
                if (Globals.DEBUG_MODE) Logger.VSRO.Print(LogLevel.Error, ex.ToString());
                return;
            }

            if (!replaced) {
                this.m_packetHandlers.Add(info);
            }
        }


        public PacketHandler GetHandler(PacketDirectionType direction, ushort opcode) {
            PacketHandlerInfo info = m_packetHandlers.FirstOrDefault(item => item.Direction == direction && item.Opcode == opcode);

            if (info.Equals(default(PacketHandlerInfo))) {
                return info.Handler;
            }

            if (direction == PacketDirectionType.TO_CLIENT) {
                return this.m_clientHandler;
            } else if (direction == PacketDirectionType.TO_MODULE) {
                return this.m_moduleHandler;
            } else {
                throw new VSROException("[PacketProcessor::GetHandler] -> PacketDirectionType can't be null");
            }

        }

        public void Reset() {
            this.m_packetHandlers.Clear();
            this.m_clientHandler = DefaultClientHandler;
            this.m_moduleHandler = DefaultModuleHandler;
        }
    }
}
