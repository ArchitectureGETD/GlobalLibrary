using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * The RichMediaActivation dictionary specifies the condition
     * that causes deactivation of the annotation.
     * See ExtensionLevel 3 p80
     * @since   5.0.0
     */
    public class RichMediaDeactivation : PdfDictionary {
        
        /**
         * Creates a RichMediaActivation dictionary.
         */
        public RichMediaDeactivation() : base(PdfName.RICHMEDIADEACTIVATION) {
        }
        
        /**
         * Sets the activation condition.
         * Set it to XD, and the annotation is explicitly deactivated by a user action
         * or script.
         * To PC, and the annotation is deactivated as soon as the page that contains
         * the annotation loses focus as the current page.
         * To PI, abd the annotation is deactivated as soon as the entire page that
         * contains the annotation is no longer visible.
         * @param   condition   possible values are:
         *      PdfName.XD, PdfName.PC, or PdfName.PI
         */
        virtual public PdfName Condition {
            set {
                Put(PdfName.CONDITION, value);
            }
        }
    }
}
