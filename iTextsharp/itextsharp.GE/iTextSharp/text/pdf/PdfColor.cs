using System;
using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf {
    /**
     * A <CODE>PdfColor</CODE> defines a Color (it's a <CODE>PdfArray</CODE> containing 3 values).
     *
     * @see        PdfDictionary
     */

    internal class PdfColor : PdfArray {
    
        // constructors
    
        /**
        * Constructs a new <CODE>PdfColor</CODE>.
        *
        * @param        red            a value between 0 and 255
        * @param        green        a value between 0 and 255
        * @param        blue        a value between 0 and 255
        */
    
        internal PdfColor(int red, int green, int blue) : base(new PdfNumber((double)(red & 0xFF) / 0xFF)) {
            Add(new PdfNumber((double)(green & 0xFF) / 0xFF));
            Add(new PdfNumber((double)(blue & 0xFF) / 0xFF));
        }
    
        internal PdfColor(BaseColor color) : this(color.R, color.G, color.B) {}
    }
}
