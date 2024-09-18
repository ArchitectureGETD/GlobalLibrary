using System;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.api {

    /**
     * @author itextpdf.com
     *
     */
    public interface IWriterOperation {
        /**
         * Receive a writer and the document to do certain operations on them.
         * @param writer the PdfWriter
         * @param doc the document
         * @throws DocumentException
         */
        void Write(PdfWriter writer, Document doc);
    }
}
