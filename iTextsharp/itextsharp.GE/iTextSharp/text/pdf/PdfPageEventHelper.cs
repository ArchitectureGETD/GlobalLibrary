using System;

using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf {

    /**
     * Helps the use of <CODE>PdfPageEvent</CODE> by implementing all the interface methods.
     * A class can extend <CODE>PdfPageEventHelper</CODE> and only implement the
     * needed methods.
     *
     * @author Paulo Soares
     */

    public class PdfPageEventHelper : IPdfPageEvent {
    
        /**
                                         * Called when the document is opened.
                                         *
                                         * @param writer the <CODE>PdfWriter</CODE> for this document
                                         * @param document the document
                                         */
        public virtual void OnOpenDocument(PdfWriter writer,Document document) {
        }
    
        /**
         * Called when a page is initialized.
         * <P>
         * Note that if even if a page is not written this method is still
         * called. It is preferable to use <CODE>onEndPage</CODE> to avoid
         * infinite loops.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         */
        public virtual void OnStartPage(PdfWriter writer,Document document) {
        }
    
        /**
         * Called when a page is finished, just before being written to the document.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         */
        public virtual void OnEndPage(PdfWriter writer,Document document) {
        }
    
        /**
         * Called when the document is closed.
         * <P>
         * Note that this method is called with the page number equal
         * to the last page plus one.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         */
        public virtual void OnCloseDocument(PdfWriter writer,Document document) {
        }
    
        /**
         * Called when a Paragraph is written.
         * <P>
         * <CODE>paragraphPosition</CODE> will hold the height at which the
         * paragraph will be written to. This is useful to insert bookmarks with
         * more control.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         * @param paragraphPosition the position the paragraph will be written to
         */
        public virtual void OnParagraph(PdfWriter writer,Document document,float paragraphPosition) {
        }
    
        /**
         * Called when a Paragraph is written.
         * <P>
         * <CODE>paragraphPosition</CODE> will hold the height of the end of the paragraph.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         * @param paragraphPosition the position of the end of the paragraph
         */
        public virtual void OnParagraphEnd(PdfWriter writer,Document document,float paragraphPosition) {
        }
    
        /**
         * Called when a Chapter is written.
         * <P>
         * <CODE>position</CODE> will hold the height at which the
         * chapter will be written to.
         *
         * @param writer            the <CODE>PdfWriter</CODE> for this document
         * @param document          the document
         * @param paragraphPosition the position the chapter will be written to
         * @param title             the title of the Chapter
         */
        public virtual void OnChapter(PdfWriter writer,Document document,float paragraphPosition,Paragraph title) {
        }
    
        /**
         * Called when the end of a Chapter is reached.
         * <P>
         * <CODE>position</CODE> will hold the height of the end of the chapter.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         * @param position the position of the end of the chapter.
         */
        public virtual void OnChapterEnd(PdfWriter writer,Document document,float position) {
        }
    
        /**
         * Called when a Section is written.
         * <P>
         * <CODE>position</CODE> will hold the height at which the
         * section will be written to.
         *
         * @param writer            the <CODE>PdfWriter</CODE> for this document
         * @param document          the document
         * @param paragraphPosition the position the chapter will be written to
         * @param title             the title of the Chapter
         */
        public virtual void OnSection(PdfWriter writer,Document document,float paragraphPosition,int depth,Paragraph title) {
        }
    
        /**
         * Called when the end of a Section is reached.
         * <P>
         * <CODE>position</CODE> will hold the height of the section end.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         * @param position the position of the end of the section
         */
        public virtual void OnSectionEnd(PdfWriter writer,Document document,float position) {
        }
    
        /**
         * Called when a <CODE>Chunk</CODE> with a generic tag is written.
         * <P>
         * It is usefull to pinpoint the <CODE>Chunk</CODE> location to generate
         * bookmarks, for example.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         * @param rect the <CODE>Rectangle</CODE> containing the <CODE>Chunk</CODE>
         * @param text the text of the tag
         */
        public virtual void OnGenericTag(PdfWriter writer,Document document,Rectangle rect,string text) {
        }
    }
}
