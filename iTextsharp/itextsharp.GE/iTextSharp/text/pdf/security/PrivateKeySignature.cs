using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using iTextSharp.GE.text.error_messages;
namespace iTextSharp.GE.text.pdf.security {


    /**
     * Implementation of the ExternalSignature interface that can be used
     * when you have a PrivateKey object.
     * @author Paulo Soares
     */
    public class PrivateKeySignature : IExternalSignature {
        
        /** The private key object. */
        private ICipherParameters pk;
        /** The hash algorithm. */
        private String hashAlgorithm;
        /** The encryption algorithm (obtained from the private key) */
        private String encryptionAlgorithm;

        /**
         * Creates an ExternalSignature instance
         * @param pk    a PrivateKey object
         * @param hashAlgorithm the hash algorithm (e.g. "SHA-1", "SHA-256",...)
         * @param provider  the security provider (e.g. "BC")
         */
        public PrivateKeySignature(ICipherParameters pk, String hashAlgorithm) {
            this.pk = pk;
            this.hashAlgorithm = DigestAlgorithms.GetDigest(DigestAlgorithms.GetAllowedDigests(hashAlgorithm));
            if (pk is RsaKeyParameters)
                encryptionAlgorithm = "RSA";
            else if (pk is DsaKeyParameters)
                encryptionAlgorithm = "DSA";
            else if (pk is ECKeyParameters)
                encryptionAlgorithm = "ECDSA";
            else
                throw new ArgumentException(MessageLocalization.GetComposedMessage("unknown.key.algorithm.1", pk.ToString()));
            
        }
        
        /**
         * Creates a message digest using the hash algorithm
         * and signs it using the encryption algorithm.
         * @param message   the message you want to be hashed and signed
         * @return  a signed message digest
         * @see com.itextpdf.text.pdf.security.ExternalSignature#sign(byte[])
         */
        public virtual byte[] Sign(byte[] b) {
            String signMode = hashAlgorithm + "with" + encryptionAlgorithm;
            ISigner sig = SignerUtilities.GetSigner(signMode);
            sig.Init(true, pk);
            sig.BlockUpdate(b, 0, b.Length);
            return sig.GenerateSignature();
        }
        
        /**
         * Returns the hash algorithm.
         * @return  the hash algorithm (e.g. "SHA-1", "SHA-256,...")
         * @see com.itextpdf.text.pdf.security.ExternalSignature#getHashAlgorithm()
         */
        public virtual String GetHashAlgorithm() {
            return hashAlgorithm;
        }
        
        /**
         * Returns the encryption algorithm used for signing.
         * @return the encryption algorithm ("RSA" or "DSA")
         * @see com.itextpdf.text.pdf.security.ExternalSignature#getEncryptionAlgorithm()
         */
        public virtual String GetEncryptionAlgorithm() {
            return encryptionAlgorithm;
        }
        
    }
}
