using System;

namespace iTextSharp.GE.text.xml.xmp {

    /**
    * An implementation of an XmpSchema.
    */
    [Obsolete]
    public class XmpMMSchema : XmpSchema {

        /** default namespace identifier*/
        public const String DEFAULT_XPATH_ID = "xmpMM";
        /** default namespace uri*/
        public const String DEFAULT_XPATH_URI = "http://ns.adobe.com/xap/1.0/mm/";
        /** A reference to the original document from which this one is derived. It is a minimal reference; missing components can be assumed to be unchanged. For example, a new version might only need to specify the instance ID and version number of the previous version, or a rendition might only need to specify the instance ID and rendition class of the original. */
        public const String DERIVEDFROM = "xmpMM:DerivedFrom"; 
        /** The common identifier for all versions and renditions of a document. */
        public const String DOCUMENTID = "xmpMM:DocumentID";
        /** An ordered array of high-level user actions that resulted in this resource. It is intended to give human readers a general indication of the steps taken to make the changes from the previous version to this one. The list should be at an abstract level; it is not intended to be an exhaustive keystroke or other detailed history. */
        public const String HISTORY = "xmpMM:History";
        /** A reference to the document as it was prior to becoming managed. It is set when a managed document is introduced to an asset management system that does not currently own it. It may or may not include references to different management systems. */
        public const String MANAGEDFROM = "xmpMM:ManagedFrom";
        /** The name of the asset management system that manages this resource. */
        public const String MANAGER = "xmpMM:Manager";
        /** A URI identifying the managed resource to the asset management system; the presence of this property is the formal indication that this resource is managed. The form and content of this URI is private to the asset management system. */
        public const String MANAGETO = "xmpMM:ManageTo";
        /** A URI that can be used to access information about the managed resource through a web browser. It might require a custom browser plugin. */
        public const String MANAGEUI = "xmpMM:ManageUI";
        /** Specifies a particular variant of the asset management system. The format of this property is private to the specific asset management system. */
        public const String MANAGERVARIANT = "xmpMM:ManagerVariant";
        /** The rendition class name for this resource.*/
        public const String RENDITIONCLASS = "xmpMM:RenditionClass";
        /**  Can be used to provide additional rendition parameters that are too complex or verbose to encode in xmpMM: RenditionClass. */
        public const String RENDITIONPARAMS = "xmpMM:RenditionParams";
        /** The document version identifier for this resource. */
        public const String VERSIONID = "xmpMM:VersionID";
        /** The version history associated with this resource.*/
        public const String VERSIONS = "xmpMM:Versions";
        
        /**
        * @throws IOException
        */
        public XmpMMSchema() : base("xmlns:" + DEFAULT_XPATH_ID + "=\"" + DEFAULT_XPATH_URI + "\"") {
        }
    }
}
