using System;
using Org.BouncyCastle.X509;

namespace iTextSharp.GE.text.pdf.interfaces {

    /**
    * Encryption settings are described in section 3.5 (more specifically
    * section 3.5.2) of the PDF Reference 1.7.
    * They are explained in section 3.3.3 of the book 'iText in Action'.
    * The values of the different  preferences were originally stored
    * in class PdfWriter, but they have been moved to this separate interface
    * for reasons of convenience.
    */

    public interface IPdfEncryptionSettings {

        /**
        * Sets the encryption options for this document. The userPassword and the
        * ownerPassword can be null or have zero length. In this case the ownerPassword
        * is replaced by a random string. The open permissions for the document can be
        * AllowPrinting, AllowModifyContents, AllowCopy, AllowModifyAnnotations,
        * AllowFillIn, AllowScreenReaders, AllowAssembly and AllowDegradedPrinting.
        * The permissions can be combined by ORing them.
        * @param userPassword the user password. Can be null or empty
        * @param ownerPassword the owner password. Can be null or empty
        * @param permissions the user permissions
        * @param encryptionType the type of encryption. It can be one of STANDARD_ENCRYPTION_40, STANDARD_ENCRYPTION_128 or ENCRYPTION_AES128.
        * Optionally DO_NOT_ENCRYPT_METADATA can be ored to output the metadata in cleartext
        * @throws DocumentException if the document is already open
        */
        void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, int encryptionType);

        /**
        * Sets the certificate encryption options for this document. An array of one or more public certificates
        * must be provided together with an array of the same size for the permissions for each certificate.
        *  The open permissions for the document can be
        *  AllowPrinting, AllowModifyContents, AllowCopy, AllowModifyAnnotations,
        *  AllowFillIn, AllowScreenReaders, AllowAssembly and AllowDegradedPrinting.
        *  The permissions can be combined by ORing them.
        * Optionally DO_NOT_ENCRYPT_METADATA can be ored to output the metadata in cleartext
        * @param certs the public certificates to be used for the encryption
        * @param permissions the user permissions for each of the certicates
        * @param encryptionType the type of encryption. It can be one of STANDARD_ENCRYPTION_40, STANDARD_ENCRYPTION_128 or ENCRYPTION_AES128.
        * @throws DocumentException if the document is already open
        */
        void SetEncryption(X509Certificate[] certs, int[] permissions, int encryptionType);
    }
}
