using System;
using System.Collections.Generic;

namespace iTextSharp.GE.text.pdf {
    /**
    * Content typically belongs to a single optional content group,
    * and is visible when the group is <B>ON</B> and invisible when it is <B>OFF</B>. To express more
    * complex visibility policies, content should not declare itself to belong to an optional
    * content group directly, but rather to an optional content membership dictionary
    * represented by this class.
    *
    * @author Paulo Soares
    */
    public class PdfLayerMembership : PdfDictionary, IPdfOCG {
        
        /**
        * Visible only if all of the entries are <B>ON</B>.
        */    
        public static readonly PdfName ALLON = new PdfName("AllOn");
        /**
        * Visible if any of the entries are <B>ON</B>.
        */    
        public static readonly PdfName ANYON = new PdfName("AnyOn");
        /**
        * Visible if any of the entries are <B>OFF</B>.
        */    
        public static readonly PdfName ANYOFF = new PdfName("AnyOff");
        /**
        * Visible only if all of the entries are <B>OFF</B>.
        */    
        public static readonly PdfName ALLOFF = new PdfName("AllOff");

        internal PdfIndirectReference refi;
        internal PdfArray members = new PdfArray();
        internal Dictionary<PdfLayer,object> layers = new Dictionary<PdfLayer,object>();
        
        /**
        * Creates a new, empty, membership layer.
        * @param writer the writer
        */    
        public PdfLayerMembership(PdfWriter writer) : base(PdfName.OCMD) {
            Put(PdfName.OCGS, members);
            refi = writer.PdfIndirectReference;
        }
        
        /**
        * Gets the <CODE>PdfIndirectReference</CODE> that represents this membership layer.
        * @return the <CODE>PdfIndirectReference</CODE> that represents this layer
        */    
        virtual public PdfIndirectReference Ref {
            get {
                return refi;
            }
        }
        
        /**
        * Adds a new member to the layer.
        * @param layer the new member to the layer
        */    
        virtual public void AddMember(PdfLayer layer) {
            if (!layers.ContainsKey(layer)) {
                members.Add(layer.Ref);
                layers[layer] = null;
            }
        }
        
        /**
        * Gets the member layers.
        * @return the member layers
        */    
        virtual public Dictionary<PdfLayer,object>.KeyCollection Layers {
            get {
                return layers.Keys;
            }
        }
        
        /**
        * Sets the visibility policy for content belonging to this
        * membership dictionary. Possible values are ALLON, ANYON, ANYOFF and ALLOFF.
        * The default value is ANYON.
        * @param type the visibility policy
        */    
        virtual public PdfName VisibilityPolicy {
            set {
                Put(PdfName.P, value);
            }
        }
        
        /**
         * Sets the visibility expression for content belonging to this
         * membership dictionary.
         * @param ve A (nested) array of which the first value is /And, /Or, or /Not
         * followed by a series of indirect references to OCGs or other visibility
         * expressions.
         * @since 5.0.2
         */
        virtual public PdfVisibilityExpression VisibilityExpression {
            set {
                Put(PdfName.VE, value);
            }
        }

        /**
        * Gets the dictionary representing the membership layer. It just returns <CODE>this</CODE>.
        * @return the dictionary representing the layer
        */    
        virtual public PdfObject PdfObject {
            get {
                return this;
            }
        }
    }
}
