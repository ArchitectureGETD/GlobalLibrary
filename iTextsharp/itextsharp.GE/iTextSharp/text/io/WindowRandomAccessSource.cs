using System;
namespace iTextSharp.GE.text.io {

    /**
     * A RandomAccessSource that wraps another RandomAccessSouce and provides a window of it at a specific offset and over
     * a specific length.  Position 0 becomes the offset position in the underlying source.
     * @since 5.3.5
     */
    public class WindowRandomAccessSource : IRandomAccessSource {
        /**
         * The source
         */
        private readonly IRandomAccessSource source;
        
        /**
         * The amount to offset the source by
         */
        private readonly long offset;
        
        /**
         * The length
         */
        private readonly long length;
        
        /**
         * Constructs a new OffsetRandomAccessSource that extends to the end of the underlying source
         * @param source the source
         * @param offset the amount of the offset to use
         */
        public WindowRandomAccessSource(IRandomAccessSource source, long offset) : this(source, offset, source.Length - offset) {
        }

        /**
         * Constructs a new OffsetRandomAccessSource with an explicit length
         * @param source the source
         * @param offset the amount of the offset to use
         * @param length the number of bytes to be included in this RAS
         */
        public WindowRandomAccessSource(IRandomAccessSource source, long offset, long length) {
            this.source = source;
            this.offset = offset;
            this.length = length;
        }
        
        /**
         * {@inheritDoc}
         * Note that the position will be adjusted to read from the corrected location in the underlying source
         */
        public virtual int Get(long position) {
            if (position >= length) return -1;
            return source.Get(offset + position);
        }

        /**
         * {@inheritDoc}
         * Note that the position will be adjusted to read from the corrected location in the underlying source
         */
        public virtual int Get(long position, byte[] bytes, int off, int len) {
            if (position >= length) 
                return -1;
            
            long toRead = Math.Min(len, length - position);
            return source.Get(offset + position, bytes, off, (int)toRead);
        }

        /**
         * {@inheritDoc}
         * Note that the length will be adjusted to read from the corrected location in the underlying source
         */
        public virtual long Length {
            get {
                return length;
            }
        }

        /**
         * {@inheritDoc}
         */
        public virtual void Close() {
            source.Close();
        }

        virtual public void Dispose() {
            Close();
        }
    }
}
