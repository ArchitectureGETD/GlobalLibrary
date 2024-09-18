using System;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.fonts.cmaps {

    /**
     *
     * @author psoares
     */
    public interface ICidLocation {
        PRTokeniser GetLocation(String location);
    }
}
