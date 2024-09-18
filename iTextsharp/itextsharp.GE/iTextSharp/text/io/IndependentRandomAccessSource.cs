using System;
namespace iTextSharp.GE.text.io {

    /**
     * A RandomAccessSource that is wraps another RandomAccessSouce but does not propagate close().  This is useful when
     * passing a RandomAccessSource to a method that would normally close the source.
     * @since 5.3.5
     */
    public class IndependentRandomAccessSource : IRandomAccessSource {
        /**
         * The source
         */
        private readonly IRandomAccessSource source;
        
        
        /**
         * Constructs a new OffsetRandomAccessSource
         * @param source the source
         */
        public IndependentRandomAccessSource(IRandomAccessSource source) {
            this.source = source;
        }

        /**
         * {@inheritDoc}
         */
        public virtual int Get(long position) {
            return source.Get(position);
        }

        /**
         * {@inheritDoc}
         */
        public virtual int Get(long position, byte[] bytes, int off, int len) {
            return source.Get(position, bytes, off, len);
        }

        /**
         * {@inheritDoc}
         */
        virtual public long Length {
            get {
                return source.Length;
            }
        }

        /**
         * Does nothing - the underlying source is not closed
         */
        public virtual void Close() {
            // do not close the source
        }

        virtual public void Dispose() {
            Close();
        }
    }
}
