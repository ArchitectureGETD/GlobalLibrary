using System;

using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf {

    /**
     * Allows a class to catch several document events.
     *
     * @author  Paulo Soares
     */

    public interface IPdfPageEvent {
    
        /**
         * Called when the document is opened.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         */
        void OnOpenDocument(PdfWriter writer, Document document);

        /**
         * Called when a page is initialized.
         * <P>
         * Note that if even if a page is not written this method is still
         * called. It is preferable to use <CODE>onEndPage</CODE> to avoid
         * infinite loops.
         * </P>
         * <P>
         * Note that this method isn't called for the first page. You should apply modifications for the first
         * page either before opening the document or by using the onOpenDocument() method.
         * </P>
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         */
        void OnStartPage(PdfWriter writer, Document document);
    
        /**
         * Called when a page is finished, just before being written to the document.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         */
        void OnEndPage(PdfWriter writer, Document document);
    
        /**
         * Called when the document is closed.
         * <P>
         * Note that this method is called with the page number equal
         * to the last page plus one.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         */
        void OnCloseDocument(PdfWriter writer, Document document);
    
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
        void OnParagraph(PdfWriter writer, Document document, float paragraphPosition);
    
        /**
         * Called when a Paragraph is written.
         * <P>
         * <CODE>paragraphPosition</CODE> will hold the height of the end of the paragraph.
         *
         * @param writer the <CODE>PdfWriter</CODE> for this document
         * @param document the document
         * @param paragraphPosition the position of the end of the paragraph
         */
        void OnParagraphEnd(PdfWriter writer,Document document,float paragraphPosition);
    
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
        void OnChapter(PdfWriter writer,Document document,float paragraphPosition, Paragraph title);
    
        /**
         * Called when the end of a Chapter is reached.
         * <P>
         * <CODE>position</CODE> will hold the height of the end of the chapter.
         *
         * @param writer            the <CODE>PdfWriter</CODE> for this document
         * @param document          the document
         * @param paragraphPosition the position the chapter will be written to
         */
        void OnChapterEnd(PdfWriter writer,Document document,float paragraphPosition);
    
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
        void OnSection(PdfWriter writer,Document document,float paragraphPosition, int depth, Paragraph title);
    
        /**
         * Called when the end of a Section is reached.
         * <P>
         * <CODE>position</CODE> will hold the height of the section end.
         *
         * @param writer            the <CODE>PdfWriter</CODE> for this document
         * @param document          the document
         * @param paragraphPosition the position the chapter will be written to
         */
        void OnSectionEnd(PdfWriter writer,Document document,float paragraphPosition);
    
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
        void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text);
    }
}
