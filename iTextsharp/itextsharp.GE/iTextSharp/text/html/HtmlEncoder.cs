using System;
using System.Text;
using System.Collections.Generic;

namespace iTextSharp.GE.text.html {
    /**
     * This class converts a <CODE>String</CODE> to the HTML-format of a String.
     * <P>
     * To convert the <CODE>String</CODE>, each character is examined:
     * <UL>
     * <LI>ASCII-characters from 000 till 031 are represented as &amp;#xxx;<BR>
     *     (with xxx = the value of the character)
     * <LI>ASCII-characters from 032 t/m 127 are represented by the character itself, except for:
     *     <UL>
     *     <LI>'\n' becomes &lt;BR&gt;\n
     *     <LI>&quot; becomes &amp;quot;
     *     <LI>&amp; becomes &amp;amp;
     *     <LI>&lt; becomes &amp;lt;
     *     <LI>&gt; becomes &amp;gt;
     *     </UL>
     * <LI>ASCII-characters from 128 till 255 are represented as &amp;#xxx;<BR>
     *     (with xxx = the value of the character)
     * </UL>
     * <P>
     * Example:
     * <P><BLOCKQUOTE><PRE>
     *    String htmlPresentation = HtmlEncoder.Encode("Marie-Th&#233;r&#232;se S&#248;rensen");
     * </PRE></BLOCKQUOTE><P>
     * for more info: see O'Reilly; "HTML: The Definitive Guide" (page 164)
     *
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public static class HtmlEncoder {
    
        // membervariables
    
        /** List with the HTML translation of all the characters. */
        private static String[] HTML_CODE = new String[256];
    
	    /**
	     * Set containing tags that trigger a new line.
	     * @since iText 5.0.6
	     */
	    private static Dictionary<string,object> NEWLINETAGS = new Dictionary<string,object>();

	    static HtmlEncoder(){
            // Following list are the basic html tags that force new lines
            // List may be extended as we discover them
            NEWLINETAGS[HtmlTags.P] = null;
            NEWLINETAGS[HtmlTags.BLOCKQUOTE] = null;
            NEWLINETAGS[HtmlTags.BR] = null;

            for (int i = 0; i < 10; i++) {
                HTML_CODE[i] = "&#00" + i + ";";
            }
        
            for (int i = 10; i < 32; i++) {
                HTML_CODE[i] = "&#0" + i + ";";
            }
        
            for (int i = 32; i < 128; i++) {
                HTML_CODE[i] = ((char)i).ToString();
            }
        
            // Special characters
            HTML_CODE['\t'] = "\t";
            HTML_CODE['\n'] = "<br />\n";
            HTML_CODE['\"'] = "&quot;"; // double quote
            HTML_CODE['&'] = "&amp;"; // ampersand
            HTML_CODE['<'] = "&lt;"; // lower than
            HTML_CODE['>'] = "&gt;"; // greater than
        
            for (int i = 128; i < 256; i++) {
                HTML_CODE[i] = "&#" + i + ";";
            }
        }
    
    
        // methods
    
        /**
         * Converts a <CODE>String</CODE> to the HTML-format of this <CODE>String</CODE>.
         *
         * @param   string  The <CODE>String</CODE> to convert
         * @return  a <CODE>String</CODE>
         */
    
        public static String Encode(String str) {
            int n = str.Length;
            char character;
            StringBuilder buffer = new StringBuilder();
            // loop over all the characters of the String.
            for (int i = 0; i < n; i++) {
                character = str[i];
                // the Htmlcode of these characters are added to a StringBuilder one by one
                if (character < 256) {
                    buffer.Append(HTML_CODE[character]);
                }
                else {
                    // Improvement posted by Joachim Eyrich
                    buffer.Append("&#").Append((int)character).Append(';');
                }
            }
            return buffer.ToString();
        }
    
        /**
         * Converts a <CODE>BaseColor</CODE> into a HTML representation of this <CODE>BaseColor</CODE>.
         *
         * @param   color   the <CODE>BaseColor</CODE> that has to be converted.
         * @return  the HTML representation of this <COLOR>BaseColor</COLOR>
         */
    
        public static String Encode(BaseColor color) {
            StringBuilder buffer = new StringBuilder("#");
            if (color.R < 16) {
                buffer.Append('0');
            }
            buffer.Append(System.Convert.ToString(color.R, 16));
            if (color.G < 16) {
                buffer.Append('0');
            }
            buffer.Append(System.Convert.ToString(color.G, 16));
            if (color.B < 16) {
                buffer.Append('0');
            }
            buffer.Append(System.Convert.ToString(color.B, 16));
            return buffer.ToString();
        }
    
        /**
         * Translates the alignment value.
         *
         * @param   alignment   the alignment value
         * @return  the translated value
         */
    
        public static String GetAlignment(int alignment) {
            switch (alignment) {
                case Element.ALIGN_LEFT:
                    return HtmlTags.ALIGN_LEFT;
                case Element.ALIGN_CENTER:
                    return HtmlTags.ALIGN_CENTER;
                case Element.ALIGN_RIGHT:
                    return HtmlTags.ALIGN_RIGHT;
                case Element.ALIGN_JUSTIFIED:
                case Element.ALIGN_JUSTIFIED_ALL:
                    return HtmlTags.ALIGN_JUSTIFY;
                case Element.ALIGN_TOP:
                    return HtmlTags.ALIGN_TOP;
                case Element.ALIGN_MIDDLE:
                    return HtmlTags.ALIGN_MIDDLE;
                case Element.ALIGN_BOTTOM:
                    return HtmlTags.ALIGN_BOTTOM;
                case Element.ALIGN_BASELINE:
                    return HtmlTags.ALIGN_BASELINE;
                default:
                    return "";
            }
        }

	    /**
	     * Returns true if the tag causes a new line like p, br etc.
	     * @since iText 5.0.6
	     */
	    public static bool IsNewLineTag(String tag) {
		    return NEWLINETAGS.ContainsKey(tag);
	    }
    }
}
