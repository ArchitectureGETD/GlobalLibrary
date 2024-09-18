using System;
using System.Collections.Generic;

namespace iTextSharp.GE.text.pdf {


    /**
     * A PdfArray object consisting of nothing but PdfNumber objects
     * @since 5.1.0
     */
    public class NumberArray : PdfArray {

        /**
         * Creates a PdfArray consisting of PdfNumber objects.
         * @param numbers float values
         */
        public NumberArray() : base() {
        }
        
        public NumberArray(float[] numbers) : base() {
            foreach (float f in numbers) {
                Add(new PdfNumber(f));
            }
        }
        
        public NumberArray(float n1) : base() {
                Add(new PdfNumber(n1));
        }
        
        public NumberArray(float n1, float n2) : base() {
                Add(new PdfNumber(n1));
                Add(new PdfNumber(n2));
        }
        
        public NumberArray(float n1, float n2, float n3) : base() {
                Add(new PdfNumber(n1));
                Add(new PdfNumber(n2));
                Add(new PdfNumber(n3));
        }
        
        public NumberArray(float n1, float n2, float n3, float n4) : base() {
                Add(new PdfNumber(n1));
                Add(new PdfNumber(n2));
                Add(new PdfNumber(n3));
                Add(new PdfNumber(n4));
        }
        
        public NumberArray(float n1, float n2, float n3, float n4, float n5) : base() {
                Add(new PdfNumber(n1));
                Add(new PdfNumber(n2));
                Add(new PdfNumber(n3));
                Add(new PdfNumber(n4));
                Add(new PdfNumber(n5));
        }
        
        public NumberArray(float n1, float n2, float n3, float n4, float n5, float n6) : base() {
                Add(new PdfNumber(n1));
                Add(new PdfNumber(n2));
                Add(new PdfNumber(n3));
                Add(new PdfNumber(n4));
                Add(new PdfNumber(n5));
                Add(new PdfNumber(n6));
        }
                
        /**
         * Creates a PdfArray consisting of PdfNumber objects.
         * @param numbers a List containing PdfNumber objects
         */
        public NumberArray(IList<PdfNumber> numbers) : base() {
            foreach (PdfNumber n in numbers) {
                Add(n);
            }
        }
    }
}
