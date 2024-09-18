using System;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.spatial.units {

/**
 * Angular display units for use in a Preferred Display Units (PDU) array.
 * @since 5.1.0
 */
    public enum Angular {
        /** a degree */
        DEGREE,
        /** a grad (1/400 of a circle, or 0.9 degrees) */
        GRAD}

    /**
     * Indicates whether and in what manner to display a fractional value
     * from the result of converting to the units
     * @since 5.1.0
     */
    public enum Fraction {
        /**
         * Show as decimal to the precision specified by D
         */
        DECIMAL,
        /**
         * Show as a fraction with denominator specified by D
         */
        FRACTION,
        /**
         * No fractional part; round to the nearest whole unit.
         */
        ROUND,
        /**
         * No fractional part; truncate to achieve whole units.
         */
        TRUNCATE}

    /**
     * Linear display units for use in a Preferred Display Units (PDU) array.
     * @since 5.1.0
     */
    public enum Linear {
        /** a meter */
        METER,
        /** a kilometer */
        KILOMETER,
        /** an international foot */
        INTERNATIONAL_FOOT,
        /** a U.S. survey foot */
        US_SURVEY_FOOT,
        /** an international mile */
        INTERNATIONAL_MILE,
        /** an international nautical mil*/
        INTERNATIONAL_NAUTICAL_MILE}

    /**
     * Identifier for use in the Names array that identifies the
     * internal data elements of the individual point arrays in the XPTS array
     * @since 5.1.0
     */
    public enum PtIdentifier {
        /** Latitude in degrees */
        LATITUDE,
        /** Longitude in degrees */
        LONGITUDE,
        /** Altitude in meters*/
        ALTITUDE}

    /**
     * Area display units for use in a Preferred Display Units (PDU) array.
     * @since 5.1.0
     */
    public enum Square {
        /** a square meter */
        SQUARE_METER,
        /** a hectare (10,000 square meters) */
        HECTARE,
        /** a square kilometer */
        SQUARE_KILOMETER,
        /** a square foot */
        SQUARE_FOOT,
        /** an acre */
        ACRE,
        /** a square mile */
        SQUARE_MILE}

    public static class DecodeUnits {
        public static PdfName Decode(Angular e) {
            switch (e) {
                case Angular.DEGREE:
                    return new PdfName("DEG");
                case Angular.GRAD:
                    return new PdfName("GRD");
                default:
                    return null;
            }
        }

        public static PdfName Decode(Fraction f) {
            switch (f) {
                case Fraction.DECIMAL:
                    return new PdfName("D");
                case Fraction.FRACTION:
                    return new PdfName("F");
                case Fraction.ROUND:
                    return new PdfName("R");
                case Fraction.TRUNCATE:
                    return new PdfName("T");
                default:
                    return null;
            }
        }

        public static PdfName Decode(Linear l) {
            switch (l) {
                case Linear.INTERNATIONAL_FOOT:
                    return new PdfName("FT");
                case Linear.INTERNATIONAL_MILE:
                    return new PdfName("MI");
                case Linear.INTERNATIONAL_NAUTICAL_MILE:
                    return new PdfName("NM");
                case Linear.KILOMETER:
                    return new PdfName("KM");
                case Linear.METER:
                    return new PdfName("M");
                case Linear.US_SURVEY_FOOT:
                    return new PdfName("USFT");
                default:
                    return null;
            }
        }

        public static PdfName Decode(PtIdentifier p) {
            switch (p) {
                case PtIdentifier.ALTITUDE:
                    return new PdfName("ALT");
                case PtIdentifier.LATITUDE:
                    return new PdfName("LAT");
                case PtIdentifier.LONGITUDE:
                    return new PdfName("LON");
                default:
                    return null;
            }
        }

        public static PdfName Decode(Square s) {
            switch (s) {
                case Square.ACRE:
                    return new PdfName("A");
                case Square.HECTARE:
                    return new PdfName("HA");
                case Square.SQUARE_FOOT:
                    return new PdfName("SQFT");
                case Square.SQUARE_KILOMETER:
                    return new PdfName("SQKM");
                case Square.SQUARE_METER:
                    return new PdfName("SQM");
                case Square.SQUARE_MILE:
                    return new PdfName("SQMI");
                default:
                    return null;
            }
        }
    }
}
