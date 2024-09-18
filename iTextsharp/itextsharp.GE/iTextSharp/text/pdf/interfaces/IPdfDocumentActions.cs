using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.interfaces {

    
    public interface IPdfDocumentActions {
        /**
        * When the document opens it will jump to the destination with
        * this name.
        * @param name the name of the destination to jump to
        */
        void SetOpenAction(String name);
        
        /**
        * When the document opens this <CODE>action</CODE> will be
        * invoked.
        * @param action the action to be invoked
        */
        void SetOpenAction(PdfAction action);
        
        /**
        * Additional-actions defining the actions to be taken in
        * response to various trigger events affecting the document
        * as a whole. The actions types allowed are: <CODE>DOCUMENT_CLOSE</CODE>,
        * <CODE>WILL_SAVE</CODE>, <CODE>DID_SAVE</CODE>, <CODE>WILL_PRINT</CODE>
        * and <CODE>DID_PRINT</CODE>.
        *
        * @param actionType the action type
        * @param action the action to execute in response to the trigger
        * @throws DocumentException on invalid action type
        */
        void SetAdditionalAction(PdfName actionType, PdfAction action);

    }
}
