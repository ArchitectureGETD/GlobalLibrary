using System.Collections.Generic;

namespace iTextSharp.GE.text.pdf.parser {

    /**
     * A utility class that makes it cleaner to process content from pages of a PdfReader
     * through a specified RenderListener.
     * @since 5.0.2
     */
    public class PdfReaderContentParser {
        /** the reader this parser will process */
        private PdfReader reader;
        
        public PdfReaderContentParser(PdfReader reader) {
            this.reader = reader;
        }

        /**
         * Processes content from the specified page number using the specified listener.
         * Also allows registration of custom ContentOperators
         * @param <E> the type of the renderListener - this makes it easy to chain calls
         * @param pageNumber the page number to process
         * @param renderListener the listener that will receive render callbacks
         * @param additionalContentOperators an optional dictionary of custom IContentOperators for rendering instructions
         * @return the provided renderListener
         * @throws IOException if operations on the reader fail
         */
        public virtual E ProcessContent<E>(int pageNumber, E renderListener, IDictionary<string, IContentOperator> additionalContentOperators) where E : IRenderListener {
            PdfDictionary pageDic = reader.GetPageN(pageNumber);
            PdfDictionary resourcesDic = pageDic.GetAsDict(PdfName.RESOURCES);
            
            PdfContentStreamProcessor processor = new PdfContentStreamProcessor(renderListener);
            foreach (KeyValuePair<string, IContentOperator> entry in additionalContentOperators) {
                processor.RegisterContentOperator(entry.Key, entry.Value);
            }
            processor.ProcessContent(ContentByteUtils.GetContentBytesForPage(reader, pageNumber), resourcesDic);        
            return renderListener;
        }

        /**
         * Processes content from the specified page number using the specified listener
         * @param <E> the type of the renderListener - this makes it easy to chain calls
         * @param pageNumber the page number to process
         * @param renderListener the listener that will receive render callbacks
         * @return the provided renderListener
         * @throws IOException if operations on the reader fail
         */
        public virtual E ProcessContent<E>(int pageNumber, E renderListener) where E : IRenderListener {
            return ProcessContent(pageNumber, renderListener, new Dictionary<string, IContentOperator>());
        }
    }
}
