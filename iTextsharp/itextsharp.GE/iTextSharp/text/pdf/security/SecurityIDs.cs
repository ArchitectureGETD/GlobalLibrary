using System;
namespace iTextSharp.GE.text.pdf.security {

    /**
     * A list of IDs that are used by the security classes
     */
    public class SecurityIDs {

	    public const String ID_PKCS7_DATA = "1.2.840.113549.1.7.1";
	    public const String ID_PKCS7_SIGNED_DATA = "1.2.840.113549.1.7.2";
	    public const String ID_RSA = "1.2.840.113549.1.1.1";
	    public const String ID_DSA = "1.2.840.10040.4.1";
	    public const String ID_ECDSA = "1.2.840.10045.2.1";
	    public const String ID_CONTENT_TYPE = "1.2.840.113549.1.9.3";
	    public const String ID_MESSAGE_DIGEST = "1.2.840.113549.1.9.4";
	    public const String ID_SIGNING_TIME = "1.2.840.113549.1.9.5";
	    public const String ID_ADBE_REVOCATION = "1.2.840.113583.1.1.8";
	    public const String ID_TSA = "1.2.840.113583.1.1.9.1";
	    public const String ID_OCSP = "1.3.6.1.5.5.7.48.1";
        public const String ID_AA_SIGNING_CERTIFICATE_V1 = "1.2.840.113549.1.9.16.2.12";
	    public const String ID_AA_SIGNING_CERTIFICATE_V2 = "1.2.840.113549.1.9.16.2.47";

    }
}
