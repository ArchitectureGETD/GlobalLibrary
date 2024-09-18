using System;

namespace iTextSharp.GE.text.pdf {
    public class PdfMediaClipData : PdfDictionary {
        
        internal PdfMediaClipData(String file, PdfFileSpecification fs, String mimeType) {
            Put(PdfName.TYPE,new PdfName("MediaClip"));
            Put(PdfName.S, new PdfName("MCD"));
            Put(PdfName.N, new PdfString("Media clip for "+file));
            Put(new PdfName("CT"), new PdfString(mimeType));
            PdfDictionary dic = new PdfDictionary();
            dic.Put(new PdfName("TF"), new PdfString("TEMPACCESS"));
            Put(new PdfName("P"), dic);
            Put(PdfName.D, fs.Reference);
        }
    }
}
