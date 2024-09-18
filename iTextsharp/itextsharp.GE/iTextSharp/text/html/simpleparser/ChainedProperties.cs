using System;
using System.Collections.Generic;

namespace iTextSharp.GE.text.html.simpleparser {
    /**
     * Stores the hierarchy of tags along with the attributes of each tag.
     * @since 5.0.6 renamed from ChainedProperties
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public class ChainedProperties {
    
        /**
         * Class that stores the info about one tag in the chain.
         */
        public sealed class TagAttributes {
            /** A possible tag */
            internal String tag;
            /** The styles corresponding with the tag */
            internal IDictionary<String, String> attrs;
            /**
             * Constructs a chained property.
             * @param   tag     an XML/HTML tag
             * @param   attrs   the tag's attributes
             */
            internal TagAttributes(String tag, IDictionary<String, String> attrs) {
                this.tag = tag;
                this.attrs = attrs;
            }
        }

    	/** A list of chained properties representing the tag hierarchy. */
        public IList<TagAttributes> chain = new List<TagAttributes>();
        
        /** Creates a new instance of ChainedProperties */
        public ChainedProperties() {
        }
        
	    /**
	     * Walks through the hierarchy (bottom-up) looking for
	     * a property key. Returns a value as soon as a match
	     * is found or null if the key can't be found.
	     * @param	key	the key of the property
	     * @return	the value of the property
	     */
        public String this[String key] {
            get {
                for (int k = chain.Count - 1; k >= 0; --k) {
                    TagAttributes p = chain[k];
                    IDictionary<String, String> attrs = p.attrs;
                    if (attrs.ContainsKey(key))
                        return attrs[key];
                }
                return null;
            }
        }
        
	    /**
	     * Walks through the hierarchy (bottom-up) looking for
	     * a property key. Returns true as soon as a match is
	     * found or false if the key can't be found.
	     * @param	key	the key of the property
	     * @return	true if the key is found
	     */
        virtual public bool HasProperty(String key) {
            for (int k = chain.Count - 1; k >= 0; --k) {
                TagAttributes p = chain[k];
                IDictionary<String, String> attrs = p.attrs;
                if (attrs.ContainsKey(key))
                    return true;
            }
            return false;
        }
        
	    /**
	     * Adds a tag and its corresponding properties to the chain.
	     * @param tag	the tags that needs to be added to the chain
	     * @param props	the tag's attributes
	     */
	    virtual public void AddToChain(String tag, IDictionary<String, String> props) {
		    AdjustFontSize(props);
		    chain.Add(new TagAttributes(tag, props));
        }
        
        virtual public void RemoveChain(String tag) {
            for (int k = chain.Count - 1; k >= 0; --k) {
                if (tag.Equals(chain[k].tag)) {
                    chain.RemoveAt(k);
                    return;
                }
            }
        }

        /**
         * If the properties contain a font size, the size may need to
         * be adjusted based on font sizes higher in the hierarchy.
         * @param   attrs the attributes that may have to be updated
         * @since 5.0.6 (renamed)
         */
        virtual protected internal void AdjustFontSize(IDictionary<String, String> attrs) {
            // fetch the font size
            String value;
            attrs.TryGetValue(HtmlTags.SIZE, out value);
            // do nothing if the font size isn't defined
            if (value == null)
                return;
            // the font is defined as a real size: remove "pt"
            if (value.EndsWith("pt")) {
                attrs[HtmlTags.SIZE] = value.Substring(0, value.Length - 2);
                return;
            }
            String old = this[HtmlTags.SIZE];
            attrs[HtmlTags.SIZE] = HtmlUtilities.GetIndexedFontSize(value, old).ToString();
        }
    }
}
