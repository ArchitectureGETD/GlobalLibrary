using System;
namespace iTextSharp.GE.text.pdf.security {

    /**
     * Interface that needs to be implemented to do the actual signing.
     * For instance: you'll have to implement this interface if you want
     * to sign a PDF using a smart card.
     */
    public interface IExternalSignature {
        
        /**
         * Returns the hash algorithm.
         * @return  the hash algorithm (e.g. "SHA-1", "SHA-256,...")
         */
        String GetHashAlgorithm();
        
        /**
         * Returns the encryption algorithm used for signing.
         * @return the encryption algorithm ("RSA" or "DSA")
         */
        String GetEncryptionAlgorithm();

        /**
         * Signs it using the encryption algorithm in combination with
         * the digest algorithm.
         * @param message   the message you want to be hashed and signed
         * @return  a signed message digest
         * @throws GeneralSecurityException
         */
        byte[] Sign(byte[] message);
    }
}
