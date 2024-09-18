using System;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.spatial {

    /**
     * A ViewPort dictionary.
     * @since 5.1.0
     */
    public class Viewport : PdfDictionary {

        /**
         * Creates a ViewPort dictionary.
         */
        public Viewport() : base(PdfName.VIEWPORT) {
        }

        /**
         * (Required) A rectangle in default user space coordinates specifying the
         * location of the viewport on the page.<br />
         * The two coordinate pairs of the rectangle shall be specified in
         * normalized form; that is, lower-left followed by upper-right, relative to
         * the measuring coordinate system. This ordering shall determine the
         * orientation of the measuring coordinate system (that is, the direction of
         * the positive x and y-axes) in this viewport, which may have a different
         * rotation from the page.<br />
         * The coordinates of this rectangle are independent of the origin of the
         * measuring coordinate system, specified in the Origin entry of the
         * measurement dictionary specified by Measure.
         *
         * @param bbox
         */
        virtual public void SetBBox(Rectangle bbox) {
            base.Put(PdfName.BBOX, new PdfRectangle(bbox, bbox.Rotation));
        }

        /**
         * (Optional) A descriptive text string or title of the viewport, intended
         * for use in a user interface.
         *
         * @param value
         */
        virtual public void SetName(PdfString value) {
            base.Put(PdfName.NAME, value);
        }

        /**
         * A measure dictionary that specifies the scale and units that shall apply
         * to measurements taken on the contents within the viewport.
         *
         * @param measure
         */
        virtual public void SetMeasure(Measure measure) {
            base.Put(PdfName.MEASURE, measure);
        }

        /**
         * {@link PointData} that shall specify the extended geospatial data that
         * applies to the image.
         *
         * @param ptData
         */
        virtual public void SetPtData(PointData ptData) {
            base.Put(PdfName.PTDATA, ptData);
        }
    }
}
