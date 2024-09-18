using System;

namespace iTextSharp.GE.text.pdf.parser {

    /**
     * Represents a Marked Content block in a PDF
     * @since 5.0.2
     */
    public class MarkedContentInfo {
        private PdfName tag;
        private PdfDictionary dictionary;
        
        public MarkedContentInfo(PdfName tag, PdfDictionary dictionary) {
            this.tag = tag;
            this.dictionary = dictionary != null ? dictionary : new PdfDictionary(); // I'd really prefer to make a defensive copy here to make this immutable
        }

        /**
         * Get the tag of this marked content
         * @return the tag of this marked content
         */
        virtual public PdfName GetTag(){
            return tag;
        }
        
        /**
         * Determine if an MCID is available
         * @return true if the MCID is available, false otherwise
         */
        virtual public bool HasMcid(){
            return dictionary.Contains(PdfName.MCID);
        }
        
        /**
         * Gets the MCID value  If the Marked Content contains
         * an MCID entry, returns that value.  Otherwise, a {@link NullPointerException} is thrown.
         * @return the MCID value
         * @throws NullPointerException if there is no MCID (see {@link MarkedContentInfo#hasMcid()})
         */
        virtual public int GetMcid(){
            PdfNumber id = dictionary.GetAsNumber(PdfName.MCID);
            if (id == null)
                throw new InvalidOperationException("MarkedContentInfo does not contain MCID");
            
            return id.IntValue;
        }
        
    }
}
