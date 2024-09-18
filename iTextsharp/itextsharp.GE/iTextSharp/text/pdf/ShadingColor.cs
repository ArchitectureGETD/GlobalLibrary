using System;

namespace iTextSharp.GE.text.pdf {

    /** Implements a shading pattern as a <code>Color</code>.
     *
     * @author Paulo Soares
     */
    public class ShadingColor : ExtendedColor {

        PdfShadingPattern shadingPattern;

        public ShadingColor(PdfShadingPattern shadingPattern) : base(TYPE_SHADING, .5f, .5f, .5f) {
            this.shadingPattern = shadingPattern;
        }

        virtual public PdfShadingPattern PdfShadingPattern {
            get {
                return shadingPattern;
            }
        }

        public override bool Equals(Object obj) {
            return obj is ShadingColor && (((ShadingColor)obj).shadingPattern).Equals(this.shadingPattern);
        }
        
        public override int GetHashCode() {
            return shadingPattern.GetHashCode();
        }
    }
}
