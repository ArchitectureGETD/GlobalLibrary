using System;

namespace iTextSharp.GE.text.pdf.qrcode {

    /**
     * <p>This class contains utility methods for performing mathematical operations over
     * the Galois Field GF(256). Operations use a given primitive polynomial in calculations.</p>
     *
     * <p>Throughout this package, elements of GF(256) are represented as an <code>int</code>
     * for convenience and speed (but at the cost of memory).
     * Only the bottom 8 bits are really used.</p>
     */
    public sealed class GF256 {

        public static readonly GF256 QR_CODE_FIELD = new GF256(0x011D); // x^8 + x^4 + x^3 + x^2 + 1
        public static readonly GF256 DATA_MATRIX_FIELD = new GF256(0x012D); // x^8 + x^5 + x^3 + x^2 + 1

        private int[] expTable;
        private int[] logTable;
        private GF256Poly zero;
        private GF256Poly one;

        /**
         * Create a representation of GF(256) using the given primitive polynomial.
         *
         * @param primitive irreducible polynomial whose coefficients are represented by
         *  the bits of an int, where the least-significant bit represents the constant
         *  coefficient
         */
        private GF256(int primitive) {
            expTable = new int[256];
            logTable = new int[256];
            int x = 1;
            for (int i = 0; i < 256; i++) {
                expTable[i] = x;
                x <<= 1; // x = x * 2; we're assuming the generator alpha is 2
                if (x >= 0x100) {
                    x ^= primitive;
                }
            }
            for (int i = 0; i < 255; i++) {
                logTable[expTable[i]] = i;
            }
            // logTable[0] == 0 but this should never be used
            zero = new GF256Poly(this, new int[] { 0 });
            one = new GF256Poly(this, new int[] { 1 });
        }

        internal GF256Poly GetZero() {
            return zero;
        }

        internal GF256Poly GetOne() {
            return one;
        }

        /**
         * @return the monomial representing coefficient * x^degree
         */
        internal GF256Poly BuildMonomial(int degree, int coefficient) {
            if (degree < 0) {
                throw new ArgumentException();
            }
            if (coefficient == 0) {
                return zero;
            }
            int[] coefficients = new int[degree + 1];
            coefficients[0] = coefficient;
            return new GF256Poly(this, coefficients);
        }

        /**
         * Implements both addition and subtraction -- they are the same in GF(256).
         *
         * @return sum/difference of a and b
         */
        internal static int AddOrSubtract(int a, int b) {
            return a ^ b;
        }

        /**
         * @return 2 to the power of a in GF(256)
         */
        internal int Exp(int a) {
            return expTable[a];
        }

        /**
         * @return base 2 log of a in GF(256)
         */
        internal int Log(int a) {
            if (a == 0) {
                throw new ArgumentException();
            }
            return logTable[a];
        }

        /**
         * @return multiplicative inverse of a
         */
        internal int Inverse(int a) {
            if (a == 0) {
                throw new ArithmeticException();
            }
            return expTable[255 - logTable[a]];
        }

        /**
         * @param a
         * @param b
         * @return product of a and b in GF(256)
         */
        internal int Multiply(int a, int b) {
            if (a == 0 || b == 0) {
                return 0;
            }
            if (a == 1) {
                return b;
            }
            if (b == 1) {
                return a;
            }
            return expTable[(logTable[a] + logTable[b]) % 255];
        }
    }
}
