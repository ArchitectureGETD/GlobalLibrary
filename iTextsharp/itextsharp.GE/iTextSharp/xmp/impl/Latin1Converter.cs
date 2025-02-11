
using System;
using System.Text;

namespace iTextSharp.GE.xmp.impl {
    /// <summary>
    /// @since   12.10.2006
    /// </summary>
    public class Latin1Converter {
        private const int STATE_START = 0;
        private const int STATE_UTF8CHAR = 11;


        /// <summary>
        /// Private constructor
        /// </summary>
        private Latin1Converter() {
            // EMPTY
        }


        /// <summary>
        /// A converter that processes a byte buffer containing a mix of UTF8 and Latin-1/Cp1252 chars.
        /// The result is a buffer where those chars have been converted to UTF-8; 
        /// that means it contains only valid UTF-8 chars.
        /// <p>
        /// <em>Explanation of the processing:</em> First the encoding of the buffer is detected looking 
        /// at the first four bytes (that works only if the buffer starts with an ASCII-char, 
        /// like xmls &apos;&lt;&apos;). UTF-16/32 flavours do not require further proccessing.
        /// <p> 
        /// In the case, UTF-8 is detected, it assumes wrong UTF8 chars to be a sequence of 
        /// Latin-1/Cp1252 encoded bytes and converts the chars to their corresponding UTF-8 byte 
        /// sequence.
        /// <p> 
        /// The 0x80..0x9F range is undefined in Latin-1, but is defined in Windows code
        /// page 1252. The bytes 0x81, 0x8D, 0x8F, 0x90, and 0x9D are formally undefined
        /// by Windows 1252. These are in XML's RestrictedChar set, so we map them to a
        /// space. 
        /// <p>
        /// The official Latin-1 characters in the range 0xA0..0xFF are converted into
        /// the Unicode Latin Supplement range U+00A0 - U+00FF.
        /// <p>
        /// <em>Example:</em> If an Euro-symbol (€) appears in the byte buffer (0xE2, 0x82, 0xAC), 
        /// it will be left as is. But if only the first two bytes are appearing, 
        /// followed by an ASCII char a (0xE2 - 0x82 - 0x41), it will be converted to 
        /// 0xC3, 0xA2 (â) - 0xE2, 0x80, 0x9A (‚) - 0x41 (a).
        /// </summary>
        /// <param name="buffer"> a byte buffer contain </param>
        /// <returns> Returns a new buffer containing valid UTF-8 </returns>
        public static ByteBuffer Convert(ByteBuffer buffer) {
            if ("UTF-8".Equals(buffer.Encoding)) {
                // the buffer containing one UTF-8 char (up to 8 bytes) 
                byte[] readAheadBuffer = new byte[8];
                // the number of bytes read ahead.
                int readAhead = 0;
                // expected UTF8 bytesto come
                int expectedBytes = 0;
                // output buffer with estimated length
                ByteBuffer outp = new ByteBuffer(buffer.Length*4/3);

                int state = STATE_START;
                for (int i = 0; i < buffer.Length; i++) {
                    int b = buffer.ByteAt(i);

                    switch (state) {
                        default:
                            goto case STATE_START;
                        case STATE_START:
                            if (b < 0x7F) {
                                outp.Append((byte) b);
                            }
                            else if (b >= 0xC0) {
                                // start of UTF8 sequence
                                expectedBytes = -1;
                                int test = b;
                                for (; expectedBytes < 8 && (test & 0x80) == 0x80; test = test << 1) {
                                    expectedBytes++;
                                }
                                readAheadBuffer[readAhead++] = (byte) b;
                                state = STATE_UTF8CHAR;
                            }
                            else //  implicitly:  b >= 0x80  &&  b < 0xC0
                            {
                                // invalid UTF8 start char, assume to be Latin-1
                                byte[] utf8 = ConvertToUtf8((byte) b);
                                outp.Append(utf8);
                            }
                            break;

                        case STATE_UTF8CHAR:
                            if (expectedBytes > 0 && (b & 0xC0) == 0x80) {
                                // valid UTF8 char, add to readAheadBuffer
                                readAheadBuffer[readAhead++] = (byte) b;
                                expectedBytes--;

                                if (expectedBytes == 0) {
                                    outp.Append(readAheadBuffer, 0, readAhead);
                                    readAhead = 0;

                                    state = STATE_START;
                                }
                            }
                            else {
                                // invalid UTF8 char: 
                                // 1. convert first of seq to UTF8 
                                byte[] utf8 = ConvertToUtf8(readAheadBuffer[0]);
                                outp.Append(utf8);

                                // 2. continue processing at second byte of sequence
                                i = i - readAhead;
                                readAhead = 0;

                                state = STATE_START;
                            }
                            break;
                    }
                }

                // loop ends with "half" Utf8 char --> assume that the bytes are Latin-1
                if (state == STATE_UTF8CHAR) {
                    for (int j = 0; j < readAhead; j++) {
                        byte b = readAheadBuffer[j];
                        byte[] utf8 = ConvertToUtf8(b);
                        outp.Append(utf8);
                    }
                }

                return outp;
            }
            // Latin-1 fixing applies only to UTF-8
            return buffer;
        }


        /// <summary>
        /// Converts a Cp1252 char (contains all Latin-1 chars above 0x80) into a
        /// UTF-8 byte sequence. The bytes 0x81, 0x8D, 0x8F, 0x90, and 0x9D are
        /// formally undefined by Windows 1252 and therefore replaced by a space
        /// (0x20).
        /// </summary>
        /// <param name="ch">
        ///            an Cp1252 / Latin-1 byte </param>
        /// <returns> Returns a byte array containing a UTF-8 byte sequence. </returns>
        private static byte[] ConvertToUtf8(byte ch) {
            int c = ch & 0xFF;
            try {
                if (c >= 0x80) {
                    if (c == 0x81 || c == 0x8D || c == 0x8F || c == 0x90 || c == 0x9D) {
                        return new byte[] {0x20}; // space for undefined
                    }

                    // interpret byte as Windows Cp1252 char
                    string str = Encoding.Default.GetString(new byte[] {ch});
                    return Encoding.UTF8.GetBytes(str);
                }
            }
            catch (Exception) {
                // EMPTY
            }
            return new byte[] {ch};
        }
    }
}
