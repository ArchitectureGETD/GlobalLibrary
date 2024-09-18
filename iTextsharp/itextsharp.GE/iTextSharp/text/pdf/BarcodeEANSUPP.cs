using System;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf {
    /** This class takes 2 barcodes, an EAN/UPC and a supplemental
     * and creates a single barcode with both combined in the
     * expected layout. The UPC/EAN should have a positive text
      * baseline and the supplemental a negative one (in the supplemental
     * the text is on the top of the barcode.<p>
     * The default parameters are:
     * <pre>
     *n = 8; // horizontal distance between the two barcodes
     * </pre>
     *
     * @author Paulo Soares
     */
    public class BarcodeEANSUPP : Barcode {
    
        /** The barcode with the EAN/UPC.
         */    
        protected Barcode ean;
        /** The barcode with the supplemental.
         */    
        protected Barcode supp;
    
        /** Creates new combined barcode.
         * @param ean the EAN/UPC barcode
         * @param supp the supplemental barcode
         */
        public BarcodeEANSUPP(Barcode ean, Barcode supp) {
            n = 8; // horizontal distance between the two barcodes
            this.ean = ean;
            this.supp = supp;
        }
    
        /** Gets the maximum area that the barcode and the text, if
         * any, will occupy. The lower left corner is always (0, 0).
         * @return the size the barcode occupies.
         */
        public override Rectangle BarcodeSize {
            get {
                Rectangle rect = ean.BarcodeSize;
                rect.Right = rect.Width + supp.BarcodeSize.Width + n;
                return rect;
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
            if (supp.Font != null)
                supp.BarHeight = ean.BarHeight + supp.Baseline - supp.Font.GetFontDescriptor(BaseFont.CAPHEIGHT, supp.Size);
            else
                supp.BarHeight = ean.BarHeight;
            Rectangle eanR = ean.BarcodeSize;
            cb.SaveState();
            ean.PlaceBarcode(cb, barColor, textColor);
            cb.RestoreState();
            cb.SaveState();
            cb.ConcatCTM(1, 0, 0, 1, eanR.Width + n, eanR.Height - ean.BarHeight);
            supp.PlaceBarcode(cb, barColor, textColor);
            cb.RestoreState();
            return this.BarcodeSize;
        }

#if DRAWING
        public override System.Drawing.Image CreateDrawingImage(System.Drawing.Color foreground, System.Drawing.Color background) {
            throw new InvalidOperationException(MessageLocalization.GetComposedMessage("the.two.barcodes.must.be.composed.externally"));
        }
#endif// DRAWING
    }
}
