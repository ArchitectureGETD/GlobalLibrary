using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.draw {

    /**
    * Element that draws a dotted line from left to right.
    * Can be added directly to a document or column.
    * Can also be used to create a separator chunk.
    * @since   2.1.2 
    */
    public class DottedLineSeparator : LineSeparator {

        /** the gap between the dots. */
        protected float gap = 5;
        
        /**
        * @see com.lowagie.text.pdf.draw.DrawInterface#draw(com.lowagie.text.pdf.PdfContentByte, float, float, float, float, float)
        */
        public override void Draw(PdfContentByte canvas, float llx, float lly, float urx, float ury, float y) {
            canvas.SaveState();
            canvas.SetLineWidth(lineWidth);
            canvas.SetLineCap(PdfContentByte.LINE_CAP_ROUND);
            canvas.SetLineDash(0, gap, gap / 2);
            DrawLine(canvas, llx, urx, y);
            canvas.RestoreState();
        }

        /**
        * Setter for the gap between the center of the dots of the dotted line.
        * @param   gap the gap between the center of the dots
        */
        virtual public float Gap {
            get {
                return gap;
            }
            set {
                gap = value;
            }
        }
    }
}
