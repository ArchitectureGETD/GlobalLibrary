using System;
using System.IO;
using iTextSharp.GE.text.error_messages;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.io;
namespace iTextSharp.GE.text.pdf.fonts.cmaps {

    /**
     *
     * @author psoares
     */
    public class CidResource : ICidLocation{

        public virtual PRTokeniser GetLocation(String location) {
            String fullName = BaseFont.RESOURCE_PATH + "cmaps." + location;
            Stream inp = StreamUtil.GetResourceStream(fullName);
            if (inp == null)
                throw new IOException(MessageLocalization.GetComposedMessage("the.cmap.1.was.not.found", fullName));
            return new PRTokeniser(new RandomAccessFileOrArray(new RandomAccessSourceFactory().CreateSource(inp)));
        }
    }
}
