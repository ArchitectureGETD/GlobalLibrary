using System;
using System.IO;
using System.Collections.Generic;
using System.util.collections;
using iTextSharp.GE.text.error_messages;
using iTextSharp.GE.text.log;
using iTextSharp.GE.text.pdf.security;
namespace iTextSharp.GE.text.pdf {

    /**
    * PdfSmartCopy has the same functionality as PdfCopy,
    * but when resources (such as fonts, images,...) are
    * encountered, a reference to these resources is saved
    * in a cache, so that they can be reused.
    * This requires more memory, but reduces the file size
    * of the resulting PDF document.
    */

    public class PdfSmartCopy : PdfCopy {

        private static readonly ILogger LOGGER = LoggerFactory.GetLogger(typeof(PdfSmartCopy));

        /** the cache with the streams and references. */
        private Dictionary<ByteStore, PdfIndirectReference> streamMap = null;
        private readonly Dictionary<RefKey, int> serialized = new Dictionary<RefKey, int>();

        protected ICounter COUNTER = CounterFactory.GetCounter(typeof(PdfSmartCopy));
        protected override ICounter GetCounter() {
    	    return COUNTER;
        }

        /** Creates a PdfSmartCopy instance. */
        public PdfSmartCopy(Document document, Stream os) : base(document, os) {
            this.streamMap = new Dictionary<ByteStore, PdfIndirectReference>();
        }
        /**
        * Translate a PRIndirectReference to a PdfIndirectReference
        * In addition, translates the object numbers, and copies the
        * referenced object to the output file if it wasn't available
        * in the cache yet. If it's in the cache, the reference to
        * the already used stream is returned.
        * 
        * NB: PRIndirectReferences (and PRIndirectObjects) really need to know what
        * file they came from, because each file has its own namespace. The translation
        * we do from their namespace to ours is *at best* heuristic, and guaranteed to
        * fail under some circumstances.
        */
        protected override PdfIndirectReference CopyIndirect(PRIndirectReference inp) {
            PdfObject srcObj = PdfReader.GetPdfObjectRelease(inp);
            ByteStore streamKey = null;
            bool validStream = false;
            if (srcObj.IsStream()) {
                streamKey = new ByteStore((PRStream)srcObj, serialized);
                validStream = true;
                PdfIndirectReference streamRef;
                if (streamMap.TryGetValue(streamKey, out streamRef)) {
                    return streamRef;
                }
            } else if (srcObj.IsDictionary()) {
                streamKey = new ByteStore((PdfDictionary)srcObj, serialized);
                validStream = true;
                PdfIndirectReference streamRef;
                if (streamMap.TryGetValue(streamKey, out streamRef)) {
                    return streamRef;
                }
            }

            PdfIndirectReference theRef;
            RefKey key = new RefKey(inp);
            IndirectReferences iRef;
            indirects.TryGetValue(key, out iRef);
            if (iRef != null) {
                theRef = iRef.Ref;
                if (iRef.Copied) {
                    return theRef;
                }
            } else {
                theRef = body.PdfIndirectReference;
                iRef = new IndirectReferences(theRef);
                indirects[key] = iRef;
            }
            if (srcObj.IsDictionary()) {
                PdfObject type = PdfReader.GetPdfObjectRelease(((PdfDictionary)srcObj).Get(PdfName.TYPE));
                if (type != null) {
                    if ((PdfName.PAGE.Equals(type))) {
                        return theRef;
                    }
                    if ((PdfName.CATALOG.Equals(type))) {
                        LOGGER.Warn(MessageLocalization.GetComposedMessage("make.copy.of.catalog.dictionary.is.forbidden"));
                        return null;
                    }
                }
            }
            iRef.SetCopied();

            if (validStream) {
                streamMap[streamKey] = theRef;
            }

            PdfObject obj = CopyObject(srcObj);
            AddToBody(obj, theRef);
            return theRef;
        }

        public override void FreeReader(PdfReader reader) {
            serialized.Clear();
            base.FreeReader(reader);
        }

        public override void AddPage(PdfImportedPage iPage)  {
            if (currentPdfReaderInstance.Reader != reader)
                serialized.Clear();
            base.AddPage(iPage);
        }
        
