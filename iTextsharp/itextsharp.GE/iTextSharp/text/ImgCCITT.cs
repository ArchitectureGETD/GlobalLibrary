using System;
using iTextSharp.GE.text.pdf.codec;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text {
    /**
     * CCITT Image data that has to be inserted into the document
     *
     * @see        Element
     * @see        Image
     *
     * @author  Paulo Soares
     */
    /// <summary>
    /// CCITT Image data that has to be inserted into the document
    /// </summary>
    /// <seealso cref="T:iTextSharp.GE.text.Element"/>
    /// <seealso cref="T:iTextSharp.GE.text.Image"/>
    public class ImgCCITT : Image {
        public ImgCCITT(Image image) : base(image) {}

        /// <summary>
        /// Creats an Image in CCITT mode.
        /// </summary>
        /// <param name="width">the exact width of the image</param>
        /// <param name="height">the exact height of the image</param>
        /// <param name="reverseBits">
        /// reverses the bits in data.
        /// Bit 0 is swapped with bit 7 and so on
        /// </param>
        /// <param name="typeCCITT">
        /// the type of compression in data. It can be
        /// CCITTG4, CCITTG31D, CCITTG32D
        /// </param>
        /// <param name="parameters">
        /// parameters associated with this stream. Possible values are
        /// CCITT_BLACKIS1, CCITT_ENCODEDBYTEALIGN, CCITT_ENDOFLINE and CCITT_ENDOFBLOCK or a
        /// combination of them
        /// </param>
        /// <param name="data">the image data</param>
        public ImgCCITT(int width, int height, bool reverseBits, int typeCCITT, int parameters, byte[] data) : base((Uri)null) {
            if (typeCCITT != Element.CCITTG4 && typeCCITT != Element.CCITTG3_1D && typeCCITT != Element.CCITTG3_2D)
                throw new BadElementException(MessageLocalization.GetComposedMessage("the.ccitt.compression.type.must.be.ccittg4.ccittg3.1d.or.ccittg3.2d"));
            if (reverseBits)
                TIFFFaxDecoder.ReverseBits(data);
            type = Element.IMGRAW;
            scaledHeight = height;
            this.Top = scaledHeight;
            scaledWidth = width;
            this.Right = scaledWidth;
            colorspace = parameters;
            bpc = typeCCITT;
            rawData = data;
            plainWidth = this.Width;
            plainHeight = this.Height;
        }
    }
}
