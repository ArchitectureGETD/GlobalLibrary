using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf
{
    /// <summary>
    /// Summary description for ICC_Profile.
    /// </summary>
    public class ICC_Profile
    {
        protected byte[] data;
        protected int numComponents;
        private static Dictionary<string,int> cstags = new Dictionary<string,int>();
        
        protected ICC_Profile() {
        }

        public static ICC_Profile GetInstance(byte[] data, int numComponents) {
            if (data.Length < 128 || data[36] != 0x61 || data[37] != 0x63 
                || data[38] != 0x73 || data[39] != 0x70)
                throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.icc.profile"));
            ICC_Profile icc = new ICC_Profile();
            icc.data = data;

            if (!cstags.TryGetValue(Encoding.ASCII.GetString(data, 16, 4), out icc.numComponents)) {
                icc.numComponents = 0;
            }
            // invalid ICC
            if (icc.numComponents != numComponents) {
                throw new ArgumentException("ICC profile contains " + icc.numComponents + " component(s), the image data contains " + numComponents + " component(s)");
            }
            return icc;
        }

        public static ICC_Profile GetInstance(byte[] data) {
            int numComponents;
            if (!cstags.TryGetValue(Encoding.ASCII.GetString(data, 16, 4), out numComponents)) {
                numComponents = 0;
            }
            return GetInstance(data, numComponents);
        }
        
        public static ICC_Profile GetInstance(Stream file) {
            byte[] head = new byte[128];
            int remain = head.Length;
            int ptr = 0;
            while (remain > 0) {
                int n = file.Read(head, ptr, remain);
                if (n <= 0)
                    throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.icc.profile"));
                remain -= n;
                ptr += n;
            }
            if (head[36] != 0x61 || head[37] != 0x63 
                || head[38] != 0x73 || head[39] != 0x70)
                throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.icc.profile"));
            remain = ((head[0] & 0xff) << 24) | ((head[1] & 0xff) << 16)
                      | ((head[2] & 0xff) <<  8) | (head[3] & 0xff);
            byte[] icc = new byte[remain];
            System.Array.Copy(head, 0, icc, 0, head.Length);
            remain -= head.Length;
            ptr = head.Length;
            while (remain > 0) {
                int n = file.Read(icc, ptr, remain);
                if (n <= 0)
                    throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.icc.profile"));
                remain -= n;
                ptr += n;
            }
            return GetInstance(icc);
        }

        public static ICC_Profile GetInstance(String fname) {
            FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read);
            ICC_Profile icc = GetInstance(fs);
            fs.Close();
            return icc;
        }

        virtual public byte[] Data {
            get {
                return data;
            }
        }
        
        virtual public int NumComponents {
            get {
                return numComponents;
            }
        }

        static ICC_Profile() {
            cstags["XYZ "] = 3;
            cstags["Lab "] = 3;
            cstags["Luv "] = 3;
            cstags["YCbr"] = 3;
            cstags["Yxy "] = 3;
            cstags["RGB "] = 3;
            cstags["GRAY"] = 1;
            cstags["HSV "] = 3;
            cstags["HLS "] = 3;
            cstags["CMYK"] = 4;
            cstags["CMY "] = 3;
            cstags["2CLR"] = 2;
            cstags["3CLR"] = 3;
            cstags["4CLR"] = 4;
            cstags["5CLR"] = 5;
            cstags["6CLR"] = 6;
            cstags["7CLR"] = 7;
            cstags["8CLR"] = 8;
            cstags["9CLR"] = 9;
            cstags["ACLR"] = 10;
            cstags["BCLR"] = 11;
            cstags["CCLR"] = 12;
            cstags["DCLR"] = 13;
            cstags["ECLR"] = 14;
            cstags["FCLR"] = 15;
        }
    }
}
