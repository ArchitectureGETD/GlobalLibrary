using System;
using iTextSharp.GE.text;

namespace iTextSharp.GE.text.html.simpleparser {

    /**
     * Allows you to do additional processing on a Paragraph that contains a link.
     * @author  psoares
     * @since 5.0.6 (renamed)
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public interface ILinkProcessor {
        /**
         * Does additional processing on a link paragraph
         * @param current   the Paragraph that has the link
         * @param attrs     the attributes
         * @return  false if the Paragraph no longer needs processing
         */
        bool Process(Paragraph current, ChainedProperties attrs);
    }
}
