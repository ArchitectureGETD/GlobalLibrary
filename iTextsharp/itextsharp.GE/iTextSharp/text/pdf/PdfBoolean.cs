using System;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf {

    /**
     * <CODE>PdfBoolean</CODE> is the bool object represented by the keywords <VAR>true</VAR> or <VAR>false</VAR>.
     * <P>
     * This object is described in the 'Portable Document Format Reference Manual version 1.3'
     * section 4.2 (page 37).
     *
     * @see        PdfObject
     * @see        BadPdfFormatException
     */

    public class PdfBoolean : PdfObject {
    
        // static membervariables (possible values of a bool object)
        public static readonly PdfBoolean PDFTRUE = new PdfBoolean(true);
        public static readonly PdfBoolean PDFFALSE = new PdfBoolean(false);
        /** A possible value of <CODE>PdfBoolean</CODE> */
        public const string TRUE = "true";
    
        /** A possible value of <CODE>PdfBoolean</CODE> */
        public const string FALSE = "false";
    
        // membervariables
    
        /** the bool value of this object */
        private bool value;
    
        // constructors
    
        /**
         * Constructs a <CODE>PdfBoolean</CODE>-object.
         *
         * @param        value            the value of the new <CODE>PdfObject</CODE>
         */
    
        public PdfBoolean(bool value) : base(BOOLEAN) {
            if (value) {
                this.Content = TRUE;
            }
            else {
                this.Content = FALSE;
            }
            this.value = value;
        }
    
        /**
         * Constructs a <CODE>PdfBoolean</CODE>-object.
         *
         * @param        value            the value of the new <CODE>PdfObject</CODE>, represented as a <CODE>string</CODE>
         *
         * @throws        BadPdfFormatException    thrown if the <VAR>value</VAR> isn't '<CODE>true</CODE>' or '<CODE>false</CODE>'
         */
    
        public PdfBoolean(string value) : base(BOOLEAN, value) {
            if (value.Equals(TRUE)) {
                this.value = true;
            }
            else if (value.Equals(FALSE)) {
                this.value = false;
            }
            else {
                throw new BadPdfFormatException(MessageLocalization.GetComposedMessage("the.value.has.to.be.true.of.false.instead.of.1", value));
            }
        }
    
        // methods returning the value of this object
    
        /**
         * Returns the primitive value of the <CODE>PdfBoolean</CODE>-object.
         *
         * @return        the actual value of the object.
         */
    
        virtual public bool BooleanValue {
            get {
                return value;
            }
        }

        public override string ToString() {
            return value ? TRUE : FALSE;
        }
    }
}
