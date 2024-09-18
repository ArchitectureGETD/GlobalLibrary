using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * the annotation handler specific to the target instance specified
     * by the TI key in the parent rich-media-execute action dictionary.
     * See table 8.45b in the Adobe 1.7 ExtensionLevel 3 document.
     * See RichMediaExecute in the Adobe document.
     * 
     * @since   5.0.0
     */
    public class RichMediaCommand : PdfDictionary {
        
        /**
         * Creates a RichMediaCommand dictionary.
         * @param   command a text string specifying the script command
         * (a primitive ActionScript or JavaScript function name).
         */
        public RichMediaCommand(PdfString command) : base(PdfName.RICHMEDIACOMMAND) {
            Put(PdfName.C, command);
        }
        
        /**
         * Sets the arguments for the command.
         * @param   args a PdfObject that specifies the arguments to the command.
         * The object can be a PdfString, PdfNumber or PdfBoolean, or a PdfArray
         * containing those objects.
         */
        virtual public PdfObject Arguments {
            set {
                Put(PdfName.A, value);
            }
        }
    }
}
