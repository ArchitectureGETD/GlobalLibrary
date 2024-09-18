using System;
using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf.codec.wmf {
    public class MetaBrush : MetaObject {

        public const int BS_SOLID = 0;
        public const int BS_NULL = 1;
        public const int BS_HATCHED = 2;
        public const int BS_PATTERN = 3;
        public const int BS_DIBPATTERN = 5;
        public const int HS_HORIZONTAL = 0;
        public const int HS_VERTICAL = 1;
        public const int HS_FDIAGONAL = 2;
        public const int HS_BDIAGONAL = 3;
        public const int HS_CROSS = 4;
        public const int HS_DIAGCROSS = 5;

        int style = BS_SOLID;
        int hatch;
        BaseColor color = BaseColor.WHITE;

        public MetaBrush() {
            type = META_BRUSH;
        }

        virtual public void Init(InputMeta meta) {
            style = meta.ReadWord();
            color = meta.ReadColor();
            hatch = meta.ReadWord();
        }
    
        virtual public int Style {
            get {
                return style;
            }
        }
    
        virtual public int Hatch {
            get {
                return hatch;
            }
        }
    
        virtual public BaseColor Color {
            get {
                return color;
            }
        }
    }
}
