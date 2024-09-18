using System;
using System.Collections.Generic;

namespace iTextSharp.GE.text.xml.simpleparser {
    /**
    * The handler for the events fired by <CODE>SimpleXMLParser</CODE>.
    * @author Paulo Soares
    */
    public interface ISimpleXMLDocHandler {
        /**
        * Called when a start tag is found.
        * @param tag the tag name
        * @param h the tag's attributes
        */    
        void StartElement(String tag, IDictionary<string,string> h);
        /**
        * Called when an end tag is found.
        * @param tag the tag name
        */    
        void EndElement(String tag);
        /**
        * Called when the document starts to be parsed.
        */    
        void StartDocument();
        /**
        * Called after the document is parsed.
        */    
        void EndDocument();
        /**
        * Called when a text element is found.
        * @param str the text element, probably a fragment.
        */    
        void Text(String str);
    }
}
