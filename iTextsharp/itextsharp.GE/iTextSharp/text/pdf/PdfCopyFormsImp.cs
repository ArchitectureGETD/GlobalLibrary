using System.IO;
using System.Collections.Generic;
using iTextSharp.GE.text.error_messages;
using iTextSharp.GE.text.exceptions;

namespace iTextSharp.GE.text.pdf {

    /**
    * Allows you to add one (or more) existing PDF document(s)
    * and add the form(s) of (an)other PDF document(s).
    * @since 2.1.5
    * @deprecated since 5.5.2
    */
    internal class PdfCopyFormsImp : PdfCopyFieldsImp {

        /**
        * This sets up the output document 
        * @param os The Outputstream pointing to the output document
        * @throws DocumentException
        */
        internal PdfCopyFormsImp(Stream os) : base(os) {
        }
        
        /**
        * This method feeds in the source document
        * @param reader The PDF reader containing the source document
        * @throws DocumentException
        */
        virtual public void CopyDocumentFields(PdfReader reader) {
            if (!reader.IsOpenedWithFullPermissions)
                throw new BadPasswordException(MessageLocalization.GetComposedMessage("pdfreader.not.opened.with.owner.password"));
            if (readers2intrefs.ContainsKey(reader)) {
                reader = new PdfReader(reader);
            }
            else {
                if (reader.Tampered)
                    throw new DocumentException(MessageLocalization.GetComposedMessage("the.document.was.reused"));
                reader.ConsolidateNamedDestinations();
                reader.Tampered = true;
            }
            reader.ShuffleSubsetNames();
            readers2intrefs[reader] = new IntHashtable();

            visited[reader] = new IntHashtable();

            fields.Add(reader.AcroFields);
            UpdateCalculationOrder(reader);
        }

        /**
        * This merge fields is slightly different from the mergeFields method
        * of PdfCopyFields.
        */
        internal override void MergeFields() {
            for (int k = 0; k < fields.Count; ++k) {
                IDictionary<string,AcroFields.Item> fd = fields[k].Fields;
                MergeWithMaster(fd);
            }
        }
    }
}
