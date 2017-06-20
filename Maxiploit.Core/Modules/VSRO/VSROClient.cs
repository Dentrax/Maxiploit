#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using Maxiploit.Runtime.Networking;

namespace Maxiploit.Modules.VSRO {
    public sealed class VSROClient : Client {

        //Use Interlocked.Add(ref m_test, 0)

        //bool lockWasTaken = false;
        //var temp = m_lock;
        //Queue<Tuple<bool, Packet>> packets = null;
        //    try
        //    {
        //        Monitor.Enter(temp, ref lockWasTaken);
        //    if (lockWasTaken)
        //            Monitor.Exit(temp);
        public VSROClient(SocketContext socketContext) : base(socketContext) {
        }
    }
}
