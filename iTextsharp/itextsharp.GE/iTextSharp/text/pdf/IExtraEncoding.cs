using System;

namespace iTextSharp.GE.text.pdf {
    /**
    * Classes implementing this interface can create custom encodings or
    * replace existing ones. It is used in the context of <code>PdfEncoding</code>.
    * @author Paulo Soares
    */
    public interface IExtraEncoding {
        
        /**
        * Converts an Unicode string to a byte array according to some encoding.
        * @param text the Unicode string
        * @param encoding the requested encoding. It's mainly of use if the same class
        * supports more than one encoding.
        * @return the conversion or <CODE>null</CODE> if no conversion is supported
        */    
        byte[] CharToByte(String text, String encoding);
        
        /**
        * Converts an Unicode char to a byte array according to some encoding.
        * @param char1 the Unicode char
        * @param encoding the requested encoding. It's mainly of use if the same class
        * supports more than one encoding.
        * @return the conversion or <CODE>null</CODE> if no conversion is supported
        */    
        byte[] CharToByte(char char1, String encoding);

        /**
        * Converts a byte array to an Unicode string according to some encoding.
        * @param b the input byte array
        * @param encoding the requested encoding. It's mainly of use if the same class
        * supports more than one encoding.
        * @return the conversion or <CODE>null</CODE> if no conversion is supported
        */
        String ByteToChar(byte[] b, String encoding);   
    }
}
