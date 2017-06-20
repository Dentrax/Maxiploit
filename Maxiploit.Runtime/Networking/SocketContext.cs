#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Net.Sockets;

namespace Maxiploit.Runtime.Networking {
    public abstract class SocketContext : IDisposable {

        protected bool m_wasDisposed;

        public TCPServer Owner { get; private set; }

        public Socket Socket { get; private set; }

        protected byte[] m_buffer;

        protected object m_context;

        protected readonly object m_locker = new object();


        public object Context {
            get { return this.m_context; }
        }


        public SocketContext() {
            m_buffer = new byte[4096];
        }

        ~SocketContext() {
            Dispose(false);
        }

        public virtual void SetContext(object context) {
            m_context = context;
        }

        public virtual void SetSocket(Socket client) {
            Socket = client;
        }

        public virtual void SetOwner(TCPServer server) {
            Owner = server;
        }

        public virtual void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!m_wasDisposed) {
                if (disposing) {
                    //
                }
                m_buffer = null;
                m_wasDisposed = true;
            }
        }

        public virtual void Clean() {
            Socket = null;
        }

        public virtual void Disconnect() {
            try {
                lock (m_locker) {
                    if (Socket != null) {
                        if (Socket.Connected) {
                            Socket.Disconnect(true);
                            Socket.Close();
                        }
                    }
                }
            } catch {
                //TODO: Logger'a hata ekle ?
            }
        }
    }
}
