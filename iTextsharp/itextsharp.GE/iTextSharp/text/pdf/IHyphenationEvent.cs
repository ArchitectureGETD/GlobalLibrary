using System;

namespace iTextSharp.GE.text.pdf {
    /** Called by <code>Chunk</code> to hyphenate a word.
     *
     * @author Paulo Soares
     */
    public interface IHyphenationEvent {

        /** Gets the hyphen symbol.
         * @return the hyphen symbol
         */    
        string HyphenSymbol {
            get;
        }
    
        /** Hyphenates a word and returns the first part of it. To get
         * the second part of the hyphenated word call <CODE>getHyphenatedWordPost()</CODE>.
         * @param word the word to hyphenate
         * @param font the font used by this word
         * @param fontSize the font size used by this word
         * @param remainingWidth the width available to fit this word in
         * @return the first part of the hyphenated word including
         * the hyphen symbol, if any
         */    
        string GetHyphenatedWordPre(string word, BaseFont font, float fontSize, float remainingWidth);
    
        /** Gets the second part of the hyphenated word. Must be called
         * after <CODE>getHyphenatedWordPre()</CODE>.
         * @return the second part of the hyphenated word
         */    
        string HyphenatedWordPost {
            get;
        }
    }
}
