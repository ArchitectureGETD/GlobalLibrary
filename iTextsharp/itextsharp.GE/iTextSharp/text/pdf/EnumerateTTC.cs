using System;
using System.IO;
using System.Collections.Generic;
using iTextSharp.GE.text.error_messages;

using iTextSharp.GE.text;

namespace iTextSharp.GE.text.pdf {

    /** Enumerates all the fonts inside a True Type Collection.
     *
     * @author  Paulo Soares
     */
    internal class EnumerateTTC : TrueTypeFont {

        protected String[] names;

        internal EnumerateTTC(String ttcFile) {
            fileName = ttcFile;
            rf = new RandomAccessFileOrArray(ttcFile);
            FindNames();
        }

        internal EnumerateTTC(byte[] ttcArray) {
            fileName = "Byte array TTC";
            rf = new RandomAccessFileOrArray(ttcArray);
            FindNames();
        }
    
        internal void FindNames() {
            tables = new Dictionary<String, int[]>();
        
            try {
                String mainTag = ReadStandardString(4);
                if (!mainTag.Equals("ttcf"))
                    throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.ttc.file", fileName));
                rf.SkipBytes(4);
                int dirCount = rf.ReadInt();
                names = new String[dirCount];
                int dirPos = (int)rf.FilePointer;
                for (int dirIdx = 0; dirIdx < dirCount; ++dirIdx) {
                    tables.Clear();
                    rf.Seek(dirPos);
                    rf.SkipBytes(dirIdx * 4);
                    directoryOffset = rf.ReadInt();
                    rf.Seek(directoryOffset);
                    if (rf.ReadInt() != 0x00010000)
                        throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.ttf.file", fileName));
                    int num_tables = rf.ReadUnsignedShort();
                    rf.SkipBytes(6);
                    for (int k = 0; k < num_tables; ++k) {
                        String tag = ReadStandardString(4);
                        rf.SkipBytes(4);
                        int[] table_location = new int[2];
                            table_location[0] = rf.ReadInt();
                        table_location[1] = rf.ReadInt();
                        tables[tag] = table_location;
                    }
                    names[dirIdx] = this.BaseFont;
                }
            }
            finally {
                if (rf != null)
                    rf.Close();
            }
        }
    
        internal String[] Names {
            get {
                return names;
            }
        }
    }
}
