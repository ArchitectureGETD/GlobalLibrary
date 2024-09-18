using System;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.spatial.units;
namespace iTextSharp.GE.text.pdf.spatial.objects {

    /**
     * A dictionary that represents a specific unit of measurement (such as miles or feet).
     * It contains information about how each unit shall be expressed in text and factors
     * for calculating the number of units.
     * @since 5.1.0
     */
    public class NumberFormatDictionary : PdfDictionary {
        
        /**
         * Creates a new NumberFormat dictionary.
         */
        public NumberFormatDictionary() : base(PdfName.NUMBERFORMAT) {
        }

        /**
         * A text string specifying a label for displaying the units represented by
         * this NumberFormat in a user interface; the label should use a universally
         * recognized abbreviation.
         * 
         * @param label
         */
        virtual public void SetLabel(PdfString label) {
            base.Put(PdfName.U, label);
        }

        /**
         * The conversion factor used to multiply a value in partial units of the
         * previous number format array element to obtain a value in the units of
         * this dictionary. When this entry is in the first number format in the
         * array, its meaning (that is, what it shall be multiplied by) depends on
         * which entry in the RectilinearMeasure references the NumberFormat
         * array.
         * 
         * @param n
         */
        virtual public void SetConversionFactor(PdfNumber n) {
            base.Put(PdfName.C, n);
        }

        /**
         * Indicate whether and in what manner to display a fractional value from
         * the result of converting to the units of this NumberFormat means of the
         * conversion factor entry.
         * 
         * @param f
         */
        virtual public void SetFractionalValue(Fraction f) {
            base.Put(PdfName.F, DecodeUnits.Decode(f));
        }

        /**
         * A positive integer that shall specify the precision or denominator of a
         * fractional amount:
         * <ul>
         * <li>
         * When the Fractional Value is {@link Fraction#DECIMAL}, this entry shall
         * be the precision of a decimal display; it shall be a multiple of 10.
         * Low-order zeros may be truncated unless FixedDenominator is true. Default
         * value: 100 (hundredths, corresponding to two decimal digits).</li>
         * <li>When the value of F is {@link Fraction#FRACTION}, this entry shall be
         * the denominator of a fractional display. The fraction may be reduced
         * unless the value of FD is true. Default value: 16.</li>
         * </ul>
         * 
         * @param precision
         */
        virtual public void SetPrecision(PdfNumber precision) {
            base.Put(PdfName.D, precision);
        }

        /**
         * If true, a fractional value formatted according to Precision may not have
         * its denominator reduced or low-order zeros truncated.
         * 
         * @param isFixedDenominator
         */
        virtual public void SetFixedDenominator(PdfBoolean isFixedDenominator) {
            base.Put(PdfName.FD, isFixedDenominator);
        }

        /**
         * Text that shall be used between orders of thousands in display of
         * numerical values. An empty string indicates that no text shall be added.<br />
         * Default value: COMMA "\u002C"
         * 
         * @param rt
         */
        virtual public void SetCipherGroupingCharacter(PdfString rt) {
            base.Put(PdfName.RT, rt);
        }

        /**
         * Text that shall be used as the decimal position in displaying numerical
         * values. An empty string indicates that the default shall be used.<br />
         * Default value: PERIOD "\u002E"
         * 
         * @param dc
         */
        virtual public void SetDecimalChartacter(PdfString dc) {
            base.Put(PdfName.RD, dc);
        }

        /**
         * Text that shall be concatenated to the left of the label specified by
         * setLabel. An empty string indicates that no text shall be added.<br />
         * Default value: A single ASCII SPACE character "\u0020"
         * 
         * @param ps
         */
        virtual public void SetLabelLeftString(PdfString ps) {
            base.Put(PdfName.PS, ps);
        }

        /**
         * Text that shall be concatenated after the label specified by setLabel. An
         * empty string indicates that no text shall be added.<br />
         * Default value: A single ASCII SPACE character "\u0020"
         * 
         * @param ss
         */
        virtual public void SetLabelRightString(PdfString ss) {
            base.Put(PdfName.SS, ss);
        }

        /**
         * A name indicating the position of the label specified by setLabel with respect
         * to the calculated unit value. The characters
         * specified by setLabelLeftString and setLabelRightString shall be concatenated before considering this
         * entry. Default value: suffix.
         * @param pos PdfName.S or PdfName.P
         */
        virtual public void SetLabelPosition(PdfName pos) {
            base.Put(PdfName.O, pos);
        }
    }
}
