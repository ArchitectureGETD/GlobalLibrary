using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.draw {
    /**
    * Interface for an Element that allows you to draw something at the current
    * vertical position. Trivial implementations are LineSeparator and VerticalPositionMark.
    * It is also used to define what has to be drawn by a separator chunk.
    * @since 2.1.2
    */
    public interface IDrawInterface {
        /**
        * Implement this method if you want to draw something at the current Y position
        * (for instance a line).
        * @param   canvas  the canvas on which you can draw
        * @param   llx     the x coordinate of the left page margin
        * @param   lly     the y coordinate of the bottom page margin
        * @param   urx     the x coordinate of the right page margin
        * @param   ury     the y coordinate of the top page margin
        * @param   y       the current y position on the page
        */
        void Draw(PdfContentByte canvas, float llx, float lly, float urx, float ury, float y);    
    }
}
