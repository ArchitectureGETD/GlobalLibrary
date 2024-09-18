using iTextSharp.GE.text.pdf.intern;

namespace iTextSharp.GE.text.pdf {
    /** The graphic state dictionary.
    *
    * @author Paulo Soares
    */
    public class PdfGState : PdfDictionary {
        /** A possible blend mode */
        public static PdfName BM_NORMAL = new PdfName("Normal");
        /** A possible blend mode */
        public static PdfName BM_COMPATIBLE = new PdfName("Compatible");
        /** A possible blend mode */
        public static PdfName BM_MULTIPLY = new PdfName("Multiply");
        /** A possible blend mode */
        public static PdfName BM_SCREEN = new PdfName("Screen");
        /** A possible blend mode */
        public static PdfName BM_OVERLAY = new PdfName("Overlay");
        /** A possible blend mode */
        public static PdfName BM_DARKEN = new PdfName("Darken");
        /** A possible blend mode */
        public static PdfName BM_LIGHTEN = new PdfName("Lighten");
        /** A possible blend mode */
        public static PdfName BM_COLORDODGE = new PdfName("ColorDodge");
        /** A possible blend mode */
        public static PdfName BM_COLORBURN = new PdfName("ColorBurn");
        /** A possible blend mode */
        public static PdfName BM_HARDLIGHT = new PdfName("HardLight");
        /** A possible blend mode */
        public static PdfName BM_SOFTLIGHT = new PdfName("SoftLight");
        /** A possible blend mode */
        public static PdfName BM_DIFFERENCE = new PdfName("Difference");
        /** A possible blend mode */
        public static PdfName BM_EXCLUSION = new PdfName("Exclusion");
        
        /**
        * Sets the flag whether to apply overprint for stroking.
        * @param ov
        */
        virtual public bool OverPrintStroking {
            set {
                Put(PdfName.OP, value ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
            }
        }

        /**
        * Sets the flag whether to apply overprint for non stroking painting operations.
        * @param ov
        */
        virtual public bool OverPrintNonStroking {
            set {
                Put(PdfName.op_, value ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
            }
        }
        
        /**
        * Sets the flag whether to toggle knockout behavior for overprinted objects.
        * @param ov - accepts 0 or 1
        */
        virtual public int OverPrintMode {
            set {
                Put(PdfName.OPM, new PdfNumber(value == 0 ? 0 : 1));
            }
        }

        /**
        * Sets the current stroking alpha constant, specifying the constant shape or
        * constant opacity value to be used for stroking operations in the transparent
        * imaging model.
        * @param n
        */
        virtual public float StrokeOpacity {
            set {
                Put(PdfName.CA, new PdfNumber(value));
            }
        }
        
        /**
        * Sets the current stroking alpha constant, specifying the constant shape or
        * constant opacity value to be used for nonstroking operations in the transparent
        * imaging model.
        * @param n
        */
        virtual public float FillOpacity {
            set {
                Put(PdfName.ca, new PdfNumber(value));
            }
        }
        
        /**
        * The alpha source flag specifying whether the current soft mask
        * and alpha constant are to be interpreted as shape values (true)
        * or opacity values (false). 
        * @param v
        */
        virtual public bool AlphaIsShape {
            set {
                Put(PdfName.AIS, value ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
            }
        }
        
        /**
        * Determines the behaviour of overlapping glyphs within a text object
        * in the transparent imaging model.
        * @param v
        */
        virtual public bool TextKnockout {
            set {
                Put(PdfName.TK, value ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
            }
        }
        
        /**
        * The current blend mode to be used in the transparent imaging model.
        * @param bm
        */
        virtual public PdfName BlendMode {
            set {
                Put(PdfName.BM, value);
            }
        }
        
        /**
         * Set the rendering intent, possible values are: PdfName.ABSOLUTECOLORIMETRIC,
         * PdfName.RELATIVECOLORIMETRIC, PdfName.SATURATION, PdfName.PERCEPTUAL.
         * @param ri
         */
        virtual public PdfName RenderingIntent {
            set {
                Put(PdfName.RI, value);
            }
        }

        public override void ToPdf(PdfWriter writer, System.IO.Stream os) {
            PdfWriter.CheckPdfIsoConformance(writer, PdfIsoKeys.PDFISOKEY_GSTATE, this);
            base.ToPdf(writer, os);
        }
    }
}
