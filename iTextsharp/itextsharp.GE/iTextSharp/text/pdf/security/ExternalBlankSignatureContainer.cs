using System.IO;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.security;

namespace com.itextpdf.text.pdf.security{

    /**
     * Produces a blank (or empty) signature. Useful for deferred signing with
     * MakeSignature.signExternalContainer().
     */
    public class ExternalBlankSignatureContainer : IExternalSignatureContainer {
        private PdfDictionary sigDic;
    
        public ExternalBlankSignatureContainer(PdfDictionary sigDic) {
            this.sigDic = sigDic;
        }
    
        public ExternalBlankSignatureContainer(PdfName filter, PdfName subFilter) {
            sigDic = new PdfDictionary();
            sigDic.Put(PdfName.FILTER, filter);
            sigDic.Put(PdfName.SUBFILTER, subFilter);
        }
    
        virtual public byte[] Sign(Stream data) {
            return new byte[0];
        }

        virtual public void ModifySigningDictionary(PdfDictionary signDic) {
            signDic.PutAll(sigDic);
        }
    
    }
}
