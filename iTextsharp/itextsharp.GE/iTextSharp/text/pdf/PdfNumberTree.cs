using System;
using System.Collections.Generic;

namespace iTextSharp.GE.text.pdf {
    /**
    * Creates a number tree.
    * @author Paulo Soares
    */
    public class PdfNumberTree {
        
        private const int leafSize = 64;
        
        /**
        * Creates a number tree.
        * @param items the item of the number tree. The key is an <CODE>Integer</CODE>
        * and the value is a <CODE>PdfObject</CODE>.
        * @param writer the writer
        * @throws IOException on error
        * @return the dictionary with the number tree.
        */    
        public static PdfDictionary WriteTree<T>(Dictionary<int, T> items, PdfWriter writer) where T : PdfObject {
            if (items.Count == 0)
                return null;
            int[] numbers = new int[items.Count];
            items.Keys.CopyTo(numbers, 0);
            Array.Sort(numbers);
            if (numbers.Length <= leafSize) {
                PdfDictionary dic = new PdfDictionary();
                PdfArray ar = new PdfArray();
                for (int k = 0; k < numbers.Length; ++k) {
                    ar.Add(new PdfNumber(numbers[k]));
                    ar.Add(items[numbers[k]]);
                }
                dic.Put(PdfName.NUMS, ar);
                return dic;
            }
            int skip = leafSize;
            PdfIndirectReference[] kids = new PdfIndirectReference[(numbers.Length + leafSize - 1) / leafSize];
            for (int k = 0; k < kids.Length; ++k) {
                int offset = k * leafSize;
                int end = Math.Min(offset + leafSize, numbers.Length);
                PdfDictionary dic = new PdfDictionary();
                PdfArray arr = new PdfArray();
                arr.Add(new PdfNumber(numbers[offset]));
                arr.Add(new PdfNumber(numbers[end - 1]));
                dic.Put(PdfName.LIMITS, arr);
                arr = new PdfArray();
                for (; offset < end; ++offset) {
                    arr.Add(new PdfNumber(numbers[offset]));
                    arr.Add(items[numbers[offset]]);
                }
                dic.Put(PdfName.NUMS, arr);
                kids[k] = writer.AddToBody(dic).IndirectReference;
            }
            int top = kids.Length;
            while (true) {
                if (top <= leafSize) {
                    PdfArray arr = new PdfArray();
                    for (int k = 0; k < top; ++k)
                        arr.Add(kids[k]);
                    PdfDictionary dic = new PdfDictionary();
                    dic.Put(PdfName.KIDS, arr);
                    return dic;
                }
                skip *= leafSize;
                int tt = (numbers.Length + skip - 1 )/ skip;
                for (int k = 0; k < tt; ++k) {
                    int offset = k * leafSize;
                    int end = Math.Min(offset + leafSize, top);
                    PdfDictionary dic = new PdfDictionary();
                    PdfArray arr = new PdfArray();
                    arr.Add(new PdfNumber(numbers[k * skip]));
                    arr.Add(new PdfNumber(numbers[Math.Min((k + 1) * skip, numbers.Length) - 1]));
                    dic.Put(PdfName.LIMITS, arr);
                    arr = new PdfArray();
                    for (; offset < end; ++offset) {
                        arr.Add(kids[offset]);
                    }
                    dic.Put(PdfName.KIDS, arr);
                    kids[k] = writer.AddToBody(dic).IndirectReference;
                }
                top = tt;
            }
        }
        
        private static void IterateItems(PdfDictionary dic, Dictionary<int, PdfObject> items) {
            PdfArray nn = (PdfArray)PdfReader.GetPdfObjectRelease(dic.Get(PdfName.NUMS));
            if (nn != null) {
                for (int k = 0; k < nn.Size; ++k) {
                    PdfNumber s = (PdfNumber)PdfReader.GetPdfObjectRelease(nn.GetPdfObject(k++));
                    items[s.IntValue] = nn.GetPdfObject(k);
                }
            }
            else if ((nn = (PdfArray)PdfReader.GetPdfObjectRelease(dic.Get(PdfName.KIDS))) != null) {
                for (int k = 0; k < nn.Size; ++k) {
                    PdfDictionary kid = (PdfDictionary)PdfReader.GetPdfObjectRelease(nn.GetPdfObject(k));
                    IterateItems(kid, items);
                }
            }
        }
        
        public static Dictionary<int, PdfObject> ReadTree(PdfDictionary dic) {
            Dictionary<int, PdfObject> items = new Dictionary<int, PdfObject>();
            if (dic != null)
                IterateItems(dic, items);
            return items;
        }
    }
}
