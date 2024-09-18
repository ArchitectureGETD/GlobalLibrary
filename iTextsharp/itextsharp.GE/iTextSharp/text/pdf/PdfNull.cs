using System;

namespace iTextSharp.GE.text.pdf {

    /**
     * <CODE>PdfNull</CODE> is the Null object represented by the keyword <VAR>null</VAR>.
     * <P>
     * This object is described in the 'Portable Document Format Reference Manual version 1.3'
     * section 4.9 (page 53).
     *
     * @see        PdfObject
     */

    public class PdfNull : PdfObject {
    
        // static membervariables
    
        /** This is an instance of the <CODE>PdfNull</CODE>-object. */
        public static PdfNull    PDFNULL = new PdfNull();
    
        // constructors
    
        /**
         * Constructs a <CODE>PdfNull</CODE>-object.
         * <P>
         * You never need to do this yourself, you can always use the static object <VAR>PDFNULL</VAR>.
         */
    
        public PdfNull() : base(NULL, "null") {}

        public override String ToString() {
    	    return "null";
        }
    }
}
