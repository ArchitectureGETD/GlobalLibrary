using System.IO;

namespace iTextSharp.GE.text.pdf.mc {

    /**
     * Removes all interactivity from an AcroForm, maintaining the
     * structure tree.
     * 
     * DISCLAIMER:
     * - Use this class only if the form is properly tagged.
     * - This class won't work with pages in which the CTM is changed
     * - This class may not work for form fields with more than one widget annotation
     */
    public class MCFieldFlattener {
        /**
         * Processes a properly tagged PDF form.
         * @param reader the PdfReader instance holding the PDF
         * @throws IOException
         * @throws DocumentException 
         */
        virtual public void Process(PdfReader reader, Stream os) {
            int n = reader.NumberOfPages;
            // getting the root dictionary
            PdfDictionary catalog = reader.Catalog;
            // flattening means: remove AcroForm
            catalog.Remove(PdfName.ACROFORM);
            // read the structure and create a parser
            StructureItems items = new StructureItems(reader);
            MCParser parser = new MCParser(items);
            // loop over all pages
            for(int i = 1; i <= n; i++) {
                // make one stream of a content stream array
                reader.SetPageContent(i, reader.GetPageContent(i));
                // parse page
                parser.Parse(reader.GetPageN(i), reader.GetPageOrigRef(i));
            }
            reader.RemoveUnusedObjects();
            // create flattened file
            PdfStamper stamper = new PdfStamper(reader, os);
            items.WriteParentTree(stamper.Writer);
            stamper.Close();
        }
    }
}
