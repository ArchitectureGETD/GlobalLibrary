using System;

using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf {

    /**
     *
     * @author  Paulo Soares
     */
    public abstract class ExtendedColor : BaseColor {
    
        public const int TYPE_RGB = 0;
        public const int TYPE_GRAY = 1;
        public const int TYPE_CMYK = 2;
        public const int TYPE_SEPARATION = 3;
        public const int TYPE_PATTERN = 4;
        public const int TYPE_SHADING = 5;
        public const int TYPE_DEVICEN = 6;
        public const int TYPE_LAB = 7;
    
        protected int type;

        public ExtendedColor(int type) : base(0, 0, 0) {
            this.type = type;
        }
    
        public ExtendedColor(int type, float red, float green, float blue) : base(Normalize(red), Normalize(green), Normalize(blue)) {
            this.type = type;
        }
    
        /**
        * Constructs an extended color of a certain type and a certain color.
        * @param type
        * @param red
        * @param green
        * @param blue
        * @param alpha
        */
        public ExtendedColor(int type, int red, int green, int blue, int alpha) : base(Normalize((float)red / 0xFF), Normalize((float)green / 0xFF), Normalize((float)blue / 0xFF), Normalize((float)alpha / 0xFF)) {
		    this.type = type;
	    }

        virtual public int Type {
            get {
                return type;
            }
        }
    
        public static int GetType(object color) {
            if (color is ExtendedColor)
                return ((ExtendedColor)color).Type;
            return TYPE_RGB;
        }

        internal static float Normalize(float value) {
            if (value < 0)
                return 0;
            if (value > 1)
                return 1;
            return (float)value;
        }
    }
}
