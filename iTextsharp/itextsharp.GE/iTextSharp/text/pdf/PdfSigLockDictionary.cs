using System;
namespace iTextSharp.GE.text.pdf {

    /**
     * A signature field lock dictionary.
     */
    public class PdfSigLockDictionary : PdfDictionary  {
        
        /**
         * Enumerates the different actions of a signature lock.
         * Indicates the set of fields that should be locked:
         * all the fields in the document,
         * all the fields specified in the /Fields array
         * all the fields except those specified in the /Fields array
         */
        public class LockAction {
            public static readonly LockAction ALL = new LockAction(PdfName.ALL);
            public static readonly LockAction INCLUDE = new LockAction(PdfName.INCLUDE);
            public static readonly LockAction EXCLUDE = new LockAction(PdfName.EXCLUDE);

            private PdfName name;
            
            private LockAction(PdfName name) {
                this.name = name;
            }
            
            virtual public PdfName Value {
                get {
                    return name;
                }
            }
        }

        /**
         * Enumerates the different levels of permissions.
         */
        public class LockPermissions {
            public static readonly LockPermissions NO_CHANGES_ALLOWED = new LockPermissions(1);
            public static readonly LockPermissions FORM_FILLING = new LockPermissions(2);
            public static readonly LockPermissions FORM_FILLING_AND_ANNOTATION = new LockPermissions(3);
            
            private PdfNumber number;
            
            private LockPermissions(int p) {
                number = new PdfNumber(p);
            }
            
            virtual public PdfNumber Value {
                get {
                    return number;
                }
            }
        }
        
        /**
         * Creates a signature lock valid for all fields in the document.
         */
        public PdfSigLockDictionary() : base(PdfName.SIGFIELDLOCK) {
            this.Put(PdfName.ACTION, LockAction.ALL.Value);
        }
        
        /**
         * Creates a signature lock for all fields in the document,
         * setting specific permissions.
         */
        public PdfSigLockDictionary(LockPermissions p) : this() {
           this.Put(PdfName.P, p.Value);
        }
        
        /**
         * Creates a signature lock for specific fields in the document.
         */
        public PdfSigLockDictionary(LockAction action, params String[] fields) : this(action, null, fields) {
        }
        
        /**
         * Creates a signature lock for specific fields in the document.
         */
        public PdfSigLockDictionary(LockAction action, LockPermissions p, params String[] fields) : base(PdfName.SIGFIELDLOCK) {
            this.Put(PdfName.ACTION, action.Value);
            if (p != null)
                this.Put(PdfName.P, p.Value);
            PdfArray fieldsArray = new PdfArray();
            foreach (String field in fields) {
                fieldsArray.Add(new PdfString(field));
            }
            this.Put(PdfName.FIELDS, fieldsArray);
        }
    }
}
