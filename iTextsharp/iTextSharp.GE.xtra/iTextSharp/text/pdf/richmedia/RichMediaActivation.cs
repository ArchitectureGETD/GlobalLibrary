using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * The RichMediaActivation dictionary specifies the style of presentation,
     * default script behavior, default view information, and animation style
     * when the annotation is activated.
     * See ExtensionLevel 3 p78
     * @since   5.0.0
     */
    public class RichMediaActivation : PdfDictionary {
        
        /**
         * Creates a RichMediaActivation dictionary.
         */
        public RichMediaActivation() : base (PdfName.RICHMEDIAACTIVATION) {
        }
        
        /**
         * Sets the activation condition.
         * Set it to XA if the annotation is explicitly activated by a user action
         * or script (this is the default).
         * To PO, if the annotation is activated as soon as the page that contains
         * the annotation receives focus as the current page.
         * To PV, if the annotation is activated as soon as any part of the page
         * that contains the annotation becomes visible. One example is in a
         * multiple-page presentation. Only one page is the current page although
         * several are visible.
         * @param   condition   possible values are:
         *      PdfName.XA, PdfName.PO, or PdfName.PV
         */
        virtual public PdfName Condition {
            set {
                Put(PdfName.CONDITION, value);
            }
        }
        
        /**
         * Sets the animation dictionary describing the preferred method
         * that conforming readers should use to drive keyframe animations
         * present in this artwork.
         * @param   animation   a RichMediaAnimation dictionary
         */
        virtual public RichMediaAnimation Animation {
            set {
                Put(PdfName.ANIMATION, value);
            }
        }
        
        /**
         * Sets an indirect object reference to a 3D view dictionary
         * that shall also be referenced by the Views array within the
         * annotation's RichMediaContent dictionary.
         * @param   view    an indirect reference
         */
        virtual public PdfIndirectReference View {
            set {
                Put(PdfName.VIEW, value);
            }
        }
        
        /**
         * Sets an indirect object reference to a RichMediaConfiguration
         * dictionary that shall also be referenced by the Configurations
         * array in the RichMediaContent dictionary (which is part of
         * the RichMediaAnnotation object).
         * @param   configuration   an indirect reference
         */
        virtual public PdfIndirectReference Configuration {
            set {
                Put(PdfName.CONFIGURATION, value);
            }
        }
        
        /**
         * Sets a RichMediaPresentation dictionary that contains information
         * as to how the annotation and user interface elements will be visually
         * laid out and drawn.
         * @param   richMediaPresentation   a RichMediaPresentation object
         */
        virtual public RichMediaPresentation Presentation {
            set {
                Put(PdfName.PRESENTATION, value);
            }
        }
        
        /**
         * Sets an array of indirect object references to file specification
         * dictionaries, each of which describe a JavaScript file that shall
         * be present in the Assets name tree of the RichMediaContent dictionary.
         * @param   scripts a PdfArray
         */
        virtual public PdfArray Scripts {
            set {
                Put(PdfName.SCRIPTS, value);
            }
        }
    }
}
