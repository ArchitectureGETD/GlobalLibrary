using System;
using System.IO;
namespace iTextSharp.GE.text.io {

    /**
     * An input stream that uses a RandomAccessSource as it's underlying source 
     * @since 5.3.5
     */
    public class RASInputStream : Stream {
        /**
         * The source
         */
        private readonly IRandomAccessSource source;
        /**
         * The current position in the source
         */
        private long position = 0;

        /**
         * Creates an input stream based on the source
         * @param source the source
         */
        public RASInputStream(IRandomAccessSource source) {
            this.source = source;
        }

        public override bool CanRead {
            get { return true; }
        }

        public override bool CanSeek {
            get { return true; }
        }

        public override bool CanWrite {
            get { return false; }
        }

        public override void Flush() {
        }

        public override long Length {
            get { return source.Length; }
        }

        public override long Position {
            get {
                return position; ;
            }
            set {
                position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int len) {
            int count = source.Get(position, buffer, offset, len);
            if (count == -1)
                return 0;
            position += count;
            return count;
        }

        public override int ReadByte() {
            int c = source.Get(position);
            if (c >= 0)
                ++position;
            return c;
        }

        public override long Seek(long offset, SeekOrigin origin) {
            switch (origin) {
                case SeekOrigin.Begin:
                    position = offset;
                    break;
                case SeekOrigin.Current:
                    position += offset;
                    break;
                default:
                    position = offset + source.Length;
                    break;
            }
            return position;
        }

        public override void SetLength(long value) {
            throw new Exception("Not supported.");
        }

        public override void Write(byte[] buffer, int offset, int count) {
            throw new Exception("Not supported.");
        }
    }
}
