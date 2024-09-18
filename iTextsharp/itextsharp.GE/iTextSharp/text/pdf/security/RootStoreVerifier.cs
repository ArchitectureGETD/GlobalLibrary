using System;
using System.Collections.Generic;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using iTextSharp.GE.text.log;

/**
 * Verifies a certificate against a <code>KeyStore</code>
 * containing trusted anchors.
 */
namespace iTextSharp.GE.text.pdf.security {
	public class RootStoreVerifier : CertificateVerifier {
        /** The Logger instance */
	    private static ILogger LOGGER = LoggerFactory.GetLogger(typeof(RootStoreVerifier));

	    /** A key store against which certificates can be verified. */
	    protected List<X509Certificate> certificates  = null;

	    /**
	     * Creates a RootStoreVerifier in a chain of verifiers.
	     * 
	     * @param verifier
	     *            the next verifier in the chain
	     */
	    public RootStoreVerifier(CertificateVerifier verifier) : base(verifier) {}

	    /**
	     * Sets the Key Store against which a certificate can be checked.
	     * 
	     * @param keyStore
	     *            a root store
	     */
        virtual public List<X509Certificate> Certificates {
            set { certificates = value; }
        }

	    /**
	     * Verifies a single certificate against a key store (if present).
	     * 
	     * @param signCert
	     *            the certificate to verify
	     * @param issuerCert
	     *            the issuer certificate
	     * @param signDate
	     *            the date the certificate needs to be valid
	     * @return a list of <code>VerificationOK</code> objects.
	     * The list will be empty if the certificate couldn't be verified.
	     */
	    override public List<VerificationOK> Verify(X509Certificate signCert, X509Certificate issuerCert, DateTime signDate) {
		    LOGGER.Info("Root store verification: " + signCert.SubjectDN);
		    // verify using the CertificateVerifier if root store is missing
		    if (certificates == null)
			    return base.Verify(signCert, issuerCert, signDate);
		    try {
			    List<VerificationOK> result = new List<VerificationOK>();
			    // loop over the trusted anchors in the root store
                foreach (X509Certificate anchor in certificates) {
				    try {
					    signCert.Verify(anchor.GetPublicKey());
					    LOGGER.Info("Certificate verified against root store");
					    result.Add(new VerificationOK(signCert, this, "Certificate verified against root store."));
					    result.AddRange(base.Verify(signCert, issuerCert, signDate));
					    return result;
				    } catch (GeneralSecurityException) {}
			    }
			    result.AddRange(base.Verify(signCert, issuerCert, signDate));
			    return result;
		    } catch (GeneralSecurityException) {
			    return base.Verify(signCert, issuerCert, signDate);
		    }
	    }
	}
}
