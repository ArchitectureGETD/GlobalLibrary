using System;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.fonts.cmaps {

    /**
     *
     * @author psoares
     */
    public class CMapCidUni : AbstractCMap {
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
            map[((PdfNumber)code).IntValue] = codepoint;
        }
        
        virtual public int Lookup(int character) {
            return map[character];
        }    
    }
}
