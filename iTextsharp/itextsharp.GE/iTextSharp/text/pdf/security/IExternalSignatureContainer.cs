using System;
using System.IO;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.security {

    /**
     * Interface to sign a document. The signing is fully done externally, including the container composition.
     */
    public interface IExternalSignatureContainer {
        /**
         * Produces the container with the signature.
         * @param data the data to sign
         * @return a container with the signature and other objects, like CRL and OCSP. The container will generally be a PKCS7 one.
         * @throws GeneralSecurityException 
         */
        byte[] Sign(Stream data);
        
        /**
         * Modifies the signature dictionary to suit the container. At least the keys PdfName.FILTER and 
         * PdfName.SUBFILTER will have to be set.
         * @param signDic the signature dictionary
         */
        void ModifySigningDictionary(PdfDictionary signDic);
    }
}
