#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Maxiploit.Core.Common.Exceptions;
using Maxiploit.Core.Common.Log;

namespace Maxiploit.Core.IO {
    public sealed class SettingsReader {
        private string m_path;
        private string m_data;

        public SettingsReader(string path) {
            m_path = path;
        }

        public bool Initialize() {
            bool flag = false;
            try {
                m_data = File.ReadAllText(m_path);

                m_data = m_data.Replace("\r", "").Replace("\t", "").Trim();

                flag = true;
            } catch { }

            return flag;
        }

        private bool IsSectionName(string line) {
            if (!IsComment(line) && !line.Contains("="))
                return true;
            return false;
        }

        private bool IsComment(string line) {
            if (line.TrimStart().StartsWith("//"))
                return true;
            return false;
        }

        private void CleanLine(ref string line) {
            line = line.Replace("\t", "");
        }

        public List<string> GetSectionContent(string sectionName) {
            string[] tmpResult = null;
            string[] lines = m_data.Split(new char[] { '\n' });

            int startIndex = 0;
            int endIndex = 0;

            for (int i = 0; i < lines.Length; i++) {
                if (IsSectionName(lines[i]) && lines[i] == sectionName) {
                    for (int j = i; j < lines.Length; j++) {
                        if (!IsComment(lines[j]) && lines[j] == "{")
                            startIndex = j;

                        if (!IsComment(lines[j]) && lines[j] == "}")
                            endIndex = j;

                        if (startIndex != 0 && endIndex != 0)
                            break;
                    }
                    break;
                }
            }

            if (startIndex == 0 || endIndex == 0)
                return null;

            int count = (endIndex - startIndex) + 1;
            tmpResult = new string[count];
            Array.Copy(lines, startIndex, tmpResult, 0, count);
            return tmpResult.ToList();
        }

        public T Read<T>(string section, string key) {
            T res = default(T);
            List<string> lines = GetSectionContent(section);
            if (lines == null) {
                Logger.IO.Print(ELogLevelType.ERROR, "[SettingsReader::Read()::0xSR1A100] -> Could not get section data : " + section);
            }

            string[] split;
            bool ok = false;

            for (int i = 0; i < lines.Count; i++) {
                if (IsComment(lines[i]) || IsSectionName(lines[i]))
                    continue;

                try {
                    split = lines[i].Split(new char[] { '=' });
                    string curKey = split[0];
                    if (curKey == key) {

                        if (typeof(T) == typeof(int)) {
                            res = (T)(object)int.Parse(split[1]);
                            ok = true;
                        }

                        if (typeof(T) == typeof(uint)) {
                            res = (T)(Object)uint.Parse(split[1]);
                            ok = true;
                        }

                        if (typeof(T) == typeof(string)) {
                            res = (T)(object)split[1];
                            ok = true;
                        }

                        if (typeof(T) == typeof(bool)) {
                            res = (T)(object)bool.Parse(split[1]);
                            ok = true;
                        }
                    }
                } catch { }

            }

            if (!ok) {
                Logger.IO.Print(ELogLevelType.ERROR, "[SettingsReader::Read()::0xSR1A101] -> Something went wrong file extracting key/value - invalid format, or unsupported type");
            }

            return res;
        }
    }
}