        internal class ByteStore {
            private readonly byte[] b;
            private readonly int hash;
            private void SerObject(PdfObject obj, int level,  ByteBuffer bb, Dictionary<RefKey, int> serialized)
            {
                if (level <= 0)
                    return;
                if (obj == null) {
                    bb.Append("$Lnull");
                    return;
                }

                PdfIndirectReference refe = null;
                ByteBuffer savedBb = null;

                if (obj.IsIndirect()) {
                    refe = (PdfIndirectReference)obj;
                    RefKey key = new RefKey(refe);
                    if (serialized.ContainsKey(key)) {
                        bb.Append(serialized[key]);
                        return;
                    }
                    else {
                        savedBb = bb;
                        bb = new ByteBuffer();
                    }
                }
                obj = PdfReader.GetPdfObject(obj);
                if (obj.IsStream()) {
                    bb.Append("$B");
                    SerDic((PdfDictionary)obj, level - 1,  bb, serialized);
                    if (level > 0) {
                        bb.Append(DigestAlgorithms.Digest("MD5", PdfReader.GetStreamBytesRaw((PRStream)obj)));
                    }
                }
                else if (obj.IsDictionary()) {
                    SerDic((PdfDictionary)obj, level - 1,  bb,serialized);
                }
                else if (obj.IsArray()) {
                    SerArray((PdfArray)obj, level - 1,   bb,serialized);
                }
                else if (obj.IsString()) {
                    bb.Append("$S").Append(obj.ToString());
                }
                else if (obj.IsName()) {
                    bb.Append("$N").Append(obj.ToString());
                }
                else
                    bb.Append("$L").Append(obj.ToString());

                if (savedBb != null) {
                    RefKey key = new RefKey(refe);
                    if (!serialized.ContainsKey(key))
                        serialized[key] = CalculateHash(bb.Buffer);
                    savedBb.Append(bb);
                }
            }

            private void SerDic(PdfDictionary dic, int level,  ByteBuffer bb, Dictionary<RefKey, int> serialized)
            {
                bb.Append("$D");
                if (level <= 0)
                    return;
                PdfName[] keys = new PdfName[dic.Size];
                dic.Keys.CopyTo(keys, 0);
                Array.Sort(keys);
                for (int k = 0; k < keys.Length; ++k) {
                    if (keys[k].Equals(PdfName.P) && (dic.Get(keys[k]).IsIndirect() || dic.Get(keys[k]).IsDictionary())) // ignore recursive call
                        continue;
                        SerObject(keys[k], level, bb, serialized);
                        SerObject(dic.Get(keys[k]), level, bb, serialized);
                    
                }
            }

            private void SerArray(PdfArray array, int level,  ByteBuffer bb, Dictionary<RefKey, int> serialized)
            {
                bb.Append("$A");
                if (level <= 0)
                    return;
                for (int k = 0; k < array.Size; ++k) {
                    SerObject(array[k], level,  bb, serialized);
                }
            }

            internal ByteStore(PRStream str, Dictionary<RefKey, int> serialized)
            {
                ByteBuffer bb = new ByteBuffer();
                int level = 100;
                SerObject(str, level,  bb, serialized);
                this.b = bb.ToByteArray();
                hash = CalculateHash(this.b);
            }

            internal ByteStore(PdfDictionary dict, Dictionary<RefKey, int> serialized)
            {
                ByteBuffer bb = new ByteBuffer();
                int level = 100;
                SerObject(dict, level, bb, serialized);
                this.b = bb.ToByteArray();
                hash = CalculateHash(this.b);
            }

            private static int CalculateHash(Byte[] b)
            {
                int hash = 0;
                int len = b.Length;
                for(int k = 0; k < len; ++k)
                    hash = hash * 31 + b[k];
                return hash;
            }

            public override bool Equals(Object obj) {
                if (obj == null || !(obj is ByteStore))
                    return false;
                if (GetHashCode() != obj.GetHashCode())
                    return false;
                byte[] b2 = ((ByteStore)obj).b;
                if (b2.Length != b.Length)
                    return false;
                int len = b.Length;
                for (int k = 0; k < len; ++k) {
                    if (b[k] != b2[k])
                        return false;
                }
                return true;
            }

            public override int GetHashCode() {
                return hash;
            }
        }
    }
}
