using System;

namespace iTextSharp.GE.text.pdf.interfaces
{

    public interface IPdfIsoConformance
    {

        /**
         * Checks if any PDF ISO conformance is necessary.
         * @return <code>true</code> if the PDF has to be in conformance with any of the PDF ISO specifications
         */
        bool IsPdfIso();

        void CheckPdfIsoConformance(int key, Object obj1);
    }
}
