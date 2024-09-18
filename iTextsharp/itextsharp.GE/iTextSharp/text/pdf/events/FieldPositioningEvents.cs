using System;
using System.IO;
using System.Collections.Generic;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf.events {

    /**
    * Class for an index.
    * 
    * @author Michael Niedermair
    */
    public class FieldPositioningEvents : PdfPageEventHelper, IPdfPCellEvent {

        /**
        * Keeps a map with fields that are to be positioned in inGenericTag.
        */
        protected Dictionary<String, PdfFormField> genericChunkFields = new Dictionary<string,PdfFormField>();

        /**
        * Keeps the form field that is to be positioned in a cellLayout event.
        */
        protected PdfFormField cellField = null;
        
        /**
        * The PdfWriter to use when a field has to added in a cell event. 
        */
        protected PdfWriter fieldWriter = null;
        /**
        * The PdfFormField that is the parent of the field added in a cell event. 
        */
        protected PdfFormField parent = null;
        
        /** Creates a new event. This constructor will be used if you need to position fields with Chunk objects. */
        public FieldPositioningEvents() {}
        
        /** Some extra padding that will be taken into account when defining the widget. */
        public float padding;
        
        /**
        * Add a PdfFormField that has to be tied to a generic Chunk.
        */
        virtual public void AddField(String text, PdfFormField field) {
            genericChunkFields[text] = field;
        }
        
        /** Creates a new event. This constructor will be used if you need to position fields with a Cell Event. */
        public FieldPositioningEvents(PdfWriter writer, PdfFormField field) {
            this.cellField = field;
            this.fieldWriter = writer;
        }  
        
        /** Creates a new event. This constructor will be used if you need to position fields with a Cell Event. */
        public FieldPositioningEvents(PdfFormField parent, PdfFormField field) {
            this.cellField = field;
            this.parent = parent;
        }
        
        /** Creates a new event. This constructor will be used if you need to position fields with a Cell Event. 
        * @throws DocumentException
        * @throws IOException*/
        public FieldPositioningEvents(PdfWriter writer, String text) {
            this.fieldWriter = writer;
            TextField tf = new TextField(writer, new Rectangle(0, 0), text);
            tf.FontSize = 14;
            cellField = tf.GetTextField();
        }   
            
        /** Creates a new event. This constructor will be used if you need to position fields with a Cell Event. 
        * @throws DocumentException
        * @throws IOException*/
        public FieldPositioningEvents(PdfWriter writer, PdfFormField parent, String text) {
            this.parent = parent;
            TextField tf = new TextField(writer, new Rectangle(0, 0), text);
            tf.FontSize = 14;
            cellField = tf.GetTextField();
        }  

        /**
        * @param padding The padding to set.
        */
        virtual public float Padding {
            set {
                padding = value;
            }
            get {
                return padding;
            }
        }
        
        /**
        * @param parent The parent to set.
        */
        virtual public PdfFormField Parent {
            set {
                parent = value;
            }
            get {
                return parent;
            }
        }

        /**
        * @see com.lowagie.text.pdf.PdfPageEvent#onGenericTag(com.lowagie.text.pdf.PdfWriter, com.lowagie.text.Document, com.lowagie.text.Rectangle, java.lang.String)
        */
        public override void OnGenericTag(PdfWriter writer, Document document,
                Rectangle rect, String text) {
            rect.Bottom = rect.Bottom - 3;
            PdfFormField field;
            genericChunkFields.TryGetValue(text, out field);
            if (field == null) {
                TextField tf = new TextField(writer, new Rectangle(rect.GetLeft(padding), rect.GetBottom(padding), rect.GetRight(padding), rect.GetTop(padding)), text);
                tf.FontSize = 14;
                field = tf.GetTextField();
            }
            else {
                field.Put(PdfName.RECT,  new PdfRectangle(rect.GetLeft(padding), rect.GetBottom(padding), rect.GetRight(padding), rect.GetTop(padding)));
            }
            if (parent == null)
                writer.AddAnnotation(field);
            else
                parent.AddKid(field);
        }

        /**
        * @see com.lowagie.text.pdf.PdfPCellEvent#cellLayout(com.lowagie.text.pdf.PdfPCell, com.lowagie.text.Rectangle, com.lowagie.text.pdf.PdfContentByte[])
        */
        virtual public void CellLayout(PdfPCell cell, Rectangle rect, PdfContentByte[] canvases) {
            if (cellField == null || (fieldWriter == null && parent == null)) throw new ArgumentException(MessageLocalization.GetComposedMessage("you.have.used.the.wrong.constructor.for.this.fieldpositioningevents.class"));
            cellField.Put(PdfName.RECT, new PdfRectangle(rect.GetLeft(padding), rect.GetBottom(padding), rect.GetRight(padding), rect.GetTop(padding)));
            if (parent == null)
                fieldWriter.AddAnnotation(cellField);
            else
                parent.AddKid(cellField);
        }
    }
}
