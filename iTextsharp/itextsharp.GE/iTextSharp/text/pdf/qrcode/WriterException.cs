using System;

namespace iTextSharp.GE.text.pdf.qrcode {

    /**
     * A base class which covers the range of exceptions which may occur when encoding a barcode using
     * the Writer framework.
     *
     */
	[Serializable]
    public sealed class WriterException : Exception {

        public WriterException()
            : base() {
        }

        public WriterException(String message)
            : base(message) {
        }

		protected WriterException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
