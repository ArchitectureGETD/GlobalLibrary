
using System;
using System.IO;

namespace iTextSharp.GE.xmp.impl {
    /// <summary>
    /// An <code>OutputStream</code> that counts the written bytes.
    /// 
    /// </summary>
    public sealed class CountOutputStream : Stream {
        /// <summary>
        /// the decorated output stream </summary>
        private readonly Stream _outp;

        /// <summary>
        /// the byte counter </summary>
        private int _bytesWritten;


        /// <summary>
        /// Constructor with providing the output stream to decorate. </summary>
        /// <param name="out"> an <code>OutputStream</code> </param>
        internal CountOutputStream(Stream outp) {
            _outp = outp;
        }


        /// <returns> the bytesWritten </returns>
        public int BytesWritten {
            get { return _bytesWritten; }
        }

        public override bool CanRead {
            get { return false; }
        }

        public override bool CanSeek {
            get { return false; }
        }

        public override bool CanWrite {
            get { return true; }
        }

        public override long Length {
            get { return BytesWritten; }
        }

        public override long Position {
            get { return Length; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        /// <summary>
        /// Counts the written bytes. </summary>
        /// <seealso cref= java.io.OutputStream#write(byte[], int, int) </seealso>
        public override void Write(byte[] buf, int off, int len) {
            _outp.Write(buf, off, len);
            _bytesWritten += len;
        }


        /// <summary>
        /// Counts the written bytes. </summary>
        /// <seealso cref= java.io.OutputStream#write(byte[]) </seealso>
        public void Write(byte[] buf) {
            Write(buf, 0, buf.Length);
        }


        /// <summary>
        /// Counts the written bytes. </summary>
        /// <seealso cref= java.io.OutputStream#write(int) </seealso>
        public void Write(int b) {
            _outp.WriteByte((byte) b);
            _bytesWritten++;
        }

        public override void Flush() {
            _outp.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetLength(long value) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int Read(byte[] buffer, int offset, int count) {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
