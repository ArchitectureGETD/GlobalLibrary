using System;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.interfaces {

    /**
    * The PDF version is described in the PDF Reference 1.7 p92
    * (about the PDF Header) and page 139 (the version entry in
    * the Catalog). You'll also find info about setting the version
    * in the book 'iText in Action' sections 2.1.3 (PDF Header)
    * and 3.3 (Version history).
    */

    public interface IPdfVersion {
        
        /**
        * If the PDF Header hasn't been written yet,
        * this changes the version as it will appear in the PDF Header.
        * If the PDF header was already written to the Stream,
        * this changes the version as it will appear in the Catalog.
        * @param version   a character representing the PDF version
        */
        char PdfVersion {
            set;
        }

        /**
        * If the PDF Header hasn't been written yet,
        * this changes the version as it will appear in the PDF Header,
        * but only if param refers to a higher version.
        * If the PDF header was already written to the Stream,
        * this changes the version as it will appear in the Catalog.
        * @param version   a character representing the PDF version
        */
        void SetAtLeastPdfVersion(char version);
        /**
        * Sets the PDF version as it will appear in the Catalog.
        * Note that this only has effect if you use a later version
        * than the one that appears in the header. This method
        * ignores the parameter if you try to set a lower version
        * than the one currently set in the Catalog.
        * @param version   the PDF name that will be used for the Version key in the catalog
        */
        void SetPdfVersion(PdfName version);
        /**
        * Adds a developer extension to the Extensions dictionary
        * in the Catalog.
        * @param de an object that contains the extensions prefix and dictionary
        * @since    2.1.6
        */
        void AddDeveloperExtension(PdfDeveloperExtension de);
    }
}
