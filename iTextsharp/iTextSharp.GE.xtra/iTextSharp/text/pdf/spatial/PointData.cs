using System;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.spatial.units;
namespace iTextSharp.GE.text.pdf.spatial {

    /**
     * A Point Data dictionary.
     * @since 5.1.0
     */
    public class PointData : PdfDictionary {

        /**
         * An array of names that identify the internal
         * data elements of the individual point arrays in the XPTS array.
         */
        protected PdfArray names = new PdfArray();
        /**
         * An array with XPTS values.
         */
        protected PdfArray xpts = new PdfArray();
        
        /**
         * Creates a Point Data dictionary.
         */
        public PointData() : base(PdfName.PTDATA) {
            base.PutEx(PdfName.SUBTYPE, PdfName.CLOUD);
            base.PutEx(PdfName.NAMES, names);
            base.PutEx(PdfName.XPTS, xpts);
        }

        /**
         * Adds a point to the Point Data dictionary.
         * @param value an XPTS value
         * @param identifier
         */
        virtual public void AddXPTSValue(PdfNumber value, PtIdentifier identifier) {
            xpts.Add(value);
            names.Add(DecodeUnits.Decode(identifier));
        }
    }
}
