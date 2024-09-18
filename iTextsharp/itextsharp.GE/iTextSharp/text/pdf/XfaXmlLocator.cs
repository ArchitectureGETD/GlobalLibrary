using System.Xml;
using iTextSharp.GE.text.pdf.security;
namespace iTextSharp.GE.text.pdf
{
    /**
     * Helps to locate xml stream inside PDF document with Xfa form.
     */
    public class XfaXmlLocator : IXmlLocator
    {
        public XfaXmlLocator(PdfStamper stamper) {
            this.stamper = stamper;
            CreateXfaForm();
        }

        private PdfStamper stamper;
        private XfaForm xfaForm;
        private string encoding;

        virtual protected void CreateXfaForm() {
            xfaForm = new XfaForm(stamper.Reader);
        }

        /**
         * Gets Document to sign
         */
        virtual public XmlDocument GetDocument() {
            return xfaForm.DomDocument;
        }

        /**
         * Save document as single XML stream in AcroForm.
         * @param document signed document
         * @throws IOException
         * @throws DocumentException
         */
        virtual public void SetDocument(XmlDocument document) {
            document.XmlResolver = null;
            byte[] outerXml = System.Text.Encoding.UTF8.GetBytes(document.OuterXml);
            //Create PdfStream
            PdfIndirectReference iref = stamper.Writer.
                    AddToBody(new PdfStream(outerXml)).IndirectReference;
            stamper.Reader.AcroForm.Put(PdfName.XFA, iref);
        }

        virtual public string GetEncoding() {
            return encoding;
        }

        virtual public void SetEncoding(string encoding) {
            this.encoding = encoding;
        }
    }
}
