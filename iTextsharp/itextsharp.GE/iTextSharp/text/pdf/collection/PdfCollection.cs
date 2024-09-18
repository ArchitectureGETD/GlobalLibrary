using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.collection {

    public class PdfCollection : PdfDictionary {

        /** A type of PDF Collection */
        public const int DETAILS = 0;
        /** A type of PDF Collection */
        public const int TILE = 1;
        /** A type of PDF Collection */
        public const int HIDDEN = 2;
        /** A type of PDF Collection */
        public const int CUSTOM = 3;
        
        /**
        * Constructs a PDF Collection.
        * @param   type    the type of PDF collection.
        */
        public PdfCollection(int type) : base(PdfName.COLLECTION) {
            switch(type) {
            case TILE:
                Put(PdfName.VIEW, PdfName.T);
                break;
            case HIDDEN:
                Put(PdfName.VIEW, PdfName.H);
                break;
            case CUSTOM:
                Put(PdfName.VIEW, PdfName.C);
                break;
            default:
                Put(PdfName.VIEW, PdfName.D);
                break;
            }
        }
        
        /**
        * Identifies the document that will be initially presented
        * in the user interface.
        * @param description   the description that was used when attaching the file to the document
        */
        virtual public String InitialDocument {
            set {
                Put(PdfName.D, new PdfString(value, null));
            }
        }
        
        /**
        * Sets the Collection schema dictionary.
        * @param schema    an overview of the collection fields
        */
        virtual public PdfCollectionSchema Schema {
            set {
                Put(PdfName.SCHEMA, value);
            }
            get {
                return (PdfCollectionSchema)Get(PdfName.SCHEMA);
            }
        }
        
        /**
        * Sets the Collection sort dictionary.
        * @param sort  a collection sort dictionary
        */
        virtual public PdfCollectionSort Sort {
            set {
                Put(PdfName.SORT, value);
            }
        }
    }
}
