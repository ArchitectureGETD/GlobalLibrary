using System;
using System.IO;

namespace iTextSharp.GE.text.exceptions {

    /**
     * Typed exception used when opening an existing PDF document.
     * Gets thrown when the document isn't a valid PDF document.
     * @since 2.1.5 It was written for iText 2.0.8, but moved to another package
     */
	[Serializable]
    public class BadPasswordException : IOException {

        /**
         * Creates an exception saying the user password was incorrect.
         */
        public BadPasswordException(String message) : base(message) {
        }

		protected BadPasswordException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
