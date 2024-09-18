using System;
using System.Collections.Generic;
using Org.BouncyCastle.X509;
namespace iTextSharp.GE.text.pdf.security {

    /**
     * An implementation of the CrlClient that handles offline
     * Certificate Revocation Lists.
     */
    public class CrlClientOffline : ICrlClient {
        
        /** The CRL as a byte array. */
        private List<byte[]> crls = new List<byte[]>();
        
        /**
         * Creates an instance of a CrlClient in case you
         * have a local cache of the Certificate Revocation List.
         * @param crlEncoded    the CRL bytes
         */
        public CrlClientOffline(byte[] crlEncoded) {
            crls.Add(crlEncoded);
        }
        
        /**
         * Returns the CRL bytes (the parameters are ignored).
         * @see com.itextpdf.text.pdf.security.CrlClient#getEncoded(java.security.cert.X509Certificate, java.lang.String)
         */
        virtual public ICollection<byte[]> GetEncoded(X509Certificate checkCert, String url) {
            return crls;
        }
    }
}
