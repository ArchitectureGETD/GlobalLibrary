using System;
using System.Text;
using System.Xml;
using iTextSharp.GE.text.pdf.security;
namespace iTextSharp.GE.text.pdf
{
    /**
     * Constructor for xpath expression for signing XfaForm
     */
    public class XfaXpathConstructor : IXpathConstructor
    {
        /**
         * Possible xdp packages to sign
         */
        public enum XdpPackage {
            Config,
            ConnectionSet,
            Datasets,
            LocaleSet,
            Pdf,
            SourceSet,
            Stylesheet,
            Template,
            Xdc,
            Xfdf,
            Xmpmeta
        }

        private const String CONFIG = "config";
        private const String CONNECTIONSET = "connectionSet";
        private const String DATASETS = "datasets";
        private const String LOCALESET = "localeSet";
        private const String PDF = "pdf";
        private const String SOURCESET = "sourceSet";
        private const String STYLESHEET = "stylesheet";
        private const String TEMPLATE = "template";
        private const String XDC = "xdc";
        private const String XFDF = "xfdf";
        private const String XMPMETA = "xmpmeta";

        /**
         * Empty constructor, no transform.
         */
        public XfaXpathConstructor() {
            this.xpathExpression = "";
        }

        /**
         * Construct for Xpath expression. Depends from selected xdp package.
         * @param xdpPackage
         */
        public XfaXpathConstructor(XdpPackage xdpPackage) {
            String strPackage;
            switch (xdpPackage) {
                case XdpPackage.Config:
                    strPackage = CONFIG;
                    break;
                case XdpPackage.ConnectionSet:
                    strPackage = CONNECTIONSET;
                    break;
                case XdpPackage.Datasets:
                    strPackage = DATASETS;
                    break;
                case XdpPackage.LocaleSet:
                    strPackage = LOCALESET;
                    break;
                case XdpPackage.Pdf:
                    strPackage = PDF;
                    break;
                case XdpPackage.SourceSet:
                    strPackage = SOURCESET;
                    break;
                case XdpPackage.Stylesheet:
                    strPackage = STYLESHEET;
                    break;
                case XdpPackage.Template:
                    strPackage = TEMPLATE;
                    break;
                case XdpPackage.Xdc:
                    strPackage = XDC;
                    break;
                case XdpPackage.Xfdf:
                    strPackage = XFDF;
                    break;
                case XdpPackage.Xmpmeta:
                    strPackage = XMPMETA;
                    break;
                default:
                    xpathExpression = "";
                    return;
            }

            StringBuilder builder = new StringBuilder("/xdp:xdp/*[local-name()='");
            builder.Append(strPackage).Append("']");
            xpathExpression = builder.ToString();
            namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("xdp", "http://ns.adobe.com/xdp/");
        }

        private String xpathExpression;
        private XmlNamespaceManager namespaceManager;

        /**
         * Get XPath expression
         */
        virtual public String GetXpathExpression() {
            return xpathExpression;
        }

        virtual public XmlNamespaceManager GetNamespaceManager() {
            return namespaceManager;
        }
    }
}
