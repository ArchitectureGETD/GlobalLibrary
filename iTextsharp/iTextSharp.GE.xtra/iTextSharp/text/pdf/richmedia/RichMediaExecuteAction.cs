using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * A rich-media-execute action identifies a rich media annotation and
     * specifies a command to be sent to that annotation's handler.
     * See table 8.45a in the Adobe 1.7 ExtensionLevel 3 document
     * @see RichMediaAnnotation
     * @see RichMediaCommand
     * @since   5.0.0
     */
    public class RichMediaExecuteAction : PdfAction {

        /**
         * Creates a RichMediaExecute action dictionary.
         * @param   ref a reference to rich media annotation dictionary for
         * an annotation for which to execute the script command.
         * @param   command the command name and arguments to be
         * executed when the rich-media-execute action is invoked.
         */
        public RichMediaExecuteAction(PdfIndirectReference refi, RichMediaCommand command) : base() {
            Put(PdfName.S, PdfName.RICHMEDIAEXECUTE);
            Put(PdfName.TA, refi);
            Put(PdfName.CMD, command);
        }
        
        /**
         * Sets the target instance for this action.
         * @param   ref a reference to a RichMediaInstance
         */
        virtual public PdfIndirectReference RichMediaInstance {
            set {
                Put(PdfName.TI, value);
            }
        }
    }
}
