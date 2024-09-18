using System;

namespace iTextSharp.GE.text.pdf {

    public class PdfTransition {
        /**
         *  Out Vertical Split
         */
        public const int SPLITVOUT      = 1;
        /**
         *  Out Horizontal Split
         */
        public const int SPLITHOUT      = 2;
        /**
         *  In Vertical Split
         */
        public const int SPLITVIN      = 3;
        /**
         *  IN Horizontal Split
         */
        public const int SPLITHIN      = 4;
        /**
         *  Vertical Blinds
         */
        public const int BLINDV      = 5;
        /**
         *  Vertical Blinds
         */
        public const int BLINDH      = 6;
        /**
         *  Inward Box
         */
        public const int INBOX       = 7;
        /**
         *  Outward Box
         */
        public const int OUTBOX      = 8;
        /**
         *  Left-Right Wipe
         */
        public const int LRWIPE      = 9;
        /**
         *  Right-Left Wipe
         */
        public const int RLWIPE     = 10;
        /**
         *  Bottom-Top Wipe
         */
        public const int BTWIPE     = 11;
        /**
         *  Top-Bottom Wipe
         */
        public const int TBWIPE     = 12;
        /**
         *  Dissolve
         */
        public const int DISSOLVE    = 13;
        /**
         *  Left-Right Glitter
         */
        public const int LRGLITTER   = 14;
        /**
         *  Top-Bottom Glitter
         */
        public const int TBGLITTER  = 15;
        /**
         *  Diagonal Glitter
         */
        public const int DGLITTER  = 16;
    
        /**
         *  duration of the transition effect
         */
        protected int duration;
        /**
         *  type of the transition effect
         */
        protected int type;
    
        /**
         *  Constructs a <CODE>Transition</CODE>.
         *
         */
        public PdfTransition() : this(BLINDH) {}
    
        /**
         *  Constructs a <CODE>Transition</CODE>.
         *
         *@param  type      type of the transition effect
         */
        public PdfTransition(int type) : this(type,1) {}
    
        /**
         *  Constructs a <CODE>Transition</CODE>.
         *
         *@param  type      type of the transition effect
         *@param  duration  duration of the transition effect
         */
        public PdfTransition(int type, int duration) {
            this.duration = duration;
            this.type = type;
        }
    
    
        virtual public int Duration {
            get {
                return duration;
            }
        }
    
    
        virtual public int Type {
            get {
                return type;
            }
        }

        virtual public PdfDictionary TransitionDictionary {
            get {
                PdfDictionary trans = new PdfDictionary(PdfName.TRANS);
                switch (type) {
                    case SPLITVOUT:
                        trans.Put(PdfName.S,PdfName.SPLIT);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DM,PdfName.V);
                        trans.Put(PdfName.M,PdfName.O);
                        break;
                    case SPLITHOUT:
                        trans.Put(PdfName.S,PdfName.SPLIT);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DM,PdfName.H);
                        trans.Put(PdfName.M,PdfName.O);
                        break;
                    case SPLITVIN:
                        trans.Put(PdfName.S,PdfName.SPLIT);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DM,PdfName.V);
                        trans.Put(PdfName.M,PdfName.I);
                        break;
                    case SPLITHIN:
                        trans.Put(PdfName.S,PdfName.SPLIT);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DM,PdfName.H);
                        trans.Put(PdfName.M,PdfName.I);
                        break;
                    case BLINDV:
                        trans.Put(PdfName.S,PdfName.BLINDS);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DM,PdfName.V);
                        break;
                    case BLINDH:
                        trans.Put(PdfName.S,PdfName.BLINDS);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DM,PdfName.H);
                        break;
                    case INBOX:
                        trans.Put(PdfName.S,PdfName.BOX);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.M,PdfName.I);
                        break;
                    case OUTBOX:
                        trans.Put(PdfName.S,PdfName.BOX);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.M,PdfName.O);
                        break;
                    case LRWIPE:
                        trans.Put(PdfName.S,PdfName.WIPE);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DI,new PdfNumber(0));
                        break;
                    case RLWIPE:
                        trans.Put(PdfName.S,PdfName.WIPE);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DI,new PdfNumber(180));
                        break;
                    case BTWIPE:
                        trans.Put(PdfName.S,PdfName.WIPE);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DI,new PdfNumber(90));
                        break;
                    case TBWIPE:
                        trans.Put(PdfName.S,PdfName.WIPE);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DI,new PdfNumber(270));
                        break;
                    case DISSOLVE:
                        trans.Put(PdfName.S,PdfName.DISSOLVE);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        break;
                    case LRGLITTER:
                        trans.Put(PdfName.S,PdfName.GLITTER);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DI,new PdfNumber(0));
                        break;
                    case TBGLITTER:
                        trans.Put(PdfName.S,PdfName.GLITTER);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DI,new PdfNumber(270));
                        break;
                    case DGLITTER:
                        trans.Put(PdfName.S,PdfName.GLITTER);
                        trans.Put(PdfName.D,new PdfNumber(duration));
                        trans.Put(PdfName.DI,new PdfNumber(315));
                        break;
                }
                return trans;
            }
        }
    }
}
