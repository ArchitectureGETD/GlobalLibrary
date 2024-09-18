using System;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf {

    /**
    * The content where Type3 glyphs are written to.
    */
    public sealed class Type3Glyph : PdfContentByte {

        private PageResources pageResources;
        private bool colorized;
        
        private Type3Glyph() : base(null) {
        }
        
        internal Type3Glyph(PdfWriter writer, PageResources pageResources, float wx, float llx, float lly, float urx, float ury, bool colorized) : base(writer) {
            this.pageResources = pageResources;
            this.colorized = colorized;
            if (colorized) {
                content.Append(wx).Append(" 0 d0\n");
            }
            else {
                content.Append(wx).Append(" 0 ").Append(llx).Append(' ').Append(lly).Append(' ').Append(urx).Append(' ').Append(ury).Append(" d1\n");
            }
        }
        
        internal override PageResources PageResources {
            get {
                return pageResources;
            }
        }
        
        public override void AddImage(Image image, float a, float b, float c, float d, float e, float f, bool inlineImage) {
            if (!colorized && (!image.IsMask() || !(image.Bpc == 1 || image.Bpc > 0xff)))
                throw new DocumentException(MessageLocalization.GetComposedMessage("not.colorized.typed3.fonts.only.accept.mask.images"));
            base.AddImage(image, a, b, c, d, e, f, inlineImage);
        }

        public PdfContentByte GetDuplicate() {
            Type3Glyph dup = new Type3Glyph();
            dup.writer = writer;
            dup.pdf = pdf;
            dup.pageResources = pageResources;
            dup.colorized = colorized;
            return dup;
        }    
    }
}
