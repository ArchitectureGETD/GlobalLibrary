using System;

namespace iTextSharp.GE.text.xml.simpleparser {
    /**
    * The handler for the events fired by <CODE>SimpleXMLParser</CODE>.
    */
    public interface ISimpleXMLDocHandlerComment {
        /**
        * Called when a comment is found.
        * @param text the comment text
        */    
        void Comment(String text);
    }
}
