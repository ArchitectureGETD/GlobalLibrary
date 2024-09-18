using System;
using Org.BouncyCastle.X509;

namespace iTextSharp.GE.text.pdf.security {

    /**
    * Interface for the OCSP Client.
    * @since 2.1.6
    */
    public interface IOcspClient {

	    /**
	     * Gets an encoded byte array with OCSP validation. The method should not throw an exception.
         * @param checkCert to certificate to check
         * @param rootCert the parent certificate
         * @param url the url to get the verification. It it's null it will be taken
         * from the check cert or from other implementation specific source
	     * @return	a byte array with the validation or null if the validation could not be obtained
	     */
        byte[] GetEncoded(X509Certificate checkCert, X509Certificate rootCert, String url);
    }
}
