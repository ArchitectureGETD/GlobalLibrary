using System;
using System.IO;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using System.Text;
namespace iTextSharp.GE.text.pdf.security {

    /**
     * This class contains a series of static methods that
     * allow you to retrieve information from a Certificate.
     */
    public static class CertificateUtil {

        // Certificate Revocation Lists
        
        /**
         * Gets the URL of the Certificate Revocation List for a Certificate
         * @param certificate   the Certificate
         * @return  the String where you can check if the certificate was revoked
         * @throws CertificateParsingException
         * @throws IOException 
         */
        public static String GetCRLURL(X509Certificate certificate)  {
            try {
                Asn1Object obj = GetExtensionValue(certificate, X509Extensions.CrlDistributionPoints.Id);
                if (obj == null) {
                    return null;
                }
                CrlDistPoint dist = CrlDistPoint.GetInstance(obj);
                DistributionPoint[] dists = dist.GetDistributionPoints();
                foreach (DistributionPoint p in dists) {
                    DistributionPointName distributionPointName = p.DistributionPointName;
                    if (DistributionPointName.FullName != distributionPointName.PointType) {
                        continue;
                    }
                    GeneralNames generalNames = (GeneralNames)distributionPointName.Name;
                    GeneralName[] names = generalNames.GetNames();
                    foreach (GeneralName name in names) {
                        if (name.TagNo != GeneralName.UniformResourceIdentifier) {
                            continue;
                        }
                        DerIA5String derStr = DerIA5String.GetInstance((Asn1TaggedObject)name.ToAsn1Object(), false);
                        return derStr.GetString();
                    }
                }
            } catch {
            }
            return null;
        }

        // Online Certificate Status Protocol

        /**
         * Retrieves the OCSP URL from the given certificate.
         * @param certificate the certificate
         * @return the URL or null
         * @throws IOException
         */
        public static String GetOCSPURL(X509Certificate certificate) {
            try {
                Asn1Object obj = GetExtensionValue(certificate, X509Extensions.AuthorityInfoAccess.Id);
                if (obj == null) {
                    return null;
                }
                
                Asn1Sequence AccessDescriptions = (Asn1Sequence) obj;
                for (int i = 0; i < AccessDescriptions.Count; i++) {
                    Asn1Sequence AccessDescription = (Asn1Sequence) AccessDescriptions[i];
                    if ( AccessDescription.Count != 2 ) {
                        continue;
                    } else {
                        if ((AccessDescription[0] is DerObjectIdentifier) && ((DerObjectIdentifier)AccessDescription[0]).Id.Equals("1.3.6.1.5.5.7.48.1")) {
                            String AccessLocation =  GetStringFromGeneralName((Asn1Object)AccessDescription[1]);
                            if ( AccessLocation == null ) {
                                return "" ;
                            } else {
                                return AccessLocation ;
                            }
                        }
                    }
                }
            } catch {
            }
            return null;
        }

        // Time Stamp Authority

        /**
         * Gets the URL of the TSA if it's available on the certificate
         * @param certificate   a certificate
         * @return  a TSA URL
         * @throws IOException
         */
        public static String GetTSAURL(X509Certificate certificate) {
            Asn1OctetString octetString = certificate.GetExtensionValue(SecurityIDs.ID_TSA);
            if (octetString == null)
                return null;
            byte[] der = octetString.GetOctets();
            if (der == null)
                return null;
            Asn1Object asn1obj;
            try {
                asn1obj = Asn1Object.FromByteArray(der);
                if (asn1obj is DerOctetString) {
                    DerOctetString octets = (DerOctetString) asn1obj;
                    asn1obj = Asn1Object.FromByteArray(octets.GetOctets());
                }
                Asn1Sequence asn1seq = Asn1Sequence.GetInstance(asn1obj);
                return GetStringFromGeneralName(asn1seq[1].ToAsn1Object());
            } catch (IOException) {
                return null;
            }
        }
        
        // helper methods

        /**
         * @param certificate   the certificate from which we need the ExtensionValue
         * @param oid the Object Identifier value for the extension.
         * @return  the extension value as an ASN1Primitive object
         * @throws IOException
         */
        private static Asn1Object GetExtensionValue(X509Certificate cert, String oid) {
            byte[] bytes = cert.GetExtensionValue(new DerObjectIdentifier(oid)).GetDerEncoded();
            if (bytes == null) {
                return null;
            }
            Asn1InputStream aIn = new Asn1InputStream(new MemoryStream(bytes));
            Asn1OctetString octs = (Asn1OctetString) aIn.ReadObject();
            aIn = new Asn1InputStream(new MemoryStream(octs.GetOctets()));
            return aIn.ReadObject();
        }

        /**
         * Gets a String from an ASN1Primitive
         * @param names the ASN1Primitive
         * @return  a human-readable String
         * @throws IOException
         */
        private static String GetStringFromGeneralName(Asn1Object names) {
            Asn1TaggedObject taggedObject = (Asn1TaggedObject) names ;
            return Encoding.GetEncoding(1252).GetString(Asn1OctetString.GetInstance(taggedObject, false).GetOctets());
        }
    }
}
