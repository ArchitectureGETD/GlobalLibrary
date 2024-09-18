using System;

namespace iTextSharp.GE.text.pdf.hyphenation {
    /**
     */
	[Serializable]
    public class HyphenationException : Exception {

        public HyphenationException(String msg) : base(msg) {
        }

		protected HyphenationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
