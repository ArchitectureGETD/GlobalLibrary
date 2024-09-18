using System;

namespace iTextSharp.GE.text.pdf.qrcode {

    public sealed class BlockPair {

        private ByteArray dataBytes;
        private ByteArray errorCorrectionBytes;

        internal BlockPair(ByteArray data, ByteArray errorCorrection) {
            dataBytes = data;
            errorCorrectionBytes = errorCorrection;
        }

        public ByteArray GetDataBytes() {
            return dataBytes;
        }

        public ByteArray GetErrorCorrectionBytes() {
            return errorCorrectionBytes;
        }

    }
}
