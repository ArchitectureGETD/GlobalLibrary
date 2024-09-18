using System;
using System.IO;
using iTextSharp.GE.text.pdf.crypto;

namespace iTextSharp.GE.text.pdf {
    public class OutputStreamEncryption : Stream {
    protected Stream outc;
    protected ARCFOUREncryption arcfour;
    protected AESCipher cipher;
    private byte[] buf = new byte[1];
    private const int AES_128 = 4;
    private const int AES_256 = 5;
    private bool aes;
    private bool finished;

        public OutputStreamEncryption(Stream outc, byte[] key, int off, int len, int revision) {
            this.outc = outc;
            aes = (revision == AES_128 || revision == AES_256);
            if (aes) {
                byte[] iv = IVGenerator.GetIV();
                byte[] nkey = new byte[len];
                System.Array.Copy(key, off, nkey, 0, len);
                cipher = new AESCipher(true, nkey, iv);
                Write(iv, 0, iv.Length);
            }
            else {
                arcfour = new ARCFOUREncryption();
                arcfour.PrepareARCFOURKey(key, off, len);
            }
        }  

        public OutputStreamEncryption(Stream outc, byte[] key, int revision) : this(outc, key, 0, key.Length, revision) {
        }

        public override bool CanRead {
            get {
                return false;
            }
        }
    
        public override bool CanSeek {
            get {
                return false;
            }
        }
    
        public override bool CanWrite {
            get {
                return true;
            }
        }
    
        public override long Length {
            get {
                throw new NotSupportedException();
            }
        }
    
        public override long Position {
            get {
                throw new NotSupportedException();
            }
            set {
                throw new NotSupportedException();
            }
        }
    
        public override void Flush() {
            outc.Flush();
        }
    
        public override int Read(byte[] buffer, int offset, int count) {
            throw new NotSupportedException();
        }
    
        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotSupportedException();
        }
    
        public override void SetLength(long value) {
            throw new NotSupportedException();
        }
    
        public override void Write(byte[] b, int off, int len) {
            if (aes) {
                byte[] b2 = cipher.Update(b, off, len);
                if (b2 == null || b2.Length == 0)
                    return;
                outc.Write(b2, 0, b2.Length);
            }
            else {
                byte[] b2 = new byte[Math.Min(len, 4192)];
                while (len > 0) {
                    int sz = Math.Min(len, b2.Length);
                    arcfour.EncryptARCFOUR(b, off, sz, b2, 0);
                    outc.Write(b2, 0, sz);
                    len -= sz;
                    off += sz;
                }
            }
        }
    
        public override void Close() {
            Finish();
            outc.Close();
        }
    
        public override void WriteByte(byte value) {
            buf[0] = value;
            Write(buf, 0, 1);
        }

        virtual public void Finish() {
            if (!finished) {
                finished = true;
                if (aes) {
                    byte[] b = cipher.DoFinal();
                    outc.Write(b, 0, b.Length);
                }
            }
        }
    }
}
