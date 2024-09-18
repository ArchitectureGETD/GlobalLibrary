using System;
using System.Text.RegularExpressions;
namespace iTextSharp.GE.text.pdf {

    /**
     * <p>
     * The default class that is used to determine whether or not a character
     * is a split character.
     * <p/>
     * You can add an array of characters or a single character on which iText
     * should split the chunk. If custom characters have been set, iText will ignore
     * the default characters this class uses to split chunks.
     *
     * @since 2.1.2
     */
    public class DefaultSplitCharacter : ISplitCharacter {
	
        /**
         * An instance of the default SplitCharacter.
         */
        public static readonly ISplitCharacter DEFAULT = new DefaultSplitCharacter();

        protected char[] characters;

        /**
         * Default constructor, has no custom characters to check.
         */
        public DefaultSplitCharacter() {
            // empty body
        }

        /**
         * Constructor with one splittable character.
         *
         * @param character char
         */
        public DefaultSplitCharacter(char character) : this(new char[] {character}) {
        }

        /**
         * Constructor with an array of splittable characters
         *
         * @param characters char[]
         */
        public DefaultSplitCharacter(char[] characters) {
            this.characters = characters;
        }

        /**
         * <p>
         * Checks if a character can be used to split a <CODE>PdfString</CODE>.
         * <p/>
         * <p>
         * The default behavior is that every character less than or equal to SPACE, the character '-'
         * and some specific unicode ranges are 'splitCharacters'.
         * </P>
         * <p/>
         * If custom splittable characters are set using the specified constructors,
         * then this class will ignore the default characters described in the
         * previous paragraph.
         * </P>
         *
         * @param start   start position in the array
         * @param current current position in the array
         * @param end     end position in the array
         * @param ck      chunk array
         * @param cc      the character array that has to be checked
         * @return <CODE>true</CODE> if the character can be used to split a string, <CODE>false</CODE> otherwise
         */
        virtual public bool IsSplitCharacter(int start, int current, int end, char[] cc, PdfChunk[] ck) {
            char[] ccTmp = CheckDatePattern(new string(cc));
            char c = GetCurrentCharacter(current, ccTmp, ck);

            if (characters != null) {
                for (int i = 0; i < characters.Length; i++) {
                    if (c == characters[i]) {
                        return true;
                    }
                }
                return false;
            }

            if (c <= ' ' || c == '-' || c == '\u2010') {
                return true;
            }
            if (c < 0x2002)
                return false;
            return ((c >= 0x2002 && c <= 0x200b)
                    || (c >= 0x2e80 && c < 0xd7a0)
                    || (c >= 0xf900 && c < 0xfb00)
                    || (c >= 0xfe30 && c < 0xfe50)
                    || (c >= 0xff61 && c < 0xffa0));
        }

        /**
         * Returns the current character
         *
         * @param current current position in the array
         * @param ck      chunk array
         * @param cc      the character array that has to be checked
         * @return the current character
         */
        virtual protected char GetCurrentCharacter(int current, char[] cc, PdfChunk[] ck) {
            if (ck == null) {
                return cc[current];
            }
            return (char) ck[Math.Min(current, ck.Length - 1)].GetUnicodeEquivalent(cc[current]);
        }

        internal char[] CheckDatePattern(string data) {
            String regex = "(\\d{2,4}-\\d{2}-\\d{2,4})";
            Match m = Regex.Match(data, regex);
            if (m.Success) {
                string tmpData = m.Groups[1].Value.Replace('-', '\u2011');
                data = data.Replace(m.Groups[1].Value, tmpData);
            }
            return data.ToCharArray();
        }
    }
}
