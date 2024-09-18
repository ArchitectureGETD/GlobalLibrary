using System;

namespace iTextSharp.GE.text.pdf {

    /** Represents a pattern. Can be used in high-level constructs (Paragraph, Cell, etc.).
     */
    public class PatternColor : ExtendedColor {
        /** The actual pattern.
                                   */    
        PdfPatternPainter painter;
    
        /** Creates a color representing a pattern.
                                   * @param painter the actual pattern
                                   */    
        public PatternColor(PdfPatternPainter painter) : base(TYPE_PATTERN, .5f, .5f, .5f) {
            this.painter = painter;
        }
    
        /** Gets the pattern.
         * @return the pattern
         */    
        virtual public PdfPatternPainter Painter {
            get {
                return this.painter;
            }
        }

        public override bool Equals(Object obj) {
            return obj is PatternColor && (((PatternColor)obj).painter).Equals(this.painter);
        }
        
        public override int GetHashCode() {
            return painter.GetHashCode();
        }
    }
}
