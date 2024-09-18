using System;
using System.IO;


namespace iTextSharp.GE.text.pdf
{

    /**
     * This class implements an output stream which can be used for memory limits aware decompression of pdf streams.
     */
    internal class MemoryLimitsAwareOutputStream : MemoryStream {

        /**
         * The maximum size of array to allocate.
         * Attempts to allocate larger arrays will result in an exception.
         */
        private static readonly int DEFAULT_MAX_STREAM_SIZE = int.MaxValue - 8;

        /**
         * The maximum size of array to allocate.
         * Attempts to allocate larger arrays will result in an exception.
         */
        private int maxStreamSize = DEFAULT_MAX_STREAM_SIZE;

        /**
         * Creates a new byte array output stream. The buffer capacity is
         * initially 32 bytes, though its size increases if necessary.
         */
        public MemoryLimitsAwareOutputStream() : base()
        {
        }

        /**
         * Creates a new byte array output stream, with a buffer capacity of
         * the specified size, in bytes.
         *
         * @param size the initial size.
         * @throws IllegalArgumentException if size is negative.
         */
        public MemoryLimitsAwareOutputStream(int size) : base(size)
        {
        }

        /**
         * Gets the maximum size which can be occupied by this output stream.
         *
         * @return the maximum size which can be occupied by this output stream.
         */
        public long GetMaxStreamSize()
        {
            return maxStreamSize;
        }

        /**
         * Sets the maximum size which can be occupied by this output stream.
         *
         * @param maxStreamSize the maximum size which can be occupied by this output stream.
         * @return this {@link MemoryLimitsAwareOutputStream}
         */
        public MemoryLimitsAwareOutputStream SetMaxStreamSize(int maxStreamSize)
        {
            this.maxStreamSize = maxStreamSize;
            return this;
        }

        /**
         * {@inheritDoc}
         */
        public override void Write(byte[] b, int off, int len)
        {
            if ((off < 0) || (off > b.Length) || (len < 0) || ((off + len) - b.Length > 0))
            {
                throw new IndexOutOfRangeException();
            }
            int minCapacity = (int)this.Position + len;
            if (minCapacity < 0)
            {
                // overflow
                throw new MemoryLimitsAwareException(MemoryLimitsAwareException.DuringDecompressionSingleStreamOccupiedMoreThanMaxIntegerValue
                );
            }
            if (minCapacity > maxStreamSize)
            {
                throw new MemoryLimitsAwareException(MemoryLimitsAwareException.DuringDecompressionSingleStreamOccupiedMoreMemoryThanAllowed
                );
            }
            // calculate new capacity
            int oldCapacity = this.GetBuffer().Length;
            int newCapacity = oldCapacity << 1;
            if (newCapacity < 0 || newCapacity - minCapacity < 0)
            {
                // overflow
                newCapacity = minCapacity;
            }
            if (newCapacity - maxStreamSize > 0)
            {
                newCapacity = maxStreamSize;
                this.Capacity = newCapacity;
            }
            base.Write(b, off, len);
        }
    }
}
