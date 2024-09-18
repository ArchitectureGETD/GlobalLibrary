
using System;
using iTextSharp.GE.text.error_messages;

using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf {

    /** Represents an imported page.
     *
     * @author Paulo Soares
     */
    public class PdfImportedPage : PdfTemplate {

        internal PdfReaderInstance readerInstance;
        internal int pageNumber;
        internal int rotation;
        /**
         * True if the imported page has been copied to a writer.
         * @since iText 5.0.4
         */
        protected internal bool toCopy = true;
    
        internal PdfImportedPage(PdfReaderInstance readerInstance, PdfWriter writer, int pageNumber) {
            this.readerInstance = readerInstance;
            this.pageNumber = pageNumber;
            this.rotation = readerInstance.Reader.GetPageRotation(pageNumber);
            this.writer = writer;
            bBox = readerInstance.Reader.GetPageSize(pageNumber);
            SetMatrix(1, 0, 0, 1, -bBox.Left, -bBox.Bottom);
            type = TYPE_IMPORTED;
        }

        /** Reads the content from this <CODE>PdfImportedPage</CODE>-object from a reader.
        *
        * @return self
        *
        */
        virtual public PdfImportedPage FromReader {
            get {
                return this;
            }
        }

        virtual public int PageNumber {
            get {
                return pageNumber;
            }
        }

        virtual public int Rotation {
            get { return rotation; }
        }

        /** Always throws an error. This operation is not allowed.
         * @param image dummy
         * @param a dummy
         * @param b dummy
         * @param c dummy
         * @param d dummy
         * @param e dummy
         * @param f dummy
         * @throws DocumentException  dummy */    
        public override void AddImage(Image image, float a, float b, float c, float d, float e, float f) {
            ThrowError();
        }
    
        /** Always throws an error. This operation is not allowed.
         * @param template dummy
         * @param a dummy
         * @param b dummy
         * @param c dummy
         * @param d dummy
         * @param e dummy
         * @param f  dummy */    
        public override void AddTemplate(PdfTemplate template, float a, float b, float c, float d, float e, float f) {
            ThrowError();
        }
    
        /** Always throws an error. This operation is not allowed.
         * @return  dummy */    
        public override PdfContentByte Duplicate {
            get {
                ThrowError();
                return null;
            }
        }
    
        /**
        * Gets the stream representing this page.
        *
        * @param   compressionLevel    the compressionLevel
        * @return the stream representing this page
        * @since   2.1.3   (replacing the method without param compressionLevel)
        */
        override public PdfStream GetFormXObject(int compressionLevel) {
            return readerInstance.GetFormXObject(pageNumber, compressionLevel);
        }
    
        public override void SetColorFill(PdfSpotColor sp, float tint) {
            ThrowError();
        }
    
        public override void SetColorStroke(PdfSpotColor sp, float tint) {
            ThrowError();
        }
    
        internal override PdfObject Resources {
            get {
                return readerInstance.GetResources(pageNumber);
            }
        }
    
        /** Always throws an error. This operation is not allowed.
         * @param bf dummy
         * @param size dummy */    
        public override void SetFontAndSize(BaseFont bf, float size) {
            ThrowError();
        }
    
        public override PdfTransparencyGroup Group {
            set {
                ThrowError();
            }
        }

        internal void ThrowError() {
            throw new Exception(MessageLocalization.GetComposedMessage("content.can.not.be.added.to.a.pdfimportedpage"));
        }
    
        internal PdfReaderInstance PdfReaderInstance {
            get {
                return readerInstance;
            }
        }

        /**
         * Checks if the page has to be copied.
         * @return true if the page has to be copied.
         * @since iText 5.0.4
         */
        virtual public bool IsToCopy() {
            return toCopy;
        }

        /**
         * Indicate that the resources of the imported page have been copied.
         * @since iText 5.0.4
         */
        virtual public void SetCopied() {
            toCopy = false;
        }
    }
}
