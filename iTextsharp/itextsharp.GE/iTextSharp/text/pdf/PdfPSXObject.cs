using System;

namespace iTextSharp.GE.text.pdf {
    /**
    * Implements the PostScript XObject.
    */
    public class PdfPSXObject : PdfTemplate {
        
        /** Creates a new instance of PdfPSXObject */
        protected PdfPSXObject() {
        }
        
        /**
        * Constructs a PSXObject
        * @param wr
        */
        public PdfPSXObject(PdfWriter wr) : base(wr) {
        }

        /**
        * Gets the stream representing this object.
        *
        * @param   compressionLevel    the compressionLevel
        * @return the stream representing this template
        * @since   2.1.3   (replacing the method without param compressionLevel)
        * @throws IOException
        */
        override public PdfStream GetFormXObject(int compressionLevel) {
            PdfStream s = new PdfStream(content.ToByteArray());
            s.Put(PdfName.TYPE, PdfName.XOBJECT);
            s.Put(PdfName.SUBTYPE, PdfName.PS);
            s.FlateCompress(compressionLevel);
            return s;
        }
            
        /**
        * Gets a duplicate of this <CODE>PdfPSXObject</CODE>. All
        * the members are copied by reference but the buffer stays different.
        * @return a copy of this <CODE>PdfPSXObject</CODE>
        */
        
        public override PdfContentByte Duplicate {
            get {
                PdfPSXObject tpl = new PdfPSXObject();
                tpl.writer = writer;
                tpl.pdf = pdf;
                tpl.thisReference = thisReference;
                tpl.pageResources = pageResources;
                tpl.separator = separator;
                return tpl;
            }
        }
    }
}
