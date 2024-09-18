using System;
using System.Collections.Generic;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.events {

    /**
    * If you want to add more than one page eventa to a PdfWriter,
    * you have to construct a PdfPageEventForwarder, add the
    * different events to this object and add the forwarder to
    * the PdfWriter.
    */

    public class PdfPageEventForwarder : IPdfPageEvent {

        /** ArrayList containing all the PageEvents that have to be executed. */
        protected List<IPdfPageEvent> events = new List<IPdfPageEvent>();
        
        /** 
        * Add a page eventa to the forwarder.
        * @param eventa an eventa that has to be added to the forwarder.
        */
        virtual public void AddPageEvent(IPdfPageEvent eventa) {
            events.Add(eventa);
        }
        
        /**
        * Called when the document is opened.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        */
        public virtual void OnOpenDocument(PdfWriter writer, Document document) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnOpenDocument(writer, document);
            }
        }

        /**
        * Called when a page is initialized.
        * <P>
        * Note that if even if a page is not written this method is still called.
        * It is preferable to use <CODE>onEndPage</CODE> to avoid infinite loops.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        */
        public virtual void OnStartPage(PdfWriter writer, Document document) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnStartPage(writer, document);
            }
        }

        /**
        * Called when a page is finished, just before being written to the
        * document.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        */
        public virtual void OnEndPage(PdfWriter writer, Document document) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnEndPage(writer, document);
            }
        }

        /**
        * Called when the document is closed.
        * <P>
        * Note that this method is called with the page number equal to the last
        * page plus one.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        */
        public virtual void OnCloseDocument(PdfWriter writer, Document document) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnCloseDocument(writer, document);
            }
        }

        /**
        * Called when a Paragraph is written.
        * <P>
        * <CODE>paragraphPosition</CODE> will hold the height at which the
        * paragraph will be written to. This is useful to insert bookmarks with
        * more control.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        * @param paragraphPosition
        *            the position the paragraph will be written to
        */
        public virtual void OnParagraph(PdfWriter writer, Document document,
                float paragraphPosition) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnParagraph(writer, document, paragraphPosition);
            }
        }

        /**
        * Called when a Paragraph is written.
        * <P>
        * <CODE>paragraphPosition</CODE> will hold the height of the end of the
        * paragraph.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        * @param paragraphPosition
        *            the position of the end of the paragraph
        */
        public virtual void OnParagraphEnd(PdfWriter writer, Document document,
                float paragraphPosition) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnParagraphEnd(writer, document, paragraphPosition);
            }
        }

        /**
        * Called when a Chapter is written.
        * <P>
        * <CODE>position</CODE> will hold the height at which the chapter will be
        * written to.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        * @param paragraphPosition
        *            the position the chapter will be written to
        * @param title
        *            the title of the Chapter
        */
        public virtual void OnChapter(PdfWriter writer, Document document,
                float paragraphPosition, Paragraph title) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnChapter(writer, document, paragraphPosition, title);
            }
        }

        /**
        * Called when the end of a Chapter is reached.
        * <P>
        * <CODE>position</CODE> will hold the height of the end of the chapter.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        * @param position
        *            the position of the end of the chapter.
        */
        public virtual void OnChapterEnd(PdfWriter writer, Document document, float position) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnChapterEnd(writer, document, position);
            }
        }

        /**
        * Called when a Section is written.
        * <P>
        * <CODE>position</CODE> will hold the height at which the section will be
        * written to.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        * @param paragraphPosition
        *            the position the section will be written to
        * @param depth
        *            the number depth of the Section
        * @param title
        *            the title of the section
        */
        public virtual void OnSection(PdfWriter writer, Document document,
                float paragraphPosition, int depth, Paragraph title) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnSection(writer, document, paragraphPosition, depth, title);
            }
        }

        /**
        * Called when the end of a Section is reached.
        * <P>
        * <CODE>position</CODE> will hold the height of the section end.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        * @param position
        *            the position of the end of the section
        */
        public virtual void OnSectionEnd(PdfWriter writer, Document document, float position) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnSectionEnd(writer, document, position);
            }
        }

        /**
        * Called when a <CODE>Chunk</CODE> with a generic tag is written.
        * <P>
        * It is usefull to pinpoint the <CODE>Chunk</CODE> location to generate
        * bookmarks, for example.
        * 
        * @param writer
        *            the <CODE>PdfWriter</CODE> for this document
        * @param document
        *            the document
        * @param rect
        *            the <CODE>Rectangle</CODE> containing the <CODE>Chunk
        *            </CODE>
        * @param text
        *            the text of the tag
        */
        public virtual void OnGenericTag(PdfWriter writer, Document document,
                Rectangle rect, String text) {
            foreach (IPdfPageEvent eventa in events) {
                eventa.OnGenericTag(writer, document, rect, text);
            }
        }
    }
}
