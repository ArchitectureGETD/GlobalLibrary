using System.Collections;

namespace iTextSharp.GE.text.pdf.parser {

    /**
     * @author Kevin Day
     * @since iText 5.0.1
     */
    public interface IXObjectDoHandler {
        void HandleXObject(PdfContentStreamProcessor processor, PdfStream stream, PdfIndirectReference refi);
        void HandleXObject(PdfContentStreamProcessor processor, PdfStream stream, PdfIndirectReference refi, ICollection markedContentInfoStack);
    }
}
