using System;

namespace iTextSharp.GE.text.pdf {
    /**
    *
    * @author  psoares
    */
	[Serializable]
    public class PdfXConformanceException : PdfIsoConformanceException {
        
        /** Creates a new instance of PdfXConformanceException. */
        public PdfXConformanceException() {
        }
        
        /**
        * Creates a new instance of PdfXConformanceException.
        * @param s
        */
        public PdfXConformanceException(String s) : base(s) {
        }   
 
		protected PdfXConformanceException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
