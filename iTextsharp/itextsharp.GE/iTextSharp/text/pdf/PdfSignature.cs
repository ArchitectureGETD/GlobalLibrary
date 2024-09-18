using iTextSharp.GE.text.pdf.security;

namespace iTextSharp.GE.text.pdf {

    /** Implements the signature dictionary.
     *
     * @author Paulo Soares
     */
    public class PdfSignature : PdfDictionary {

        /** Creates new PdfSignature */
        public PdfSignature(PdfName filter, PdfName subFilter) : base(PdfName.SIG) {
            Put(PdfName.FILTER, filter);
            Put(PdfName.SUBFILTER, subFilter);
        }
        
        virtual public int[] ByteRange {
            set {
                PdfArray array = new PdfArray();
                for (int k = 0; k < value.Length; ++k)
                    array.Add(new PdfNumber(value[k]));
                Put(PdfName.BYTERANGE, array);
            }
        }
        
        virtual public byte[] Contents {
            set {
                Put(PdfName.CONTENTS, new PdfString(value).SetHexWriting(true));
            }
        }
        
        virtual public byte[] Cert {
            set {
                Put(PdfName.CERT, new PdfString(value));
            }
        }
        
        virtual public string Name {
            set {
                Put(PdfName.NAME, new PdfString(value, PdfObject.TEXT_UNICODE));
            }
        }

        virtual public PdfDate Date {
            set {
                Put(PdfName.M, value);
            }
        }

        virtual public string Location {
            set {
                Put(PdfName.LOCATION, new PdfString(value, PdfObject.TEXT_UNICODE));
            }
        }

        virtual public string Reason {
            set {
                Put(PdfName.REASON, new PdfString(value, PdfObject.TEXT_UNICODE));
            }
        }

        /**
         * Sets the signature creator name in the
         * {@link PdfSignatureBuildProperties} dictionary.
         * 
         * @param name
         */
        virtual public string SignatureCreator {
            set {
                if (value != null) {
                    PdfSignatureBuildProperties.SignatureCreator = value;
                }
            }
        }

        /**
         * Gets the {@link PdfSignatureBuildProperties} instance if it exists, if
         * not it adds a new one and returns this.
         * 
         * @return {@link PdfSignatureBuildProperties}
         */
        private PdfSignatureBuildProperties PdfSignatureBuildProperties {
            get {
                PdfSignatureBuildProperties buildPropDic = (PdfSignatureBuildProperties) GetAsDict(PdfName.PROP_BUILD);
                if (buildPropDic == null) {
                    buildPropDic = new PdfSignatureBuildProperties();
                    Put(PdfName.PROP_BUILD, buildPropDic);
                }
                return buildPropDic;
            }
        }
        
        virtual public string Contact {
            set {
                Put(PdfName.CONTACTINFO, new PdfString(value, PdfObject.TEXT_UNICODE));
            }
        }
    }
}
