using System;
using System.Text;
using System.util;

namespace iTextSharp.GE.text.xml.xmp {

    /**
    * Abstract superclass of the XmpSchemas supported by iText.
    */
    [Obsolete]
    public abstract class XmpSchema : Properties {
        /** the namesspace */
        protected String xmlns;
        
        /** Constructs an XMP schema. 
        * @param xmlns
        */
        public XmpSchema(String xmlns) : base() {
            this.xmlns = xmlns;
        }
        /**
        * The String representation of the contents.
        * @return a String representation.
        */
        public override String ToString() {
            StringBuilder buf = new StringBuilder();
            foreach (object key in Keys) {
                Process(buf, key);
            }
            return buf.ToString();
        }
        /**
        * Processes a property
        * @param buf
        * @param p
        */
        virtual protected void Process(StringBuilder buf, Object p) {
            buf.Append('<');
            buf.Append(p);
            buf.Append('>');
            buf.Append(this[p.ToString()]);
            buf.Append("</");
            buf.Append(p);
            buf.Append('>');
        }

        /**
        * @return Returns the xmlns.
        */
        virtual public String Xmlns {
            get {
                return xmlns;
            }
        }

        /**
        * @param key
        * @param value
        * @return the previous property (null if there wasn't one)
        */
        virtual public void AddProperty(String key, String value) {
            this[key] = value;
        }
        
        public override string this[string key] {
            set {
                base[key] = XMLUtil.EscapeXML(value, false);
            }
        }
        
        virtual public void SetProperty(string key, XmpArray value) {
            base[key] = value.ToString();
        }
        
        /**
        * @see java.util.Properties#setProperty(java.lang.String, java.lang.String)
        * 
        * @param key
        * @param value
        * @return the previous property (null if there wasn't one)
        */
        virtual public void SetProperty(String key, LangAlt value) {
            base[key] = value.ToString();
        }
        
        /**
        * @param content
        * @return
        */
        public static String Escape(String content) {
            return XMLUtil.EscapeXML(content, false);
        }
    }
}
