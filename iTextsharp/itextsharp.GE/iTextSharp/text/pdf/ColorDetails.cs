namespace iTextSharp.GE.text.pdf {

    /** Each colorSpace in the document will have an instance of this class
     *
     * @author Phillip Pan (phillip@formstar.com)
     */
    public class ColorDetails {

        /** The indirect reference to this color
         */
        PdfIndirectReference indirectReference;
        /** The color name that appears in the document body stream
         */
        PdfName colorSpaceName;
        /** The color
         */
        ICachedColorSpace colorSpace;

        /** Each spot color used in a document has an instance of this class.
         * @param colorName the color name
         * @param indirectReference the indirect reference to the font
         * @param scolor the <CODE>PDfSpotColor</CODE>
         */
        internal ColorDetails(PdfName colorName, PdfIndirectReference indirectReference, ICachedColorSpace scolor) {
            this.colorSpaceName = colorName;
            this.indirectReference = indirectReference;
            this.colorSpace = scolor;
        }

        /** Gets the indirect reference to this color.
         * @return the indirect reference to this color
         */
        virtual public PdfIndirectReference IndirectReference {
            get {
                return indirectReference;
            }
        }

        /** Gets the color name as it appears in the document body.
         * @return the color name
         */
        internal virtual PdfName ColorSpaceName {
            get {
                return colorSpaceName;
            }
        }

        /** Gets the <CODE>SpotColor</CODE> object.
         * @return the <CODE>PdfSpotColor</CODE>
         */
        virtual public PdfObject GetPdfObject(PdfWriter writer) {
            return colorSpace.GetPdfObject(writer);
        }
    }
}
