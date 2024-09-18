using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.xmp;
using iTextSharp.GE.xmp.options;

namespace iTextSharp.GE.text.xml.xmp {


    /**
     * With this class you can create an Xmp Stream that can be used for adding
     * Metadata to a PDF Dictionary. Remark that this class doesn't cover the
     * complete XMP specification.
     */

    public class XmpWriter {

        /** A possible charset for the XMP. */
        public static String UTF8 = "UTF-8";
        /** A possible charset for the XMP. */
        public static String UTF16 = "UTF-16";
        /** A possible charset for the XMP. */
        public static String UTF16BE = "UTF-16BE";
        /** A possible charset for the XMP. */
        public static String UTF16LE = "UTF-16LE";

        protected IXmpMeta xmpMeta;
        protected Stream outputStream;
        protected SerializeOptions serializeOptions;

        /**
         * Creates an XmpWriter.
         * @param os
         * @param utfEncoding
         * @param extraSpace
         * @throws IOException
         */

        public XmpWriter(Stream os, String utfEncoding, int extraSpace) {
            outputStream = os;
            serializeOptions = new SerializeOptions();
            if (UTF16BE.Equals(utfEncoding) || UTF16.Equals(utfEncoding))
                serializeOptions.EncodeUtf16Be = true;
            else if (UTF16LE.Equals(utfEncoding))
                serializeOptions.EncodeUtf16Le = true;
            serializeOptions.Padding = extraSpace;
            xmpMeta = XmpMetaFactory.Create();
            xmpMeta.ObjectName = XmpConst.TAG_XMPMETA;
            xmpMeta.ObjectName = "";
            try {
                xmpMeta.SetProperty(XmpConst.NS_DC, DublinCoreProperties.FORMAT, "application/pdf");
                xmpMeta.SetProperty(XmpConst.NS_PDF, PdfProperties.PRODUCER, Version.GetInstance().GetVersion);
            }
            catch (XmpException) {}
        }

        /**
         * Creates an XmpWriter.
         * @param os
         * @throws IOException
         */
        public XmpWriter(Stream os) : this(os, UTF8, 2000) {}

        /**
         * @param os
         * @param info
         * @throws IOException
         */

        public XmpWriter(Stream os, PdfDictionary info)
            : this(os) {
            if (info != null) {
                PdfName key;
                PdfObject obj;
                String value;
                foreach (PdfName pdfName in info.Keys) {
                    key = pdfName;
                    obj = info.Get(key);
                    if (obj == null)
                        continue;
                    if (!obj.IsString())
                        continue;
                    value = ((PdfString) obj).ToUnicodeString();
                    try {
                        AddDocInfoProperty(key, value);
                    }
                    catch (XmpException xmpExc) {
                        throw new IOException(xmpExc.Message);
                    }
                }
            }
        }

        /**
         * @param os
         * @param info
         * @throws IOException
         * @since 5.0.1 (generic type in signature)
         */

        public XmpWriter(Stream os, IDictionary<String, String> info)
            : this(os) {
            if (info != null) {
                String key;
                String value;
                foreach (KeyValuePair<string, string> entry in info) {
                    key = entry.Key;
                    value = entry.Value;
                    if (value == null)
                        continue;
                    try {
                        AddDocInfoProperty(key, value);
                    }
                    catch (XmpException xmpExc) {
                        throw new IOException(xmpExc.Message);
                    }
                }
            }
        }

        virtual public IXmpMeta XmpMeta {
            get { return xmpMeta; }
        }

        /** Sets the XMP to read-only */

        virtual public bool ReadOnly {
            get { return serializeOptions.ReadOnlyPacket; }
            set { serializeOptions.ReadOnlyPacket = value; }
        }

        /**
         * @param about The about to set.
         */

        virtual public String About {
            get { return xmpMeta.ObjectName; }
            set { xmpMeta.ObjectName = value; }
        }

        /**
         * Adds an rdf:Description.
         * @param xmlns
         * @param content
         * @throws IOException
         */

        [Obsolete]
        virtual public void AddRdfDescription(String xmlns, String content) {
            try {
                String str = "<rdf:RDF xmlns:rdf=\"" + XmpConst.NS_RDF + "\">" +
                             "<rdf:Description rdf:about=\"" + xmpMeta.ObjectName +
                             "\" " +
                             xmlns +
                             ">" +
                             content +
                             "</rdf:Description></rdf:RDF>\n";
                IXmpMeta extMeta = XmpMetaFactory.ParseFromString(str);
                XmpUtils.AppendProperties(extMeta, xmpMeta, true, true);
            }
            catch (XmpException xmpExc) {
                throw new IOException(xmpExc.Message);
            }
        }

        /**
         * Adds an rdf:Description.
         * @param s
         * @throws IOException
         */

        [Obsolete]
        virtual public void AddRdfDescription(XmpSchema s) {
            try {
                String str = "<rdf:RDF xmlns:rdf=\"" + XmpConst.NS_RDF + "\">" +
                             "<rdf:Description rdf:about=\"" + xmpMeta.ObjectName +
                             "\" " +
                             s.Xmlns +
                             ">" +
                             s.ToString() +
                             "</rdf:Description></rdf:RDF>\n";
                IXmpMeta extMeta = XmpMetaFactory.ParseFromString(str);
                XmpUtils.AppendProperties(extMeta, xmpMeta, true, true);
            }
            catch (XmpException xmpExc) {
                throw new IOException(xmpExc.Message);
            }
        }

