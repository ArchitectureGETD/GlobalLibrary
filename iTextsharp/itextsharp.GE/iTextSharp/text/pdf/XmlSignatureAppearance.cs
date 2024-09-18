using System;
using Org.BouncyCastle.X509;
using iTextSharp.GE.text.pdf.security;

namespace iTextSharp.GE.text.pdf
{
    public class XmlSignatureAppearance {
         
        /**
         * Constructs XmlSignatureAppearance object.
         * @param writer the writer to which the signature will be written.
         */
        internal XmlSignatureAppearance(PdfStamperImp writer) {
            this.writer = writer;
        }

        private PdfStamperImp writer;
        private PdfStamper stamper;
        private X509Certificate signCertificate;
        private IXmlLocator xmlLocator;
        private IXpathConstructor xpathConstructor;
        
        /** Holds value of property xades:SigningTime. */
        private DateTime signDate = DateTime.MinValue;

        /** Holds value of property xades:Description. */
        private String description;

        /** Holds value of property xades:MimeType. */
        private String mimeType = "text/xml";



        virtual public PdfStamperImp GetWriter() {
            return writer;
        }

        virtual public PdfStamper GetStamper() {
            return stamper;
        }

        virtual public void SetStamper(PdfStamper stamper) {
            this.stamper = stamper;
        }

        /**
         * Sets the certificate used to provide the text in the appearance.
         * This certificate doesn't take part in the actual signing process.
         * @param signCertificate the certificate
         */
        virtual public void SetCertificate(X509Certificate signCertificate) {
            this.signCertificate = signCertificate;
        }

        virtual public X509Certificate GetCertificate() {
            return signCertificate;
        }


        virtual public void SetDescription(String description) {
            this.description = description;
        }

        virtual public String GetDescription() {
            return description;
        }

        virtual public String GetMimeType() {
            return mimeType;
        }

        virtual public void SetMimeType(String mimeType) {
            this.mimeType = mimeType;
        }

        /**
         * Gets the signature date.
         * @return the signature date
         */
        virtual public DateTime GetSignDate() {
            if(signDate == DateTime.MinValue)
                signDate = DateTime.Now;
            return signDate;
        }

        /**
         * Sets the signature date.
         * @param signDate the signature date
         */
        virtual public void SetSignDate(DateTime signDate) {
            this.signDate = signDate;
        }

        /**
         * Helps to locate xml stream
         * @return XmlLocator, cannot be null.
         */
        virtual public IXmlLocator GetXmlLocator() {
            return xmlLocator;
        }


        virtual public void SetXmlLocator(IXmlLocator xmlLocator) {
            this.xmlLocator = xmlLocator;
        }

        /**
         * Constructor for xpath expression in case signing only part of XML document.
         * @return XpathConstructor, can be null
         */
        virtual public IXpathConstructor GetXpathConstructor() {
            return xpathConstructor;
        }

        virtual public void SetXpathConstructor(IXpathConstructor xpathConstructor) {
            this.xpathConstructor = xpathConstructor;
        }

        /**
         * Close PdfStamper
         * @throws IOException
         * @throws DocumentException
         */
        virtual public void Close() {
            writer.Close(stamper.MoreInfo);
        }
    }
}
