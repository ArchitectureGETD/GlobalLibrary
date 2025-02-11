using System;

namespace iTextSharp.GE.text.pdf.qrcode {

    /**
     * These are a set of hints that you may pass to Writers to specify their behavior.
     *
     */
    public sealed class EncodeHintType {

        /**
         * Specifies what degree of error correction to use, for example in QR Codes (type Integer).
         */
        public static readonly EncodeHintType ERROR_CORRECTION = new EncodeHintType();

        /**
         * Specifies what character encoding to use where applicable (type String)
         */
        public static readonly EncodeHintType CHARACTER_SET = new EncodeHintType();

        private EncodeHintType() {
        }
    }
}
