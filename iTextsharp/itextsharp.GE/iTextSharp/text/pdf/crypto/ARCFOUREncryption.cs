using System;
namespace iTextSharp.GE.text.pdf.crypto {

    public class ARCFOUREncryption {

        private byte[] state = new byte[256];
        private int x;
        private int y;

        /** Creates a new instance of ARCFOUREncryption */
        public ARCFOUREncryption() {
        }
        
        virtual public void PrepareARCFOURKey(byte[] key) {
            PrepareARCFOURKey(key, 0, key.Length);
        }

        virtual public void PrepareARCFOURKey(byte[] key, int off, int len) {
            int index1 = 0;
            int index2 = 0;
            for (int k = 0; k < 256; ++k)
                state[k] = (byte)k;
            x = 0;
            y = 0;
            byte tmp;
            for (int k = 0; k < 256; ++k) {
                index2 = (key[index1 + off] + state[k] + index2) & 255;
                tmp = state[k];
                state[k] = state[index2];
                state[index2] = tmp;
                index1 = (index1 + 1) % len;
            }
        }

        virtual public void EncryptARCFOUR(byte[] dataIn, int off, int len, byte[] dataOut, int offOut) {
            int length = len + off;
            byte tmp;
            for (int k = off; k < length; ++k) {
                x = (x + 1) & 255;
                y = (state[x] + y) & 255;
                tmp = state[x];
                state[x] = state[y];
                state[y] = tmp;
                dataOut[k - off + offOut] = (byte)(dataIn[k] ^ state[(state[x] + state[y]) & 255]);
            }
        }

        virtual public void EncryptARCFOUR(byte[] data, int off, int len) {
            EncryptARCFOUR(data, off, len, data, off);
        }

        virtual public void EncryptARCFOUR(byte[] dataIn, byte[] dataOut) {
            EncryptARCFOUR(dataIn, 0, dataIn.Length, dataOut, 0);
        }

        virtual public void EncryptARCFOUR(byte[] data) {
            EncryptARCFOUR(data, 0, data.Length, data, 0);
        }   
    }
}
