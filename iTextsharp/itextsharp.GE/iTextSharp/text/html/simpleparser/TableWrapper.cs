using System;
using System.Collections.Generic;
using System.Globalization;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.html;

namespace iTextSharp.GE.text.html.simpleparser {

    /**
     * We use a TableWrapper because PdfPTable is rather complex
     * to put on the HTMLWorker stack.
     * @author  psoares
     * @since 5.0.6 (renamed)
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public class TableWrapper : IElement {
        /**
         * The styles that need to be applied to the table
         * @since 5.0.6 renamed from props
         */
        private IDictionary<String, String> styles = new Dictionary<String, String>();
        /**
         * Nested list containing the PdfPCell elements that are part of this table.
         */
        private IList<IList<PdfPCell>> rows = new List<IList<PdfPCell>>();
        
        /**
         * Array containing the widths of the columns.
         * @since iText 5.0.6
         */
        private float[] colWidths;

        /**
         * Creates a new instance of IncTable.
         * @param   attrs   a Map containing attributes
         */
        public TableWrapper(IDictionary<String, String> attrs) {
            foreach (KeyValuePair<string,string> kv in attrs) {
                styles[kv.Key] = kv.Value;
            }
        }

        /**
         * Adds a new row to the table.
         * @param row a list of PdfPCell elements
         */
        virtual public void AddRow(IList<PdfPCell> row) {
            if (row != null) {
                List<PdfPCell> t = new List<PdfPCell>(row);
                t.Reverse();
                rows.Add(t);
            }
        }

        /**
         * Setter for the column widths
         * @since iText 5.0.6
         */
        virtual public float[] ColWidths {
            set { colWidths = value; }
        }

        /**
         * Creates a new PdfPTable based on the info assembled
         * in the table stub.
         * @return  a PdfPTable
         */
        virtual public PdfPTable CreateTable() {
            // no rows = simplest table possible
            if (rows.Count == 0)
                return new PdfPTable(1);
            // how many columns?
            int ncol = 0;
            foreach (PdfPCell pc in rows[0]) {
                ncol += pc.Colspan;
            }
            PdfPTable table = new PdfPTable(ncol);
            // table width
            String width;
            styles.TryGetValue(HtmlTags.WIDTH, out width);
            if (width == null)
                table.WidthPercentage = 100;
            else {
                if (width.EndsWith("%"))
                    table.WidthPercentage = float.Parse(width.Substring(0, width.Length - 1), CultureInfo.InvariantCulture);
                else {
                    table.TotalWidth = float.Parse(width, CultureInfo.InvariantCulture);
                    table.LockedWidth = true;
                }
            }
            // horizontal alignment
            String alignment;
            styles.TryGetValue(HtmlTags.ALIGN, out alignment);
            int align = Element.ALIGN_LEFT;
            if (alignment != null) {
                align = HtmlUtilities.AlignmentValue(alignment);
            }
            table.HorizontalAlignment = align;
            // column widths
            try {
                if (colWidths != null)
                    table.SetWidths(colWidths);
            } catch {
                // fail silently
            }
            // add the cells
            foreach (IList<PdfPCell> col in rows) {
                foreach (PdfPCell pc in col) {
                    table.AddCell(pc);
                }
            }
            return table;
        }

        // these Element methods are irrelevant for a table stub.
        
        virtual public bool Process(IElementListener listener) {
            return false;
        }

        virtual public int Type {
            get { 
                return 0;
            }
        }

        virtual public bool IsContent() {
            return false;
        }

        virtual public bool IsNestable() {
            return false;
        }

        virtual public IList<Chunk> Chunks {
            get {
                return null;    
            }
        }
    }
}
