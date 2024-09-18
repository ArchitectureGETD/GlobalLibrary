using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * The RichMediaConfiguration dictionary describes a set of instances
     * that are loaded for a given scene configuration. The configuration
     * to be loaded when an annotation is activated is referenced by the
     * Configuration key in the RichMediaActivation dictionary specified
     * in the RichMediaSettings dictionary.
     * see ExtensionLevel 3 p88
     * @see RichMediaAnnotation
     * @see RichMediaInstance
     * @since	5.0.0
     */
    public class RichMediaConfiguration : PdfDictionary {

	    /** An array of indirect object references to RichMediaInstance dictionaries. */
	    protected PdfArray instances = new PdfArray();
    	
	    /**
	     * Creates a RichMediaConfiguration object. Also specifies the primary
	     * content type for the configuration. Valid values are 3D, Flash, Sound,
	     * and Video.
	     * @param	subtype	Possible values are:
	     * PdfName._3D, PdfName.FLASH, PdfName.SOUND, and PdfName.VIDEO.
	     */
	    public RichMediaConfiguration(PdfName subtype) : base(PdfName.RICHMEDIACONFIGURATION) {
		    Put(PdfName.SUBTYPE, subtype);
		    Put(PdfName.INSTANCES, instances);
	    }
    	
	    /**
	     * Sets the name of the configuration (must be unique).
	     * @param	name	the name
	     */
	    virtual public PdfString Name {
            set {
		        Put(PdfName.NAME, value);
            }
	    }
    	
	    /**
	     * Adds a RichMediaInstance to the instances array of this
	     * configuration.
	     * @param	instance	a RichMediaInstance
	     */
	    virtual public void AddInstance(RichMediaInstance instance) {
		    instances.Add(instance);
	    }
    }
}
