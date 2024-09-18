using System;

namespace iTextSharp.GE.text.pdf.qrcode {


    public sealed class Mode {

        // No, we can't use an enum here. J2ME doesn't support it.

        public static readonly Mode TERMINATOR = new Mode(new int[] { 0, 0, 0 }, 0x00, "TERMINATOR"); // Not really a mode...
        public static readonly Mode NUMERIC = new Mode(new int[] { 10, 12, 14 }, 0x01, "NUMERIC");
        public static readonly Mode ALPHANUMERIC = new Mode(new int[] { 9, 11, 13 }, 0x02, "ALPHANUMERIC");
        public static readonly Mode STRUCTURED_APPEND = new Mode(new int[] { 0, 0, 0 }, 0x03, "STRUCTURED_APPEND"); // Not supported
        public static readonly Mode BYTE = new Mode(new int[] { 8, 16, 16 }, 0x04, "BYTE");
        public static readonly Mode ECI = new Mode(null, 0x07, "ECI"); // character counts don't apply
        public static readonly Mode KANJI = new Mode(new int[] { 8, 10, 12 }, 0x08, "KANJI");
        public static readonly Mode FNC1_FIRST_POSITION = new Mode(null, 0x05, "FNC1_FIRST_POSITION");
        public static readonly Mode FNC1_SECOND_POSITION = new Mode(null, 0x09, "FNC1_SECOND_POSITION");

        private int[] characterCountBitsForVersions;
        private int bits;
        private String name;

        private Mode(int[] characterCountBitsForVersions, int bits, String name) {
            this.characterCountBitsForVersions = characterCountBitsForVersions;
            this.bits = bits;
            this.name = name;
        }

        /**
         * @param bits four bits encoding a QR Code data mode
         * @return {@link Mode} encoded by these bits
         * @throws IllegalArgumentException if bits do not correspond to a known mode
         */
        public static Mode ForBits(int bits) {
            switch (bits) {
                case 0x0:
                    return TERMINATOR;
                case 0x1:
                    return NUMERIC;
                case 0x2:
                    return ALPHANUMERIC;
                case 0x3:
                    return STRUCTURED_APPEND;
                case 0x4:
                    return BYTE;
                case 0x5:
                    return FNC1_FIRST_POSITION;
                case 0x7:
                    return ECI;
                case 0x8:
                    return KANJI;
                case 0x9:
                    return FNC1_SECOND_POSITION;
                default:
                    throw new ArgumentException();
            }
        }

        /**
         * @param version version in question
         * @return number of bits used, in this QR Code symbol {@link Version}, to encode the
         *         count of characters that will follow encoded in this {@link Mode}
         */
        public int GetCharacterCountBits(Version version) {
            if (characterCountBitsForVersions == null) {
                throw new ArgumentException("Character count doesn't apply to this mode");
            }
            int number = version.GetVersionNumber();
            int offset;
            if (number <= 9) {
                offset = 0;
            }
            else if (number <= 26) {
                offset = 1;
            }
            else {
                offset = 2;
            }
            return characterCountBitsForVersions[offset];
        }

        public int GetBits() {
            return bits;
        }

        public String GetName() {
            return name;
        }

        public override String ToString() {
            return name;
        }
    }
}
