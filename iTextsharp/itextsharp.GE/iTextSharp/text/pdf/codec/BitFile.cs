using System;
using System.IO;
namespace iTextSharp.GE.text.pdf.codec {

    /**
     * Came from GIFEncoder initially.
     * Modified - to allow for output compressed data without the block counts
     * which breakup the compressed data stream for GIF.
     **/
    public class BitFile {
        Stream output_;
        byte[] buffer_;
        int index_;
        int bitsLeft_;	// bits left at current index that are avail.

        /** note this also indicates gif format BITFile. **/
        bool blocks_ = false;

        /**
         * @param output destination for output data
         * @param blocks GIF LZW requires block counts for output data
         **/
        public BitFile(Stream output, bool blocks) {
            output_ = output;
            blocks_ = blocks;
            buffer_ = new byte[256];
            index_ = 0;
            bitsLeft_ = 8;
        }

        virtual public void Flush() {
            int numBytes = index_ + (bitsLeft_ == 8 ? 0 : 1);
            if (numBytes > 0) {
                if (blocks_)
                    output_.WriteByte((byte)numBytes);
                output_.Write(buffer_, 0, numBytes);
                buffer_[0] = 0;
                index_ = 0;
                bitsLeft_ = 8;
            }
        }

        virtual public void WriteBits(int bits, int numbits) {
            int bitsWritten = 0;
            int numBytes = 255;		// gif block count
            do {
                // This handles the GIF block count stuff
                if ((index_ == 254 && bitsLeft_ == 0) || index_ > 254) {
                    if (blocks_)
                        output_.WriteByte((byte)numBytes);

                    output_.Write(buffer_, 0, numBytes);

                    buffer_[0] = 0;
                    index_ = 0;
                    bitsLeft_ = 8;
                }

                if (numbits <= bitsLeft_) { // bits contents fit in current index byte
                    if (blocks_) { // GIF
                        buffer_[index_] |= (byte)((bits & ((1 << numbits) - 1)) << (8 - bitsLeft_));
                        bitsWritten += numbits;
                        bitsLeft_ -= numbits;
                        numbits = 0;
                    }
                    else {
                        buffer_[index_] |= (byte)((bits & ((1 << numbits) - 1)) << (bitsLeft_ - numbits));
                        bitsWritten += numbits;
                        bitsLeft_ -= numbits;
                        numbits = 0;

                    }
                }
                else {	// bits overflow from current byte to next.
                    if (blocks_) { // GIF
                        // if bits  > space left in current byte then the lowest order bits
                        // of code are taken and put in current byte and rest put in next.
                        buffer_[index_] |= (byte)((bits & ((1 << bitsLeft_) - 1)) << (8 - bitsLeft_));
                        bitsWritten += bitsLeft_;
                        bits >>= bitsLeft_;
                        numbits -= bitsLeft_;
                        buffer_[++index_] = 0;
                        bitsLeft_ = 8;
                    }
                    else {
                        // if bits  > space left in current byte then the highest order bits
                        // of code are taken and put in current byte and rest put in next.
                        // at highest order bit location !! 
                        int topbits = (byte)(((uint)bits >> (numbits - bitsLeft_)) & ((1 << bitsLeft_) - 1));
                        buffer_[index_] |= (byte)topbits;
                        numbits -= bitsLeft_;	// ok this many bits gone off the top
                        bitsWritten += bitsLeft_;
                        buffer_[++index_] = 0;	// next index
                        bitsLeft_ = 8;
                    }
                }

            } while (numbits != 0);
        }
    }
}
