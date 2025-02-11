using System;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf {

    /**
     * An array specifying a visibility expression, used to compute visibility
     * of content based on a set of optional content groups.
     * @since 5.0.2
     */
    public class PdfVisibilityExpression : PdfArray {

        /** A boolean operator. */
        public const int OR = 0;
        /** A boolean operator. */
        public const int AND = 1;
        /** A boolean operator. */
        public const int NOT = -1;
        
        /**
         * Creates a visibility expression.
         * @param type should be AND, OR, or NOT
         */
        public PdfVisibilityExpression(int type) : base() {
            switch(type) {
            case OR:
                base.Add(PdfName.OR);
                break;
            case AND:
                base.Add(PdfName.AND);
                break;
            case NOT:
                base.Add(PdfName.NOT);
                break;
            default:
                throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));    
            } 
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#add(int, com.itextpdf.text.pdf.PdfObject)
         */
        public override void Add(int index, PdfObject element) {
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#add(com.itextpdf.text.pdf.PdfObject)
         */
        public override bool Add(PdfObject obj) {
            if (obj is PdfLayer)
                return base.Add(((PdfLayer)obj).Ref);
            if (obj is PdfVisibilityExpression)
                return base.Add(obj);
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#addFirst(com.itextpdf.text.pdf.PdfObject)
         */
        public override void AddFirst(PdfObject obj) {
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#add(float[])
         */
        public override bool Add(float[] values) {
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#add(int[])
         */
        public override bool Add(int[] values) {
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }
        
    }
}
