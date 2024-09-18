using System;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.spatial.objects;
namespace iTextSharp.GE.text.pdf.spatial {

    /**
     * Rectilinear Measure dictionary.
     * @since 5.1.0
     */
    public class MeasureRectilinear : Measure {
        
        /**
         * Gets the subtype.
         * In this case RL for a rectilinear coordinate system.
         */
        internal override PdfName GetSubType() {
            return PdfName.RL;
        }
        
        /**
         * A text string expressing the scale ratio of the drawing in the region
         * corresponding to this dictionary. Universally recognized unit
         * abbreviations should be used, either matching those of the number format
         * arrays in this dictionary or those of commonly used scale ratios.<br />
         * If the scale ratio differs in the x and y directions, both scales should
         * be specified.
         * 
         * @param scaleratio
         */
        virtual public void SetScaleRatio(PdfString scaleratio) {
            Put(new PdfName("R"), scaleratio);
        }

        /**
         * A number format array for measurement of change along the x axis and, if
         * Y is not present, along the y axis as well. The first element in the
         * array shall contain the scale factor for converting from default user
         * space units to the largest units in the measuring coordinate system along
         * that axis.<br />
         * The directions of the x and y axes are in the measuring coordinate system
         * and are independent of the page rotation. These directions shall be
         * determined by the BBox of the containing {@link Viewport}
         * 
         * @param x
         */
        virtual public void SetX(NumberFormatArray x) {
            Put(new PdfName("X"), x);
        }

        /**
         * A number format array for measurement of change along the y axis. The
         * first element in the array shall contain the scale factor for converting
         * from default user space units to the largest units in the measuring
         * coordinate system along the y axis.(Required when the x and y scales have
         * different units or conversion factors)
         * 
         * @param y
         */
        virtual public void SetY(NumberFormatArray y) {
            Put(new PdfName("Y"), y);
        }

        /**
         * A number format array for measurement of distance in any direction. The
         * first element in the array shall specify the conversion to the largest
         * distance unit from units represented by the first element in X. The scale
         * factors from X, Y (if present) and CYX (if Y is present) shall be used to
         * convert from default user space to the appropriate units before applying
         * the distance function.
         * 
         * @param d
         */
        virtual public void SetD(NumberFormatArray d) {
            Put(new PdfName("D"), d);
        }

        /**
         * A number format array for measurement of area. The first element in the
         * array shall specify the conversion to the largest area unit from units
         * represented by the first element in X, squared. The scale factors from X,
         * Y (if present) and CYX (if Y is present) shall be used to convert from
         * default user space to the appropriate units before applying the area
         * function.
         * 
         * @param a
         */
        virtual public void SetA(NumberFormatArray a) {
            Put(new PdfName("A"), a);
        }

        /**
         * A number format array for measurement of angles. The first element in the
         * array shall specify the conversion to the largest angle unit from
         * degrees. The scale factor from CYX (if present) shall be used to convert
         * from default user space to the appropriate units before applying the
         * angle function.
         * 
         * @param t a PdfArray containing PdfNumber objects
         */
        virtual public void SetT(NumberFormatArray t) {
            Put(new PdfName("T"), t);
        }

        /**
         * A number format array for measurement of the slope of a line. The first
         * element in the array shall specify the conversion to the largest slope
         * unit from units represented by the first element in Y divided by the
         * first element in X. The scale factors from X, Y (if present) and CYX (if
         * Y is present) shall be used to convert from default user space to the
         * appropriate units before applying the slope function.
         * 
         * @param s a PdfArray containing PdfNumber objects
         */
        virtual public void SetS(NumberFormatArray s) {
            Put(new PdfName("S"), s);
        }

        /**
         * An array of two numbers that shall specify the origin of the measurement
         * coordinate system in default user space coordinates. The directions by
         * which x and y increase in value from this origin shall be determined by
         * {@link Viewport#setBBox(com.itextpdf.text.Rectangle)} entry.<br />
         * Default value: the first coordinate pair (lower-left corner) of the
         * rectangle specified by the viewport's BBox entry.
         * 
         * @param o an XYArray
         */
        virtual public void SetO(XYArray o) {
            Put(new PdfName("O"), o);
        }

        /**
         * A factor that shall be used to convert the largest units along the y axis
         * to the largest units along the x axis. It shall be used for calculations
         * (distance, area, and angle) where the units are be equivalent; if not
         * specified, these calculations may not be performed (which would be the
         * case in situations such as x representing time and y representing
         * temperature). Other calculations (change in x, change in y, and slope)
         * shall not require this value.
         * 
         * @param cyx
         */
        virtual public void SetCYX(PdfNumber cyx) {
            Put(PdfName.CYX, cyx);
        }
    }
}
