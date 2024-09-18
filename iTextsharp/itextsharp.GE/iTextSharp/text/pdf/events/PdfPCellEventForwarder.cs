using System;
using System.Collections.Generic;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.events {

    /**
    * If you want to add more than one event to a cell,
    * you have to construct a PdfPCellEventForwarder, add the
    * different events to this object and add the forwarder to
    * the PdfPCell.
    */

    public class PdfPCellEventForwarder : IPdfPCellEvent {

        /** ArrayList containing all the PageEvents that have to be executed. */
        protected List<IPdfPCellEvent> events = new List<IPdfPCellEvent>();
        
        /** 
        * Add a page event to the forwarder.
        * @param event an event that has to be added to the forwarder.
        */
        virtual public void AddCellEvent(IPdfPCellEvent eventa) {
            events.Add(eventa);
        }

        /**
        * @see com.lowagie.text.pdf.PdfPCellEvent#cellLayout(com.lowagie.text.pdf.PdfPCell, com.lowagie.text.Rectangle, com.lowagie.text.pdf.PdfContentByte[])
        */
        virtual public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases) {
            foreach (IPdfPCellEvent eventa in events) {
                eventa.CellLayout(cell, position, canvases);
            }
        }
    }
}
