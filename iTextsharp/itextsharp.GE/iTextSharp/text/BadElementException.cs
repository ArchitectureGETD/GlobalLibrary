using System;

namespace iTextSharp.GE.text 
{
    /// <summary>
    /// Signals an attempt to create an Element that hasn't got the right form.
    /// </summary>
    /// <seealso cref="T:iTextSharp.GE.text.Cell"/>
    /// <seealso cref="T:iTextSharp.GE.text.Table"/>
	[Serializable]
    public class BadElementException : DocumentException 
    {
        public BadElementException() : base() {}

        public BadElementException(string message) : base(message) {}

		protected BadElementException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
