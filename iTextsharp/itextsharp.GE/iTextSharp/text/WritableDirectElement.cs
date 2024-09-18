using System;
using System.Collections.Generic;
using iTextSharp.GE.text.api;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.draw;
namespace iTextSharp.GE.text {

    /**
     * An element that is not an element, it holds {@link Element#WRITABLE_DIRECT}
     * as Element type. It implements WriterOperation to do operations on the
     * {@link PdfWriter} and the {@link Document} that must be done at the time of
     * the writing. Much like a {@link VerticalPositionMark} but little different.
     *
     * @author itextpdf.com
     *
     */
    public abstract class WritableDirectElement : IElement, IWriterOperation {


        public static readonly int DIRECT_ELEMENT_TYPE_UNKNOWN = 0;
        public static readonly int DIRECT_ELEMENT_TYPE_HEADER = 1;

        protected int directElementType = DIRECT_ELEMENT_TYPE_UNKNOWN;

        public int DirectElemenType
        {
            get { return directElementType; }
        }

        public WritableDirectElement()
        {
        }

        public WritableDirectElement(int directElementType)
        {
            this.directElementType = directElementType;
        }

        /*
         * (non-Javadoc)
         *
         * @see com.itextpdf.text.Element#process(com.itextpdf.text.ElementListener)
         */
        virtual public bool Process(IElementListener listener) {
            throw new NotSupportedException();
        }

        /**
         * @return {@link Element#WRITABLE_DIRECT}
         */
        virtual public int Type {
            get {
                return Element.WRITABLE_DIRECT;
            }
        }

        /*
         * (non-Javadoc)
         *
         * @see com.itextpdf.text.Element#isContent()
         */
        virtual public bool IsContent() {
            return false;
        }

        /*
         * (non-Javadoc)
         *
         * @see com.itextpdf.text.Element#isNestable()
         */
        virtual public bool IsNestable() {
            throw new NotSupportedException();
        }

        /*
         * (non-Javadoc)
         *
         * @see com.itextpdf.text.Element#getChunks()
         */
        virtual public IList<Chunk> Chunks {
            get {
                return new List<Chunk>();
            }
        }

        public abstract void Write(PdfWriter writer, Document doc);
    }
}
