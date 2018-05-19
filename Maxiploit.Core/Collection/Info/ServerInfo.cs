using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maxiploit.Core.Collection.Info {
    public sealed class ServerInfo {

        public string Name { get; private set; }

        public IPAddress IP { get; private set; }

        public int Port { get; private set; }



    }
}
