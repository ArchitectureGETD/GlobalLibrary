using System;
using System.IO;
using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf.codec.wmf {
    /// <summary>
    /// Summary description for InputMeta.
    /// </summary>
    public class InputMeta {
    
        Stream sr;
        int length;
    
        public InputMeta(Stream istr) {
            this.sr = istr;
        }

        virtual public int ReadWord() {
            length += 2;
            int k1 = sr.ReadByte();
            if (k1 < 0)
                return 0;
            return (k1 + (sr.ReadByte() << 8)) & 0xffff;
        }

        virtual public int ReadShort() {
            int k = ReadWord();
            if (k > 0x7fff)
                k -= 0x10000;
            return k;
        }

        virtual public Int32 ReadInt() {
            length += 4;
            int k1 = sr.ReadByte();
            if (k1 < 0)
                return 0;
            int k2 = sr.ReadByte() << 8;
            int k3 = sr.ReadByte() << 16;
            return k1 + k2 + k3 + (sr.ReadByte() << 24);
        }
    
        virtual public int ReadByte() {
            ++length;
            return sr.ReadByte() & 0xff;
        }
    
        virtual public void Skip(int len) {
            length += len;
            Utilities.Skip(sr, len);
        }
    
        virtual public int Length {
            get {
                return length;
            }
        }
    
        virtual public BaseColor ReadColor() {
            int red = ReadByte();
            int green = ReadByte();
            int blue = ReadByte();
            ReadByte();
            return new BaseColor(red, green, blue);
        }
    }
}
