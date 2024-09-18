using System;

namespace iTextSharp.GE.text.pdf {

    /**
     * <CODE>PdfFormObject</CODE> is a type of XObject containing a template-object.
     */

    public class PdfFormXObject : PdfStream {
    
        // public static variables
    
        /** This is a PdfNumber representing 0. */
        public static PdfNumber ZERO = new PdfNumber(0);
    
        /** This is a PdfNumber representing 1. */
        public static PdfNumber ONE = new PdfNumber(1);
    
        /** This is the 1 - matrix. */
        public static PdfLiteral MATRIX = new PdfLiteral("[1 0 0 1 0 0]");
    
        /**
         * Constructs a <CODE>PdfFormXObject</CODE>-object.
         *
         * @param        template        the template
         * @param   compressionLevel    the compression level for the stream
         * @since   2.1.3 (Replacing the existing constructor with param compressionLevel)
         */
    
        internal PdfFormXObject(PdfTemplate template, int compressionLevel) : base() {
            Put(PdfName.TYPE, PdfName.XOBJECT);
            Put(PdfName.SUBTYPE, PdfName.FORM);
            Put(PdfName.RESOURCES, template.Resources);
            Put(PdfName.BBOX, new PdfRectangle(template.BoundingBox));
            Put(PdfName.FORMTYPE, ONE);
            PdfArray matrix = template.Matrix;
            if (template.Layer != null)
                Put(PdfName.OC, template.Layer.Ref);
            if (template.Group != null)
                Put(PdfName.GROUP, template.Group);
            if (matrix == null)
                Put(PdfName.MATRIX, MATRIX);
            else
                Put(PdfName.MATRIX, matrix);
            bytes = template.ToPdf(null);
            Put(PdfName.LENGTH, new PdfNumber(bytes.Length));
            if (template.Additional != null) {
                Merge(template.Additional);
            }
            FlateCompress(compressionLevel);
        }
    
    }
}
