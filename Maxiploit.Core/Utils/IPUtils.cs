#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion


using System;
using System.Linq;
using System.Net;

namespace Maxiploit.Core.Utils {
    public static class IPUtils {

        public static bool IsIPv4Input(string ip) {
            if (String.IsNullOrWhiteSpace(ip)) {
                return false;
            }

            if (ip.StartsWith(".") || ip.EndsWith(".")) {
                return false;
            }

            string[] splitValues = ip.Split('.');
            if (splitValues.Length != 4) {
                return false;
            }

            byte temp;

            return splitValues.All(r => byte.TryParse(r, out temp));
        }

        public static bool IPValidateIPv4(string strIP) {
            IPAddress result = null;
            return !String.IsNullOrEmpty(strIP) && IPAddress.TryParse(strIP, out result);
        }
    }
}
