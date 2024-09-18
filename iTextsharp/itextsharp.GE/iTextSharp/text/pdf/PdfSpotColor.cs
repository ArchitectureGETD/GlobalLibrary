using System;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf {

    /**
     * A <CODE>PdfSpotColor</CODE> defines a ColorSpace
     *
     * @see     PdfDictionary
     */
    public class PdfSpotColor : ICachedColorSpace, IPdfSpecialColorSpace {
    
        /*  The color name */
        public PdfName name;
    
        /* The alternative color space */
        public BaseColor altcs;
        // constructors

        public ColorDetails altColorDetails;
    
        /**
         * Constructs a new <CODE>PdfSpotColor</CODE>.
         *
         * @param       name        a string value
         * @param       tint        a tint value between 0 and 1
         * @param       altcs       a altnative colorspace value
         */
        public PdfSpotColor(string name, BaseColor altcs) {
            this.name = new PdfName(name);
            this.altcs = altcs;
        }

        public virtual ColorDetails[] GetColorantDetails(PdfWriter writer) {
            if (altColorDetails == null && this.altcs is ExtendedColor &&
                ((ExtendedColor) this.altcs).Type == ExtendedColor.TYPE_LAB) {
                altColorDetails = writer.AddSimple(((LabColor) altcs).LabColorSpace);
            }
            return new ColorDetails[] {altColorDetails};
        }
    
        virtual public BaseColor AlternativeCS {
            get {
                return altcs;
            }
        }

        public virtual PdfName Name {
            get { return name; }
        }

        [Obsolete]
        protected internal virtual PdfObject GetSpotObject(PdfWriter writer) {
            return GetPdfObject(writer);
        }

        public virtual PdfObject GetPdfObject(PdfWriter writer) {
            PdfArray array = new PdfArray(PdfName.SEPARATION);
            array.Add(name);
            PdfFunction func = null;
            if (altcs is ExtendedColor) {
                int type = ((ExtendedColor) altcs).Type;
                switch (type) {
                    case ExtendedColor.TYPE_GRAY:
                        array.Add(PdfName.DEVICEGRAY);
                        func = PdfFunction.Type2(writer, new float[] {0, 1}, null, new float[] {1},
                            new float[] {((GrayColor) altcs).Gray}, 1);
                        break;
                    case ExtendedColor.TYPE_CMYK:
                        array.Add(PdfName.DEVICECMYK);
                        CMYKColor cmyk = (CMYKColor) altcs;
                        func = PdfFunction.Type2(writer, new float[] {0, 1}, null, new float[] {0, 0, 0, 0},
                            new float[] {cmyk.Cyan, cmyk.Magenta, cmyk.Yellow, cmyk.Black}, 1);
                        break;
                    case ExtendedColor.TYPE_LAB:
                        LabColor lab = (LabColor) altcs;
                        if (altColorDetails != null)
                            array.Add(altColorDetails.IndirectReference);
                        else
                            array.Add(lab.LabColorSpace.GetPdfObject(writer));
                        func = PdfFunction.Type2(writer, new float[] {0, 1}, null, new float[] {100f, 0f, 0f},
                            new float[] {lab.L, lab.A, lab.B}, 1);
                        break;
                    default:
                        throw new Exception(
                            MessageLocalization.GetComposedMessage(
                                "only.rgb.gray.and.cmyk.are.supported.as.alternative.color.spaces"));
                }
            } else {
                array.Add(PdfName.DEVICERGB);
                func = PdfFunction.Type2(writer, new float[] {0, 1}, null, new float[] {1, 1, 1},
                    new float[] {(float) altcs.R/255, (float) altcs.G/255, (float) altcs.B/255}, 1);
            }
            array.Add(func.Reference);
            return array;
        }


        public override bool Equals(Object o) {
            if (this == o) return true;
            if (!(o is PdfSpotColor)) return false;

            PdfSpotColor spotColor = (PdfSpotColor) o;

            if (!altcs.Equals(spotColor.altcs)) return false;
            if (!name.Equals(spotColor.name)) return false;

            return true;
        }

        public override int GetHashCode() {
            int result = name.GetHashCode();
            result = 31*result + altcs.GetHashCode();
            return result;
        }
    }
}
