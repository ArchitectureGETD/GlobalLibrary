using System;
using System.IO;

using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf {

    /**
     * A <CODE>PdfDashPattern</CODE> defines a dash pattern as described in
     * the PDF Reference Manual version 1.3 p 325 (section 8.4.3).
     *
     * @see        PdfArray
     */

    public class PdfDashPattern : PdfArray {
    
        // membervariables
    
        /** This is the length of a dash. */
        private float dash = -1;
    
        /** This is the length of a gap. */
        private float gap = -1;
    
        /** This is the phase. */
        private float phase = -1;
    
        // constructors
    
        /**
         * Constructs a new <CODE>PdfDashPattern</CODE>.
         */
    
        public PdfDashPattern() : base() {}
    
        /**
         * Constructs a new <CODE>PdfDashPattern</CODE>.
         */
    
        public PdfDashPattern(float dash) : base(new PdfNumber(dash)) {
            this.dash = dash;
        }
    
        /**
         * Constructs a new <CODE>PdfDashPattern</CODE>.
         */
    
        public PdfDashPattern(float dash, float gap) : base(new PdfNumber(dash)) {
            Add(new PdfNumber(gap));
            this.dash = dash;
            this.gap = gap;
        }
    
        /**
         * Constructs a new <CODE>PdfDashPattern</CODE>.
         */
    
        public PdfDashPattern(float dash, float gap, float phase) : base(new PdfNumber(dash)) {
            Add(new PdfNumber(gap));
            this.dash = dash;
            this.gap = gap;
            this.phase = phase;
        }
    
        virtual public void Add(float n) {
            Add(new PdfNumber(n));
        }
    
        /**
         * Returns the PDF representation of this <CODE>PdfArray</CODE>.
         *
         * @return        an array of <CODE>byte</CODE>s
         */
    
        public override void ToPdf(PdfWriter writer, Stream os) {
            os.WriteByte((byte)'[');

            if (dash >= 0) {
                new PdfNumber(dash).ToPdf(writer, os);
                if (gap >= 0) {
                    os.WriteByte((byte)' ');
                    new PdfNumber(gap).ToPdf(writer, os);
                }
            }
            os.WriteByte((byte)']');
            if (phase >=0) {
                os.WriteByte((byte)' ');
                new PdfNumber(phase).ToPdf(writer, os);
            }
        }
    }
}
