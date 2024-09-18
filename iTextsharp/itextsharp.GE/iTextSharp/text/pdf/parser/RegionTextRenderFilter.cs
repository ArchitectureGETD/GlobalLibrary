using System.util;
namespace iTextSharp.GE.text.pdf.parser {


    /**
     * A {@link RenderFilter} that only allows text within a specified rectangular region
     * @since 5.0.1
     */
    public class RegionTextRenderFilter : RenderFilter {

        /** the region to allow text from */
        private RectangleJ filterRect;
        
        /**
         * Constructs a filter
         * @param filterRect the rectangle to filter text against.  Note that this is a java.awt.Rectangle !
         */
        public RegionTextRenderFilter(RectangleJ filterRect) {
            this.filterRect = filterRect;
        }

        /**
         * Constructs a filter
         * @param filterRect the rectangle to filter text against.
         */
        public RegionTextRenderFilter(iTextSharp.GE.text.Rectangle filterRect) {
            this.filterRect = new RectangleJ(filterRect);
        }
 
        /** 
         * @see com.itextpdf.text.pdf.parser.RenderFilter#allowText(com.itextpdf.text.pdf.parser.TextRenderInfo)
         */
        public override bool AllowText(TextRenderInfo renderInfo){
            LineSegment segment = renderInfo.GetBaseline();
            Vector startPoint = segment.GetStartPoint();
            Vector endPoint = segment.GetEndPoint();
            
            float x1 = startPoint[Vector.I1];
            float y1 = startPoint[Vector.I2];
            float x2 = endPoint[Vector.I1];
            float y2 = endPoint[Vector.I2];
            
            return filterRect.IntersectsLine(x1, y1, x2, y2);
        }
    }
}
