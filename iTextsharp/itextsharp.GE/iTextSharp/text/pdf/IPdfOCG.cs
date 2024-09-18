using System;

namespace iTextSharp.GE.text.pdf {
    /**
    * The interface common to all layer types.
    *
    * @author Paulo Soares
    */
    public interface IPdfOCG {

        /**
        * Gets the <CODE>PdfIndirectReference</CODE> that represents this layer.
        * @return the <CODE>PdfIndirectReference</CODE> that represents this layer
        */    
        PdfIndirectReference Ref {
            get;
        }
        
        /**
        * Gets the object representing the layer.
        * @return the object representing the layer
        */    
        PdfObject PdfObject {
            get;
        }
    }
}
