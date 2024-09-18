using System.Collections.Generic;
using System.IO;
using System.util.collections;

namespace iTextSharp.GE.text.pdf.ocg {
    /// <summary>
    /// Class that knows how to remove OCG layers.
    /// </summary>
    public class OCGRemover {

        /// <summary>
        /// Removes layers from a PDF document </summary>
        /// <param name="reader">	a PdfReader containing a PDF document </param>
        /// <param name="layers">	a sequence of names of OCG layers </param>
        /// <exception cref="IOException"> </exception>
        public virtual void RemoveLayers(PdfReader reader, params string[] layers) {
            int n = reader.NumberOfPages;
            for (int i = 1; i <= n; i++)
                reader.SetPageContent(i, reader.GetPageContent(i));
            ICollection<string> ocgs = new HashSet2<string>();
            for (int i = 0; i < layers.Length; i++) {
                ocgs.Add(layers[i]);
            }
            OCGParser parser = new OCGParser(ocgs);
            for (int i = 1; i <= n; i++) {
                PdfDictionary page = reader.GetPageN(i);
                Parse(parser, page);
                page.Remove(PdfName.PIECEINFO);
                RemoveAnnots(page, ocgs);
                RemoveProperties(page, ocgs);
            }
            PdfDictionary root = reader.Catalog;
            PdfDictionary ocproperties = root.GetAsDict(PdfName.OCPROPERTIES);
            if (ocproperties != null) {
                RemoveOCGsFromArray(ocproperties, PdfName.OCGS, ocgs);
                PdfDictionary d = ocproperties.GetAsDict(PdfName.D);
                if (d != null) {
                    RemoveOCGsFromArray(d, PdfName.ON, ocgs);
                    RemoveOCGsFromArray(d, PdfName.OFF, ocgs);
                    RemoveOCGsFromArray(d, PdfName.LOCKED, ocgs);
                    RemoveOCGsFromArray(d, PdfName.RBGROUPS, ocgs);
                    RemoveOCGsFromArray(d, PdfName.ORDER, ocgs);
                    RemoveOCGsFromArray(d, PdfName.AS, ocgs);
                }
                PdfArray ocgsArray = ocproperties.GetAsArray(PdfName.OCGS);
                if (ocgsArray != null && ocgsArray.IsEmpty()) {
                    root.Remove(PdfName.OCPROPERTIES);
                    if (PdfName.USEOC.Equals(root.GetAsName(PdfName.PAGEMODE))) {
                        root.Remove(PdfName.PAGEMODE);
                    }
                }
            }
            reader.RemoveUnusedObjects();
        }

        /// <summary>
        /// Gets an array from a dictionary and checks if it contains references to OCGs that need to be removed </summary>
        /// <param name="dict">	the dictionary </param>
        /// <param name="name">	the name of an array entry </param>
        /// <param name="ocgs">	the removal list </param>
        private void RemoveOCGsFromArray(PdfDictionary dict, PdfName name, ICollection<string> ocgs) {
            if (dict == null) {
                return;
            }
            PdfArray array = dict.GetAsArray(name);
            if (array == null) {
                return;
            }
            RemoveOCGsFromArray(array, ocgs);
        }

        /// <summary>
        /// Searches an array for references to OCGs that need to be removed. </summary>
        /// <param name="array">	the array </param>
        /// <param name="ocgs">	the removal list </param>
        private void RemoveOCGsFromArray(PdfArray array, ICollection<string> ocgs) {
            if (array == null) {
                return;
            }
            PdfObject o;
            PdfDictionary dict;
            IList<int?> remove = new List<int?>();
            for (int i = array.Size; i > 0;) {
                o = array.GetDirectObject(--i);
                if (o.IsDictionary()) {
                    dict = (PdfDictionary) o;
                    if (IsToBeRemoved(dict, ocgs)) {
                        remove.Add(i);
                    } else {
                        RemoveOCGsFromArray(dict, PdfName.OCGS, ocgs);
                    }
                }
                if (o.IsArray()) {
                    RemoveOCGsFromArray((PdfArray) o, ocgs);
                }
            }
            foreach (int i in remove) {
                array.Remove(i);
            }
        }

        /// <summary>
        /// Removes annotations from a page dictionary </summary>
        /// <param name="page">	a page dictionary </param>
        /// <param name="ocgs">	a set of names of OCG layers </param>
        private void RemoveAnnots(PdfDictionary page, ICollection<string> ocgs)
        {
            PdfArray annots = page.GetAsArray(PdfName.ANNOTS);
            if (annots == null)
            {
                return;
            }
            IList<int?> remove = new List<int?>();
            for (int i = annots.Size; i > 0; )
            {
                PdfDictionary annot = annots.GetAsDict(--i);
                if (IsToBeRemoved(annot.GetAsDict(PdfName.OC), ocgs))
                {
                    remove.Add(i);
                }
                else
                {
                    RemoveOCGsFromArray(annot.GetAsDict(PdfName.A), PdfName.STATE, ocgs);
                }
            }
            foreach (int i in remove)
            {
                annots.Remove(i);
            }
        }

        /// <summary>
        /// Removes ocgs from a page resources </summary>
        /// <param name="page">	a page dictionary </param>
        /// <param name="ocgs">	a set of names of OCG layers </param>
        private void RemoveProperties(PdfDictionary page, ICollection<string> ocgs) {
            PdfDictionary resources = page.GetAsDict(PdfName.RESOURCES);
            if (resources == null) {
                return;
            }
            PdfDictionary properties = resources.GetAsDict(PdfName.PROPERTIES);
            if (properties == null) {
                return;
            }
            ICollection<PdfName> names = properties.Keys;
            IList<PdfName> remove = new List<PdfName>();
            foreach (PdfName name in names) {
                PdfDictionary dict = properties.GetAsDict(name);
                if (IsToBeRemoved(dict, ocgs)) {
                    remove.Add(name);
                } else {
                    RemoveOCGsFromArray(dict, PdfName.OCGS, ocgs);
                }
            }
            foreach (PdfName name in remove) {
                properties.Remove(name);
            }
        }

        /// <summary>
        /// Checks if an OCG dictionary is on the list for removal. </summary>
        /// <param name="ocg">	a dictionary </param>
        /// <param name="names">	the removal list
        /// @return	true if the dictionary should be removed </param>
        private bool IsToBeRemoved(PdfDictionary ocg, ICollection<string> names) {
            if (ocg == null) {
                return false;
            }
            PdfString n = ocg.GetAsString(PdfName.NAME);
            if (n == null) {
                return false;
            }
            return names.Contains(n.ToString());
        }

        /// <summary>
        /// Uses the OCGParser on a page </summary>
        /// <param name="parser">	the OCGParser </param>
        /// <param name="page">		the page dictionary of the page that needs to be parsed. </param>
        /// <exception cref="IOException"> </exception>
        private void Parse(OCGParser parser, PdfDictionary page) {
            PRStream stream = (PRStream) page.GetAsStream(PdfName.CONTENTS);
            PdfDictionary resources = page.GetAsDict(PdfName.RESOURCES);
            parser.Parse(stream, resources);
        }
    }

}
