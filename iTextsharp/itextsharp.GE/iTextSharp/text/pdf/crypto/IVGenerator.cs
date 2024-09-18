using System;

namespace iTextSharp.GE.text.pdf.crypto {

/**
 * An initialization vector generator for a CBC block encryption. It's a random generator based on RC4.
 * @author Paulo Soares
 */
    public sealed class IVGenerator {
        
        private static ARCFOUREncryption rc4;
        
        static IVGenerator(){
            rc4 = new ARCFOUREncryption();
            byte[] longBytes = new byte[8];
            long val = DateTime.Now.Ticks;
            for (int i = 0; i != 8; i++) {
                longBytes[i] = (byte)val;
                val = (long)((ulong)val >> 8);
            }
            rc4.PrepareARCFOURKey(longBytes);
        }
        
        /** Creates a new instance of IVGenerator */
        private IVGenerator() {
        }
        
    /**
     * Gets a 16 byte random initialization vector.
     * @return a 16 byte random initialization vector
     */
        public static byte[] GetIV() {
            return GetIV(16);
        }

        /**
        * Gets a random initialization vector.
        * @param len the length of the initialization vector
        * @return a random initialization vector
        */
        public static byte[] GetIV(int len) {
            byte[] b = new byte[len];
            lock (rc4) {
                rc4.EncryptARCFOUR(b);
            }
            return b;
        }    
    }
}
