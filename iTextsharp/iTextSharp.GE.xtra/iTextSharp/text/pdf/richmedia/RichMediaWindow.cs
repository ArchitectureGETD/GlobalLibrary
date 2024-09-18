using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * The RichMediaWindow dictionary stores the dimensions and position of the
     * floating window presented to the user. It is used only if Style is set
     * to Windowed.
     * See ExtensionLevel 3 p84
     * @see RichMediaPresentation
     * @since   5.0.0
     */
    public class RichMediaWindow : PdfDictionary {

        /**
         * Creates a RichMediaWindow dictionary.
         */
        public RichMediaWindow() : base(PdfName.RICHMEDIAWINDOW) {
        }
        
        /**
         * Sets a dictionary with keys Default, Max, and Min describing values for
         * the width of the Window in default user space units.
         * @param   defaultWidth    the default width
         * @param   maxWidth        the maximum width
         * @param   minWidth        the minimum width
         */
        virtual public void SetWidth(float defaultWidth, float maxWidth, float minWidth) {
            Put(PdfName.WIDTH, CreateDimensionDictionary(defaultWidth, maxWidth, minWidth));
        }

        /**
         * Sets a dictionary with keys Default, Max, and Min describing values for
         * the height of the Window in default user space units.
         * @param   defaultHeight   the default height
         * @param   maxHeight       the maximum height
         * @param   minHeight       the minimum height
         */
        virtual public void SetHeight(float defaultHeight, float maxHeight, float minHeight) {
            Put(PdfName.HEIGHT, CreateDimensionDictionary(defaultHeight, maxHeight, minHeight));
        }
        
        /**
         * Creates a dictionary that can be used for the HEIGHT and WIDTH entries
         * of the RichMediaWindow dictionary.
         * @param   d       the default
         * @param   max     the maximum
         * @param   min     the minimum
         */
        private PdfDictionary CreateDimensionDictionary(float d, float max, float min) {
            PdfDictionary dict = new PdfDictionary();
            dict.Put(PdfName.DEFAULT, new PdfNumber(d));
            dict.Put(PdfName.MAX_CAMEL_CASE, new PdfNumber(max));
            dict.Put(PdfName.MIN_CAMEL_CASE, new PdfNumber(min));
            return dict;
        }
        
        /**
         * Sets a RichMediaPosition dictionary describing the position of the RichMediaWindow.
         * @param   position    a RichMediaPosition object
         */
        virtual public RichMediaPosition Position {
            set {
                Put(PdfName.POSITION, value);
            }
        }
    }
}
