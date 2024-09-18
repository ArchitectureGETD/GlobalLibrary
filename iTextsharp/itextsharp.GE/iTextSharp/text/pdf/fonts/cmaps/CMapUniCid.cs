using System;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.fonts.cmaps {

   
    public class CMapUniCid : AbstractCMap {
        private IntHashtable map = new IntHashtable(65537);
        
        internal override void AddChar(PdfString mark, PdfObject code) {
            if (!(code is PdfNumber))
                return;
            int codepoint;
            String s = DecodeStringToUnicode(mark);
            if (Utilities.IsSurrogatePair(s, 0))
                codepoint = Utilities.ConvertToUtf32(s, 0);
            else
                codepoint = (int)s[0];
            map[codepoint] = ((PdfNumber)code).IntValue;
        }
        
        virtual public int Lookup(int character) {
            return map[character];
        }

        virtual public CMapToUnicode ExportToUnicode() {
            CMapToUnicode uni = new CMapToUnicode();
            int[] keys = map.GetKeys();
            foreach (int key in keys) {
                uni.AddChar(map[key], Utilities.ConvertFromUtf32(key));
            }
            return uni;
        }
    }
}
