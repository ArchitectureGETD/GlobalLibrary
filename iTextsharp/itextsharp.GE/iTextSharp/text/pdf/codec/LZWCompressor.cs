using System;
using System.IO;

namespace iTextSharp.GE.text.pdf.codec {

    /**
     * Modified from original LZWCompressor to change interface to passing a
     * buffer of data to be compressed.
     **/
    public class LZWCompressor {
        /** base underlying code size of data being compressed 8 for TIFF, 1 to 8 for GIF **/
        int codeSize_;

        /** reserved clear code based on code size **/
        int clearCode_;

        /** reserved end of data code based on code size **/
        int endOfInfo_;

        /** current number bits output for each code **/
        int numBits_;

        /** limit at which current number of bits code size has to be increased **/
        int limit_;

        /** the prefix code which represents the predecessor string to current input point **/
        short prefix_;

        /** output destination for bit codes **/
        BitFile bf_;

        /** general purpose LZW string table **/
        LZWStringTable lzss_;

        /** modify the limits of the code values in LZW encoding due to TIFF bug / feature **/
        bool tiffFudge_;

        /**
         * @param outp destination for compressed data
         * @param codeSize the initial code size for the LZW compressor
         * @param TIFF flag indicating that TIFF lzw fudge needs to be applied
         * @exception IOException if underlying output stream error
         **/
        public LZWCompressor(Stream outp, int codeSize, bool TIFF) {
            bf_ = new BitFile(outp, !TIFF);	// set flag for GIF as NOT tiff
            codeSize_ = codeSize;
            tiffFudge_ = TIFF;
            clearCode_ = 1 << codeSize_;
            endOfInfo_ = clearCode_ + 1;
            numBits_ = codeSize_ + 1;

            limit_ = (1 << numBits_) - 1;
            if (tiffFudge_)
                --limit_;

            prefix_ = -1;
            lzss_ = new LZWStringTable();
            lzss_.ClearTable(codeSize_);
            bf_.WriteBits(clearCode_, numBits_);
        }

        /**
         * @param buf data to be compressed to output stream
         * @exception IOException if underlying output stream error
         **/
        virtual public void Compress(byte[] buf, int offset, int length) {
            int idx;
            byte c;
            short index;

            int maxOffset = offset + length;
            for (idx = offset; idx < maxOffset; ++idx) {
                c = buf[idx];
                if ((index = lzss_.FindCharString(prefix_, c)) != -1)
                    prefix_ = index;
                else {
                    bf_.WriteBits(prefix_, numBits_);
                    if (lzss_.AddCharString(prefix_, c) > limit_) {
                        if (numBits_ == 12) {
                            bf_.WriteBits(clearCode_, numBits_);
                            lzss_.ClearTable(codeSize_);
                            numBits_ = codeSize_ + 1;
                        }
                        else
                            ++numBits_;

                        limit_ = (1 << numBits_) - 1;
                        if (tiffFudge_)
                            --limit_;
                    }
                    prefix_ = (short)((short)c & 0xFF);
                }
            }
        }

        /**
         * Indicate to compressor that no more data to go so write outp
         * any remaining buffered data.
         *
         * @exception IOException if underlying output stream error
         **/
        virtual public void Flush() {
            if (prefix_ != -1)
                bf_.WriteBits(prefix_, numBits_);

            bf_.WriteBits(endOfInfo_, numBits_);
            bf_.Flush();
        }
    }
}
