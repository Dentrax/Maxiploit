#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace Maxiploit.Modules.VSRO.Data {

    [Flags]
    public enum AuthType {
        GM = 1,
        DEV = 2,
        NORMAL = 3,
        NONE = 4
    }

    [Flags]
    public enum ChatType : byte {
        All = 1,
        PM = 2,
        GM = 3,
        PT = 4,
        GUILD = 5,
        UNION = 11,
        Academy = 16
    }

    public enum PacketOperationType {
        REPLACE,
        INJECT,
        DISCONNECT,
        IGNORE,
        BLOCK_IP,
        SPECIAL,
        NOTHING,
        NONE
    }

    public enum PacketDirectionType {
        TO_CLIENT,
        TO_MODULE,
        NONE
    }

    public enum PacketExploitType {
        GUILD_SQL_INJECTION,
        AVATAR_BLUE,
        INVISIBLE,
        INVINCIBLE,
        DEAD_STATE,
        CHARNAME,
        CLIENT_MOVE,
        GM_PACKET,
        EXCHANGE_BUG,
        STALL_BUG,
        EXIT_BUG,
        WIZARD_BUG,
        BERSERKER_BUG,
        FORTRESSWAR_INFO_BUG,
        FLOOD,
        UNKNOWN
    }

    public enum ServerType {
        AGENT,
        GATEWAY,
        DOWNLOAD,
        SR_GAME,
        SR_SHARD,
        GLOBAL,
        FARM,
        MACHINE,
        NONE
    }
}
