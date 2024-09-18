using System;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.spatial {

    /**
     * Parent class for the Measure dictionaries.
     * @since 5.1.0
     */
    public abstract class Measure : PdfDictionary {

        /**
         * Creates a Measure dictionary.
         */
        public Measure() : base(PdfName.MEASURE) {
            base.Put(PdfName.SUBTYPE, GetSubType());
        }
        
        /**
         * Gets the subtype (for internal use only).
         * @return the name of the SubType.
         */
        internal abstract PdfName GetSubType();
    }
}
