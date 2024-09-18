using System;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text {
    /// <summary>
    /// Interface for customizing the split character.
    /// </summary>
    public interface ISplitCharacter {
        /**
        * Returns <CODE>true</CODE> if the character can split a line. The splitting implementation
        * is free to look ahead or look behind characters to make a decision.
        * <p>
        * The default implementation is:
        * <p>
        * <pre>
        * public boolean IsSplitCharacter(int start, int current, int end, char[] cc, PdfChunk[] ck) {
        *    char c;
        *    if (ck == null)
        *        c = cc[current];
        *    else
        *        c = ck[Math.Min(current, ck.length - 1)].GetUnicodeEquivalent(cc[current]);
        *    if (c <= ' ' || c == '-') {
        *        return true;
        *    }
        *    if (c < 0x2e80)
        *        return false;
        *    return ((c >= 0x2e80 && c < 0xd7a0)
        *    || (c >= 0xf900 && c < 0xfb00)
        *    || (c >= 0xfe30 && c < 0xfe50)
        *    || (c >= 0xff61 && c < 0xffa0));
        * }
        * </pre>
        * @param start the lower limit of <CODE>cc</CODE> inclusive
        * @param current the pointer to the character in <CODE>cc</CODE>
        * @param end the upper limit of <CODE>cc</CODE> exclusive
        * @param cc an array of characters at least <CODE>end</CODE> sized
        * @param ck an array of <CODE>PdfChunk</CODE>. The main use is to be able to call
        * {@link PdfChunk#getUnicodeEquivalent(char)}. It may be <CODE>null</CODE>
        * or shorter than <CODE>end</CODE>. If <CODE>null</CODE> no convertion takes place.
        * If shorter than <CODE>end</CODE> the last element is used
        * @return <CODE>true</CODE> if the Character(s) can split a line
        */
        
        bool IsSplitCharacter(int start, int current, int end, char[] cc, PdfChunk[] ck);
    }
}
