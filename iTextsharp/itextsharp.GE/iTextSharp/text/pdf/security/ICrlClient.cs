using System;
using System.Collections.Generic;
using Org.BouncyCastle.X509;

namespace iTextSharp.GE.text.pdf.security {

    /**
     * Interface that needs to be implemented if you want to embed
     * Certificate Revocation Lists into your PDF.
     * @author Paulo Soares
     */
    public interface ICrlClient {
        /**
         * Gets a collection of byte array each representing a crl.
         * @param	checkCert	the certificate from which a CRL URL can be obtained
         * @param	url		a CRL url if you don't want to obtain it from the certificate
         * @return	a collection of byte array each representing a crl. It may return null or an empty collection
         */
        ICollection<byte[]> GetEncoded(X509Certificate checkCert, String url);
    }
}
