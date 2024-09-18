using System;
using System.Collections.Generic;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf.security {

    /**
     * PAdES-LTV Timestamp
     * */
    public static class LtvTimestamp {
        /**
         * Signs a document with a PAdES-LTV Timestamp. The document is closed at the end.
         * @param sap the signature appearance
         * @param tsa the timestamp generator
         * @param signatureName the signature name or null to have a name generated
         * automatically
         * @throws Exception
         */
        public static void Timestamp(PdfSignatureAppearance sap, ITSAClient tsa, String signatureName) {
            int contentEstimated = tsa.GetTokenSizeEstimate();
            sap.AddDeveloperExtension(PdfDeveloperExtension.ESIC_1_7_EXTENSIONLEVEL5);
            sap.SetVisibleSignature(new Rectangle(0,0,0,0), 1, signatureName);

            PdfSignature dic = new PdfSignature(PdfName.ADOBE_PPKLITE, PdfName.ETSI_RFC3161);
            dic.Put(PdfName.TYPE, PdfName.DOCTIMESTAMP);
            sap.CryptoDictionary = dic;

            Dictionary<PdfName,int> exc = new Dictionary<PdfName,int>();
            exc[PdfName.CONTENTS] = contentEstimated * 2 + 2;
            sap.PreClose(exc);
            Stream data = sap.GetRangeStream();
            IDigest messageDigest = tsa.GetMessageDigest();
            byte[] buf = new byte[4096];
            int n;
            while ((n = data.Read(buf, 0, buf.Length)) > 0) {
                messageDigest.BlockUpdate(buf, 0, n);
            }
            byte[] tsImprint = new byte[messageDigest.GetDigestSize()];
            messageDigest.DoFinal(tsImprint, 0);
            byte[] tsToken;
            try {
        	    tsToken = tsa.GetTimeStampToken(tsImprint);
            }
            catch(Exception e) {
        	    throw new GeneralSecurityException(e.Message);
            }
            if (contentEstimated + 2 < tsToken.Length)
                throw new IOException("Not enough space");

            byte[] paddedSig = new byte[contentEstimated];
            System.Array.Copy(tsToken, 0, paddedSig, 0, tsToken.Length);

            PdfDictionary dic2 = new PdfDictionary();
            dic2.Put(PdfName.CONTENTS, new PdfString(paddedSig).SetHexWriting(true));
            sap.Close(dic2);
        }
    }
}
