using System;
using System.Collections.Generic;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.draw {

    /**
    * Helper class implementing the DrawInterface. Can be used to add
    * horizontal or vertical separators. Won't draw anything unless
    * you implement the draw method.
    * @since   2.1.2
    */

    public class VerticalPositionMark : IDrawInterface, IElement {

        /** Another implementation of the DrawInterface; its draw method will overrule LineSeparator.Draw(). */
        protected IDrawInterface drawInterface = null;

        /** The offset for the line. */
        protected float offset = 0;
        
        /**
        * Creates a vertical position mark that won't draw anything unless
        * you define a DrawInterface.
        */
        public VerticalPositionMark() { 
        }

        /**
        * Creates a vertical position mark that won't draw anything unless
        * you define a DrawInterface.
        * @param   drawInterface   the drawInterface for this vertical position mark.
        * @param   offset          the offset for this vertical position mark.
        */
        public VerticalPositionMark(IDrawInterface drawInterface, float offset) {
            this.drawInterface = drawInterface;
            this.offset = offset;
        }
        
        /**
        * @see com.lowagie.text.pdf.draw.DrawInterface#draw(com.lowagie.text.pdf.PdfContentByte, float, float, float, float, float)
        */
        public virtual void Draw(PdfContentByte canvas, float llx, float lly, float urx, float ury, float y) {
            if (drawInterface != null) {
                drawInterface.Draw(canvas, llx, lly, urx, ury, y + offset);
            }
        }
        
        /**
        * @see com.lowagie.text.Element#process(com.lowagie.text.ElementListener)
        */
        virtual public bool Process(IElementListener listener) {
            try {
                return listener.Add(this);
            } catch (DocumentException) {
                return false;
            }
        }

        /**
        * @see com.lowagie.text.Element#type()
        */
        virtual public int Type {
            get {
                return Element.YMARK;
            }
        }

        /**
        * @see com.lowagie.text.Element#isContent()
        */
        virtual public bool IsContent() {
            return true;
        }

        /**
        * @see com.lowagie.text.Element#isNestable()
        */
        virtual public bool IsNestable() {
            return false;
        }

        /**
        * @see com.lowagie.text.Element#getChunks()
        */
        virtual public IList<Chunk> Chunks {
            get {
                List<Chunk> list = new List<Chunk>();
                list.Add(new Chunk(this, true));
                return list;
            }
        }

        /**
        * Setter for the interface with the overruling Draw() method.
        * @param drawInterface a DrawInterface implementation
        */
        public virtual IDrawInterface DrawInterface {
            set {
                drawInterface = value;
            }
            get {
                return drawInterface;
            }
        }

        /**
        * Setter for the offset. The offset is relative to the current
        * Y position. If you want to underline something, you have to
        * choose a negative offset.
        * @param offset    an offset
        */
        public virtual float Offset {
            set {
                offset = value;
            }
            get {
                return offset;
            }
        }
    }
}
