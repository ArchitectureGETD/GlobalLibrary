using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * A RichMediaAnimation dictionary specifies the preferred method that
     * conforming readers should use to apply timeline scaling to keyframe
     * animations. It can also specify that keyframe animations be played
     * repeatedly.
     * See ExtensionLevel 3 p80
     * @see     RichMediaActivation
     * @since   5.0.0
     */
    public class RichMediaAnimation : PdfDictionary {
        
        /**
         * Constructs a RichMediaAnimation. Also sets the animation style
         * described by this dictionary. Valid values are None, Linear, and
         * Oscillating.
         * @param   subtype possible values are
         *      PdfName.NONE, PdfName.LINEAR, PdfName.OSCILLATING
         */
        public RichMediaAnimation(PdfName subtype) : base (PdfName.RICHMEDIAANIMATION) {
            Put(PdfName.SUBTYPE, subtype);
        }
        
        /**
         * Sets the number of times the animation is played.
         * @param   playCount   the play count
         */
        virtual public int PlayCount {
            set {
                Put(PdfName.PLAYCOUNT, new PdfNumber(value));
            }
        }
        
        /**
         * Sets the speed to be used when running the animation.
         * A value greater than one shortens the time it takes to play
         * the animation, or effectively speeds up the animation.
         * @param   speed   a speed value
         */
        virtual public float Speed {
            set {
                Put(PdfName.SPEED, new PdfNumber(value));
            }
        }
    }
}
