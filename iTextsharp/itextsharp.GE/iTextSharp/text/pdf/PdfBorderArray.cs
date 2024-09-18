using System;

namespace iTextSharp.GE.text.pdf {

    /**
     * A <CODE>PdfBorderArray</CODE> defines the border of a <CODE>PdfAnnotation</CODE>.
     *
     * @see        PdfArray
     */

    public class PdfBorderArray : PdfArray {
    
        // constructors
    
        /**
         * Constructs a new <CODE>PdfBorderArray</CODE>.
         */
    
        public PdfBorderArray(float hRadius, float vRadius, float width) : this(hRadius, vRadius, width, null) {}
    
        /**
         * Constructs a new <CODE>PdfBorderArray</CODE>.
         */
    
        public PdfBorderArray(float hRadius, float vRadius, float width, PdfDashPattern dash) : base(new PdfNumber(hRadius)) {
            Add(new PdfNumber(vRadius));
            Add(new PdfNumber(width));
            if (dash != null)
                Add(dash);
        }
    }
}
