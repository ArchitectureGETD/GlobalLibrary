using System;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf {

    /**
     * <CODE>PdfNumber</CODE> provides two types of numbers, int and real.
     * <P>
     * ints may be specified by signed or unsigned constants. Reals may only be
     * in decimal format.<BR>
     * This object is described in the 'Portable Document Format Reference Manual version 1.3'
     * section 4.3 (page 37).
     *
     * @see        PdfObject
     * @see        BadPdfFormatException
     */

    public class PdfNumber : PdfObject {
    
        /** actual value of this <CODE>PdfNumber</CODE>, represented as a <CODE>double</CODE> */
        private double value;
    
        // constructors
    
        /**
         * Constructs a <CODE>PdfNumber</CODE>-object.
         *
         * @param        content            value of the new <CODE>PdfNumber</CODE>-object
         */
    
        public PdfNumber(string content) : base(NUMBER) {
            try {
                value = Double.Parse(content.Trim(), System.Globalization.NumberFormatInfo.InvariantInfo);
                this.Content = content;
            }
            catch (Exception nfe){
                throw new Exception(MessageLocalization.GetComposedMessage("1.is.not.a.valid.number.2", content, nfe.ToString()));
            }
        }
    
        /**
         * Constructs a new int <CODE>PdfNumber</CODE>-object.
         *
         * @param        value                value of the new <CODE>PdfNumber</CODE>-object
         */
    
        public PdfNumber(int value) : base(NUMBER) {
            this.value = value;
            this.Content = value.ToString();
        }

        /**
         * Constructs a new long <CODE>PdfNumber</CODE>-object.
         *
         * @param        value                value of the new <CODE>PdfNumber</CODE>-object
         */
    
        public PdfNumber(long value) : base(NUMBER) {
            this.value = value;
            this.Content = value.ToString();
        }
    
        /**
         * Constructs a new REAL <CODE>PdfNumber</CODE>-object.
         *
         * @param        value                value of the new <CODE>PdfNumber</CODE>-object
         */
    
        public PdfNumber(double value) : base(NUMBER) {
            this.value = value;
            Content = ByteBuffer.FormatDouble(value);
        }
    
        /**
         * Constructs a new REAL <CODE>PdfNumber</CODE>-object.
         *
         * @param        value                value of the new <CODE>PdfNumber</CODE>-object
         */
    
        public PdfNumber(float value) : this((double)value) {}
    
        // methods returning the value of this object
    
        /**
         * Returns the primitive <CODE>int</CODE> value of this object.
         *
         * @return        a value
         */
    
        virtual public int IntValue {
            get {
                return (int) value;
            }
        }
    
        /**
         * Returns the primitive <CODE>long</CODE> value of this object.
         *
         * @return        a value
         */
    
        virtual public long LongValue {
            get {
                return (long) value;
            }
        }
    
        /**
         * Returns the primitive <CODE>double</CODE> value of this object.
         *
         * @return        a value
         */
    
        virtual public double DoubleValue {
            get {
                return value;
            }
        }
    
        virtual public float FloatValue {
            get {
                return (float)value;
            }
        }
    
        // other methods
    
        /**
         * Increments the value of the <CODE>PdfNumber</CODE>-object with 1.
         */
    
        virtual public void Increment() {
            value += 1.0;
            Content = ByteBuffer.FormatDouble(value);
        }
    }
}
