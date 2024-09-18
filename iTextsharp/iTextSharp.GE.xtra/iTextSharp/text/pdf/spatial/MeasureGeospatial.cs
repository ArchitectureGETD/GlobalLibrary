using System;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.spatial.units;
namespace iTextSharp.GE.text.pdf.spatial {

    /**
     * Geospatial Measure dictionary.
     * @since 5.1.0
     */
    public class MeasureGeospatial : Measure {

        /**
         * Gets the subtype.
         * In this case RL for a rectalinear coordinate system.
         */
        internal override PdfName GetSubType() {
            return PdfName.GEO;
        }

        /**
         * An array of numbers that shall be taken pairwise to define a series of
         * points that describes the bounds of an area for which geospatial
         * transformations are valid.
         *
         * @param bounds
         */
        virtual public void SetBounds(NumberArray bounds) {
            base.Put(PdfName.BOUNDS, bounds);
        }

        /**
         * A projected or geographic coordinate system dictionary.
         *
         * @param cs
         */
        virtual public void SetCoordinateSystem(CoordinateSystem cs) {
            base.Put(PdfName.GCS, cs);
        }

        /**
         * Optional coordinate system that allows a document to be authored
         * to display values in a coordinate system other than that associated
         * with the source data. For example, a map may be created in a state
         * plane coordinate system based on a 1927 datum, but it is possible
         * to display its latitude and longitude values in the WGS84 datum
         * corresponding to values reported by a GPS device.
         *
         * @param cs
         */
        virtual public void SetDisplayCoordinateSystem(GeographicCoordinateSystem cs) {
            base.Put(PdfName.DCS, cs);
        }

        /**
         * Three names that identify in order a linear display unit, an area display
         * unit, and an angular display unit.
         *
         * @param l
         * @param s
         * @param a
         */
        virtual public void SetDisplayUnits(Linear l, Square s, Angular a) {
            PdfArray arr = new PdfArray();
            arr.Add(DecodeUnits.Decode(l));
            arr.Add(DecodeUnits.Decode(s));
            arr.Add(DecodeUnits.Decode(a));
            base.Put(PdfName.PDU, arr);
        }

        /**
         * An array of numbers that shall be taken pairwise, defining points in
         * geographic space as degrees of latitude and longitude. These values shall
         * be based on the geographic coordinate system described in the GCS
         * dictionary.
         *
         * @param pairedpoints
         */
        virtual public void SetGPTS(NumberArray pairedpoints) {
            Put(PdfName.GPTS, pairedpoints);
        }

        /**
         * An array of numbers that shall be taken pairwise to define points in a 2D
         * unit square. The unit square is mapped to the rectangular bounds of the
         * {@link Viewport}, image XObject, or forms XObject that contains the
         * measure dictionary. This array shall contain the same number of number
         * pairs as the GPTS array; each number pair is the unit square object
         * position corresponding to the geospatial position in the GPTS array.
         *
         * @param pairedpoints
         */
        virtual public void SetLPTS(NumberArray pairedpoints) {
            Put(PdfName.LPTS, pairedpoints);
        }
    }
}
