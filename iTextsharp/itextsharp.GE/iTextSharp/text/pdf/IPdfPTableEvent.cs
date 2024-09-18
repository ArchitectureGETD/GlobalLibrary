using System;

namespace iTextSharp.GE.text.pdf {

    /** An interface that can be used to retrieve the position of cells in <CODE>PdfPTable</CODE>.
     *
     * @author Paulo Soares
     */
    public interface IPdfPTableEvent {
    
        /** This method is called at the end of the table rendering. The text or graphics are added to
        * one of the 4 <CODE>PdfContentByte</CODE> contained in
        * <CODE>canvases</CODE>.<br>
        * The indexes to <CODE>canvases</CODE> are:<p>
        * <ul>
        * <li><CODE>PdfPTable.BASECANVAS</CODE> - the original <CODE>PdfContentByte</CODE>. Anything placed here
        * will be under the table.
        * <li><CODE>PdfPTable.BACKGROUNDCANVAS</CODE> - the layer where the background goes to.
        * <li><CODE>PdfPTable.LINECANVAS</CODE> - the layer where the lines go to.
        * <li><CODE>PdfPTable.TEXTCANVAS</CODE> - the layer where the text go to. Anything placed here
        * will be over the table.
        * </ul>
        * The layers are placed in sequence on top of each other.
        * <p>
        * The <CODE>widths</CODE> and <CODE>heights</CODE> have the coordinates of the cells.<br>
        * The size of the <CODE>widths</CODE> array is the number of rows.
        * Each sub-array in <CODE>widths</CODE> corresponds to the x column border positions where
        * the first element is the x coordinate of the left table border and the last
        * element is the x coordinate of the right table border. 
        * If colspan is not used all the sub-arrays in <CODE>widths</CODE>
        * are the same.<br>
        * For the <CODE>heights</CODE> the first element is the y coordinate of the top table border and the last
        * element is the y coordinate of the bottom table border.
        * @param table the <CODE>PdfPTable</CODE> in use
        * @param widths an array of arrays with the cells' x positions. It has the length of the number
        * of rows
        * @param heights an array with the cells' y positions. It has a length of the number
        * of rows + 1
        * @param headerRows the number of rows defined for the header.
        * @param rowStart the first row number after the header
        * @param canvases an array of <CODE>PdfContentByte</CODE>
        */    
        void TableLayout(PdfPTable table, float[][] widths, float[] heights, int headerRows, int rowStart, PdfContentByte[] canvases);

    }
}
