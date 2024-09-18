using System;
using System.Collections.Generic;
using iTextSharp.GE.text;

namespace iTextSharp.GE.text.html.simpleparser {
    /**
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public interface IImageProvider {
        Image GetImage(String src, IDictionary<string,string> attrs, ChainedProperties chain, IDocListener doc);
    }
}