        /**
         * @param schemaNS The namespace URI for the property. Has the same usage as in getProperty.
         * @param propName The name of the property.
         *                 Has the same usage as in <code>getProperty()</code>.
         * @param value    the value for the property (only leaf properties have a value).
         *                 Arrays and non-leaf levels of structs do not have values.
         *                 Must be <code>null</code> if the value is not relevant.<br/>
         *                 The value is automatically detected: Boolean, Integer, Long, Double, XMPDateTime and
         *                 byte[] are handled, on all other <code>toString()</code> is called.
         * @throws XMPException Wraps all errors and exceptions that may occur.
         */

        virtual public void SetProperty(String schemaNS, String propName, Object value) {
            xmpMeta.SetProperty(schemaNS, propName, value);
        }

        /**
         * Simplifies the construction of an array by not requiring that you pre-create an empty array.
         * The array that is assigned is created automatically if it does not yet exist. Each call to
         * AppendArrayItem() appends an item to the array.
         *
         * @param schemaNS  The namespace URI for the array.
         * @param arrayName The name of the array. May be a general path expression, must not be null or
         *                  the empty string.
         * @param value     the value of the array item.
         * @throws XMPException Wraps all errors and exceptions that may occur.
         */

        virtual public void AppendArrayItem(String schemaNS, String arrayName, String value) {
            xmpMeta.AppendArrayItem(schemaNS, arrayName, new PropertyOptions(PropertyOptions.ARRAY), value, null);
        }

        /**
         * Simplifies the construction of an ordered array by not requiring that you pre-create an empty array.
         * The array that is assigned is created automatically if it does not yet exist. Each call to
         * AppendArrayItem() appends an item to the array.
         *
         * @param schemaNS  The namespace URI for the array.
         * @param arrayName The name of the array. May be a general path expression, must not be null or
         *                  the empty string.
         * @param value     the value of the array item.
         * @throws XMPException Wraps all errors and exceptions that may occur.
         */

        virtual public void AppendOrderedArrayItem(String schemaNS, String arrayName, String value) {
            xmpMeta.AppendArrayItem(schemaNS, arrayName, new PropertyOptions(PropertyOptions.ARRAY_ORDERED), value, null);
        }

        /**
         * Simplifies the construction of an alternate array by not requiring that you pre-create an empty array.
         * The array that is assigned is created automatically if it does not yet exist. Each call to
         * AppendArrayItem() appends an item to the array.
         *
         * @param schemaNS  The namespace URI for the array.
         * @param arrayName The name of the array. May be a general path expression, must not be null or
         *                  the empty string.
         * @param value     the value of the array item.
         * @throws XMPException Wraps all errors and exceptions that may occur.
         */

        virtual public void AppendAlternateArrayItem(String schemaNS, String arrayName, String value) {
            xmpMeta.AppendArrayItem(schemaNS, arrayName, new PropertyOptions(PropertyOptions.ARRAY_ALTERNATE), value,
                                    null);
        }

        /**
         * Flushes and closes the XmpWriter.
         * @throws IOException
         */

        virtual public void Serialize(Stream externalOutputStream) {
            XmpMetaFactory.Serialize(xmpMeta, externalOutputStream, serializeOptions);
        }

        /**
         * Flushes and closes the XmpWriter.
         * @throws IOException
         */

        virtual public void Close() {
            if (outputStream == null)
                return;
            try {
                XmpMetaFactory.Serialize(xmpMeta, outputStream, serializeOptions);
                outputStream = null;
            }
            catch (XmpException xmpExc) {
                throw new IOException(xmpExc.Message);
            }
        }

        virtual public void AddDocInfoProperty(Object key, String value) {
            if (key is String)
                key = new PdfName((String) key);
            if (PdfName.TITLE.Equals(key)) {
                xmpMeta.SetLocalizedText(XmpConst.NS_DC, DublinCoreProperties.TITLE, XmpConst.X_DEFAULT,
                                         XmpConst.X_DEFAULT, value);
            } else if (PdfName.AUTHOR.Equals(key)) {
                xmpMeta.AppendArrayItem(XmpConst.NS_DC, DublinCoreProperties.CREATOR,
                                        new PropertyOptions(PropertyOptions.ARRAY_ORDERED), value, null);
            } else if (PdfName.SUBJECT.Equals(key)) {
                xmpMeta.SetLocalizedText(XmpConst.NS_DC, DublinCoreProperties.DESCRIPTION, XmpConst.X_DEFAULT,
                                         XmpConst.X_DEFAULT, value);
            } else if (PdfName.KEYWORDS.Equals(key)) {
                foreach (String v in value.Split(',', ';'))
                    if (v.Trim().Length > 0)
                        xmpMeta.AppendArrayItem(XmpConst.NS_DC, DublinCoreProperties.SUBJECT, new PropertyOptions(PropertyOptions.ARRAY), v.Trim(), null);
                xmpMeta.SetProperty(XmpConst.NS_PDF, PdfProperties.KEYWORDS, value);
            } else if (PdfName.PRODUCER.Equals(key)) {
                xmpMeta.SetProperty(XmpConst.NS_PDF, PdfProperties.PRODUCER, value);
            } else if (PdfName.CREATOR.Equals(key)) {
                xmpMeta.SetProperty(XmpConst.NS_XMP, XmpBasicProperties.CREATORTOOL, value);
            } else if (PdfName.CREATIONDATE.Equals(key)) {
                xmpMeta.SetProperty(XmpConst.NS_XMP, XmpBasicProperties.CREATEDATE, PdfDate.GetW3CDate(value));
            } else if (PdfName.MODDATE.Equals(key)) {
                xmpMeta.SetProperty(XmpConst.NS_XMP, XmpBasicProperties.MODIFYDATE, PdfDate.GetW3CDate(value));
            }
        }
    }
}
