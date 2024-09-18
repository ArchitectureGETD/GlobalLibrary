using System;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.io;
namespace iTextSharp.GE.text.pdf.fonts.cmaps {

    /**
     *
     * @author psoares
     */
    public class CidLocationFromByte : ICidLocation {
        private byte[] data;

        public CidLocationFromByte(byte[] data) {
            this.data = data;
        }
        
        public virtual PRTokeniser GetLocation(String location) {
            return new PRTokeniser(new RandomAccessFileOrArray(new RandomAccessSourceFactory().CreateSource(data)));
        }
    }
}
