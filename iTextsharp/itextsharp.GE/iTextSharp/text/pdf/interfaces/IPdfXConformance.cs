using System;

namespace iTextSharp.GE.text.pdf.interfaces {

    public interface IPdfXConformance : IPdfIsoConformance {
        /**
        * Sets the PDF/X conformance level.
        * Allowed values are PDFX1A2001, PDFX32002, PDFA1A and PDFA1B.
        * It must be called before opening the document.
        * @param pdfxConformance the conformance level
        */    
        int PDFXConformance{
            set;
            get;
        }

        /**
        * Checks if the PDF/X Conformance is necessary.
        * @return true if the PDF has to be in conformance with any of the PDF/X specifications
        */
        bool IsPdfX();
    }
}
