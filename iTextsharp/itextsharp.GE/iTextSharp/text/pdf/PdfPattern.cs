using System;

namespace iTextSharp.GE.text.pdf {

    /**
     * A <CODE>PdfPattern</CODE> defines a ColorSpace
     *
     * @see     PdfStream
     */

    public class PdfPattern : PdfStream {
    
        /**
        * Creates a PdfPattern object.
        * @param   painter a pattern painter instance
        */
        internal PdfPattern(PdfPatternPainter painter) : this(painter, DEFAULT_COMPRESSION) {
        }

        /**
        * Creates a PdfPattern object.
        * @param   painter a pattern painter instance
        * @param   compressionLevel the compressionLevel for the stream
        * @since   2.1.3
        */
        internal PdfPattern(PdfPatternPainter painter, int compressionLevel) : base() {
            PdfNumber one = new PdfNumber(1);
            PdfArray matrix = painter.Matrix;
            if ( matrix != null ) {
                Put(PdfName.MATRIX, matrix);
            }
            Put(PdfName.TYPE, PdfName.PATTERN);
            Put(PdfName.BBOX, new PdfRectangle(painter.BoundingBox));
            Put(PdfName.RESOURCES, painter.Resources);
            Put(PdfName.TILINGTYPE, one);
            Put(PdfName.PATTERNTYPE, one);
            if (painter.IsStencil())
                Put(PdfName.PAINTTYPE, new PdfNumber(2));
            else
                Put(PdfName.PAINTTYPE, one);
            Put(PdfName.XSTEP, new PdfNumber(painter.XStep));
            Put(PdfName.YSTEP, new PdfNumber(painter.YStep));
            bytes = painter.ToPdf(null);
            Put(PdfName.LENGTH, new PdfNumber(bytes.Length));
            FlateCompress(compressionLevel);
        }
    }
}
