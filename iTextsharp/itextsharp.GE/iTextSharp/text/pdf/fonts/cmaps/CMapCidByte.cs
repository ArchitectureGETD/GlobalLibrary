using System;
using System.Collections.Generic;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.fonts.cmaps {

    public class CMapCidByte : AbstractCMap {
        private Dictionary<int,byte[]> map = new Dictionary<int,byte[]>();
        private static byte[] EMPTY = {};
        
        internal override void AddChar(PdfString mark, PdfObject code) {
            if (!(code is PdfNumber))
                return;
            byte[] ser = DecodeStringToByte(mark);
            map[((PdfNumber)code).IntValue] = ser;
        }
        
        virtual public byte[] Lookup(int cid) {
            byte[] ser;
            map.TryGetValue(cid, out ser);
            if (ser == null)
                return EMPTY;
            else
                return ser;
        }
    }
}
