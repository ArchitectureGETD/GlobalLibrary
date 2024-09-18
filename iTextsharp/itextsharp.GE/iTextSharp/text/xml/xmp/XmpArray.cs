using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.GE.text.xml.xmp {

    /**
    * StringBuilder to construct an XMP array.
    */
    [Obsolete]
    public class XmpArray : List<string> {

        /** An array that is unordered. */
        public const String UNORDERED = "rdf:Bag";
        /** An array that is ordered. */
        public const String ORDERED = "rdf:Seq";
        /** An array with alternatives. */
        public const String ALTERNATIVE = "rdf:Alt";
        
        /** the type of array. */
        protected String type;
        
        /**
        * Creates an XmpArray.
        * @param type the type of array: UNORDERED, ORDERED or ALTERNATIVE.
        */
        public XmpArray(String type) {
            this.type = type;
        }
        
        /**
        * Returns the String representation of the XmpArray.
        * @return a String representation
        */
        public override String ToString() {
            StringBuilder buf = new StringBuilder("<");
            buf.Append(type);
            buf.Append('>');
            foreach (String s in this) {
                buf.Append("<rdf:li>");
                buf.Append(XMLUtil.EscapeXML(s, false));
                buf.Append("</rdf:li>");
            }
            buf.Append("</");
            buf.Append(type);
            buf.Append('>');
            return buf.ToString();
        }
    }
}
