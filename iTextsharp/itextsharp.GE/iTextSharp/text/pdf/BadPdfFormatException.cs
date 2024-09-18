using System;

namespace iTextSharp.GE.text.pdf
{
    /**
     * Signals that a bad PDF format has been used to construct a <CODE>PdfObject</CODE>.
     *
     * @see        PdfException
     * @see        PdfBoolean
     * @see        PdfNumber
     * @see        PdfString
     * @see        PdfName
     * @see        PdfDictionary
     * @see        PdfFont
     */

	[Serializable]
    public class BadPdfFormatException : Exception
    {
        public BadPdfFormatException() : base() {}

        public BadPdfFormatException(string message) : base(message) {}

		protected BadPdfFormatException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
