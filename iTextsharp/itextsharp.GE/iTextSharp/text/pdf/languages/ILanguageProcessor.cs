using System;

namespace iTextSharp.GE.text.pdf.languages
{

    /**
     * Interface that needs to be implemented by classes that process bytes
     * representing text in specific languages. Processing involves changing
     * order to Right to Left and/or applying ligatures.
     */

    public interface ILanguageProcessor
    {

        /**
         * Processes a String
         * @param s	the original String
         * @return the processed String
         */
        String Process(String s);

        /**
         * Indicates if the rundirection is right-to-left.
         * @return true if text needs to be rendered from right to left.
         */
        bool IsRTL();
    }
}
