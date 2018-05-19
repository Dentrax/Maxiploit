using Maxiploit.Core.Collection.Info;

using System.Collections.Generic;
using System.Net.Sockets;

namespace Maxiploit.Core.Collection {
    public sealed class DataCollection {

        public List<ServerInfo> ServerInfos;

        public List<SQLInfo> SQLInfos;

        public DataCollection() {
            this.ServerInfos = new List<ServerInfo>();
            this.SQLInfos = new List<SQLInfo>();
        }

    }
}
