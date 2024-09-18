using System;

using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf {

    /**
     * Signals that an unspecified problem while constructing a PDF document.
     *
     * @see        BadPdfFormatException
     */

	[Serializable]
    public class PdfException : DocumentException {    
        public PdfException() : base() {}

        public PdfException(string message) : base(message) {}

		protected PdfException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
