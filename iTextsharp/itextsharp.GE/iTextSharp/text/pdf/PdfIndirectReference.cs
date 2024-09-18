using System;
using System.Text;

namespace iTextSharp.GE.text.pdf {

    /**
     * <CODE>PdfIndirectReference</CODE> contains a reference to a <CODE>PdfIndirectObject</CODE>.
     * <P>
     * Any object used as an element of an array or as a value in a dictionary may be specified
     * by either a direct object of an indirect reference. An <I>indirect reference</I> is a reference
     * to an indirect object, and consists of the indirect object's object number, generation number
     * and the <B>R</B> keyword.<BR>
     * This object is described in the 'Portable Document Format Reference Manual version 1.3'
     * section 4.11 (page 54).
     *
     * @see        PdfObject
     * @see        PdfIndirectObject
     */

    public class PdfIndirectReference : PdfObject {
    
        // membervariables
    
        /** the object number */
        protected int number;
    
        /** the generation number */
        protected int generation = 0;
    
        // constructors
    
        protected PdfIndirectReference() : base(0) {
        }
        
        /**
         * Constructs a <CODE>PdfIndirectReference</CODE>.
         *
         * @param        type            the type of the <CODE>PdfObject</CODE> that is referenced to
         * @param        number            the object number.
         * @param        generation        the generation number.
         */
    
        internal PdfIndirectReference(int type, int number, int generation) : base(0, new StringBuilder().Append(number).Append(' ').Append(generation).Append(" R").ToString()) {
            this.number = number;
            this.generation = generation;
        }
    
        /**
         * Constructs a <CODE>PdfIndirectReference</CODE>.
         *
         * @param        type            the type of the <CODE>PdfObject</CODE> that is referenced to
         * @param        number            the object number.
         */
    
        internal protected PdfIndirectReference(int type, int number) : this(type, number, 0) {}
    
        // methods
    
        /**
         * Returns the number of the object.
         *
         * @return        a number.
         */
    
        virtual public int Number {
            get {
                return number;
            }
        }
    
        /**
         * Returns the generation of the object.
         *
         * @return        a number.
         */
    
        virtual public int Generation {
            get {
                return generation;
            }
        }

        public override String ToString() {
    	    return new StringBuilder().Append(number).Append(' ').Append(generation).Append(" R").ToString();
        }
    }
}
