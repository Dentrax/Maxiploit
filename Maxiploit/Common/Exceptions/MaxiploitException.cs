#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace Maxiploit.Common.Exceptions {
    public abstract class MaxiploitException : ProgramException {
        public MaxiploitException() : base() {
        }

        public MaxiploitException(string msg) : base(msg) {

        }

        public MaxiploitException(string msg, params object[] args) : base(string.Format(msg, args)) {

        }
    }
}
