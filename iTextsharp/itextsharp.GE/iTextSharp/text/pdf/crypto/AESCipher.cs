using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
namespace iTextSharp.GE.text.pdf.crypto {

/**
 * Creates an AES Cipher with CBC and no padding.
 * @author Paulo Soares
 */
    public class AESCipherCBCnoPad {
        private IBlockCipher cbc;
        
        /** Creates a new instance of AESCipher */
        public AESCipherCBCnoPad(bool forEncryption, byte[] key) {
            IBlockCipher aes = new AesEngine();
            cbc = new CbcBlockCipher(aes);
            KeyParameter kp = new KeyParameter(key);
            cbc.Init(forEncryption, kp);
        }
        
        virtual public byte[] ProcessBlock(byte[] inp, int inpOff, int inpLen) {
            if ((inpLen % cbc.GetBlockSize()) != 0)
                throw new ArgumentException("Not multiple of block: " + inpLen);
            byte[] outp = new byte[inpLen];
            int baseOffset = 0;
            while (inpLen > 0) {
                cbc.ProcessBlock(inp, inpOff, outp, baseOffset);
                inpLen -= cbc.GetBlockSize();
                baseOffset += cbc.GetBlockSize();
                inpOff += cbc.GetBlockSize();
            }
            return outp;
        }        
    }
}
