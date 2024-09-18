using System;

using iTextSharp.GE.text.pdf.hyphenation;

namespace iTextSharp.GE.text.pdf {

    /** Hyphenates words automatically accordingly to the language and country.
     * The hyphenator engine was taken from FOP and uses the TEX patterns. If a language
     * is not provided and a TEX pattern for it exists, it can be easily adapted.
     *
     * @author Paulo Soares
     */
    public class HyphenationAuto : IHyphenationEvent {

        /** The hyphenator engine.
                                      */    
        protected Hyphenator hyphenator;
        /** The second part of the hyphenated word.
                                      */    
        protected string post;
    
        /** Creates a new hyphenation instance usable in <CODE>Chunk</CODE>.
        * @param lang the language ("en" for english, for example)
        * @param country the country ("GB" for Great-Britain or "none" for no country, for example)
        * @param leftMin the minimun number of letters before the hyphen
        * @param rightMin the minimun number of letters after the hyphen
        */    
        public HyphenationAuto(string lang, string country, int leftMin, int rightMin) {
            hyphenator = new Hyphenator(lang, country, leftMin, rightMin);
        }

        /** Gets the hyphen symbol.
         * @return the hyphen symbol
         */    
        virtual public string HyphenSymbol {
            get {
                return "-";
            }
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
        virtual public string GetHyphenatedWordPre(string word, BaseFont font, float fontSize, float remainingWidth) {
            post = word;
            string hyphen = this.HyphenSymbol;
            float hyphenWidth = font.GetWidthPoint(hyphen, fontSize);
            if (hyphenWidth > remainingWidth)
                return "";
            Hyphenation hyphenation = hyphenator.Hyphenate(word);
            if (hyphenation == null) {
                return "";
            }
            int len = hyphenation.Length;
            int k;
            for (k = 0; k < len; ++k) {
                if (font.GetWidthPoint(hyphenation.GetPreHyphenText(k), fontSize) + hyphenWidth > remainingWidth)
                    break;
            }
            --k;
            if (k < 0)
                return "";
            post = hyphenation.GetPostHyphenText(k);
            return hyphenation.GetPreHyphenText(k) + hyphen;
        }
    
        /** Gets the second part of the hyphenated word. Must be called
         * after <CODE>getHyphenatedWordPre()</CODE>.
         * @return the second part of the hyphenated word
         */    
        virtual public string HyphenatedWordPost {
            get {
                return post;
            }
        }
    }
}
