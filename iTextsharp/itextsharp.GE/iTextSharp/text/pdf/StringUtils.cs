namespace iTextSharp.GE.text.pdf {
    public class StringUtils {

        private static readonly byte[] r = DocWriter.GetISOBytes("\\r");
        private static readonly byte[] n = DocWriter.GetISOBytes("\\n");
        private static readonly byte[] t = DocWriter.GetISOBytes("\\t");
        private static readonly byte[] b = DocWriter.GetISOBytes("\\b");
        private static readonly byte[] f = DocWriter.GetISOBytes("\\f");

        private StringUtils() {
            
        }

        /**
         * Escapes a <CODE>byte</CODE> array according to the PDF conventions.
         *
         * @param b the <CODE>byte</CODE> array to escape
         * @return an escaped <CODE>byte</CODE> array
         */
        public static byte[] EscapeString(byte[] b) {
            ByteBuffer content = new ByteBuffer();
            EscapeString(b, content);
            return content.ToByteArray();
        }

        /**
         * Escapes a <CODE>byte</CODE> array according to the PDF conventions.
         *
         * @param b the <CODE>byte</CODE> array to escape
         */
        public static void EscapeString(byte[] bytes, ByteBuffer content) {
            content.Append_i('(');
            for (int k = 0; k < bytes.Length; ++k) {
                byte c = bytes[k];
                switch ((int) c) {
                    case '\r':
                        content.Append(r);
                        break;
                    case '\n':
                        content.Append(n);
                        break;
                    case '\t':
                        content.Append(t);
                        break;
                    case '\b':
                        content.Append(b);
                        break;
                    case '\f':
                        content.Append(f);
                        break;
                    case '(':
                    case ')':
                    case '\\':
                        content.Append_i('\\').Append_i(c);
                        break;
                    default:
                        content.Append_i(c);
                        break;
                }
            }
            content.Append(')');
        }

         /**
         * Converts an array of unsigned 16bit numbers to an array of bytes.
         * The input values are presented as chars for convenience.
         * 
         * @param chars the array of 16bit numbers that should be converted
         * @return the resulting byte array, twice as large as the input
         */
        public static byte[] ConvertCharsToBytes(char[] chars) {
            byte[] result = new byte[chars.Length * 2];
            for (int i = 0; i < chars.Length; i++) {
                result[2 * i] = (byte)(chars[i] / 256);
                result[2 * i + 1] = (byte)(chars[i] % 256);
            }
            return result;
        }

    }
}
