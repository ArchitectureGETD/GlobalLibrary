using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.util;

namespace iTextSharp.GE.text.error_messages {
    public class LocalizedResource {
        private static readonly char[] splt = new char[]{'_'};
        private Properties msgs = new Properties();

        public LocalizedResource(string resourceRoot, CultureInfo culture, Assembly assembly) {
            Stream istr = null;
            string name = culture.Name.Replace('-', '_');
            if (name != "") {
                try {
                    istr = assembly.GetManifestResourceStream(resourceRoot + "_" + name + ".properties");
                }
                catch {}
                if (istr == null) {
                    string[] nameSplit = name.Split(splt, 2);
                    if (nameSplit.Length == 2) {
                        try {
                            istr = assembly.GetManifestResourceStream(resourceRoot + "_" + nameSplit[0] + ".properties");
                        }
                        catch {}
                    }
                }
            }
            if (istr == null) {
                try {
                    istr = assembly.GetManifestResourceStream(resourceRoot + ".properties");
                }
                catch {}
            }
            if (istr != null) {
                msgs.Load(istr);
            }
            List<string> keys = new List<string>(msgs.Keys);
            foreach (string key in keys) {
                string v = msgs[key];
                StringBuilder sb = new StringBuilder();
                int n = 0;
                int state = 0;
                foreach (char c in v) {
                    switch (state) {
                        case 0:
                            if (c == '%') {
                                state = 1;
                                break;
                            }
                            else if (c == '{' || c == '}') {
                                sb.Append(c);
                            }
                            sb.Append(c);
                            break;
                        case 1:
                            if (c == '%') {
                                sb.Append(c);
                                state = 0;
                            }
                            else if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                                state = 0;
                            sb.Append('{').Append(n++).Append('}');
                            break;
                    }
                }
                msgs[key] = sb.ToString();
            }
        }

        virtual public string GetMessage(string key) {
            string v = msgs[key];
            if (v == null)
                return key;
            else
                return v;
        }
    }
}
