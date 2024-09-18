using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.error_messages;
namespace iTextSharp.GE.text.pdf.fonts.cmaps {

    public class CMapByteCid : AbstractCMap {
        private List<char[]> planes = new List<char[]>();

        public CMapByteCid() {
            planes.Add(new char[256]);
        }
        
        internal override void AddChar(PdfString mark, PdfObject code) {
            if (!(code is PdfNumber))
                return;
            EncodeSequence(DecodeStringToByte(mark), (char)((PdfNumber)code).IntValue);
        }
        
        private void EncodeSequence(byte[] seqs, char cid) {
            int size = seqs.Length - 1;
            int nextPlane = 0;
            int one;
            char[] plane;
            for (int idx = 0; idx < size; ++idx) {
                plane = planes[nextPlane];
                one = seqs[idx] & 0xff;
                char c = plane[one];
                if (c != 0 && (c & 0x8000) == 0)
                    throw new ArgumentException(MessageLocalization.GetComposedMessage("inconsistent.mapping"));
                if (c == 0) {
                    planes.Add(new char[256]);
                    c = (char)(planes.Count - 1 | 0x8000);
                    plane[one] = c;
                }
                nextPlane = c & 0x7fff;
            }
            plane = planes[nextPlane];
            one = seqs[size] & 0xff;
            char c2 = plane[one];
            if ((c2 & 0x8000) != 0)
                throw new ArgumentException(MessageLocalization.GetComposedMessage("inconsistent.mapping"));
            plane[one] = cid;
        }
        
        /**
         * 
         * @param seq
         * @return the cid code or -1 for end
         */
        virtual public int DecodeSingle(CMapSequence seq) {
            int end = seq.off + seq.len;
            int currentPlane = 0;
            while (seq.off < end) {
                int one = seq.seq[seq.off++] & 0xff;
                --seq.len;
                char[] plane = planes[currentPlane];
                int cid = plane[one];
                if ((cid & 0x8000) == 0) {
                    return cid;
                }
                else
                    currentPlane = cid & 0x7fff;
            }
            return -1;
        }

        virtual public String DecodeSequence(CMapSequence seq) {
            StringBuilder sb = new StringBuilder();
            int cid = 0;
            while ((cid = DecodeSingle(seq)) >= 0) {
                sb.Append((char)cid);
            }
            return sb.ToString();
        }
    }
}
