#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace Maxiploit.Modules.VSRO {
    public class VSROException : ModuleException {
        public VSROException() : base() {
        }

        public VSROException(string msg) : base(msg) {

        }

        public VSROException(string msg, params object[] args) : base(string.Format(msg, args)) {

        }
    }
}
