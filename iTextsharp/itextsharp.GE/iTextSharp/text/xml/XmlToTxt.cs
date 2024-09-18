using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using iTextSharp.GE.text.xml.simpleparser;

namespace iTextSharp.GE.text.xml {

    /**
     * This class converts XML into plain text stripping all tags.
     */
    public class XmlToTxt : ISimpleXMLDocHandler {

        /**
         * Buffer that stores all content that is encountered.
         */
        protected internal StringBuilder buf;

        /**
         * Static method that parses an XML Stream.
         * @param is    the XML input that needs to be parsed
         * @return  a String obtained by removing all tags from the XML
         */
        public static String Parse(Stream isp) {
            XmlToTxt handler = new XmlToTxt();
            SimpleXMLParser.Parse(handler, null, new StreamReader(isp), true);
            return handler.ToString();
        }
        
        /**
         * Creates an instance of XML to TXT.
         */
        protected XmlToTxt() {
            buf = new StringBuilder();
        }
        
        /**
         * @return  the String after parsing.
         */
        public override String ToString() {
            return buf.ToString();
        }
        
        /**
         * @see com.itextpdf.text.xml.simpleparser.SimpleXMLDocHandler#startElement(java.lang.String, java.util.Map)
         */
        virtual public void StartElement(String tag, IDictionary<String, String> h) {
        }

        /**
         * @see com.itextpdf.text.xml.simpleparser.SimpleXMLDocHandler#endElement(java.lang.String)
         */
        virtual public void EndElement(String tag) {
        }

        /**
         * @see com.itextpdf.text.xml.simpleparser.SimpleXMLDocHandler#startDocument()
         */
        virtual public void StartDocument() {
        }

        /**
         * @see com.itextpdf.text.xml.simpleparser.SimpleXMLDocHandler#endDocument()
         */
        virtual public void EndDocument() {
        }

        /**
         * @see com.itextpdf.text.xml.simpleparser.SimpleXMLDocHandler#text(java.lang.String)
         */
        virtual public void Text(String str) {
            buf.Append(str);
        }
    }
}
