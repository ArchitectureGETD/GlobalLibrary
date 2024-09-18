using System;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.interfaces {

    /**
    * A PDF page can have an open and/or close action.
    */

    public interface IPdfPageActions {
        /**
        * Sets the open and close page additional action.
        * @param actionType the action type. It can be <CODE>PdfWriter.PAGE_OPEN</CODE>
        * or <CODE>PdfWriter.PAGE_CLOSE</CODE>
        * @param action the action to perform
        * @throws DocumentException if the action type is invalid
        */    
        void SetPageAction(PdfName actionType, PdfAction action);

        /**
        * Sets the display duration for the page (for presentations)
        * @param seconds   the number of seconds to display the page
        */
        int Duration {
            set;
        }
        
        /**
        * Sets the transition for the page
        * @param transition   the Transition object
        */
        PdfTransition Transition {
            set;
        }
    }
}
