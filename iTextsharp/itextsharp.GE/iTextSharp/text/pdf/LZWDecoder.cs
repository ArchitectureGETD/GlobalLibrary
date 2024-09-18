using System;
using System.IO;
using System.Collections;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf {
    /**
     * A class for performing LZW decoding.
     *
     *
     */
    public class LZWDecoder {
    
        byte[][] stringTable;
        byte[] data = null;
        Stream uncompData;
        int tableIndex, bitsToGet = 9;
        int bytePointer;
        int nextData = 0;
        int nextBits = 0;
    
        internal int[] andTable = {
                             511,
                             1023,
                             2047,
                             4095
                         };
    
        public LZWDecoder() {
        }
    
        /**
         * Method to decode LZW compressed data.
         *
         * @param data            The compressed data.
         * @param uncompData      Array to return the uncompressed data in.
         */
        virtual public void Decode(byte[] data, Stream uncompData) {
        
            if (data[0] == (byte)0x00 && data[1] == (byte)0x01) {
                throw new Exception(MessageLocalization.GetComposedMessage("lzw.flavour.not.supported"));
            }
        
            InitializeStringTable();
        
            this.data = data;
            this.uncompData = uncompData;
        
            // Initialize pointers
            bytePointer = 0;
        
            nextData = 0;
            nextBits = 0;
        
            int code, oldCode = 0;
            byte[] str;
        
            while ((code = this.NextCode) != 257) {
            
                if (code == 256) {
                
                    InitializeStringTable();
                    code = NextCode;
                
                    if (code == 257) {
                        break;
                    }
                
                    WriteString(stringTable[code]);
                    oldCode = code;
                
                } else {
                
                    if (code < tableIndex) {
                    
                        str = stringTable[code];
                    
                        WriteString(str);
                        AddStringToTable(stringTable[oldCode], str[0]);
                        oldCode = code;
                    
                    } else {
                    
                        str = stringTable[oldCode];
                        str = ComposeString(str, str[0]);
                        WriteString(str);
                        AddStringToTable(str);
                        oldCode = code;
                    }
                }
            }
        }
    
    
        /**
         * Initialize the string table.
         */
        virtual public void InitializeStringTable() {
        
            stringTable = new byte[8192][];
        
            for (int i=0; i<256; i++) {
                stringTable[i] = new byte[1];
                stringTable[i][0] = (byte)i;
            }
        
            tableIndex = 258;
            bitsToGet = 9;
        }
    
        /**
         * Write out the string just uncompressed.
         */
        virtual public void WriteString(byte[] str) {
            uncompData.Write(str, 0, str.Length);
        }
    
        /**
         * Add a new string to the string table.
         */
        virtual public void AddStringToTable(byte[] oldstring, byte newstring) {
            int length = oldstring.Length;
            byte[] str = new byte[length + 1];
            Array.Copy(oldstring, 0, str, 0, length);
            str[length] = newstring;
        
            // Add this new string to the table
            stringTable[tableIndex++] = str;
        
            if (tableIndex == 511) {
                bitsToGet = 10;
            } else if (tableIndex == 1023) {
                bitsToGet = 11;
            } else if (tableIndex == 2047) {
                bitsToGet = 12;
            }
        }
    
        /**
         * Add a new string to the string table.
         */
        virtual public void AddStringToTable(byte[] str) {
        
            // Add this new string to the table
            stringTable[tableIndex++] = str;
        
            if (tableIndex == 511) {
                bitsToGet = 10;
            } else if (tableIndex == 1023) {
                bitsToGet = 11;
            } else if (tableIndex == 2047) {
                bitsToGet = 12;
            }
        }
    
        /**
         * Append <code>newstring</code> to the end of <code>oldstring</code>.
         */
        virtual public byte[] ComposeString(byte[] oldstring, byte newstring) {
            int length = oldstring.Length;
            byte[] str = new byte[length + 1];
            Array.Copy(oldstring, 0, str, 0, length);
            str[length] = newstring;
        
            return str;
        }
    
        // Returns the next 9, 10, 11 or 12 bits
        virtual public int NextCode {
            get {
                // Attempt to get the next code. The exception is caught to make
                // this robust to cases wherein the EndOfInformation code has been
                // omitted from a strip. Examples of such cases have been observed
                // in practice.
                try {
                    nextData = (nextData << 8) | (data[bytePointer++] & 0xff);
                    nextBits += 8;
            
                    if (nextBits < bitsToGet) {
                        nextData = (nextData << 8) | (data[bytePointer++] & 0xff);
                        nextBits += 8;
                    }
            
                    int code =
                        (nextData >> (nextBits - bitsToGet)) & andTable[bitsToGet-9];
                    nextBits -= bitsToGet;
            
                    return code;
                } catch  {
                    // Strip not terminated as expected: return EndOfInformation code.
                    return 257;
                }
            }
        }
    }
}
