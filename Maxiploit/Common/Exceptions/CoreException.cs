#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace Maxiploit.Common.Exceptions {
    public abstract class CoreException : Exception {

        public CoreException() : base() {
        }

        public CoreException(string msg) : base(msg) {

        }

        public CoreException(string msg, params object[] args) : base(string.Format(msg, args)) {

        }
    }
}
