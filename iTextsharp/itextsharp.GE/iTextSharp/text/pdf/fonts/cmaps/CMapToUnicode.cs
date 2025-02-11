using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp.GE.text.error_messages;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.fonts.cmaps {

    public class CMapToUnicode : AbstractCMap {

        private IDictionary<int, String> singleByteMappings = new Dictionary<int, String>();
        private IDictionary<int, String> doubleByteMappings = new Dictionary<int, String>();
        private Encoding enc = new UnicodeEncoding(true, false);

        /**
         * Creates a new instance of CMap.
         */
        public CMapToUnicode() {
            //default constructor
        }

        /**
         * This will tell if this cmap has any one byte mappings.
         *
         * @return true If there are any one byte mappings, false otherwise.
         */
        virtual public bool HasOneByteMappings() {
            return singleByteMappings.Count != 0;
        }

        /**
         * This will tell if this cmap has any two byte mappings.
         *
         * @return true If there are any two byte mappings, false otherwise.
         */
        virtual public bool HasTwoByteMappings() {
            return doubleByteMappings.Count != 0;
        }


        /**
         * This will perform a lookup into the map.
         *
         * @param code The code used to lookup.
         * @param offset The offset into the byte array.
         * @param length The length of the data we are getting.
         *
         * @return The string that matches the lookup.
         */
        virtual public String Lookup( byte[] code, int offset, int length )
        {
            String result = null;
            int key = 0;
            if ( length == 1 ) {
                key = code[offset] & 0xff;
                singleByteMappings.TryGetValue(key, out result);
            }
            else if ( length == 2 ) {
                int intKey = code[offset] & 0xff;
                intKey <<= 8;
                intKey += code[offset+1] & 0xff;
                doubleByteMappings.TryGetValue( intKey, out result );
            }
            return result;
        }

        virtual public IDictionary<int, int> CreateReverseMapping() {
            IDictionary<int, int> result = new Dictionary<int, int>();
            foreach (KeyValuePair<int, String> entry in singleByteMappings) {
                result[ConvertToInt(entry.Value)] = entry.Key;
            }
            foreach (KeyValuePair<int, String> entry in doubleByteMappings) {
                result[ConvertToInt(entry.Value)] = entry.Key;
            }
            return result;
        }

        virtual public IDictionary<int, int> CreateDirectMapping() {
            IDictionary<int, int> result = new Dictionary<int, int>();
            foreach (KeyValuePair<int, String> entry in singleByteMappings) {
                result[entry.Key] = ConvertToInt(entry.Value);
            }
            foreach (KeyValuePair<int, String> entry in doubleByteMappings) {
                result[entry.Key] = ConvertToInt(entry.Value);
            }
            return result;
        }

        private int ConvertToInt(String s) {
            UnicodeEncoding ue = new UnicodeEncoding(true, false);
            byte[] b = ue.GetBytes(s);
            int value = 0;
            for (int i = 0; i < b.Length - 1; i++) {
                value += b[i] & 0xff;
                value <<= 8;
            }
            value += b[b.Length - 1] & 0xff;
            return value;
        }

        internal void AddChar(int cid, String uni) {
            doubleByteMappings[cid] = uni;
        }

        internal override void AddChar(PdfString mark, PdfObject code) {
            byte[] src = mark.GetBytes();
            String dest = CreateStringFromBytes(code.GetBytes());
            if (src.Length == 1) {
                singleByteMappings[src[0] & 0xff] = dest;
            } else if (src.Length == 2) {
                int intSrc = src[0] & 0xFF;
                intSrc <<= 8;
                intSrc |= src[1] & 0xFF;
                doubleByteMappings[intSrc] = dest;
            } else {
                throw new IOException(MessageLocalization.GetComposedMessage("mapping.code.should.be.1.or.two.bytes.and.not.1", src.Length));
            }
        }
        
        private String CreateStringFromBytes(byte[] bytes) {
            String retval = null;
            if (bytes.Length == 1) {
                retval = new String((char)(bytes[0] & 0xff), 1);
            } else {
                retval = enc.GetString(bytes);
            }
            return retval;
        }

        public static CMapToUnicode GetIdentity() {
            CMapToUnicode uni = new CMapToUnicode();
            for (int i = 0; i < 65537; i++) {
                uni.AddChar(i, Utilities.ConvertFromUtf32(i));
            }
            return uni;
        }
    }
}
