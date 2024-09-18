using System.Xml;
namespace iTextSharp.GE.text.pdf.security
{
    /**
     * Helps to locate xml stream
     */
    public interface IXmlLocator
    {
        XmlDocument GetDocument();

        void SetDocument(XmlDocument document);

        string GetEncoding();
    }
}
