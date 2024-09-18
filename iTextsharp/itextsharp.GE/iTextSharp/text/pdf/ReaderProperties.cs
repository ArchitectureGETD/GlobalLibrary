using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;

namespace iTextSharp.GE.text.pdf
{
    public class ReaderProperties
    {
        internal X509Certificate certificate = null;
        internal ICipherParameters certificateKey = null;
        internal byte[] ownerPassword = null;
        internal bool partialRead = false;
        internal bool closeSourceOnconstructorError = true;
        internal MemoryLimitsAwareHandler memoryLimitsAwareHandler = null;

        public ReaderProperties SetCertificate(X509Certificate certificate)
        {
            this.certificate = certificate;
            return this;
        }

        public ReaderProperties SetCertificateKey(ICipherParameters certificateKey)
        {
            this.certificateKey = certificateKey;
            return this;
        }

        public ReaderProperties SetOwnerPassword(byte[] ownerPassword)
        {
            this.ownerPassword = ownerPassword;
            return this;
        }

        public ReaderProperties SetPartialRead(bool partialRead)
        {
            this.partialRead = partialRead;
            return this;
        }

        public ReaderProperties SetCloseSourceOnconstructorError(bool closeSourceOnconstructorError)
        {
            this.closeSourceOnconstructorError = closeSourceOnconstructorError;
            return this;
        }

        public ReaderProperties SetMemoryLimitsAwareHandler(MemoryLimitsAwareHandler memoryLimitsAwareHandler)
        {
            this.memoryLimitsAwareHandler = memoryLimitsAwareHandler;
            return this;
        }
    }
}
