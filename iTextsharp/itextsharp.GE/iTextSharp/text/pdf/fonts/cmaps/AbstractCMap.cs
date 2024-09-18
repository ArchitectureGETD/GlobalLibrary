using System;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.fonts.cmaps {

    /**
     *
     * @author psoares
     */
    public abstract class AbstractCMap {

        private String cmapName;
        private String registry;

        private String ordering;

        private int supplement;

        virtual public int Supplement {
            get { return supplement; }
            set { supplement = value; }
        }
        
        virtual public String Name {
            get { return cmapName; }
            set { cmapName = value; }
        }

        virtual public String Ordering {
            get { return ordering; }
            set { ordering = value; }
        }
        
        virtual public String Registry {
            get { return registry; }
            set { registry = value; }
        }


        internal abstract void AddChar(PdfString mark, PdfObject code);
        
        internal void AddRange(PdfString from, PdfString to, PdfObject code) {
            byte[] a1 = DecodeStringToByte(from);
            byte[] a2 = DecodeStringToByte(to);
            if (a1.Length != a2.Length || a1.Length == 0)
                throw new ArgumentException("Invalid map.");
            byte[] sout = null;
            if (code is PdfString)
                sout = DecodeStringToByte((PdfString)code);
            int start = ByteArrayToInt(a1);
            int end = ByteArrayToInt(a2);
            for (int k = start; k <= end; ++k) {
                IntToByteArray(k, a1);
                PdfString s = new PdfString(a1);
                s.SetHexWriting(true);
                if (code is PdfArray) {
                    AddChar(s, ((PdfArray)code)[k - start]);
                }
                else if (code is PdfNumber) {
                    int nn = ((PdfNumber)code).IntValue + k - start;
                    AddChar(s, new PdfNumber(nn));
                }
                else if (code is PdfString) {
                    PdfString s1 = new PdfString(sout);
                    s1.SetHexWriting(true);
                    ++sout[sout.Length - 1];
                    AddChar(s, s1);
                }
            }
        }
        
        private static void IntToByteArray(int v, byte[] b) {
            for (int k = b.Length - 1; k >= 0; --k) {
                b[k] = (byte)v;
                v = v >> 8;
            }
        }
    
        private static int ByteArrayToInt(byte[] b) {
            int v = 0;
            for (int k = 0; k < b.Length; ++k) {
                v = v << 8;
                v |= b[k] & 0xff;
            }
            return v;
        }

        public static byte[] DecodeStringToByte(PdfString s) {
            byte[] b = s.GetBytes();
            byte[] br = new byte[b.Length];
            System.Array.Copy(b, 0, br, 0, b.Length);
            return br;
        }

        virtual public String DecodeStringToUnicode(PdfString ps) {
            if (ps.IsHexWriting())
                return PdfEncodings.ConvertToString(ps.GetBytes(), "UnicodeBigUnmarked");
            else
                return ps.ToUnicodeString();
        }
    }
}
