using System;

namespace iTextSharp.GE.text.pdf {

    /**
     *
     * @author  Paulo Soares
     */
    public class CMYKColor : ExtendedColor {

        float ccyan;
        float cmagenta;
        float cyellow;
        float cblack;

        public CMYKColor(int intCyan, int intMagenta, int intYellow, int intBlack) :
            this((float)intCyan / 255f, (float)intMagenta / 255f, (float)intYellow / 255f, (float)intBlack / 255f) {}

        public CMYKColor(float floatCyan, float floatMagenta, float floatYellow, float floatBlack) :
            base(TYPE_CMYK, 1f - floatCyan - floatBlack, 1f - floatMagenta - floatBlack, 1f - floatYellow - floatBlack) {
            ccyan = Normalize(floatCyan);
            cmagenta = Normalize(floatMagenta);
            cyellow = Normalize(floatYellow);
            cblack = Normalize(floatBlack);
        }
    
        virtual public float Cyan {
            get {
                return ccyan;
            }
        }

        virtual public float Magenta {
            get {
                return cmagenta;
            }
        }

        virtual public float Yellow {
            get {
                return cyellow;
            }
        }

        virtual public float Black {
            get {
                return cblack;
            }
        }

        public override bool Equals(Object obj) {
            if (!(obj is CMYKColor))
                return false;
            CMYKColor c2 = (CMYKColor)obj;
            return (ccyan == c2.ccyan && cmagenta == c2.cmagenta && cyellow == c2.cyellow && cblack == c2.cblack);
        }
    
        public override int GetHashCode() {
            return ccyan.GetHashCode() ^ cmagenta.GetHashCode() ^ cyellow.GetHashCode() ^ cblack.GetHashCode();
        }
    }
}
