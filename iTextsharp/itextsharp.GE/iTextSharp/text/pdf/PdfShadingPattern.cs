using System;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf {

    /** Implements the shading pattern dictionary.
     *
     * @author Paulo Soares
     */
    public class PdfShadingPattern : PdfDictionary {

        protected PdfShading shading;
    
        protected PdfWriter writer;
    
        protected float[] matrix = {1, 0, 0, 1, 0, 0};
    
        protected PdfName patternName;

        protected PdfIndirectReference patternReference;

        /** Creates new PdfShadingPattern */
        public PdfShadingPattern(PdfShading shading) {
            writer = shading.Writer;
            Put(PdfName.PATTERNTYPE, new PdfNumber(2));
            this.shading = shading;
        }
        
        internal PdfName PatternName {
            get {
                return patternName;
            }
        }

        internal PdfName ShadingName {
            get {
                return shading.ShadingName;
            }
        }
    
        internal PdfIndirectReference PatternReference {
            get {
                if (patternReference == null)
                    patternReference = writer.PdfIndirectReference;
                return patternReference;
            }
        }
    
        internal PdfIndirectReference ShadingReference {
            get {
                return shading.ShadingReference;
            }
        }
    
        internal int Name {
            set {
                patternName = new PdfName("P" + value);
            }
        }
    
        virtual public void AddToBody() {
            Put(PdfName.SHADING, ShadingReference);
            Put(PdfName.MATRIX, new PdfArray(matrix));
            writer.AddToBody(this, PatternReference);
        }
    
        virtual public float[] Matrix {
            get {
                return matrix;
            }

            set {
                if (value.Length != 6)
                    throw new Exception(MessageLocalization.GetComposedMessage("the.matrix.size.must.be.6"));
                this.matrix = value;
            }
        }
    
        virtual public PdfShading Shading {
            get {
                return shading;
            }
        }
    
        internal ColorDetails ColorDetails {
            get {
                return shading.ColorDetails;
            }
        }

    }
}
