using System;

namespace iTextSharp.GE.text.pdf.interfaces {

    public interface IPdfRunDirection {
        /** Sets the run direction. This is only used as a placeholder
        * as it does not affect anything.
        * @param runDirection the run direction
        */    
        int RunDirection {
            set;
            get;
        }
    }
}
