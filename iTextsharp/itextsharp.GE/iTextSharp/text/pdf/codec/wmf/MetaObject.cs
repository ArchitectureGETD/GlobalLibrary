using System;

namespace iTextSharp.GE.text.pdf.codec.wmf
{
    public class MetaObject
    {
        public const int META_NOT_SUPPORTED = 0;
        public const int META_PEN = 1;
        public const int META_BRUSH = 2;
        public const int META_FONT = 3;
        public int type = META_NOT_SUPPORTED;

        public MetaObject() {
        }

        public MetaObject(int type) {
            this.type = type;
        }
    
        virtual public int Type {
            get {
                return type;
            }
        }
    }
}
