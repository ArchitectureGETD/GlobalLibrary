namespace iTextSharp.GE.text.pdf.parser {

    /**
     * Represents an inline image from a PDF
     * @since 5.1.4
     */
    public class InlineImageInfo {
        private byte[] samples;
        private PdfDictionary imageDictionary;
        
        public InlineImageInfo(byte[] samples, PdfDictionary imageDictionary) {
            this.samples = samples;
            this.imageDictionary = imageDictionary;
        }
        
        /**
         * @return the image dictionary associated with this inline image
         */
        virtual public PdfDictionary ImageDictionary {
            get {
                return imageDictionary;
            }
        }
        
        /**
         * @return the raw samples associated with this inline image
         */
        virtual public byte[] Samples {
            get {
                return samples;
            }
        }
    }
}
