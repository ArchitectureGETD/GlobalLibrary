namespace iTextSharp.GE.text.pdf.codec.wmf {
    public class MetaPen : MetaObject {

        public const int PS_SOLID = 0;
        public const int PS_DASH = 1;
        public const int PS_DOT = 2;
        public const int PS_DASHDOT = 3;
        public const int PS_DASHDOTDOT = 4;
        public const int PS_NULL = 5;
        public const int PS_INSIDEFRAME = 6;

        int style = PS_SOLID;
        int penWidth = 1;
        BaseColor color = BaseColor.BLACK;

        public MetaPen() {
            type = META_PEN;
        }

        virtual public void Init(InputMeta meta) {
            style = meta.ReadWord();
            penWidth = meta.ReadShort();
            meta.ReadWord();
            color = meta.ReadColor();
        }
    
        virtual public int Style {
            get {
                return style;
            }
        }
    
        virtual public int PenWidth {
            get {
                return penWidth;
            }
        }
    
        virtual public BaseColor Color {
            get {
                return color;
            }
        }
    }
}
