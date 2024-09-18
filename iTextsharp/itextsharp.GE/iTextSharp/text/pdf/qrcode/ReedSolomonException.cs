using System;

namespace iTextSharp.GE.text.pdf.qrcode {

    /**
     * <p>Thrown when an exception occurs during Reed-Solomon decoding, such as when
     * there are too many errors to correct.</p>
     *
     */
	[Serializable]
    public sealed class ReedSolomonException : Exception {

        public ReedSolomonException(String message)
            : base(message) {
        }

		protected ReedSolomonException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
