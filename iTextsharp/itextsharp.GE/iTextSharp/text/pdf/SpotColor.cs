using System;

namespace iTextSharp.GE.text.pdf {

    /**
     *
     * @author  psoares
     */
    public class SpotColor : ExtendedColor {

        PdfSpotColor spot;
        float tint;

        public SpotColor(PdfSpotColor spot, float tint) : 
            base(TYPE_SEPARATION,
                ((float)spot.AlternativeCS.R / 255f - 1f) * tint + 1,
                ((float)spot.AlternativeCS.G / 255f - 1f) * tint + 1,
                ((float)spot.AlternativeCS.B / 255f - 1f) * tint + 1) {
            this.spot = spot;
            this.tint = tint;
        }
    
        virtual public PdfSpotColor PdfSpotColor {
            get {
                return spot;
            }
        }
    
        virtual public float Tint {
            get {
                return tint;
            }
        }

        public override bool Equals(Object obj) {
            return obj is SpotColor && (((SpotColor)obj).spot).Equals(this.spot) && ((SpotColor)obj).tint == this.tint;
        }
        
        public override int GetHashCode() {
            return spot.GetHashCode() ^ tint.GetHashCode();
        }
    }
}
