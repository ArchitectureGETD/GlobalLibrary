using System;

namespace iTextSharp.GE.text.pdf {
    /**
    * A Rendition dictionary (pdf spec 1.5)
    */
    public class PdfRendition : PdfDictionary {
        public PdfRendition(String file, PdfFileSpecification fs, String mimeType) {
            Put(PdfName.S, new PdfName("MR"));
            Put(PdfName.N, new PdfString("Rendition for "+file));
            Put(PdfName.C, new PdfMediaClipData(file, fs, mimeType));
        }
    }
}
