#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace Maxiploit.Modules.VSRO.Network {
    public struct RedirectionRule {

        public string SRC_IP { get; private set; }

        public int SRC_PORT { get; private set; }

        public string DEST_IP { get; private set; }

        public int DEST_PORT { get; private set; }

        public RedirectionRule(string srcIp, int srcPort, string destIp, int destPort) {
            this.SRC_IP = srcIp;
            this.SRC_PORT = srcPort;
            this.DEST_IP = destIp;
            this.DEST_PORT = destPort;
        }
    }
}
