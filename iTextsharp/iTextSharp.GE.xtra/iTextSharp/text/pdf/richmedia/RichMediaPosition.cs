using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * The position of the window in the reader presentation area is described
     * by the RichMediaPosition dictionary. The position of the window remains
     * fixed, regardless of the page translation.
     * See ExtensionLevel 3 p84
     * @since   5.0.0
     */
    public class RichMediaPosition : PdfDictionary {

        /**
         * Constructs a RichMediaPosition dictionary.
         */
        public RichMediaPosition() : base(PdfName.RICHMEDIAPOSITION) {
        }
        
        /**
         * Set the horizontal alignment.
         * @param   hAlign possible values are
         * PdfName.NEAR, PdfName.CENTER, or PdfName.FAR
         */
        virtual public PdfName HAlign {
            set {
                Put(PdfName.HALIGN, value);
            }
        }
        
        /**
         * Set the horizontal alignment.
         * @param   vAlign possible values are
         * PdfName.NEAR, PdfName.CENTER, or PdfName.FAR
         */
        virtual public PdfName VAlign {
            set {
                Put(PdfName.VALIGN, value);
            }
        }
        
        /**
         * Sets the offset from the alignment point specified by the HAlign key.
         * A positive value for HOffset, when HAlign is either Near or Center,
         * offsets the position towards the Far direction. A positive value for
         * HOffset, when HAlign is Far, offsets the position towards the Near
         * direction.
         * @param   hOffset an offset
         */
        virtual public float HOffset {
            set {
                Put(PdfName.HOFFSET, new PdfNumber(value));
            }
        }
        
        /**
         * Sets the offset from the alignment point specified by the VAlign key.
         * A positive value for VOffset, when VAlign is either Near or Center,
         * offsets the position towards the Far direction. A positive value for
         * VOffset, when VAlign is Far, offsets the position towards the Near
         * direction.
         * @param   vOffset an offset
         */
        virtual public float VOffset {
            set {
                Put(PdfName.VOFFSET, new PdfNumber(value));
            }
        }
    }
}
