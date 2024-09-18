using System;
using System.Collections.Generic;
using Org.BouncyCastle.X509;

/**
 * Superclass for a series of certificate verifiers that will typically
 * be used in a chain. It wraps another <code>CertificateVerifier</code>
 * that is the next element in the chain of which the <code>verify()</code>
 * method will be called.
 */
namespace iTextSharp.GE.text.pdf.security {
	public class CertificateVerifier {
        /** The previous CertificateVerifier in the chain of verifiers. */
	    protected CertificateVerifier verifier;
    	
	    /** Indicates if going online to verify a certificate is allowed. */
	    protected bool onlineCheckingAllowed = true;

	    /**
	     * Creates the CertificateVerifier in a chain of verifiers.
	     * @param verifier	the previous verifier in the chain
	     */
	    public CertificateVerifier(CertificateVerifier verifier) {
		    this.verifier = verifier;
	    }

	    /**
	     * Decide whether or not online checking is allowed.
	     * @param onlineCheckingAllowed
	     */
	    virtual public bool OnlineCheckingAllowed {
            set { onlineCheckingAllowed = value; }
	    }
    	
	    /**
	     * Checks the validity of the certificate, and calls the next
	     * verifier in the chain, if any.
	     * @param signCert	the certificate that needs to be checked
	     * @param issuerCert	its issuer
	     * @param signDate		the date the certificate needs to be valid
	     * @return a list of <code>VerificationOK</code> objects.
	     * The list will be empty if the certificate couldn't be verified.
	     * @throws GeneralSecurityException
	     * @throws IOException
	     */
	    virtual public List<VerificationOK> Verify(X509Certificate signCert, X509Certificate issuerCert, DateTime signDate) {
		    // Check if the certificate is valid on the signDate
		    //if (signDate != null)
			    signCert.CheckValidity(signDate);
		    // Check if the signature is valid
		    if (issuerCert != null) {
			    signCert.Verify(issuerCert.GetPublicKey());
		    }
		    // Also in case, the certificate is self-signed
		    else {
			    signCert.Verify(signCert.GetPublicKey());
		    }
		    List<VerificationOK> result = new List<VerificationOK>();
		    if (verifier != null)
			    result.AddRange(verifier.Verify(signCert, issuerCert, signDate));
		    return result;
	    }
	}
}
