using System;
using System.Collections;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf.interfaces;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf.intern {

    public class PdfXConformanceImp : IPdfXConformance {

        /**
        * The value indicating if the PDF has to be in conformance with PDF/X.
        */
        protected internal int pdfxConformance = PdfWriter.PDFXNONE;

        protected PdfWriter writer;

        public PdfXConformanceImp(PdfWriter writer) {
            this.writer = writer;
        }

        
        /**
        * @see com.lowagie.text.pdf.interfaces.PdfXConformance#setPDFXConformance(int)
        */
        virtual public int PDFXConformance {
            set {
                this.pdfxConformance = value;
            }
            get {
                return pdfxConformance;
            }
        }

        /**
	     * @see com.itextpdf.text.pdf.interfaces.PdfIsoConformance#isPdfIso()
	     */
        virtual public bool IsPdfIso()
        {
            return IsPdfX();
        }

        /**
        * Checks if the PDF/X Conformance is necessary.
        * @return true if the PDF has to be in conformance with any of the PDF/X specifications
        */
        virtual public bool IsPdfX() {
            return pdfxConformance != PdfWriter.PDFXNONE;
        }
        /**
        * Checks if the PDF has to be in conformance with PDF/X-1a:2001
        * @return true of the PDF has to be in conformance with PDF/X-1a:2001
        */
        virtual public bool IsPdfX1A2001() {
            return pdfxConformance == PdfWriter.PDFX1A2001;
        }
        /**
        * Checks if the PDF has to be in conformance with PDF/X-3:2002
        * @return true of the PDF has to be in conformance with PDF/X-3:2002
        */
        virtual public bool IsPdfX32002() {
            return pdfxConformance == PdfWriter.PDFX32002;
        }
        
        /**
        * Business logic that checks if a certain object is in conformance with PDF/X.
        * @param writer    the writer that is supposed to write the PDF/X file
        * @param key       the type of PDF ISO conformance that has to be checked
        * @param obj1      the object that is checked for conformance
        */
        virtual public void CheckPdfIsoConformance(int key, Object obj1) {
            if (writer == null || !writer.IsPdfX())
                return;
            int conf = writer.PDFXConformance;
            switch (key) {
                case PdfIsoKeys.PDFISOKEY_COLOR:
                    switch (conf) {
                        case PdfWriter.PDFX1A2001:
                            if (obj1 is ExtendedColor) {
                                ExtendedColor ec = (ExtendedColor)obj1;
                                switch (ec.Type) {
                                    case ExtendedColor.TYPE_CMYK:
                                    case ExtendedColor.TYPE_GRAY:
                                        return;
                                    case ExtendedColor.TYPE_RGB:
                                        throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.rgb.is.not.allowed"));
                                    case ExtendedColor.TYPE_SEPARATION:
                                        SpotColor sc = (SpotColor)ec;
                                        CheckPdfIsoConformance(PdfIsoKeys.PDFISOKEY_COLOR, sc.PdfSpotColor.AlternativeCS);
                                        break;
                                    case ExtendedColor.TYPE_SHADING:
                                        ShadingColor xc = (ShadingColor)ec;
                                        CheckPdfIsoConformance(PdfIsoKeys.PDFISOKEY_COLOR, xc.PdfShadingPattern.Shading.ColorSpace);
                                        break;
                                    case ExtendedColor.TYPE_PATTERN:
                                        PatternColor pc = (PatternColor)ec;
                                        CheckPdfIsoConformance(PdfIsoKeys.PDFISOKEY_COLOR, pc.Painter.DefaultColor);
                                        break;
                                }
                            }
                            else if (obj1 is BaseColor)
                                throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.rgb.is.not.allowed"));
                            break;
                    }
                    break;
                case PdfIsoKeys.PDFISOKEY_CMYK:
                    break;
                case PdfIsoKeys.PDFISOKEY_RGB:
                    if (conf == PdfWriter.PDFX1A2001)
                        throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.rgb.is.not.allowed"));
                    break;
                case PdfIsoKeys.PDFISOKEY_FONT:
                    if (!((BaseFont)obj1).IsEmbedded())
                        throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("all.the.fonts.must.be.embedded.this.one.isn.t.1", ((BaseFont)obj1).PostscriptFontName));
                    break;
                case PdfIsoKeys.PDFISOKEY_IMAGE:
                    PdfImage image = (PdfImage)obj1;
                    if (image.Get(PdfName.SMASK) != null)
                        throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("the.smask.key.is.not.allowed.in.images"));
                    switch (conf) {
                        case PdfWriter.PDFX1A2001:
                            PdfObject cs = image.Get(PdfName.COLORSPACE);
                            if (cs == null)
                                return;
                            if (cs.IsName()) {
                                if (PdfName.DEVICERGB.Equals(cs))
                                    throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.rgb.is.not.allowed"));
                            }
                            else if (cs.IsArray()) {
                                if (PdfName.CALRGB.Equals(((PdfArray)cs)[0]))
                                    throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.calrgb.is.not.allowed"));
                            }
                            break;
                    }
                    break;
                case PdfIsoKeys.PDFISOKEY_GSTATE:
                    PdfDictionary gs = (PdfDictionary)obj1;
                    // The example PdfXPdfA threw a NullPointerException because gs was null
                    if (gs == null)
                        break;
                    PdfObject obj = gs.Get(PdfName.BM);
                    if (obj != null && !PdfGState.BM_NORMAL.Equals(obj) && !PdfGState.BM_COMPATIBLE.Equals(obj))
                        throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("blend.mode.1.not.allowed", obj.ToString()));
                    obj = gs.Get(PdfName.CA);
                    double v = 0.0;
                    if (obj != null && (v = ((PdfNumber)obj).DoubleValue) != 1.0)
                        throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("transparency.is.not.allowed.ca.eq.1", v));
                    obj = gs.Get(PdfName.ca);
                    v = 0.0;
                    if (obj != null && (v = ((PdfNumber)obj).DoubleValue) != 1.0)
                        throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("transparency.is.not.allowed.ca.eq.1", v));
                    break;
                case PdfIsoKeys.PDFISOKEY_LAYER:
                    throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("layers.are.not.allowed"));
            }
        }
    }
}
