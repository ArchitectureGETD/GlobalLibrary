using System;

namespace iTextSharp.GE.text.pdf
{
    public class MemoryLimitsAwareException : Exception {
        public static readonly string DuringDecompressionMultipleStreamsInSumOccupiedMoreMemoryThanAllowed = "During decompression multiple streams in sum occupied more memory than allowed. Please either check your pdf or increase the allowed single decompressed pdf stream maximum size value by setting the appropriate parameter of ReaderProperties's MemoryLimitsAwareHandler.";
        public static readonly string DuringDecompressionSingleStreamOccupiedMoreMemoryThanAllowed = "During decompression a single stream occupied more memory than allowed. Please either check your pdf or increase the allowed multiple decompressed pdf streams maximum size value by setting the appropriate parameter of ReaderProperties's MemoryLimitsAwareHandler.";
        public static readonly string DuringDecompressionSingleStreamOccupiedMoreThanMaxIntegerValue = "During decompression a single stream occupied more than a maximum integer value. Please check your pdf.";
        public static readonly string UnknownPdfException = "Unknown PdfException.";
        /**
         * Creates a new instance of MemoryLimitsAwareException.
         *
         * @param message the detail message.
         */
        public MemoryLimitsAwareException(String message) : base(message) {

        }
    }
}
