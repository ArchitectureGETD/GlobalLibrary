using System;
using iTextSharp.GE.xmp;
using iTextSharp.GE.xmp.options;

namespace iTextSharp.GE.text.xml.xmp {

    public class DublinCoreProperties {
        /** External Contributors to the resource (other than the authors). */
        public static readonly String CONTRIBUTOR = "contributor";
        /** The extent or scope of the resource. */
        public static readonly String COVERAGE = "coverage";
        /** The authors of the resource (listed in order of precedence, if significant). */
        public static readonly String CREATOR = "creator";
        /** Date(s) that something interesting happened to the resource. */
        public static readonly String DATE = "date";
        /** A textual description of the content of the resource. Multiple values may be present for different languages. */
        public static readonly String DESCRIPTION = "description";
        /** The file format used when saving the resource. Tools and applications should set this property to the save format of the data. It may include appropriate qualifiers. */
        public static readonly String FORMAT = "format";
        /** Unique identifier of the resource. */
        public static readonly String IDENTIFIER = "identifier";
        /** An unordered array specifying the languages used in the	resource. */
        public static readonly String LANGUAGE = "language";
        /** Publishers. */
        public static readonly String PUBLISHER = "publisher";
        /** Relationships to other documents. */
        public static readonly String RELATION = "relation";
        /** Informal rights statement, selected by language. */
        public static readonly String RIGHTS = "rights";
        /** Unique identifier of the work from which this resource was derived. */
        public static readonly String SOURCE = "source";
        /** An unordered array of descriptive phrases or keywords that specify the topic of the content of the resource. */
        public static readonly String SUBJECT = "subject";
        /** The title of the document, or the name given to the resource. Typically, it will be a name by which the resource is formally known. */
        public static readonly String TITLE = "title";
        /** A document type; for example, novel, poem, or working paper. */
        public static readonly String TYPE = "type";

        /**
         * Adds a title.
         *
         * @param xmpMeta
         * @param title
         */

        public static void AddTitle(IXmpMeta xmpMeta, String title) {
            xmpMeta.AppendArrayItem(XmpConst.NS_DC, TITLE, new PropertyOptions(PropertyOptions.ARRAY_ALTERNATE), title,
                                    null);
        }

        /**
         * Sets a title.
         *
         * @param xmpMeta
         * @param title
         * @param genericLang  The name of the generic language
         * @param specificLang The name of the specific language
         */

        public static void SetTitle(IXmpMeta xmpMeta, String title, String genericLang, String specificLang) {
            xmpMeta.SetLocalizedText(XmpConst.NS_DC, TITLE, genericLang, specificLang, title);
        }

        /**
         * Adds a description.
         *
         * @param xmpMeta
         * @param desc
         */

        public static void AddDescription(IXmpMeta xmpMeta, String desc) {
            xmpMeta.AppendArrayItem(XmpConst.NS_DC, DESCRIPTION, new PropertyOptions(PropertyOptions.ARRAY_ALTERNATE),
                                    desc, null);

        }

        /**
         * Sets a description.
         *
         * @param xmpMeta
         * @param desc
         * @param genericLang  The name of the generic language
         * @param specificLang The name of the specific language
         */

        public static void SetDescription(IXmpMeta xmpMeta, String desc, String genericLang, String specificLang) {
            xmpMeta.SetLocalizedText(XmpConst.NS_DC, DESCRIPTION, genericLang, specificLang, desc);
        }

        /**
         * Adds a subject.
         *
         * @param xmpMeta
         * @param subject
         */

        public static void AddSubject(IXmpMeta xmpMeta, String subject) {
            xmpMeta.AppendArrayItem(XmpConst.NS_DC, SUBJECT, new PropertyOptions(PropertyOptions.ARRAY), subject, null);
        }


        /**
         * Sets a subject.
         *
         * @param xmpMeta
         * @param subject array of subjects
         */

        public static void SetSubject(IXmpMeta xmpMeta, String[] subject) {
            XmpUtils.RemoveProperties(xmpMeta, XmpConst.NS_DC, SUBJECT, true, true);
            for (int i = 0; i < subject.Length; i++) {
                xmpMeta.AppendArrayItem(XmpConst.NS_DC, SUBJECT, new PropertyOptions(PropertyOptions.ARRAY), subject[i],
                                        null);
            }
        }

        /**
         * Adds a single author.
         *
         * @param xmpMeta
         * @param author
         */

        public static void AddAuthor(IXmpMeta xmpMeta, String author) {
            xmpMeta.AppendArrayItem(XmpConst.NS_DC, CREATOR, new PropertyOptions(PropertyOptions.ARRAY_ORDERED), author,
                                    null);
        }

        /**
         * Sets an array of authors.
         *
         * @param xmpMeta
         * @param author
         */

        public static void SetAuthor(IXmpMeta xmpMeta, String[] author) {
            XmpUtils.RemoveProperties(xmpMeta, XmpConst.NS_DC, CREATOR, true, true);
            for (int i = 0; i < author.Length; i++) {
                xmpMeta.AppendArrayItem(XmpConst.NS_DC, CREATOR, new PropertyOptions(PropertyOptions.ARRAY_ORDERED),
                                        author[i], null);
            }
        }

        /**
         * Adds a single publisher.
         *
         * @param xmpMeta
         * @param publisher
         */

        public static void AddPublisher(IXmpMeta xmpMeta, String publisher) {
            xmpMeta.AppendArrayItem(XmpConst.NS_DC, PUBLISHER, new PropertyOptions(PropertyOptions.ARRAY_ORDERED),
                                    publisher, null);
        }

        /**
         * Sets an array of publishers.
         *
         * @param xmpMeta
         * @param publisher
         */

        public static void SetPublisher(IXmpMeta xmpMeta, String[] publisher) {
            XmpUtils.RemoveProperties(xmpMeta, XmpConst.NS_DC, PUBLISHER, true, true);
            for (int i = 0; i < publisher.Length; i++) {
                xmpMeta.AppendArrayItem(XmpConst.NS_DC, PUBLISHER, new PropertyOptions(PropertyOptions.ARRAY_ORDERED),
                                        publisher[i], null);
            }
        }
    }
}
