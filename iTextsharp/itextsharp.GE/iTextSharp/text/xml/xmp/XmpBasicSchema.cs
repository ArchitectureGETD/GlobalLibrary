using System;

namespace iTextSharp.GE.text.xml.xmp {

    /**
    * An implementation of an XmpSchema.
    */
    [Obsolete]
    public class XmpBasicSchema : XmpSchema {
        
        /** default namespace identifier*/
        public const String DEFAULT_XPATH_ID = "xmp";
        /** default namespace uri*/
        public const String DEFAULT_XPATH_URI = "http://ns.adobe.com/xap/1.0/";
        /** An unordered array specifying properties that were edited outside the authoring application. Each item should contain a single namespace and XPath separated by one ASCII space (U+0020). */
        public const String ADVISORY = "xmp:Advisory";
        /** The base URL for relative URLs in the document content. If this document contains Internet links, and those links are relative, they are relative to this base URL. This property provides a standard way for embedded relative URLs to be interpreted by tools. Web authoring tools should set the value based on their notion of where URLs will be interpreted. */
        public const String BASEURL = "xmp:BaseURL";
        /** The date and time the resource was originally created. */
        public const String CREATEDATE = "xmp:CreateDate";
        /** The name of the first known tool used to create the resource. If history is present in the metadata, this value should be equivalent to that of xmpMM:History�s softwareAgent property. */
        public const String CREATORTOOL = "xmp:CreatorTool";
        /** An unordered array of text strings that unambiguously identify the resource within a given context. */
        public const String IDENTIFIER = "xmp:Identifier";
        /** The date and time that any metadata for this resource was last changed. */
        public const String METADATADATE = "xmp:MetadataDate";
        /** The date and time the resource was last modified. */
        public const String MODIFYDATE = "xmp:ModifyDate";
        /** A short informal name for the resource. */
        public const String NICKNAME = "xmp:Nickname";
        /** An alternative array of thumbnail images for a file, which can differ in characteristics such as size or image encoding. */
        public const String THUMBNAILS = "xmp:Thumbnails";
        
        /**
        * @param shorthand
        * @throws IOException
        */
        public XmpBasicSchema() : base("xmlns:" + DEFAULT_XPATH_ID + "=\"" + DEFAULT_XPATH_URI + "\"") {
        }
        
        /**
        * Adds the creatortool.
        * @param creator
        */
        virtual public void AddCreatorTool(String creator) {
            this[CREATORTOOL] = creator;
        }
        
        /**
        * Adds the creation date.
        * @param date
        */
        virtual public void AddCreateDate(String date) {
            this[CREATEDATE] = date;
        }
        
        /**
        * Adds the modification date.
        * @param date
        */
        virtual public void AddModDate(String date) {
            this[MODIFYDATE] = date;
        }

	    /**
	    * Adds the meta data date.
	    * @param date
	    */
	    virtual public void AddMetaDataDate(String date) {
		    this[METADATADATE] = date;
	    }

        /** Adds the identifier.
        * @param id
        */
        virtual public void AddIdentifiers(String[] id) {
            XmpArray array = new XmpArray(XmpArray.UNORDERED);
            for (int i = 0; i < id.Length; i++) {
                array.Add(id[i]);
            }
            SetProperty(IDENTIFIER, array);
        }

        /** Adds the nickname.
        * @param name
        */
        virtual public void AddNickname(String name) {
            this[NICKNAME] = name;
        }
    }
}
