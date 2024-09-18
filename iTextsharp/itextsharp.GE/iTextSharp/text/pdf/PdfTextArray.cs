using System;
using System.Collections.Generic;

namespace iTextSharp.GE.text.pdf {

    /**
    * <CODE>PdfTextArray</CODE> defines an array with displacements and <CODE>PdfString</CODE>-objects.
    * <P>
    * A <CODE>TextArray</CODE> is used with the operator <VAR>TJ</VAR> in <CODE>PdfText</CODE>.
    * The first object in this array has to be a <CODE>PdfString</CODE>;
    * see reference manual version 1.3 section 8.7.5, pages 346-347.
    *       OR
    * see reference manual version 1.6 section 5.3.2, pages 378-379.
    */

    public class PdfTextArray{
        List<Object> arrayList = new List<Object>();
        
        // To emit a more efficient array, we consolidate
        // repeated numbers or strings into single array entries.
        // "add( 50 ); Add( -50 );" will REMOVE the combined zero from the array.
        // the alternative (leaving a zero in there) was Just Weird.
        // --Mark Storer, May 12, 2008
        private String lastStr;
        private float lastNum = float.NaN;
        
        // constructors
        public PdfTextArray(String str) {
            Add(str);
        }
        
        public PdfTextArray() {
        }
        
        /**
        * Adds a <CODE>PdfNumber</CODE> to the <CODE>PdfArray</CODE>.
        *
        * @param  number   displacement of the string
        */
        virtual public void Add(PdfNumber number) {
            Add((float)number.DoubleValue);
        }
        
        virtual public void Add(float number) {
            if (number != 0) {
                if (!float.IsNaN(lastNum)) {
                    lastNum += number;
                    if (lastNum != 0) {
                        ReplaceLast(lastNum);
                    } else {
                        arrayList.RemoveAt(arrayList.Count - 1);
                    }
                } else {
                    lastNum = number;
                    arrayList.Add(lastNum);
                }
                lastStr = null;
            }
            // adding zero doesn't modify the TextArray at all
        }
        
        virtual public void Add(String str) {
            if (str.Length > 0) {
                if (lastStr != null) {
                    lastStr = lastStr + str;
                    ReplaceLast(lastStr);
                } else {
                    lastStr = str;
                    arrayList.Add(lastStr);
                }
                lastNum = float.NaN;
            }
            // adding an empty string doesn't modify the TextArray at all
        }
        
        internal List<Object> ArrayList {
            get {
                return arrayList;
            }
        }
        
        private void ReplaceLast(Object obj) {
            // deliberately throw the IndexOutOfBoundsException if we screw up.
            arrayList[arrayList.Count - 1] = obj;
        }
    }
}
