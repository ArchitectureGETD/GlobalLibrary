using System;
using System.Xml;

namespace iTextSharp.GE.text.pdf.security
{
    /**
     * Constructor for XPath2 expression
     */
    public interface IXpathConstructor
    {
        /**
         * Get XPath2 expression
         */
        String GetXpathExpression();

        /**
         * Get XmlNamespaceManager to resolve namespace conflicts
         */
        XmlNamespaceManager GetNamespaceManager();
    }
}
