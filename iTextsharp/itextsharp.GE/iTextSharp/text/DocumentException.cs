using System;

namespace iTextSharp.GE.text {
    /// <summary>
    /// Signals that an error has occurred in a Document.
    /// </summary>
    /// <seealso cref="T:iTextSharp.GE.text.BadElementException"/>
    /// <seealso cref="T:iTextSharp.GE.text.Document"/>
    /// <seealso cref="T:iTextSharp.GE.text.DocWriter"/>
    /// <seealso cref="T:iTextSharp.GE.text.IDocListener"/>
	[Serializable]
    public class DocumentException : Exception {
        /// <summary>
        /// Constructs a new DocumentException
        /// </summary>
        /// <overloads>
        /// Has two overloads.
        /// </overloads>
        public DocumentException() : base() {}

        /// <summary>
        /// Construct a new DocumentException
        /// </summary>
        /// <param name="message">error message</param>
        public DocumentException(string message) : base(message) {}

		protected DocumentException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Constructs a DocumentException with a message and a Exception.
        /// </summary>
        /// <param name="message">a message describing the exception</param>
        /// <param name="ex">an exception that has to be turned into a DocumentException</param>
        public DocumentException(String message, Exception ex) : base (message, ex) { }
    }
}
