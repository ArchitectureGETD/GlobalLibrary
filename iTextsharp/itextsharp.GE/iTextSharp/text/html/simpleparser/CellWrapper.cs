using System;
using System.Collections.Generic;
using System.Globalization;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.html;

namespace iTextSharp.GE.text.html.simpleparser {

    /**
     * We use a CellWrapper because we need some extra info
     * that isn't available in PdfPCell.
     * @author  psoares
     * @since 5.0.6 (renamed)
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public class CellWrapper : ITextElementArray {
    	
	    /** The cell that is wrapped in this stub. */
        private PdfPCell cell;
        
        /**
         * The width of the cell.
         * @since iText 5.0.6
         */
        private float width;
        
        /**
         * Indicates if the width is a percentage.
         * @since iText 5.0.6
         */
        private bool percentage;

        /**
         * Creates a new instance of IncCell.
         * @param	tag		the cell that is wrapped in this object.
         * @param	chain	properties such as width
         * @since	5.0.6
         */
        public CellWrapper(String tag, ChainedProperties chain) {
            this.cell = CreatePdfPCell(tag, chain);
    	    String value = chain[HtmlTags.WIDTH];
            if (value != null) {
                value = value.Trim();
        	    if (value.EndsWith("%")) {
        		    percentage = true;
        		    value = value.Substring(0, value.Length - 1);
        	    }
                width = float.Parse(value, CultureInfo.InvariantCulture);
            }
        }
        
        /**
         * Creates a PdfPCell element based on a tag and its properties.
         * @param	tag		a cell tag
         * @param	chain	the hierarchy chain
         */
	    virtual public PdfPCell CreatePdfPCell(String tag, ChainedProperties chain) {
		    PdfPCell cell = new PdfPCell((Phrase)null);
            // colspan
		    String value = chain[HtmlTags.COLSPAN];
            if (value != null)
                cell.Colspan = int.Parse(value);
            // rowspan
            value = chain[HtmlTags.ROWSPAN];
            if (value != null)
                cell.Rowspan = int.Parse(value);
            // horizontal alignment
            if (tag.Equals(HtmlTags.TH))
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
            value = chain[HtmlTags.ALIGN];
            if (value != null) {
                cell.HorizontalAlignment = HtmlUtilities.AlignmentValue(value);
            }
            // vertical alignment
            value = chain[HtmlTags.VALIGN];
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            if (value != null) {
                cell.VerticalAlignment = HtmlUtilities.AlignmentValue(value);
            }
            // border
            value = chain[HtmlTags.BORDER];
            float border = 0;
            if (value != null)
                border = float.Parse(value, CultureInfo.InvariantCulture);
            cell.BorderWidth = border;
            // cellpadding
            value = chain[HtmlTags.CELLPADDING];
            if (value != null)
                cell.Padding = float.Parse(value, CultureInfo.InvariantCulture);
            cell.UseDescender = true;
            // background color
            value = chain[HtmlTags.BGCOLOR];
            cell.BackgroundColor = HtmlUtilities.DecodeColor(value);
            return cell;
	    }

        virtual public bool Add(IElement o) {
            cell.AddElement(o);
            return true;
        }
        
        virtual public IList<Chunk> Chunks {
            get {
                return null;
            }
        }
        
        virtual public bool Process(IElementListener listener) {
            return true;
        }
        
        virtual public int Type {
            get {
                return Element.RECTANGLE;
            }
        }
        
        virtual public PdfPCell Cell {
            get {
                return cell;
            }
        }    

        /**
        * @see com.lowagie.text.Element#isContent()
        * @since   iText 2.0.8
        */
        virtual public bool IsContent() {
            return true;
        }

        /**
        * @see com.lowagie.text.Element#isNestable()
        * @since   iText 2.0.8
        */
        virtual public bool IsNestable() {
            return true;
        }
  

        virtual public float Width {
            get { return width; }
        }

        virtual public bool IsPercentage {
            get {
                return percentage;
            }
        }

        public override string ToString() {
            return base.ToString();
        }
    }
}
