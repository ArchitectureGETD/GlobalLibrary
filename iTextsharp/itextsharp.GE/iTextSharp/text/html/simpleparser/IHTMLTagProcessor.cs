using System;
using System.Collections.Generic;

namespace iTextSharp.GE.text.html.simpleparser {

    /**
     * Interface that needs to be implemented by every tag that is supported by HTMLWorker.
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public interface IHTMLTagProcessor {
        
        /**
         * Implement this class to tell the HTMLWorker what to do
         * when an open tag is encountered.
         * @param worker    the HTMLWorker
         * @param tag       the tag that was encountered
         * @param attrs     the current attributes of the tag
         * @throws DocumentException
         * @throws IOException
         */
        void StartElement(HTMLWorker worker, String tag, IDictionary<String, String> attrs);
        
        /**
         * Implement this class to tell the HTMLWorker what to do
         * when an close tag is encountered.
         * @param worker    the HTMLWorker
         * @param tag       the tag that was encountered
         * @throws DocumentException
         */
        void EndElement(HTMLWorker worker, String tag);
    }
}
