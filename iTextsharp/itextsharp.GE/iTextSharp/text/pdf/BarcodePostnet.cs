using System;

namespace iTextSharp.GE.text.pdf {

    /** Implements the Postnet and Planet barcodes. The default parameters are:
     * <pre>
     *n = 72f / 22f; // distance between bars
     *x = 0.02f * 72f; // bar width
     *barHeight = 0.125f * 72f; // height of the tall bars
     *size = 0.05f * 72f; // height of the short bars
     *codeType = POSTNET; // type of code
     * </pre>
     *
     * @author Paulo Soares
     */
    public class BarcodePostnet : Barcode{

        /** The bars for each character.
         */    
        private static readonly byte[][] BARS = {
         new byte[] {1,1,0,0,0},
         new byte[] {0,0,0,1,1},
         new byte[] {0,0,1,0,1},
         new byte[] {0,0,1,1,0},
         new byte[] {0,1,0,0,1},
         new byte[] {0,1,0,1,0},
         new byte[] {0,1,1,0,0},
         new byte[] {1,0,0,0,1},
         new byte[] {1,0,0,1,0},
         new byte[] {1,0,1,0,0}
    };
    
        /** Creates new BarcodePostnet */
        public BarcodePostnet() {
            n = 72f / 22f; // distance between bars
            x = 0.02f * 72f; // bar width
            barHeight = 0.125f * 72f; // height of the tall bars
            size = 0.05f * 72f; // height of the short bars
            codeType = POSTNET; // type of code
        }
    
        /** Creates the bars for Postnet.
         * @param text the code to be created without checksum
         * @return the bars
         */    
        public static byte[] GetBarsPostnet(string text) {
            int total = 0;
            for (int k = text.Length - 1; k >= 0; --k) {
                int n = text[k] - '0';
                total += n;
            }
            text += (char)(((10 - (total % 10)) % 10) + '0');
            byte[] bars = new byte[text.Length * 5 + 2];
            bars[0] = 1;
            bars[bars.Length - 1] = 1;
            for (int k = 0; k < text.Length; ++k) {
                int c = text[k] - '0';
                Array.Copy(BARS[c], 0, bars, k * 5 + 1, 5);
            }
            return bars;
        }

        /** Gets the maximum area that the barcode and the text, if
         * any, will occupy. The lower left corner is always (0, 0).
         * @return the size the barcode occupies.
         */
        public override  Rectangle BarcodeSize {
            get {
                float width = ((code.Length + 1) * 5 + 1) * n + x;
                return new Rectangle(width, barHeight);
            }
        }
    
        /** Places the barcode in a <CODE>PdfContentByte</CODE>. The
         * barcode is always placed at coodinates (0, 0). Use the
         * translation matrix to move it elsewhere.<p>
         * The bars and text are written in the following colors:<p>
         * <P><TABLE BORDER=1>
         * <TR>
         *   <TH><P><CODE>barColor</CODE></TH>
         *   <TH><P><CODE>textColor</CODE></TH>
         *   <TH><P>Result</TH>
         *   </TR>
         * <TR>
         *   <TD><P><CODE>null</CODE></TD>
         *   <TD><P><CODE>null</CODE></TD>
         *   <TD><P>bars and text painted with current fill color</TD>
         *   </TR>
         * <TR>
         *   <TD><P><CODE>barColor</CODE></TD>
         *   <TD><P><CODE>null</CODE></TD>
         *   <TD><P>bars and text painted with <CODE>barColor</CODE></TD>
         *   </TR>
         * <TR>
         *   <TD><P><CODE>null</CODE></TD>
         *   <TD><P><CODE>textColor</CODE></TD>
         *   <TD><P>bars painted with current color<br>text painted with <CODE>textColor</CODE></TD>
         *   </TR>
         * <TR>
         *   <TD><P><CODE>barColor</CODE></TD>
         *   <TD><P><CODE>textColor</CODE></TD>
         *   <TD><P>bars painted with <CODE>barColor</CODE><br>text painted with <CODE>textColor</CODE></TD>
         *   </TR>
         * </TABLE>
         * @param cb the <CODE>PdfContentByte</CODE> where the barcode will be placed
         * @param barColor the color of the bars. It can be <CODE>null</CODE>
         * @param textColor the color of the text. It can be <CODE>null</CODE>
         * @return the dimensions the barcode occupies
         */
        public override Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor) {
            if (barColor != null)
                cb.SetColorFill(barColor);
            byte[] bars = GetBarsPostnet(code);
            byte flip = 1;
            if (codeType == PLANET) {
                flip = 0;
                bars[0] = 0;
                bars[bars.Length - 1] = 0;
            }
            float startX = 0;
            for (int k = 0; k < bars.Length; ++k) {
                cb.Rectangle(startX, 0, x - inkSpreading, bars[k] == flip ? barHeight : size);
                startX += n;
            }
            cb.Fill();
            return this.BarcodeSize;
        }

#if DRAWING
        public override System.Drawing.Image CreateDrawingImage(System.Drawing.Color foreground, System.Drawing.Color background) {
            int barWidth = (int)x;
            if (barWidth <= 0)
                barWidth = 1;
            int barDistance = (int)n;
            if (barDistance <= barWidth)
                barDistance = barWidth + 1;
            int barShort = (int)size;
            if (barShort <= 0)
                barShort = 1;
            int barTall = (int)barHeight;
            if (barTall <= barShort)
                barTall = barShort + 1;
            byte[] bars = GetBarsPostnet(code);
            int width = bars.Length * barDistance;
            byte flip = 1;
            if (codeType == PLANET) {
                flip = 0;
                bars[0] = 0;
                bars[bars.Length - 1] = 0;
            }
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, barTall);
            int seg1 = barTall - barShort;
            for (int i = 0; i < seg1; ++i) {
                int idx = 0;
                for (int k = 0; k < bars.Length; ++k) {
                    bool dot = (bars[k] == flip);
                    for (int j = 0; j < barDistance; ++j) {
                        bmp.SetPixel(idx++, i, (dot && j < barWidth) ? foreground : background);
                    }
                }
            }
            for (int i = seg1; i < barTall; ++i) {
                int idx = 0;
                for (int k = 0; k < bars.Length; ++k) {
                    for (int j = 0; j < barDistance; ++j) {
                        bmp.SetPixel(idx++, i, (j < barWidth) ? foreground : background);
                    }
                }
            }
            return bmp;
        }
#endif// DRAWING
    }
}
