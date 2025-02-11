using System;
using System.IO;
using System.Collections.Generic;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf.codec {

    /**
    * Class to read a JBIG2 file at a basic level: understand all the segments, 
    * understand what segments belong to which pages, how many pages there are,
    * what the width and height of each page is, and global segments if there
    * are any.  Or: the minimum required to be able to take a normal sequential
    * or random-access organized file, and be able to embed JBIG2 pages as images 
    * in a PDF.
    * 
    * TODO: the indeterminate-segment-size value of dataLength, else?
    * 
    * @since 2.1.5
    */

    public class JBIG2SegmentReader {

        public const int SYMBOL_DICTIONARY = 0; //see 7.4.2.                                               

        public const int INTERMEDIATE_TEXT_REGION = 4; //see 7.4.3.                                        
        public const int IMMEDIATE_TEXT_REGION = 6; //see 7.4.3.                                           
        public const int IMMEDIATE_LOSSLESS_TEXT_REGION = 7; //see 7.4.3.                                  
        public const int PATTERN_DICTIONARY = 16; //see 7.4.4.                                             
        public const int INTERMEDIATE_HALFTONE_REGION = 20; //see 7.4.5.                                   
        public const int IMMEDIATE_HALFTONE_REGION = 22; //see 7.4.5.                                      
        public const int IMMEDIATE_LOSSLESS_HALFTONE_REGION = 23; //see 7.4.5.                             
        public const int INTERMEDIATE_GENERIC_REGION = 36; //see 7.4.6.                                    
        public const int IMMEDIATE_GENERIC_REGION = 38; //see 7.4.6.                                       
        public const int IMMEDIATE_LOSSLESS_GENERIC_REGION = 39; //see 7.4.6.                              
        public const int INTERMEDIATE_GENERIC_REFINEMENT_REGION = 40; //see 7.4.7.                          
        public const int IMMEDIATE_GENERIC_REFINEMENT_REGION = 42; //see 7.4.7.                             
        public const int IMMEDIATE_LOSSLESS_GENERIC_REFINEMENT_REGION = 43; //see 7.4.7.                    

        public const int PAGE_INFORMATION = 48; //see 7.4.8.                                               
        public const int END_OF_PAGE = 49; //see 7.4.9.                                                    
        public const int END_OF_STRIPE = 50; //see 7.4.10.                                                 
        public const int END_OF_FILE = 51; //see 7.4.11.                                                   
        public const int PROFILES = 52; //see 7.4.12.                                                      
        public const int TABLES = 53; //see 7.4.13.                                                        
        public const int EXTENSION = 62; //see 7.4.14.                                                     

        private SortedDictionary<int, JBIG2Segment> segments = new SortedDictionary<int, JBIG2Segment>();
        private SortedDictionary<int, JBIG2Page> pages = new SortedDictionary<int, JBIG2Page>();
        private SortedDictionary<JBIG2Segment, object> globals = new SortedDictionary<JBIG2Segment, object>();
        private RandomAccessFileOrArray ra;
        private bool sequential;
        private bool number_of_pages_known;
        private int number_of_pages = -1;
        private bool read = false;

        /**
        * Inner class that holds information about a JBIG2 segment.
        * @since   2.1.5
        */
        public class JBIG2Segment : IComparable<JBIG2Segment> {

            public int segmentNumber;
            public long dataLength = -1;
            public int page = -1;
            public int[] referredToSegmentNumbers = null;
            public bool[] segmentRetentionFlags = null;
            public int type = -1;
            public bool deferredNonRetain = false;
            public int countOfReferredToSegments = -1;
            public byte[] data = null;
            public byte[] headerData = null;
            public bool page_association_size = false;
            public int page_association_offset = -1;

            public JBIG2Segment(int segment_number) {
                this.segmentNumber = segment_number;
            }

            virtual public int CompareTo(JBIG2Segment s) {
                return this.segmentNumber - s.segmentNumber;
            }
        }
        /**
        * Inner class that holds information about a JBIG2 page.
        * @since   2.1.5
        */
        public class JBIG2Page {
            public int page;
            private JBIG2SegmentReader sr;
            private SortedDictionary<int, JBIG2Segment> segs = new SortedDictionary<int, JBIG2Segment>();
            public int pageBitmapWidth = -1;
            public int pageBitmapHeight = -1;
            public JBIG2Page(int page, JBIG2SegmentReader sr) {
                this.page = page;
                this.sr = sr;
            }
            /**
            * return as a single byte array the header-data for each segment in segment number
            * order, EMBEDDED organization, but i am putting the needed segments in SEQUENTIAL organization.
            * if for_embedding, skip the segment types that are known to be not for acrobat. 
            * @param for_embedding
            * @return  a byte array
            * @throws IOException
            */
            virtual public byte[] GetData(bool for_embedding) {
                MemoryStream os = new MemoryStream();
                foreach (int sn in segs.Keys) {
                    JBIG2Segment s = segs[sn];

                    // pdf reference 1.4, section 3.3.6 JBIG2Decode Filter
                    // D.3 Embedded organisation
                    if (for_embedding &&
                            (s.type == END_OF_FILE || s.type == END_OF_PAGE)) {
                        continue;
                    }

                    if (for_embedding) {
                        // change the page association to page 1
                        byte[] headerData_emb = CopyByteArray(s.headerData);
                        if (s.page_association_size) {
                            headerData_emb[s.page_association_offset] = 0x0;
                            headerData_emb[s.page_association_offset + 1] = 0x0;
                            headerData_emb[s.page_association_offset + 2] = 0x0;
                            headerData_emb[s.page_association_offset + 3] = 0x1;
                        }
                        else {
                            headerData_emb[s.page_association_offset] = 0x1;
                        }
                        os.Write(headerData_emb, 0, headerData_emb.Length);
                    }
                    else {
                        os.Write(s.headerData, 0, s.headerData.Length);
                    }
                    os.Write(s.data, 0, s.data.Length);
                }
                os.Close();
                return os.ToArray();
            }
            virtual public void AddSegment(JBIG2Segment s) {
                segs[s.segmentNumber] = s;
            }
        }

        public JBIG2SegmentReader(RandomAccessFileOrArray ra) {
            this.ra = ra;
        }

        public static byte[] CopyByteArray(byte[] b) {
            byte[] bc = new byte[b.Length];
            System.Array.Copy(b, 0, bc, 0, b.Length);
            return bc;
        }

        virtual public void Read() {
            if (this.read) {
                throw new InvalidOperationException(MessageLocalization.GetComposedMessage("already.attempted.a.read.on.this.jbig2.file"));
            }
            this.read = true;

            ReadFileHeader();
            // Annex D
            if (this.sequential) {
                // D.1
                do {
                    JBIG2Segment tmp = ReadHeader();
                    ReadSegment(tmp);
                    segments[tmp.segmentNumber] = tmp;
                } while (this.ra.FilePointer < this.ra.Length);
            }
            else {
                // D.2
                JBIG2Segment tmp;
                do {
                    tmp = ReadHeader();
                    segments[tmp.segmentNumber] = tmp;
                } while (tmp.type != END_OF_FILE);
                foreach (int ss in segments.Keys) {
                    ReadSegment(segments[ss]);
                }
            }
        }

        void ReadSegment(JBIG2Segment s) {
            int ptr = (int)ra.FilePointer;

            if (s.dataLength == 0xffffffffL) {
                // TODO figure this bit out, 7.2.7
                return;
            }

            byte[] data = new byte[(int)s.dataLength];
            ra.Read(data);
            s.data = data;

            if (s.type == PAGE_INFORMATION) {
                int last = (int)ra.FilePointer;
                ra.Seek(ptr);
                int page_bitmap_width = ra.ReadInt();
                int page_bitmap_height = ra.ReadInt();
                ra.Seek(last);
                JBIG2Page p = pages[s.page];
                if (p == null) {
                    throw new InvalidOperationException(MessageLocalization.GetComposedMessage("referring.to.widht.height.of.page.we.havent.seen.yet.1", s.page));
                }

                p.pageBitmapWidth = page_bitmap_width;
                p.pageBitmapHeight = page_bitmap_height;
            }
        }

        JBIG2Segment ReadHeader() {
            int ptr = (int)ra.FilePointer;
            // 7.2.1
            int segment_number = ra.ReadInt();
            JBIG2Segment s = new JBIG2Segment(segment_number);

            // 7.2.3
            int segment_header_flags = ra.Read();
            bool deferred_non_retain = ((segment_header_flags & 0x80) == 0x80);
            s.deferredNonRetain = deferred_non_retain;
            bool page_association_size = ((segment_header_flags & 0x40) == 0x40);
            int segment_type = (segment_header_flags & 0x3f);
            s.type = segment_type;

            //7.2.4
            int referred_to_byte0 = ra.Read();
            int count_of_referred_to_segments = (referred_to_byte0 & 0xE0) >> 5;
            int[] referred_to_segment_numbers = null;
            bool[] segment_retention_flags = null;

            if (count_of_referred_to_segments == 7) {
                // at least five bytes
                ra.Seek(ra.FilePointer - 1);
                count_of_referred_to_segments = (ra.ReadInt() & 0x1fffffff);
                segment_retention_flags = new bool[count_of_referred_to_segments + 1];
                int i = 0;
                int referred_to_current_byte = 0;
                do {
                    int j = i % 8;
                    if (j == 0) {
                        referred_to_current_byte = ra.Read();
                    }
                    segment_retention_flags[i] = ((((0x1 << j) & referred_to_current_byte) >> j) == 0x1);
                    i++;
                } while (i <= count_of_referred_to_segments);

            }
            else if (count_of_referred_to_segments <= 4) {
                // only one byte
                segment_retention_flags = new bool[count_of_referred_to_segments + 1];
                referred_to_byte0 &= 0x1f;
                for (int i = 0; i <= count_of_referred_to_segments; i++) {
                    segment_retention_flags[i] = ((((0x1 << i) & referred_to_byte0) >> i) == 0x1);
                }

            }
            else if (count_of_referred_to_segments == 5 || count_of_referred_to_segments == 6) {
                throw new InvalidOperationException(MessageLocalization.GetComposedMessage("count.of.referred.to.segments.had.bad.value.in.header.for.segment.1.starting.at.2", segment_number, ptr));
            }
            s.segmentRetentionFlags = segment_retention_flags;
            s.countOfReferredToSegments = count_of_referred_to_segments;

            // 7.2.5
            referred_to_segment_numbers = new int[count_of_referred_to_segments + 1];
            for (int i = 1; i <= count_of_referred_to_segments; i++) {
                if (segment_number <= 256) {
                    referred_to_segment_numbers[i] = ra.Read();
                }
                else if (segment_number <= 65536) {
                    referred_to_segment_numbers[i] = ra.ReadUnsignedShort();
                }
                else {
                    referred_to_segment_numbers[i] = (int)ra.ReadUnsignedInt(); // TODO wtf ack
                }
            }
            s.referredToSegmentNumbers = referred_to_segment_numbers;

            // 7.2.6
            int segment_page_association;
            int page_association_offset = (int)ra.FilePointer - ptr;
            if (page_association_size) {
                segment_page_association = ra.ReadInt();
            }
            else {
                segment_page_association = ra.Read();
            }
            if (segment_page_association < 0) {
                throw new InvalidOperationException(MessageLocalization.GetComposedMessage("page.1.invalid.for.segment.2.starting.at.3", segment_page_association, segment_number, ptr));
            }
            s.page = segment_page_association;
            // so we can change the page association at embedding time.
            s.page_association_size = page_association_size;
            s.page_association_offset = page_association_offset;

            if (segment_page_association > 0 && !pages.ContainsKey(segment_page_association)) {
                pages[segment_page_association] = new JBIG2Page(segment_page_association, this);
            }
            if (segment_page_association > 0) {
                pages[segment_page_association].AddSegment(s);
            }
            else {
                globals[s] = null;
            }

            // 7.2.7
            long segment_data_length = ra.ReadUnsignedInt();
            // TODO the 0xffffffff value that might be here, and how to understand those afflicted segments
            s.dataLength = segment_data_length;

            int end_ptr = (int)ra.FilePointer;
            ra.Seek(ptr);
            byte[] header_data = new byte[end_ptr - ptr];
            ra.Read(header_data);
            s.headerData = header_data;

            return s;
        }

        void ReadFileHeader() {
            ra.Seek(0);
            byte[] idstring = new byte[8];
            ra.Read(idstring);

            byte[] refidstring = { (byte)0x97, 0x4A, 0x42, 0x32, 0x0D, 0x0A, 0x1A, 0x0A };

            for (int i = 0; i < idstring.Length; i++) {
                if (idstring[i] != refidstring[i]) {
                    throw new InvalidOperationException(MessageLocalization.GetComposedMessage("file.header.idstring.not.good.at.byte.1", i));
                }
            }

            int fileheaderflags = ra.Read();

            this.sequential = ((fileheaderflags & 0x1) == 0x1);
            this.number_of_pages_known = ((fileheaderflags & 0x2) == 0x0);

            if ((fileheaderflags & 0xfc) != 0x0) {
                throw new InvalidOperationException(MessageLocalization.GetComposedMessage("file.header.flags.bits.2.7.not.0"));
            }

            if (this.number_of_pages_known) {
                this.number_of_pages = ra.ReadInt();
            }
        }

        virtual public int NumberOfPages() {
            return pages.Count;
        }

        virtual public int GetPageHeight(int i) {
            return pages[i].pageBitmapHeight;
        }

        virtual public int GetPageWidth(int i) {
            return pages[i].pageBitmapWidth;
        }

        virtual public JBIG2Page GetPage(int page) {
            return pages[page];
        }

        virtual public byte[] GetGlobal(bool for_embedding) {
            MemoryStream os = new MemoryStream();
            try {
                foreach (JBIG2Segment s in globals.Keys) {
                    if (for_embedding && (s.type == END_OF_FILE || s.type == END_OF_PAGE)) {
                        continue;
                    }
                    os.Write(s.headerData, 0, s.headerData.Length);
                    os.Write(s.data, 0, s.data.Length);
                }
            }
            catch {
            }
            if (os.Length <= 0) {
                return null;
            }
            return os.ToArray();
        }

        public override String ToString() {
            if (this.read) {
                return "Jbig2SegmentReader: number of pages: " + this.NumberOfPages();
            }
            else {
                return "Jbig2SegmentReader in indeterminate state.";
            }
        }
    }
}
