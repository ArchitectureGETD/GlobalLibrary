using System;

namespace iTextSharp.GE.text {
    /**
    * These two methods are used by FactoryProperties (for HTMLWorker).
    * It's implemented by FontFactoryImp.
    * @since   iText 5.0
    */
    public interface IFontProvider {
        /**
        * Checks if a certain font is registered.
        *
        * @param   fontname    the name of the font that has to be checked.
        * @return  true if the font is found
        */
        bool IsRegistered(String fontname);

        /**
        * Constructs a <CODE>Font</CODE>-object.
        *
        * @param   fontname    the name of the font
        * @param   encoding    the encoding of the font
        * @param       embedded    true if the font is to be embedded in the PDF
        * @param   size        the size of this font
        * @param   style       the style of this font
        * @param   color       the <CODE>BaseColor</CODE> of this font.
        * @return the Font constructed based on the parameters
        */
        Font GetFont(String fontname, String encoding, bool embedded, float size, int style, BaseColor color);
    }
}
