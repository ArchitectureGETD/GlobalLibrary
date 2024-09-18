using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.X509;
using iTextSharp.GE.text.log;
using iTextSharp.GE.text.error_messages;
namespace iTextSharp.GE.text.pdf.security {

    /**
     * An implementation of the CrlClient that fetches the CRL bytes
     * from an URL.
     */
    public class CrlClientOnline : ICrlClient {

        /** The Logger instance. */
        private static readonly ILogger LOGGER = LoggerFactory.GetLogger(typeof(CrlClientOnline));
        
        /** The URLs of the CRLs. */
        protected IList<string> urls = new List<string>();

        /**
         * Creates a CrlClientOnline instance that will try to find
         * a single CRL by walking through the certificate chain.
         */
        public CrlClientOnline() {
        }
        
        /**
         * Creates a CrlClientOnline instance using one or more URLs.
         */
        public CrlClientOnline(params String[] crls) {
            foreach (String url in crls) {
                AddUrl(url);
            }
        }
        
        /**
         * Creates a CrlClientOnline instance using a certificate chain.
         */
        public CrlClientOnline(ICollection<X509Certificate> chain) {
            foreach (X509Certificate cert in chain) {
                String url = null;
                try {
                    LOGGER.Info("Checking certificate: " + cert.SubjectDN.ToString());
                    url = CertificateUtil.GetCRLURL(cert);
                    if (url != null) {
                        AddUrl(url);
                    }
                }
                catch (CertificateParsingException)
                {
                    LOGGER.Info("Skipped CRL url: (certificate could not be parsed)");
                }
            }
        }

        /**
         * Adds an URL to the list of CRL URLs
         * @param url	an URL in the form of a String
         */
        virtual protected void AddUrl(String url)
        {
            if (urls.Contains(url))
            {
                LOGGER.Info("Skipped CRL url (duplicate): " + url);
                return;
            }

            LOGGER.Info("Added CRL url: " + url);
            urls.Add(url);
        }

        /**
         * Fetches the CRL bytes from an URL.
         * If no url is passed as parameter, the url will be obtained from the certificate.
         * If you want to load a CRL from a local file, subclass this method and pass an
         * URL with the path to the local file to this method. An other option is to use
         * the CrlClientOffline class.
         * @see com.itextpdf.text.pdf.security.CrlClient#getEncoded(java.security.cert.X509Certificate, java.lang.String)
         */
        virtual public ICollection<byte[]> GetEncoded(X509Certificate checkCert, String url) {
            if (checkCert == null)
                return null;
            List<String> urllist = new List<string>(urls);
            if (urllist.Count == 0) {
                LOGGER.Info("Looking for CRL for certificate " + checkCert.SubjectDN.ToString());
                try {
                    if (url == null)
                        url = CertificateUtil.GetCRLURL(checkCert);
                    if (url == null)
                        throw new ArgumentNullException();
                    urllist.Add(url);
                    LOGGER.Info("Found CRL url: " + url);
                }
                catch (Exception e) {
                    LOGGER.Info("Skipped CRL url: " + e.Message);
                }
            }
            List<byte[]> ar = new List<byte[]>();
            foreach (string urlt in urllist) {
                try {
                    LOGGER.Info("Checking CRL: " + urlt);
                    HttpWebRequest con = (HttpWebRequest)WebRequest.Create(urlt);
                    HttpWebResponse response = (HttpWebResponse)con.GetResponse();
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new IOException(MessageLocalization.GetComposedMessage("invalid.http.response.1", (int)response.StatusCode));
                    //Get Response
                    Stream inp = response.GetResponseStream();
                    byte[] buf = new byte[1024];
                    MemoryStream bout = new MemoryStream();
                    while (true) {
                        int n = inp.Read(buf, 0, buf.Length);
                        if (n <= 0)
                            break;
                        bout.Write(buf, 0, n);
                    }
                    inp.Close();
                    ar.Add(bout.ToArray());
                    LOGGER.Info("Added CRL found at: " + urlt);
                }
                catch (Exception e) {
                    LOGGER.Info("Skipped CRL: " + e.Message + " for " + urlt);
                }
            }
            return ar;
        }
    }
}
