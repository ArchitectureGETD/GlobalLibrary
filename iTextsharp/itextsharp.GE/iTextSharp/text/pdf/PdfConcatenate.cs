using System;
using System.IO;
using iTextSharp.GE.text;
namespace iTextSharp.GE.text.pdf {

    /**
     * Wrapper class for PdfCopy and PdfSmartCopy.
     * Allows you to concatenate existing PDF documents with much less code.
     */
    public class PdfConcatenate {
        /** The Document object for PdfCopy. */
        protected internal Document document;
        /** The actual PdfWriter */
        protected internal PdfCopy copy;
        
        /**
         * Creates an instance of the concatenation class.
         * @param os    the Stream for the PDF document
         */
        public PdfConcatenate(Stream os) : this(os, false) {
        }

        /**
         * Creates an instance of the concatenation class.
         * @param os    the Stream for the PDF document
         * @param smart do we want PdfCopy to detect redundant content?
         */
        public PdfConcatenate(Stream os, bool smart) {
            document = new Document();
            if (smart)
                copy = new PdfSmartCopy(document, os);
            else
                copy = new PdfCopy(document, os);   
        }
        
        /**
         * Adds the pages from an existing PDF document.
         * @param reader    the reader for the existing PDF document
         * @return          the number of pages that were added
         * @throws DocumentException
         * @throws IOException
         */
        virtual public int AddPages(PdfReader reader) {
            Open();
            int n = reader.NumberOfPages;
            for (int i = 1; i <= n; i++) {
                copy.AddPage(copy.GetImportedPage(reader, i));
            }
            copy.FreeReader(reader);
            reader.Close();
            return n;
        }
        
        /**
         * Gets the PdfCopy instance so that you can add bookmarks or change preferences before you close PdfConcatenate.
         */
        virtual public PdfCopy Writer {
            get {
                return copy;
            }
        }
        
        /**
         * Opens the document (if it isn't open already).
         * Opening the document is done implicitly.
         */
        virtual public void Open() {
            if (!document.IsOpen()) {
                document.Open();
            }
        }
        
        /**
         * We've finished writing the concatenated document.
         */
        virtual public void Close() {
            document.Close();
        }
    }
}
