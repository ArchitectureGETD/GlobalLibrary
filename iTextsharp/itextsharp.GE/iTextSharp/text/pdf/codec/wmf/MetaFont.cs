using System;

namespace iTextSharp.GE.text.pdf.codec.wmf {
    public class MetaFont : MetaObject {
        static string[] fontNames = {
                                        "Courier", "Courier-Bold", "Courier-Oblique", "Courier-BoldOblique",
                                        "Helvetica", "Helvetica-Bold", "Helvetica-Oblique", "Helvetica-BoldOblique",
                                        "Times-Roman", "Times-Bold", "Times-Italic", "Times-BoldItalic",
                                        "Symbol", "ZapfDingbats"};

        internal const int MARKER_BOLD = 1;
        internal const int MARKER_ITALIC = 2;
        internal const int MARKER_COURIER = 0;
        internal const int MARKER_HELVETICA = 4;
        internal const int MARKER_TIMES = 8;
        internal const int MARKER_SYMBOL = 12;

        internal const int DEFAULT_PITCH = 0;
        internal const int FIXED_PITCH = 1;
        internal const int VARIABLE_PITCH = 2;
        internal const int FF_DONTCARE = 0;
        internal const int FF_ROMAN = 1;
        internal const int FF_SWISS = 2;
        internal const int FF_MODERN = 3;
        internal const int FF_SCRIPT = 4;
        internal const int FF_DECORATIVE = 5;
        internal const int BOLDTHRESHOLD = 600;    
        internal const int nameSize = 32;
        internal const int ETO_OPAQUE = 2;
        internal const int ETO_CLIPPED = 4;

        int height;
        float angle;
        int bold;
        int italic;
        bool underline;
        bool strikeout;
        int charset;
        int pitchAndFamily;
        string faceName = "arial";
        BaseFont font = null;

        public MetaFont() {
            type = META_FONT;
        }

        virtual public void Init(InputMeta meta) {
            height = Math.Abs(meta.ReadShort());
            meta.Skip(2);
            angle = (float)(meta.ReadShort() / 1800.0 * Math.PI);
            meta.Skip(2);
            bold = (meta.ReadShort() >= BOLDTHRESHOLD ? MARKER_BOLD : 0);
            italic = (meta.ReadByte() != 0 ? MARKER_ITALIC : 0);
            underline = (meta.ReadByte() != 0);
            strikeout = (meta.ReadByte() != 0);
            charset = meta.ReadByte();
            meta.Skip(3);
            pitchAndFamily = meta.ReadByte();
            byte[] name = new byte[nameSize];
            int k;
            for (k = 0; k < nameSize; ++k) {
                int c = meta.ReadByte();
                if (c == 0) {
                    break;
                }
                name[k] = (byte)c;
            }
            try {
                faceName = System.Text.Encoding.GetEncoding(1252).GetString(name, 0, k);
            }
            catch {
                faceName = System.Text.ASCIIEncoding.ASCII.GetString(name, 0, k);
            }
            faceName = faceName.ToLower(System.Globalization.CultureInfo.InvariantCulture);
        }
    
        virtual public BaseFont Font {
            get {
                if (font != null)
                    return font;
                iTextSharp.GE.text.Font ff2 = FontFactory.GetFont(faceName, BaseFont.CP1252, true, 10, ((italic != 0) ? iTextSharp.GE.text.Font.ITALIC : 0) | ((bold != 0) ? iTextSharp.GE.text.Font.BOLD : 0));
                font = ff2.BaseFont;
                if (font != null)
                    return font;
                string fontName;
                if (faceName.IndexOf("courier") != -1 || faceName.IndexOf("terminal") != -1
                    || faceName.IndexOf("fixedsys") != -1) {
                    fontName = fontNames[MARKER_COURIER + italic + bold];
                }
                else if (faceName.IndexOf("ms sans serif") != -1 || faceName.IndexOf("arial") != -1
                    || faceName.IndexOf("system") != -1) {
                    fontName = fontNames[MARKER_HELVETICA + italic + bold];
                }
                else if (faceName.IndexOf("arial black") != -1) {
                    fontName = fontNames[MARKER_HELVETICA + italic + MARKER_BOLD];
                }
                else if (faceName.IndexOf("times") != -1 || faceName.IndexOf("ms serif") != -1
                    || faceName.IndexOf("roman") != -1) {
                    fontName = fontNames[MARKER_TIMES + italic + bold];
                }
                else if (faceName.IndexOf("symbol") != -1) {
                    fontName = fontNames[MARKER_SYMBOL];
                }
                else {
                    int pitch = pitchAndFamily & 3;
                    int family = (pitchAndFamily >> 4) & 7;
                    switch (family) {
                        case FF_MODERN:
                            fontName = fontNames[MARKER_COURIER + italic + bold];
                            break;
                        case FF_ROMAN:
                            fontName = fontNames[MARKER_TIMES + italic + bold];
                            break;
                        case FF_SWISS:
                        case FF_SCRIPT:
                        case FF_DECORATIVE:
                            fontName = fontNames[MARKER_HELVETICA + italic + bold];
                            break;
                        default: {
                            switch (pitch) {
                                case FIXED_PITCH:
                                    fontName = fontNames[MARKER_COURIER + italic + bold];
                                    break;
                                default:
                                    fontName = fontNames[MARKER_HELVETICA + italic + bold];
                                    break;
                            }
                            break;
                        }
                    }
                }
                font = BaseFont.CreateFont(fontName, BaseFont.CP1252, false);
        
                return font;
            }
        }
    
        virtual public float Angle {
            get {
                return angle;
            }
        }
    
        virtual public bool IsUnderline() {
            return underline;
        }
    
        virtual public bool IsStrikeout() {
            return strikeout;
        }
    
        virtual public float GetFontSize(MetaState state) {
            return Math.Abs(state.TransformY(height) - state.TransformY(0)) * Document.WmfFontCorrection;
        }
    }
}
