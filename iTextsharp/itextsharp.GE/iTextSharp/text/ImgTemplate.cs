using System;
using iTextSharp.GE.text.error_messages;

using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text {
    /// <summary>
    /// PdfTemplate that has to be inserted into the document
    /// </summary>
    /// <seealso cref="T:iTextSharp.GE.text.Element"/>
    /// <seealso cref="T:iTextSharp.GE.text.Image"/>
    public class ImgTemplate : Image {
    
        /// <summary>
        /// Creats an Image from a PdfTemplate.
        /// </summary>
        /// <param name="image">the Image</param>
        public ImgTemplate(Image image) : base(image) {}
    
        /// <summary>
        /// Creats an Image from a PdfTemplate.
        /// </summary>
        /// <param name="template">the PdfTemplate</param>
        public ImgTemplate(PdfTemplate template) : base((Uri)null) {
            if (template == null)
                throw new BadElementException(MessageLocalization.GetComposedMessage("the.template.can.not.be.null"));
            if (template.Type == PdfTemplate.TYPE_PATTERN)
                throw new BadElementException(MessageLocalization.GetComposedMessage("a.pattern.can.not.be.used.as.a.template.to.create.an.image"));
            type = Element.IMGTEMPLATE;
            scaledHeight = template.Height;
            this.Top = scaledHeight;
            scaledWidth = template.Width;
            this.Right = scaledWidth;
            TemplateData = template;
            plainWidth = this.Width;
            plainHeight = this.Height;
        }
    }
}
