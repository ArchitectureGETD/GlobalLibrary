using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * Dictionary containing parameters related to an active Flash subtype
     * in a RichMediaInstance dictionary.
     * See ExtensionLevel 3 p90
     * @since   5.0.0
     */
    public class RichMediaParams : PdfDictionary {

        /**
         * Creates a RichMediaParams object.
         */
        public RichMediaParams() : base(PdfName.RICHMEDIAPARAMS) {
        }
        
        /**
         * Sets a text string containing formatted name value pairs passed
         * to the Flash Player context when activated.
         * @param   flashVars   a String with the Flash variables
         */
        virtual public string FlashVars {
            set {
                Put(PdfName.FLASHVARS, new PdfString(value));
            }
        }
        
        /**
         * Sets the binding.
         * @param   binding possible values:
         * PdfName.NONE, PdfName.FOREGROUND, PdfName.BACKGROUND, PdfName.MATERIAL
         */
        virtual public PdfName Binding {
            set {
                Put(PdfName.BINDING, value);
            }
        }
        
        /**
         * Stores the material name that content is to be bound to.
         * Required if Binding value is Material.
         * @param   bindingMaterialName a material name
         */
        virtual public PdfString BindingMaterialName {
            set {
                Put(PdfName.BINDINGMATERIALNAME, value);
            }
        }
        
        /**
         * Sets an array of CuePoint dictionaries containing points
         * in time within a Flash animation.
         * @param   cuePoints   a PdfArray with CuePoint objects
         */
        virtual public PdfArray CuePoints {
            set {
                Put(PdfName.CUEPOINTS, value);
            }
        }
        
        /**
         * A text string used to store settings information associated
         * with a Flash RichMediaInstance. It is to be stored and loaded
         * by the scripting run time.
         * @param   settings    a PdfString
         */
        virtual public PdfString Settings {
            set {
                Put(PdfName.SETTINGS, value);
            }
        }
    }
}
