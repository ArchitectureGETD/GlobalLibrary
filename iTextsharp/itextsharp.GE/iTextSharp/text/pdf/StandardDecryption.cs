using System;
using iTextSharp.GE.text.pdf.crypto;

namespace iTextSharp.GE.text.pdf.crypto {

    public class StandardDecryption {
        protected ARCFOUREncryption arcfour;
        protected AESCipher cipher;
        private byte[] key;
        private const int AES_128 = 4;
        private const int AES_256 = 5;
        private bool aes;
        private bool initiated;
        private byte[] iv = new byte[16];
        private int ivptr;

        /** Creates a new instance of StandardDecryption */
        public StandardDecryption(byte[] key, int off, int len, int revision) {
            aes = (revision == AES_128 || revision == AES_256);
            if (aes) {
                this.key = new byte[len];
                System.Array.Copy(key, off, this.key, 0, len);
            }
            else {
                arcfour = new ARCFOUREncryption();
                arcfour.PrepareARCFOURKey(key, off, len);
            }
        }
        
        virtual public byte[] Update(byte[] b, int off, int len) {
            if (aes) {
                if (initiated)
                    return cipher.Update(b, off, len);
                else {
                    int left = Math.Min(iv.Length - ivptr, len);
                    System.Array.Copy(b, off, iv, ivptr, left);
                    off += left;
                    len -= left;
                    ivptr += left;
                    if (ivptr == iv.Length) {
                        cipher = new AESCipher(false, key, iv);
                        initiated = true;
                        if (len > 0)
                            return cipher.Update(b, off, len);
                    }
                    return null;
                }
            }
            else {
                byte[] b2 = new byte[len];
                arcfour.EncryptARCFOUR(b, off, len, b2, 0);
                return b2;
            }
        }
        
        virtual public byte[] Finish() {
            if (cipher != null && aes) {
                return cipher.DoFinal();
            }
            else
                return null;
        }
    }
}
