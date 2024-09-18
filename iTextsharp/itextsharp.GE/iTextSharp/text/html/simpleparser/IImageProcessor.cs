using System;
using System.Collections.Generic;
using iTextSharp.GE.text;

namespace iTextSharp.GE.text.html.simpleparser {

    /**
     * Implement this interface to process images and
     * to indicate if the image needs to be added or
     * skipped.
     * @since 5.0.6 (renamed)
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public interface IImageProcessor {
        /**
         * Allows you to (pre)process the image before (or instead of)
         * adding it to the DocListener with HTMLWorker.
         * @param img   the Image object
         * @param attrs attributes of the image
         * @param chain hierarchy of attributes
         * @param doc   the DocListener to which the Image needs to be added
         * @return  false if you still want HTMLWorker to add the Image
         */
        bool Process(Image img, IDictionary<String, String> attrs, ChainedProperties chain, IDocListener doc);
    }
}
