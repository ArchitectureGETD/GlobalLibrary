using System;
using System.Collections.Generic;

namespace iTextSharp.GE.text.pdf.hyphenation {

    public interface IPatternConsumer {

        /**
        * Add a character class.
        * A character class defines characters that are considered
        * equivalent for the purpose of hyphenation (e.g. "aA"). It
        * usually means to ignore case.
        * @param chargroup character group
        */
        void AddClass(String chargroup);

        /**
        * Add a hyphenation exception. An exception replaces the
        * result obtained by the algorithm for cases for which this
        * fails or the user wants to provide his own hyphenation.
        * A hyphenatedword is a vector of alternating String's and
        * {@link Hyphen Hyphen} instances
        */
        void AddException(String word, List<object> hyphenatedword);

        /**
        * Add hyphenation patterns.
        * @param pattern the pattern
        * @param values interletter values expressed as a string of
        * digit characters.
        */
        void AddPattern(String pattern, String values);
    }
}
