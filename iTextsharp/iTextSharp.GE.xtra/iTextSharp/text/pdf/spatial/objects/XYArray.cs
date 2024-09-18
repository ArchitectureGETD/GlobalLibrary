using System;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.spatial.objects {

    /**
     * Creates an Array with two PdfNumber elements, representing an X and Y coordinate.
     * @since 5.1.0
     */
    public class XYArray : NumberArray {
        
        /**
         * Creates an XYArray object.
         * @param x the value of the X coordinate
         * @param y the value of the Y coordinate
         */
        public XYArray(float x, float y) : base(x, y) {
        }
    }
}
