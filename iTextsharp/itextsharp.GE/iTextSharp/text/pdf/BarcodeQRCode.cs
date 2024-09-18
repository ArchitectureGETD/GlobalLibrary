using System;
using System.Collections.Generic;
using iTextSharp.GE.text.pdf.qrcode;
using iTextSharp.GE.text.pdf.codec;

namespace iTextSharp.GE.text.pdf {

    /**
     * A QRCode implementation based on the zxing code.
     * @author Paulo Soares
     * @since 5.0.2
     */
    public class BarcodeQRCode {
        ByteMatrix bm;

        /**
         * Creates the QR barcode. The barcode is always created with the smallest possible size and is then stretched
         * to the width and height given. Set the width and height to 1 to get an unscaled barcode.
         * @param content the text to be encoded
         * @param width the barcode width
         * @param height the barcode height
         * @param hints modifiers to change the way the barcode is create. They can be EncodeHintType.ERROR_CORRECTION
         * and EncodeHintType.CHARACTER_SET. For EncodeHintType.ERROR_CORRECTION the values can be ErrorCorrectionLevel.L, M, Q, H.
         * For EncodeHintType.CHARACTER_SET the values are strings and can be Cp437, Shift_JIS and ISO-8859-1 to ISO-8859-16.
         * You can also use UTF-8, but correct behaviour is not guaranteed as Unicode is not supported in QRCodes.
         * The default value is ISO-8859-1.
         * @throws WriterException
         */
        public BarcodeQRCode(String content, int width, int height, IDictionary<EncodeHintType, Object> hints) {
            QRCodeWriter qc = new QRCodeWriter();
            bm = qc.Encode(content, width, height, hints);
        }

        private byte[] GetBitMatrix() {
            int width = bm.GetWidth();
            int height = bm.GetHeight();
            int stride = (width + 7) / 8;
            byte[] b = new byte[stride * height];
            sbyte[][] mt = bm.GetArray();
            for (int y = 0; y < height; ++y) {
                sbyte[] line = mt[y];
                for (int x = 0; x < width; ++x) {
                    if (line[x] != 0) {
                        int offset = stride * y + x / 8;
                        b[offset] |= (byte)(0x80 >> (x % 8));
                    }
                }
            }
            return b;
        }

        /** Gets an <CODE>Image</CODE> with the barcode.
         * @return the barcode <CODE>Image</CODE>
         * @throws BadElementException on error
         */
        virtual public Image GetImage() {
        byte[] b = GetBitMatrix();
        byte[] g4 = CCITTG4Encoder.Compress(b, bm.GetWidth(), bm.GetHeight());
        return Image.GetInstance(bm.GetWidth(), bm.GetHeight(), false, Image.CCITTG4, Image.CCITT_BLACKIS1, g4, null);
    }

    //    /** Creates a <CODE>java.awt.Image</CODE>.
    //     * @param foreground the color of the bars
    //     * @param background the color of the background
    //     * @return the image
    //     */
    //    public java.awt.Image CreateAwtImage(java.awt.Color foreground, java.awt.Color background) {
    //    int f = foreground.GetRGB();
    //    int g = background.GetRGB();
    //    Canvas canvas = new Canvas();

    //    int width = bm.GetWidth();
    //    int height = bm.GetHeight();
    //    int[] pix = new int[width * height];
    //    byte[][] mt = bm.GetArray();
    //    for (int y = 0; y < height; ++y) {
    //        byte[] line = mt[y];
    //        for (int x = 0; x < width; ++x) {
    //            pix[y * width + x] = line[x] == 0 ? f : g;
    //        }
    //    }

    //    java.awt.Image img = canvas.CreateImage(new MemoryImageSource(width, height, pix, 0, width));
    //    return img;
    //}

        public void PlaceBarcode(PdfContentByte cb, BaseColor foreground, float moduleSide)
        {
            int width = bm.GetWidth();
            int height = bm.GetHeight();
            sbyte[][] mt = bm.GetArray();

            cb.SetColorFill(foreground);

            for (int y = 0; y < height; ++y)
            {
                sbyte[] line = mt[y];
                for (int x = 0; x < width; ++x)
                {
                    if (line[x] == 0)
                    {
                        cb.Rectangle(x * moduleSide, (height - y - 1) * moduleSide, moduleSide, moduleSide);
                    }
                }
            }
            cb.Fill();
        }

        /** Gets the size of the barcode grid. */
        public Rectangle GetBarcodeSize()
        {
            return new Rectangle(0, 0, bm.GetWidth(), bm.GetHeight());
        }

    }
}
