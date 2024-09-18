using System.Collections.Generic;

namespace iTextSharp.GE.text.pdf.events {

    /**
    * If you want to add more than one page event to a PdfPTable,
    * you have to construct a PdfPTableEventForwarder, add the
    * different events to this object and add the forwarder to
    * the PdfWriter.
    */

    public class PdfPTableEventForwarder : IPdfPTableEventAfterSplit {

        /** ArrayList containing all the PageEvents that have to be executed. */
        protected List<IPdfPTableEvent> events = new List<IPdfPTableEvent>();
        
        /** 
        * Add a page event to the forwarder.
        * @param event an event that has to be added to the forwarder.
        */
        virtual public void AddTableEvent(IPdfPTableEvent eventa) {
            events.Add(eventa);
        }

        /**
        * @see com.lowagie.text.pdf.PdfPTableEvent#tableLayout(com.lowagie.text.pdf.PdfPTable, float[][], float[], int, int, com.lowagie.text.pdf.PdfContentByte[])
        */
        virtual public void TableLayout(PdfPTable table, float[][] widths, float[] heights, int headerRows, int rowStart, PdfContentByte[] canvases) {
            foreach (IPdfPTableEvent eventa in events) {
                eventa.TableLayout(table, widths, heights, headerRows, rowStart, canvases);
            }
        }
        virtual public void SplitTable(PdfPTable table) {
		    foreach (IPdfPTableEvent eventa in events) {
			    if (eventa is IPdfPTableEventSplit)
                    ((IPdfPTableEventSplit)eventa).SplitTable(table);
		    }
        }
        /**
         * @see com.itextpdf.text.pdf.PdfPTableEventAfterSplit#afterSplitTable(com.itextpdf.text.pdf.PdfPTable, com.itextpdf.text.pdf.PdfPRow, int)
         * @since iText 5.4.3
         */
        virtual public void AfterSplitTable(PdfPTable table, PdfPRow startRow, int startIdx) {
            foreach (IPdfPTableEvent evente in events) {
                if (evente is IPdfPTableEventAfterSplit)
                    ((IPdfPTableEventAfterSplit)evente).AfterSplitTable(table, startRow, startIdx);
            }
        }

    }
}
