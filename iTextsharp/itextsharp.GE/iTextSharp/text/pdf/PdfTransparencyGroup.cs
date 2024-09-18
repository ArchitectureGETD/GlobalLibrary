using System;

namespace iTextSharp.GE.text.pdf {
    /** The transparency group dictionary.
    *
    * @author Paulo Soares
    */
    public class PdfTransparencyGroup : PdfDictionary {
        
        /**
        * Constructs a transparencyGroup.
        */
        public PdfTransparencyGroup() {
            Put(PdfName.S, PdfName.TRANSPARENCY);
        }
     
        /**
        * Determining the initial backdrop against which its stack is composited.
        * @param isolated
        */
        virtual public bool Isolated {
            set {
                if (value)
                    Put(PdfName.I, PdfBoolean.PDFTRUE);
                else
                    Remove(PdfName.I);
            }
        }
        
        /**
        * Determining whether the objects within the stack are composited with one another or only with the group's backdrop.
        * @param knockout
        */
        virtual public bool Knockout {
            set {
                if (value)
                    Put(PdfName.K, PdfBoolean.PDFTRUE);
                else
                    Remove(PdfName.K);
            }
        }

    }
}
