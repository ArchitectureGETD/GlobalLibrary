using System;

namespace iTextSharp.GE.text.pdf {

    /**
     * A <CODE>PdfColor</CODE> defines a Color (it's a <CODE>PdfArray</CODE> containing 3 values).
     *
     * @see        PdfDictionary
     */

    public class PdfDestination : PdfArray {
    
        // public static member-variables
    
        /** This is a possible destination type */
        public const int XYZ = 0;
    
        /** This is a possible destination type */
        public const int FIT = 1;
    
        /** This is a possible destination type */
        public const int FITH = 2;
    
        /** This is a possible destination type */
        public const int FITV = 3;
    
        /** This is a possible destination type */
        public const int FITR = 4;
    
        /** This is a possible destination type */
        public const int FITB = 5;
    
        /** This is a possible destination type */
        public const int FITBH = 6;
    
        /** This is a possible destination type */
        public const int FITBV = 7;
    
        // member variables
    
        /** Is the indirect reference to a page already added? */
        private bool status = false;
    
        // constructors
    
        /**
         * Constructs a new <CODE>PdfDestination</CODE>.
         * <P>
         * If <VAR>type</VAR> equals <VAR>FITB</VAR>, the bounding box of a page
         * will fit the window of the Reader. Otherwise the type will be set to
         * <VAR>FIT</VAR> so that the entire page will fit to the window.
         *
         * @param        type        The destination type
         */
    
        public PdfDestination(int type) : base() {
            if (type == FITB) {
                Add(PdfName.FITB);
            }
            else {
                Add(PdfName.FIT);
            }
        }
    
        /**
         * Constructs a new <CODE>PdfDestination</CODE>.
         * <P>
         * If <VAR>type</VAR> equals <VAR>FITBH</VAR> / <VAR>FITBV</VAR>,
         * the width / height of the bounding box of a page will fit the window
         * of the Reader. The parameter will specify the y / x coordinate of the
         * top / left edge of the window. If the <VAR>type</VAR> equals <VAR>FITH</VAR>
         * or <VAR>FITV</VAR> the width / height of the entire page will fit
         * the window and the parameter will specify the y / x coordinate of the
         * top / left edge. In all other cases the type will be set to <VAR>FITH</VAR>.
         *
         * @param        type        the destination type
         * @param        parameter    a parameter to combined with the destination type
         */
    
        public PdfDestination(int type, float parameter) : base(new PdfNumber(parameter)) {
            switch (type) {
                default:
                    AddFirst(PdfName.FITH);
                    break;
                case FITV:
                    AddFirst(PdfName.FITV);
                    break;
                case FITBH:
                    AddFirst(PdfName.FITBH);
                    break;
                case FITBV:
                    AddFirst(PdfName.FITBV);
                    break;
            }
        }
    
        /** Constructs a new <CODE>PdfDestination</CODE>.
         * <P>
         * Display the page, with the coordinates (left, top) positioned
         * at the top-left corner of the window and the contents of the page magnified
         * by the factor zoom. A negative value for any of the parameters left or top, or a
         * zoom value of 0 specifies that the current value of that parameter is to be retained unchanged.
         * @param type must be a <VAR>PdfDestination.XYZ</VAR>
         * @param left the left value. Negative to place a null
         * @param top the top value. Negative to place a null
         * @param zoom The zoom factor. A value of 0 keeps the current value
         */
    
        public PdfDestination(int type, float left, float top, float zoom) : base(PdfName.XYZ) {
            if (left < 0)
                Add(PdfNull.PDFNULL);
            else
                Add(new PdfNumber(left));
            if (top < 0)
                Add(PdfNull.PDFNULL);
            else
                Add(new PdfNumber(top));
            Add(new PdfNumber(zoom));
        }
    
        /** Constructs a new <CODE>PdfDestination</CODE>.
         * <P>
         * Display the page, with its contents magnified just enough
         * to fit the rectangle specified by the coordinates left, bottom, right, and top
         * entirely within the window both horizontally and vertically. If the required
         * horizontal and vertical magnification factors are different, use the smaller of
         * the two, centering the rectangle within the window in the other dimension.
         *
         * @param type must be PdfDestination.FITR
         * @param left a parameter
         * @param bottom a parameter
         * @param right a parameter
         * @param top a parameter
         * @since iText0.38
         */
    
        public PdfDestination(int type, float left, float bottom, float right, float top) : base(PdfName.FITR) {
            Add(new PdfNumber(left));
            Add(new PdfNumber(bottom));
            Add(new PdfNumber(right));
            Add(new PdfNumber(top));
        }

        public PdfDestination(PdfDestination d):base(d) {
            status = d.status;
        }

    
        /**
        * Creates a PdfDestination based on a String.
        * Valid Strings are for instance the values returned by SimpleNamedDestination:
        * "Fit", "XYZ 36 806 0",...
        * @param    dest    a String notation of a destination.
        * @since    iText 5.0
        */
        public PdfDestination(String dest) : base() {
            string[] ss = dest.Trim().Split(null);
            if (ss.Length > 0)
                Add(new PdfName(ss[0]));
            for (int k = 1; k < ss.Length; ++k) {
                if (ss[k].Length == 0)
                    continue;
                if ("null".Equals(ss[k]))
                    Add(new PdfNull());
                else {
                    try {
                        Add(new PdfNumber(ss[k]));
                    } catch (Exception) {
                        Add(new PdfNull());
                    }
                }
            }
        }

        // methods
    
        /**
         * Checks if an indirect reference to a page has been added.
         *
         * @return    <CODE>true</CODE> or <CODE>false</CODE>
         */
    
        virtual public bool HasPage() {
            return status;
        }
    
        /** Adds the indirect reference of the destination page.
         *
         * @param page    an indirect reference
         * @return true if the page reference was added
         */
    
        virtual public bool AddPage(PdfIndirectReference page) {
            if (!status) {
                AddFirst(page);
                status = true;
                return true;
            }
            return false;
        }
    }
}
