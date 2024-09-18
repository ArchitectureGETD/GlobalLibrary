using System;
using System.IO;
using System.Collections.Generic;
using iTextSharp.GE.text.pdf.crypto;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security;


namespace iTextSharp.GE.text.pdf {

    /**
    * @author Aiken Sam (aikensam@ieee.org)
    */
    public class PdfPublicKeySecurityHandler {
        
        private const int SEED_LENGTH = 20;
        
        private List<PdfPublicKeyRecipient> recipients = null;
        
        private byte[] seed;

        public PdfPublicKeySecurityHandler() {
            seed = IVGenerator.GetIV(SEED_LENGTH);
            recipients = new List<PdfPublicKeyRecipient>();
        }


        virtual public void AddRecipient(PdfPublicKeyRecipient recipient) {
            recipients.Add(recipient);
        }
        
        virtual protected internal byte[] GetSeed() {
            return (byte[])seed.Clone();
        }
        
        virtual public int GetRecipientsSize() {
            return recipients.Count;
        }
        
        virtual public byte[] GetEncodedRecipient(int index) {
            //Certificate certificate = recipient.GetX509();
            PdfPublicKeyRecipient recipient = recipients[index];
            byte[] cms = recipient.Cms;
            
            if (cms != null) return cms;
            
            X509Certificate certificate  = recipient.Certificate;
            int permission =  recipient.Permission;//PdfWriter.AllowCopy | PdfWriter.AllowPrinting | PdfWriter.AllowScreenReaders | PdfWriter.AllowAssembly;   
            int revision = 3;
            
            permission |= (int)(revision==3 ? (uint)0xfffff0c0 : (uint)0xffffffc0);
            permission &= unchecked((int)0xfffffffc);
            permission += 1;
          
            byte[] pkcs7input = new byte[24];
            
            byte one = (byte)(permission);
            byte two = (byte)(permission >> 8);
            byte three = (byte)(permission >> 16);
            byte four = (byte)(permission >> 24);

            System.Array.Copy(seed, 0, pkcs7input, 0, 20); // put this seed in the pkcs7 input
                                
            pkcs7input[20] = four;
            pkcs7input[21] = three;                
            pkcs7input[22] = two;
            pkcs7input[23] = one;

            Asn1Object obj = CreateDERForRecipient(pkcs7input, certificate);
                
            MemoryStream baos = new MemoryStream();
                
            DerOutputStream k = new DerOutputStream(baos);
                
            k.WriteObject(obj);  
            
            cms = baos.ToArray();

            recipient.Cms = cms;
            
            return cms;    
        }
        
        virtual public PdfArray GetEncodedRecipients() {
            PdfArray EncodedRecipients = new PdfArray();
            byte[] cms = null;
            for (int i=0; i<recipients.Count; i++) {
                try {
                    cms = GetEncodedRecipient(i);
                    EncodedRecipients.Add(new PdfLiteral(StringUtils.EscapeString(cms)));
                } catch {
                    EncodedRecipients = null;
                }
            }            
            return EncodedRecipients;
        }
        
        private Asn1Object CreateDERForRecipient(byte[] inp, X509Certificate cert) {
            
            String s = "1.2.840.113549.3.2";
            
            byte[] outp = new byte[100];
            DerObjectIdentifier derob = new DerObjectIdentifier(s);
            byte[] keyp = IVGenerator.GetIV(16);
            IBufferedCipher cf = CipherUtilities.GetCipher(derob);
            KeyParameter kp = new KeyParameter(keyp);
            byte[] iv = IVGenerator.GetIV(cf.GetBlockSize());
            ParametersWithIV piv = new ParametersWithIV(kp, iv);
            cf.Init(true, piv);
            int len = cf.DoFinal(inp, outp, 0);

            byte[] abyte1 = new byte[len];
            System.Array.Copy(outp, 0, abyte1, 0, len);
            DerOctetString deroctetstring = new DerOctetString(abyte1);
            KeyTransRecipientInfo keytransrecipientinfo = ComputeRecipientInfo(cert, keyp);
            DerSet derset = new DerSet(new RecipientInfo(keytransrecipientinfo));
            Asn1EncodableVector ev = new Asn1EncodableVector();
            ev.Add(new DerInteger(58));
            ev.Add(new DerOctetString(iv));
            DerSequence seq = new DerSequence(ev);
            AlgorithmIdentifier algorithmidentifier = new AlgorithmIdentifier(derob, seq);
            EncryptedContentInfo encryptedcontentinfo = 
                new EncryptedContentInfo(PkcsObjectIdentifiers.Data, algorithmidentifier, deroctetstring);
            Asn1Set set = null;
            EnvelopedData env = new EnvelopedData(null, derset, encryptedcontentinfo, set);
            Org.BouncyCastle.Asn1.Cms.ContentInfo contentinfo = 
                new Org.BouncyCastle.Asn1.Cms.ContentInfo(PkcsObjectIdentifiers.EnvelopedData, env);
            return contentinfo.ToAsn1Object();        
        }
        
        private KeyTransRecipientInfo ComputeRecipientInfo(X509Certificate x509certificate, byte[] abyte0) {
            Asn1InputStream asn1inputstream = 
                new Asn1InputStream(new MemoryStream(x509certificate.GetTbsCertificate()));
            TbsCertificateStructure tbscertificatestructure = 
                TbsCertificateStructure.GetInstance(asn1inputstream.ReadObject());
            AlgorithmIdentifier algorithmidentifier = tbscertificatestructure.SubjectPublicKeyInfo.AlgorithmID;
            Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber issuerandserialnumber = 
                new Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber(
                    tbscertificatestructure.Issuer, 
                    tbscertificatestructure.SerialNumber.Value);
            IBufferedCipher cipher = CipherUtilities.GetCipher(algorithmidentifier.Algorithm);
            cipher.Init(true, x509certificate.GetPublicKey());
            byte[] outp = new byte[10000];
            int len = cipher.DoFinal(abyte0, outp, 0);
            byte[] abyte1 = new byte[len];
            System.Array.Copy(outp, 0, abyte1, 0, len);
            DerOctetString deroctetstring = new DerOctetString(abyte1);
            RecipientIdentifier recipId = new RecipientIdentifier(issuerandserialnumber);
            return new KeyTransRecipientInfo( recipId, algorithmidentifier, deroctetstring);
        }        
    }
}
