using System;
using iTextSharp.GE.text.xml.simpleparser;
/**
 *
 */

namespace iTextSharp.GE.text.xml.simpleparser.handler {

    /**
     * Always returns false.
     *
     */
    public class NeverNewLineHandler : INewLineHandler {

        /*
         * (non-Javadoc)
         *
         * @see
         * com.itextpdf.text.xml.simpleparser.NewLineHandler#isNewLineTag(java.lang
         * .String)
         */
        virtual public bool IsNewLineTag(String tag) {
            return false;
        }
    }
}
