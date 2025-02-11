using System;
using System.Text;

namespace iTextSharp.GE.text.pdf.qrcode {

    public sealed class QRCode {

        public const int NUM_MASK_PATTERNS = 8;

        private Mode mode;
        private ErrorCorrectionLevel ecLevel;
        private int version;
        private int matrixWidth;
        private int maskPattern;
        private int numTotalBytes;
        private int numDataBytes;
        private int numECBytes;
        private int numRSBlocks;
        private ByteMatrix matrix;

        public QRCode() {
            mode = null;
            ecLevel = null;
            version = -1;
            matrixWidth = -1;
            maskPattern = -1;
            numTotalBytes = -1;
            numDataBytes = -1;
            numECBytes = -1;
            numRSBlocks = -1;
            matrix = null;
        }

        // Mode of the QR Code.
        public Mode GetMode() {
            return mode;
        }

        // Error correction level of the QR Code.
        public ErrorCorrectionLevel GetECLevel() {
            return ecLevel;
        }

        // Version of the QR Code.  The bigger size, the bigger version.
        public int GetVersion() {
            return version;
        }

        // ByteMatrix width of the QR Code.
        public int GetMatrixWidth() {
            return matrixWidth;
        }

        // Mask pattern of the QR Code.
        public int GetMaskPattern() {
            return maskPattern;
        }

        // Number of total bytes in the QR Code.
        public int GetNumTotalBytes() {
            return numTotalBytes;
        }

        // Number of data bytes in the QR Code.
        public int GetNumDataBytes() {
            return numDataBytes;
        }

        // Number of error correction bytes in the QR Code.
        public int GetNumECBytes() {
            return numECBytes;
        }

        // Number of Reedsolomon blocks in the QR Code.
        public int GetNumRSBlocks() {
            return numRSBlocks;
        }

        // ByteMatrix data of the QR Code.
        public ByteMatrix GetMatrix() {
            return matrix;
        }


        // Return the value of the module (cell) pointed by "x" and "y" in the matrix of the QR Code. They
        // call cells in the matrix "modules". 1 represents a black cell, and 0 represents a white cell.
        public int At(int x, int y) {
            // The value must be zero or one.
            int value = matrix.Get(x, y);
            if (!(value == 0 || value == 1)) {
                // this is really like an assert... not sure what better exception to use?
                throw new ArgumentException("Bad value");
            }
            return value;
        }

        // Checks all the member variables are set properly. Returns true on success. Otherwise, returns
        // false.
        public bool IsValid() {
            return
                // First check if all version are not uninitialized.
                mode != null &&
                ecLevel != null &&
                version != -1 &&
                matrixWidth != -1 &&
                maskPattern != -1 &&
                numTotalBytes != -1 &&
                numDataBytes != -1 &&
                numECBytes != -1 &&
                numRSBlocks != -1 &&
                // Then check them in other ways..
                IsValidMaskPattern(maskPattern) &&
                numTotalBytes == numDataBytes + numECBytes &&
                // ByteMatrix stuff.
                matrix != null &&
                matrixWidth == matrix.GetWidth() &&
                // See 7.3.1 of JISX0510:2004 (p.5).
                matrix.GetWidth() == matrix.GetHeight(); // Must be square.
        }

        // Return debug String.
        public override String ToString() {
            StringBuilder result = new StringBuilder(200);
            result.Append("<<\n");
            result.Append(" mode: ");
            result.Append(mode);
            result.Append("\n ecLevel: ");
            result.Append(ecLevel);
            result.Append("\n version: ");
            result.Append(version);
            result.Append("\n matrixWidth: ");
            result.Append(matrixWidth);
            result.Append("\n maskPattern: ");
            result.Append(maskPattern);
            result.Append("\n numTotalBytes: ");
            result.Append(numTotalBytes);
            result.Append("\n numDataBytes: ");
            result.Append(numDataBytes);
            result.Append("\n numECBytes: ");
            result.Append(numECBytes);
            result.Append("\n numRSBlocks: ");
            result.Append(numRSBlocks);
            if (matrix == null) {
                result.Append("\n matrix: null\n");
            }
            else {
                result.Append("\n matrix:\n");
                result.Append(matrix.ToString());
            }
            result.Append(">>\n");
            return result.ToString();
        }

        public void SetMode(Mode value) {
            mode = value;
        }

        public void SetECLevel(ErrorCorrectionLevel value) {
            ecLevel = value;
        }

        public void SetVersion(int value) {
            version = value;
        }

        public void SetMatrixWidth(int value) {
            matrixWidth = value;
        }

        public void SetMaskPattern(int value) {
            maskPattern = value;
        }

        public void SetNumTotalBytes(int value) {
            numTotalBytes = value;
        }

        public void SetNumDataBytes(int value) {
            numDataBytes = value;
        }

        public void SetNumECBytes(int value) {
            numECBytes = value;
        }

        public void SetNumRSBlocks(int value) {
            numRSBlocks = value;
        }

        // This takes ownership of the 2D array.
        public void SetMatrix(ByteMatrix value) {
            matrix = value;
        }

        // Check if "mask_pattern" is valid.
        public static bool IsValidMaskPattern(int maskPattern) {
            return maskPattern >= 0 && maskPattern < NUM_MASK_PATTERNS;
        }

        // Return true if the all values in the matrix are binary numbers.
        //
        // JAVAPORT: This is going to be super expensive and unnecessary, we should not call this in
        // production. I'm leaving it because it may be useful for testing. It should be removed entirely
        // if ByteMatrix is changed never to contain a -1.
        /*
        private static bool EverythingIsBinary(final ByteMatrix matrix) {
          for (int y = 0; y < matrix.Height(); ++y) {
            for (int x = 0; x < matrix.Width(); ++x) {
              int value = matrix.Get(y, x);
              if (!(value == 0 || value == 1)) {
                // Found non zero/one value.
                return false;
              }
            }
          }
          return true;
        }
         */
    }
}
