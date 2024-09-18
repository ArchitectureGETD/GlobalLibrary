using System;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.spatial {

    /**
     * The value of the GCS or DCS entry of a geospatial measure dictionary.
     * Can be a GeographicCoordinateSystem or a ProjectedCoordinateSystem.
     * @since 5.1.0
     */
    public abstract class CoordinateSystem : PdfDictionary {

        /**
         * Creates a specific type of CoordinateSystem:
         * GEOGCS (GeographicCoordinateSystem) or PROJCS (ProjectedCoordinateSystem).
         */
        public CoordinateSystem(PdfName type) : base(type) {
        }
        
        /**
         * Sets the EPSG code.
         * @param epsg
         */
        virtual public void SetEPSG(int epsg) {
            base.Put(PdfName.EPSG, new PdfNumber(epsg));
        }
        
        /**
         * Sets the Well Known Text.
         * @param wkt a String that will be converted to a PdfString in ASCII.
         */
        virtual public void SetWKT(String wkt) {
            base.Put(PdfName.WKT, new PdfString(wkt));
        }
    }
}
