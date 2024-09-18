using System;
using System.IO;
using System.Collections;
using System.Net;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.Ocsp;
using iTextSharp.GE.text.error_messages;
using iTextSharp.GE.text.log;

namespace iTextSharp.GE.text.pdf.security {

    /**
    * OcspClient implementation using BouncyCastle.
    * @author Paulo Soares
    */
    public class OcspClientBouncyCastle : IOcspClient {
        private static readonly ILogger LOGGER = LoggerFactory.GetLogger(typeof(OcspClientBouncyCastle));
        
        private readonly OcspVerifier verifier;

        /**
         * Create default implemention of {@code OcspClient}.
         * Note, if you use this constructor, OCSP response will not be verified.
         */
        [Obsolete]
        public OcspClientBouncyCastle() {
            verifier = null;
        }

        /**
         * Create {@code OcspClient}
         * @param verifier will be used for response verification. {@see OCSPVerifier}.
         */
        public OcspClientBouncyCastle(OcspVerifier verifier) {
            this.verifier = verifier;
        }

        /**
         * Gets OCSP response. If {@see OCSPVerifier} was set, the response will be checked.
         */
        public virtual BasicOcspResp GetBasicOCSPResp(X509Certificate checkCert, X509Certificate rootCert, String url) {
            try {
                OcspResp ocspResponse = GetOcspResponse(checkCert, rootCert, url);
                if (ocspResponse == null) {
                    return null;
                }
                if (ocspResponse.Status != OcspRespStatus.Successful) {
                    return null;
                }
                BasicOcspResp basicResponse = (BasicOcspResp) ocspResponse.GetResponseObject();
                if (verifier != null) {
                    verifier.IsValidResponse(basicResponse, rootCert);
                }
                return basicResponse;
            } catch (Exception ex) {
                if (LOGGER.IsLogging(Level.ERROR))
                    LOGGER.Error(ex.Message);
            }
            return null;
        }

        /**
         * Gets an encoded byte array with OCSP validation. The method should not throw an exception.
         *
         * @param checkCert to certificate to check
         * @param rootCert  the parent certificate
         * @param url       to get the verification. It it's null it will be taken
         *                  from the check cert or from other implementation specific source
         * @return a byte array with the validation or null if the validation could not be obtained
         */
        public byte[] GetEncoded(X509Certificate checkCert, X509Certificate rootCert, String url) {
            try {
                BasicOcspResp basicResponse = GetBasicOCSPResp(checkCert, rootCert, url);
                if (basicResponse != null) {
                    SingleResp[] responses = basicResponse.Responses;
                    if (responses.Length == 1) {
                        SingleResp resp = responses[0];
                        Object status = resp.GetCertStatus();
                        if (status == CertificateStatus.Good) {
                            return basicResponse.GetEncoded();
                        } else if (status is RevokedStatus) {
                            throw new IOException(MessageLocalization.GetComposedMessage("ocsp.status.is.revoked"));
                        } else {
                            throw new IOException(MessageLocalization.GetComposedMessage("ocsp.status.is.unknown"));
                        }
                    }
                }
            } catch (Exception ex) {
                if (LOGGER.IsLogging(Level.ERROR))
                    LOGGER.Error(ex.Message);
            }
            return null;
        }

        /**
        * Generates an OCSP request using BouncyCastle.
        * @param issuerCert	certificate of the issues
        * @param serialNumber	serial number
        * @return	an OCSP request
        * @throws OCSPException
        * @throws IOException
        */
        private static OcspReq GenerateOCSPRequest(X509Certificate issuerCert, BigInteger serialNumber) {
            // Generate the id for the certificate we are looking for
            CertificateID id = new CertificateID(CertificateID.HashSha1, issuerCert, serialNumber);
            
            // basic request generation with nonce
            OcspReqGenerator gen = new OcspReqGenerator();
            gen.AddRequest(id);
            
            // create details for nonce extension
            IDictionary extensions = new Hashtable();
            
            extensions[OcspObjectIdentifiers.PkixOcspNonce] = new X509Extension(false, new DerOctetString(new DerOctetString(PdfEncryption.CreateDocumentId()).GetEncoded()));
            
            gen.SetRequestExtensions(new X509Extensions(extensions));
            return gen.Generate();
        }
        
        private OcspResp GetOcspResponse(X509Certificate checkCert, X509Certificate rootCert, String url) {
            if (checkCert == null || rootCert == null)
                return null;
            if (url == null) {
                url = CertificateUtil.GetOCSPURL(checkCert);
            }
            if (url == null)
                return null;
            LOGGER.Info("Getting OCSP from " + url);
            OcspReq request = GenerateOCSPRequest(rootCert, checkCert.SerialNumber);
            byte[] array = request.GetEncoded();
            
            HttpWebRequest con = (HttpWebRequest)WebRequest.Create(url);
            con.ContentLength = array.Length;
            con.ContentType = "application/ocsp-request";
            con.Accept = "application/ocsp-response";
            con.Method = "POST";
            Stream outp = con.GetRequestStream();
            outp.Write(array, 0, array.Length);
            outp.Close();
            HttpWebResponse response = (HttpWebResponse)con.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new IOException(MessageLocalization.GetComposedMessage("invalid.http.response.1", (int)response.StatusCode));
            Stream inp = response.GetResponseStream();
            OcspResp ocspResponse = new OcspResp(inp);
            inp.Close();
            response.Close();
            return ocspResponse;
        }
    }
}
