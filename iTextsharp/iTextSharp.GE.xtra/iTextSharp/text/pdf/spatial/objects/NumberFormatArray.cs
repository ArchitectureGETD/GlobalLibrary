using System;
using System.Collections.Generic;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.spatial.objects {

    /**
     * An array consisting of NumberFormatDictionary objects.
     * @since 5.1.0
     */
    public class NumberFormatArray : PdfArray {

        /**
         * Creates a PdfArray consisting of NumberFormatDictionary objects.
         * @param formats NumberFormatDictionary objects
         */
        public NumberFormatArray() : base() {
        }
        
        public NumberFormatArray(NumberFormatDictionary[] dics) : base() {
            foreach (NumberFormatDictionary n in dics) {
                Add(n);
            }
        }
        
        public NumberFormatArray(NumberFormatDictionary n1) : base() {
                Add(n1);
        }
        
        public NumberFormatArray(NumberFormatDictionary n1, NumberFormatDictionary n2) : base() {
                Add(n1);
                Add(n2);
        }
        
        public NumberFormatArray(NumberFormatDictionary n1, NumberFormatDictionary n2, NumberFormatDictionary n3) : base() {
                Add(n1);
                Add(n2);
                Add(n3);
        }
        
        public NumberFormatArray(NumberFormatDictionary n1, NumberFormatDictionary n2, NumberFormatDictionary n3, NumberFormatDictionary n4) : base() {
                Add(n1);
                Add(n2);
                Add(n3);
                Add(n4);
        }
        
        public NumberFormatArray(NumberFormatDictionary n1, NumberFormatDictionary n2, NumberFormatDictionary n3, NumberFormatDictionary n4, NumberFormatDictionary n5) : base() {
                Add(n1);
                Add(n2);
                Add(n3);
                Add(n4);
                Add(n5);
        }
        
        public NumberFormatArray(NumberFormatDictionary n1, NumberFormatDictionary n2, NumberFormatDictionary n3, NumberFormatDictionary n4, NumberFormatDictionary n5, NumberFormatDictionary n6) : base() {
                Add(n1);
                Add(n2);
                Add(n3);
                Add(n4);
                Add(n5);
                Add(n6);
        }
        
        /**
         * Creates a PdfArray consisting of NumberFormatDictionary objects.
         * @param formats a List containing NumberFormatDictionary objects
         */
        public NumberFormatArray(IList<NumberFormatDictionary> formats) : base() {
            foreach (NumberFormatDictionary dict in formats) {
                Add(dict);
            }
        }
    }
}
