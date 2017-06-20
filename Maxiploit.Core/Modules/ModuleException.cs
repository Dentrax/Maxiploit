#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using Maxiploit.Common.Exceptions;

namespace Maxiploit.Modules {
    public abstract class ModuleException : MaxiploitException {
        public ModuleException() : base() {
        }

        public ModuleException(string msg) : base(msg) {

        }

        public ModuleException(string msg, params object[] args) : base(string.Format(msg, args)) {

        }
    }
}
