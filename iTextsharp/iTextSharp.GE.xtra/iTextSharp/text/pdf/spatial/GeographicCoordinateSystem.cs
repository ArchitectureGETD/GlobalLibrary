using System;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.spatial {

    /**
     * A geographic coordinate system (GEOGCS) specifies an ellipsoidal object
     * in geographic coordinates: angular units of latitude and longitude.
     * The geographic coordinate system shall be described in either or
     * both of two well-established standards: as a numeric EPSG reference code,
     * or as a Well Known Text (WKT) string, which contains a description
     * of algorithms and parameters needed for transformations.
     * @since @5.1.0
     */
    public class GeographicCoordinateSystem : CoordinateSystem {
        /**
         * Creates a GeographicCoordinateSystem.
         */
        public GeographicCoordinateSystem() : base(PdfName.GEOGCS) {
        }
    }
}
