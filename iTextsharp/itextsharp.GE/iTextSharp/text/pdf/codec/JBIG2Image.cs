using System;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf.codec {

    /**
    * Support for JBIG2 Images.
    * This class assumes that we are always embedding into a pdf.
    * 
    * @since 2.1.5
    */
    public class JBIG2Image {

        /**
        * Gets a byte array that can be used as a /JBIG2Globals,
        * or null if not applicable to the given jbig2.
        * @param   ra  an random access file or array
        * @return  a byte array
        */
        public static byte[] GetGlobalSegment(RandomAccessFileOrArray ra ) {
            try {
                JBIG2SegmentReader sr = new JBIG2SegmentReader(ra);
                sr.Read();
                return sr.GetGlobal(true);
            } catch {
                return null;
            }
        }
        
        /**
        * returns an Image representing the given page.
        * @param ra    the file or array containing the image
        * @param page  the page number of the image
        * @return  an Image object
        */
        public static Image GetJbig2Image(RandomAccessFileOrArray ra, int page) {
            if (page < 1)
                throw new ArgumentException(MessageLocalization.GetComposedMessage("the.page.number.must.be.gt.eq.1"));
            
            JBIG2SegmentReader sr = new JBIG2SegmentReader(ra);
            sr.Read();
            JBIG2SegmentReader.JBIG2Page p = sr.GetPage(page);
            Image img = new ImgJBIG2(p.pageBitmapWidth, p.pageBitmapHeight, p.GetData(true), sr.GetGlobal(true));
            return img;
        }

        /***
        * Gets the number of pages in a JBIG2 image.
        * @param ra    a random acces file array containing a JBIG2 image
        * @return  the number of pages
        */
        public static int GetNumberOfPages(RandomAccessFileOrArray ra) {
            JBIG2SegmentReader sr = new JBIG2SegmentReader(ra);
            sr.Read();
            return sr.NumberOfPages();
        }
    }
}
