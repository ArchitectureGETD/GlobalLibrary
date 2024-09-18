using System.Collections.Generic;
using iTextSharp.GE.text.pdf.interfaces;

namespace iTextSharp.GE.text.pdf {

    /**
    * The structure tree root corresponds to the highest hierarchy level in a tagged PDF.
    * @author Paulo Soares
    */
    public class PdfStructureTreeRoot : PdfDictionary, IPdfStructureElement {
        
        private Dictionary<int, PdfObject> parentTree = new Dictionary<int,PdfObject>();
        private PdfIndirectReference reference;
        private PdfDictionary classMap;
        internal Dictionary<PdfName, PdfObject> classes;
        private Dictionary<int, PdfIndirectReference> numTree = null;
        private Dictionary<string, PdfObject> idTreeMap;

        /**
        * Holds value of property writer.
        */
        private PdfWriter writer;
        
        /** Creates a new instance of PdfStructureTreeRoot */
        internal PdfStructureTreeRoot(PdfWriter writer) : base(PdfName.STRUCTTREEROOT) {
            this.writer = writer;
            reference = writer.PdfIndirectReference;
        }

        private void CreateNumTree()
        {
            if (numTree != null)
                return;
            numTree = new Dictionary<int, PdfIndirectReference>();
            foreach (int i in parentTree.Keys) {
                PdfObject obj = parentTree[i];
                if (obj.IsArray()) {
                    PdfArray ar = (PdfArray)obj;
                    numTree[i] = writer.AddToBody(ar).IndirectReference;
                } else if (obj is PdfIndirectReference) {
                    numTree[i] = (PdfIndirectReference)obj;
                }            
            }
        }

        /**
        * Maps the user tags to the standard tags. The mapping will allow a standard application to make some sense of the tagged
        * document whatever the user tags may be.
        * @param used the user tag
        * @param standard the standard tag
        */    
        virtual public void MapRole(PdfName used, PdfName standard) {
            PdfDictionary rm = (PdfDictionary)Get(PdfName.ROLEMAP);
            if (rm == null) {
                rm = new PdfDictionary();
                Put(PdfName.ROLEMAP, rm);
            }
            rm.Put(used, standard);
        }
        
        virtual public void MapClass(PdfName name, PdfObject obj) {
            if (classMap == null) {
                classMap = new PdfDictionary();
                classes = new Dictionary<PdfName, PdfObject>();
            }
            classes[name] = obj;
        }

        virtual internal void PutIDTree(string record, PdfObject reference) {
            if (idTreeMap == null)
                idTreeMap = new Dictionary<string, PdfObject>();
            idTreeMap[record] = reference;
        }

        virtual public PdfObject GetMappedClass(PdfName name) {
            if (classes == null)
                return null;
            PdfObject result;
            classes.TryGetValue(name, out result);
            return result;
        }

        /**
        * Gets the writer.
        * @return the writer
        */
        virtual public PdfWriter Writer {
            get {
                return this.writer;
            }
        }

        virtual public Dictionary<int, PdfIndirectReference> NumTree
        {
            get
            {
                if (numTree == null)
                    CreateNumTree();
                return numTree;
            }
        }

        /**
        * Gets the reference this object will be written to.
        * @return the reference this object will be written to
        */    
        virtual public PdfIndirectReference Reference {
            get {
                return this.reference;
            }
        }

        internal void SetPageMark(int page, PdfIndirectReference struc) {
            PdfArray ar;
            if (!parentTree.ContainsKey(page)) {
                ar = new PdfArray();
                parentTree[page] = ar;
            }
            else
                ar = (PdfArray)parentTree[page];
             ar.Add(struc);
            
        }

        internal void SetAnnotationMark(int structParentIndex, PdfIndirectReference struc) { 
            parentTree[structParentIndex] = struc;
        }

        private void NodeProcess(PdfDictionary struc, PdfIndirectReference reference) {
            PdfObject obj = struc.Get(PdfName.K);
            if (obj != null && obj.IsArray()) {
                PdfArray ar = (PdfArray)obj;
                for (int k = 0; k < ar.Size; ++k) {
                    PdfDictionary dictionary = ar.GetAsDict(k);
                    if (dictionary == null)
                        continue;
                    if (!PdfName.STRUCTELEM.Equals(dictionary.Get(PdfName.TYPE)))
                        continue;
                    if (ar.GetPdfObject(k) is PdfStructureElement) {
                        PdfStructureElement e = (PdfStructureElement) dictionary;
                        ar.Set(k, e.Reference);
                        NodeProcess(e, e.Reference);
                    }
                }
            }
            if (reference != null)
                writer.AddToBody(struc, reference);
        }
        
        internal void BuildTree() {
            CreateNumTree();
            PdfDictionary dicTree = PdfNumberTree.WriteTree(numTree, writer);
            if (dicTree != null)
                Put(PdfName.PARENTTREE, writer.AddToBody(dicTree).IndirectReference);
            if (classMap != null && classes.Count > 0) {
                foreach (KeyValuePair<PdfName,PdfObject> entry in classes) {
                    PdfObject value = entry.Value;
                    if (value.IsDictionary())
                        classMap.Put(entry.Key, writer.AddToBody(value).IndirectReference);
                    else if (value.IsArray()) {
                        PdfArray newArray = new PdfArray();
                        PdfArray array = (PdfArray)value;
                        for (int i = 0; i < array.Size; ++i) {
                            if (array.GetPdfObject(i).IsDictionary())
                                newArray.Add(writer.AddToBody(array.GetAsDict(i)).IndirectReference);
                        }
                        classMap.Put(entry.Key,newArray);
                    }
                }
                Put(PdfName.CLASSMAP, writer.AddToBody(classMap).IndirectReference);
            }
            if (idTreeMap != null && idTreeMap.Count > 0) {
                PdfDictionary dic = PdfNameTree.WriteTree(idTreeMap, writer);
                this.Put(PdfName.IDTREE, dic);
            }
            NodeProcess(this, reference);
        }


        /**
         * Gets the first entarance of attribute.
         * @returns PdfObject
         * @since 5.3.4
         */
        virtual public PdfObject GetAttribute(PdfName name) {
            PdfDictionary attr = GetAsDict(PdfName.A);
            if (attr != null) {
                if (attr.Contains(name))
                    return attr.Get(name);
            }
            return null;
        }

        /**
         * Sets the attribute value.
         * @since 5.3.4
         */
        virtual public void SetAttribute(PdfName name, PdfObject obj) {
            PdfDictionary attr = GetAsDict(PdfName.A);
            if (attr == null) {
                attr = new PdfDictionary();
                Put(PdfName.A, attr);
            }
            attr.Put(name, obj);
        }
    }
}
