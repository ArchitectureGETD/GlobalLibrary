using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.languages {

    public class HebrewProcessor : ILanguageProcessor {

        protected int runDirection = PdfWriter.RUN_DIRECTION_RTL;

        public HebrewProcessor() {
        }

        public HebrewProcessor(int runDirection) {
            this.runDirection = runDirection;
        }

        virtual public String Process(String s) {
            return BidiLine.ProcessLTR(s, runDirection, 0);
        }

        /**
         * Hebrew is written from right to left.
         * @return true
         * @see com.itextpdf.text.pdf.languages.LanguageProcessor#isRTL()
         */

        virtual public bool IsRTL() {
            return true;
        }

    }
}
