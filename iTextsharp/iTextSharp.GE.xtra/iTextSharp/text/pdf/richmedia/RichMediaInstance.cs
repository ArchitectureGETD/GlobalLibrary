using System;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.exceptions;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * The RichMediaInstance dictionary, referenced by the Instances entry
     * of the RichMediaConfiguration, describes a single instance of an asset
     * with settings to populate the artwork of an annotation.
     * See ExtensionLevel 3 p88
     * @see RichMediaConfiguration
     * @since   5.0.0
     */
    public class RichMediaInstance : PdfDictionary {

        /** True if the instance is a flash animation. */
        protected bool flash;
        
        /**
         * Creates a RichMediaInstance. Also specifies the content type
         * for the instance. Valid values are 3D, Flash, Sound, and Video.
         * The subtype must match the asset file type of the instance.
         * @param   subtype possible values are:
         * PdfName._3D, PdfName.FLASH, PdfName.SOUND, and PdfName.VIDEO.
         */
        public RichMediaInstance(PdfName subtype) : base(PdfName.RICHMEDIAINSTANCE) {
            Put(PdfName.SUBTYPE, subtype);
            flash = PdfName.FLASH.Equals(subtype);
        }
        
        /**
         * Sets the parameters. This will only work for Flash.
         * @param params    a RichMediaParams object
         */
        virtual public RichMediaParams Params {
            set {
                if (flash) {
                    Put(PdfName.PARAMS, value);
                }
                else {
                    throw new IllegalPdfSyntaxException("Parameters can only be set for Flash instances.");
                }
            }
        }
        
        /**
         * Sets a dictionary that shall be an indirect object reference
         * to a file specification dictionary that is also referenced
         * in the Assets name tree of the content of the annotation.
         * @param   asset   a reference to a dictionary present in the Assets name tree
         */
        virtual public PdfIndirectReference Asset {
            set {
                Put(PdfName.ASSET, value);
            }
        }
    }
}
