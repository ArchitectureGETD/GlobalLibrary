using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.interfaces {

    public interface IPdfAnnotations {
       
        PdfAcroForm AcroForm {
            get;
        }
        
        /**
        * Use this methods to add a <CODE>PdfAnnotation</CODE> or a <CODE>PdfFormField</CODE>
        * to the document. Only the top parent of a <CODE>PdfFormField</CODE>
        * needs to be added.
        * @param annot the <CODE>PdfAnnotation</CODE> or the <CODE>PdfFormField</CODE> to add
        */
        void AddAnnotation(PdfAnnotation annot);

        /**
        * Use this method to adds the <CODE>PdfAnnotation</CODE>
        * to the calculation order array.
        * @param annot the <CODE>PdfAnnotation</CODE> to be added
        */
        void AddCalculationOrder(PdfFormField annot);
        
        /**
        * Use this method to set the signature flags.
        * @param f the flags. This flags are ORed with current ones
        */
        int SigFlags {
            set;
        }
    }
}
