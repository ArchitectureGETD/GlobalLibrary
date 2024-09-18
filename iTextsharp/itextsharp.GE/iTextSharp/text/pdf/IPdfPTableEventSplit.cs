using System;
namespace iTextSharp.GE.text.pdf {

    /**
     * Signals that a table will continue in the next page.
     * 
     * @since 5.0.6
     */
    public interface IPdfPTableEventSplit : IPdfPTableEvent {
        /**
         * This method is called to indicate that table is being split. It's called
         * before the <CODE>tableLayout</CODE> method and before the table is drawn.
         *
         * @param table the <CODE>PdfPTable</CODE> in use
         */
        void SplitTable(PdfPTable table);
    }
}
