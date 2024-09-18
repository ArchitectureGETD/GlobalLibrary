using System;
using System.IO;

namespace iTextSharp.GE.text.pdf {

    /** Implements PDF functions.
     *
     * @author Paulo Soares
     */
    public class PdfFunction {
    
        protected PdfWriter writer;
    
        protected PdfIndirectReference reference;
    
        protected PdfDictionary dictionary;
    
        /** Creates new PdfFunction */
        protected PdfFunction(PdfWriter writer) {
            this.writer = writer;
        }
    
        internal PdfIndirectReference Reference {
            get {
                if (reference == null) {
                    reference = writer.AddToBody(dictionary).IndirectReference;
                }
                return reference;
            }
        }
        
        public static PdfFunction Type0(PdfWriter writer, float[] domain, float[] range, int[] size,
            int bitsPerSample, int order, float[] encode, float[] decode, byte[] stream) {
            PdfFunction func = new PdfFunction(writer);
            func.dictionary = new PdfStream(stream);
            ((PdfStream)func.dictionary).FlateCompress(writer.CompressionLevel);
            func.dictionary.Put(PdfName.FUNCTIONTYPE, new PdfNumber(0));
            func.dictionary.Put(PdfName.DOMAIN, new PdfArray(domain));
            func.dictionary.Put(PdfName.RANGE, new PdfArray(range));
            func.dictionary.Put(PdfName.SIZE, new PdfArray(size));
            func.dictionary.Put(PdfName.BITSPERSAMPLE, new PdfNumber(bitsPerSample));
            if (order != 1)
                func.dictionary.Put(PdfName.ORDER, new PdfNumber(order));
            if (encode != null)
                func.dictionary.Put(PdfName.ENCODE, new PdfArray(encode));
            if (decode != null)
                func.dictionary.Put(PdfName.DECODE, new PdfArray(decode));
            return func;
        }

        public static PdfFunction Type2(PdfWriter writer, float[] domain, float[] range, float[] c0, float[] c1, float n) {
            PdfFunction func = new PdfFunction(writer);
            func.dictionary = new PdfDictionary();
            func.dictionary.Put(PdfName.FUNCTIONTYPE, new PdfNumber(2));
            func.dictionary.Put(PdfName.DOMAIN, new PdfArray(domain));
            if (range != null)
                func.dictionary.Put(PdfName.RANGE, new PdfArray(range));
            if (c0 != null)
                func.dictionary.Put(PdfName.C0, new PdfArray(c0));
            if (c1 != null)
                func.dictionary.Put(PdfName.C1, new PdfArray(c1));
            func.dictionary.Put(PdfName.N, new PdfNumber(n));
            return func;
        }

        public static PdfFunction Type3(PdfWriter writer, float[] domain, float[] range, PdfFunction[] functions, float[] bounds, float[] encode) {
            PdfFunction func = new PdfFunction(writer);
            func.dictionary = new PdfDictionary();
            func.dictionary.Put(PdfName.FUNCTIONTYPE, new PdfNumber(3));
            func.dictionary.Put(PdfName.DOMAIN, new PdfArray(domain));
            if (range != null)
                func.dictionary.Put(PdfName.RANGE, new PdfArray(range));
            PdfArray array = new PdfArray();
            for (int k = 0; k < functions.Length; ++k)
                array.Add(functions[k].Reference);
            func.dictionary.Put(PdfName.FUNCTIONS, array);
            func.dictionary.Put(PdfName.BOUNDS, new PdfArray(bounds));
            func.dictionary.Put(PdfName.ENCODE, new PdfArray(encode));
            return func;
        }
    
        public static PdfFunction Type4(PdfWriter writer, float[] domain, float[] range, string postscript) {
            byte[] b = new byte[postscript.Length];
                for (int k = 0; k < b.Length; ++k)
                    b[k] = (byte)postscript[k];
            PdfFunction func = new PdfFunction(writer);
            func.dictionary = new PdfStream(b);
            ((PdfStream)func.dictionary).FlateCompress(writer.CompressionLevel);
            func.dictionary.Put(PdfName.FUNCTIONTYPE, new PdfNumber(4));
            func.dictionary.Put(PdfName.DOMAIN, new PdfArray(domain));
            func.dictionary.Put(PdfName.RANGE, new PdfArray(range));
            return func;
        }
    }
}
