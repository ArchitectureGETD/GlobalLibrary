using System;

namespace iTextSharp.GE.text.pdf {

    /**
     *
     * @author  Paulo Soares
     */
    public class GrayColor : ExtendedColor {

        private float cgray;

        public static readonly GrayColor GRAYBLACK = new GrayColor(0f);
        public static readonly GrayColor GRAYWHITE = new GrayColor(1f);

        public GrayColor(int intGray) : this((float)intGray / 255f) {}

        public GrayColor(float floatGray) : base(TYPE_GRAY, floatGray, floatGray, floatGray) {
            cgray = Normalize(floatGray);
        }
    
        virtual public float Gray {
            get {
                return cgray;
            }
        }

        public override bool Equals(Object obj) {
            return (obj is GrayColor) && ((GrayColor)obj).cgray == this.cgray;
        }
    
        public override int GetHashCode() {
            return cgray.GetHashCode();
        }
    }
}
