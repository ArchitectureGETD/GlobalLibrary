using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
namespace iTextSharp.GE.text.pdf.crypto {

/**
 * Creates an AES Cipher with CBC and padding PKCS5/7.
 * @author Paulo Soares
 */
    public class AESCipher {
        private PaddedBufferedBlockCipher bp;
        
        /** Creates a new instance of AESCipher */
        public AESCipher(bool forEncryption, byte[] key, byte[] iv) {
            IBlockCipher aes = new AesEngine();
            IBlockCipher cbc = new CbcBlockCipher(aes);
            bp = new PaddedBufferedBlockCipher(cbc);
            KeyParameter kp = new KeyParameter(key);
            ParametersWithIV piv = new ParametersWithIV(kp, iv);
            bp.Init(forEncryption, piv);
        }
        
        virtual public byte[] Update(byte[] inp, int inpOff, int inpLen) {
            int neededLen = bp.GetUpdateOutputSize(inpLen);
            byte[] outp = null;
            if (neededLen > 0)
                outp = new byte[neededLen];
            else
                neededLen = 0;
            bp.ProcessBytes(inp, inpOff, inpLen, outp, 0);
            return outp;
        }
        
        virtual public byte[] DoFinal() {
            int neededLen = bp.GetOutputSize(0);
            byte[] outp = new byte[neededLen];
            int n = 0;
            try {
                n = bp.DoFinal(outp, 0);
            }
            catch {
                return outp;
            }
            if (n != outp.Length) {
                byte[] outp2 = new byte[n];
                System.Array.Copy(outp, 0, outp2, 0, n);
                return outp2;
            }
            else
                return outp;
        }
        
    }
}
