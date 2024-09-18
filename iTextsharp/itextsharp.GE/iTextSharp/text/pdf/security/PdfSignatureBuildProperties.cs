namespace iTextSharp.GE.text.pdf.security {

    /**
     * Dictionary that stores signature build properties.
     * @author Kwinten Pisman
     */
    internal class PdfSignatureBuildProperties : PdfDictionary {

        /** Creates new PdfSignatureBuildProperties */
        public PdfSignatureBuildProperties() : base() {
        }

        /**
         * Sets the signatureCreator property in the underlying
         * {@link PdfSignatureAppDictionary} dictionary.
         * 
         * @param name
         */
        virtual public string SignatureCreator {
            set { GetPdfSignatureAppProperty().SignatureCreator = value; }
        }

        /**
         * Gets the {@link PdfSignatureAppDictionary} from this dictionary. If it
         * does not exist, it adds a new {@link PdfSignatureAppDictionary} and
         * returns this instance.
         * 
         * @return {@link PdfSignatureAppDictionary}
         */
        private PdfSignatureAppDictionary GetPdfSignatureAppProperty() {
            PdfSignatureAppDictionary appPropDic = (PdfSignatureAppDictionary) GetAsDict(PdfName.APP);
            if (appPropDic == null) {
                appPropDic = new PdfSignatureAppDictionary();
                Put(PdfName.APP, appPropDic);
            }
            return appPropDic;
        }
    }
}
