using System;
using System.Text;

namespace iTextSharp.GE.text.pdf.qrcode {

    /**
     * A class which wraps a 2D array of bytes. The default usage is signed. If you want to use it as a
     * unsigned container, it's up to you to do byteValue & 0xff at each location.
     *
     * JAVAPORT: The original code was a 2D array of ints, but since it only ever gets assigned
     * -1, 0, and 1, I'm going to use less memory and go with bytes.
     */
    public sealed class ByteMatrix {

        private sbyte[][] bytes;
        private int width;
        private int height;

        public ByteMatrix(int width, int height) {
            bytes = new sbyte[height][];
            for (int k = 0; k < height; ++k) {
                bytes[k] = new sbyte[width];
            }
            this.width = width;
            this.height = height;
        }

        public int GetHeight() {
            return height;
        }

        public int GetWidth() {
            return width;
        }

        public sbyte Get(int x, int y) {
            return bytes[y][x];
        }

        public sbyte[][] GetArray() {
            return bytes;
        }

        public void Set(int x, int y, sbyte value) {
            bytes[y][x] = value;
        }

        public void Set(int x, int y, int value) {
            bytes[y][x] = (sbyte)value;
        }

        public void Clear(sbyte value) {
            for (int y = 0; y < height; ++y) {
                for (int x = 0; x < width; ++x) {
                    bytes[y][x] = value;
                }
            }
        }

        public override String ToString() {
            StringBuilder result = new StringBuilder(2 * width * height + 2);
            for (int y = 0; y < height; ++y) {
                for (int x = 0; x < width; ++x) {
                    switch (bytes[y][x]) {
                        case 0:
                            result.Append(" 0");
                            break;
                        case 1:
                            result.Append(" 1");
                            break;
                        default:
                            result.Append("  ");
                            break;
                    }
                }
                result.Append('\n');
            }
            return result.ToString();
        }
    }
}
