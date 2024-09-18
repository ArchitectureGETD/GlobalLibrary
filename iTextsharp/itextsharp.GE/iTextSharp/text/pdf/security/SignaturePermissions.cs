using System;
using System.Collections.Generic;

/**
 * A helper class that tells you more about the type of signature
 * (certification or approval) and the signature's DMP settings.
 */
namespace iTextSharp.GE.text.pdf.security {
	public class SignaturePermissions {

        /**
	     * Class that contains a field lock action and
	     * an array of the fields that are involved.
	     */
        public class FieldLock {
            /** Can be /All, /Exclude or /Include */
            readonly PdfName action;
            /** An array of PdfString values with fieldnames */
            readonly PdfArray fields;
            /** Creates a FieldLock instance */
            public FieldLock(PdfName action, PdfArray fields) {
                this.action = action;
                this.fields = fields;
            }
            /** Getter for the field lock action. */
            virtual public PdfName Action {
                get { return action; }
            }
            /** Getter for the fields involved in the lock action. */
            virtual public PdfArray Fields {
                get { return fields; }
            }
            /** toString method */
            public override String ToString() {
                return action + (fields == null ? "" : fields.ToString());
            }
        }

        /** Is the signature a cerification signature (true) or an approval signature (false)? */
        readonly bool certification;
        /** Is form filling allowed by this signature? */
        readonly bool fillInAllowed = true;
        /** Is adding annotations allowed by this signature? */
        readonly bool annotationsAllowed = true;
        /** Does this signature lock specific fields? */
        readonly List<FieldLock> fieldLocks = new List<FieldLock>();

        /**
         * Creates an object that can inform you about the type of signature
         * in a signature dictionary as well as some of the permissions
         * defined by the signature.
         */
        public SignaturePermissions(PdfDictionary sigDict, SignaturePermissions previous) {
	        if (previous != null) {
		        annotationsAllowed &= previous.AnnotationsAllowed;
		        fillInAllowed &= previous.FillInAllowed;
		        fieldLocks.AddRange(previous.FieldLocks);
	        }
	        PdfArray reference = sigDict.GetAsArray(PdfName.REFERENCE);
	        if (reference != null) {
		        for (int i = 0; i < reference.Size; i++) {
			        PdfDictionary dict = reference.GetAsDict(i);
			        PdfDictionary parameters = dict.GetAsDict(PdfName.TRANSFORMPARAMS);
			        if (PdfName.DOCMDP.Equals(dict.GetAsName(PdfName.TRANSFORMMETHOD)))
				        certification = true;
    			    
			        PdfName action = parameters.GetAsName(PdfName.ACTION);
			        if (action != null)
                        fieldLocks.Add(new FieldLock(action, parameters.GetAsArray(PdfName.FIELDS)));
    			    
			        PdfNumber p = parameters.GetAsNumber(PdfName.P);
			        if (p == null)
				        continue;
			        switch (p.IntValue) {
			        case 1:
				        fillInAllowed = false;
                        break;
			        case 2:
				        annotationsAllowed = false;
                        break;
			        }
		        }
	        }
        }

        /**
         * Getter to find out if the signature is a certification signature.
         * @return true if the signature is a certification signature, false for an approval signature.
         */
        virtual public bool Certification {
            get { return certification; }
        }
        /**
         * Getter to find out if filling out fields is allowed after signing.
         * @return true if filling out fields is allowed
         */
        virtual public bool FillInAllowed {
            get { return fillInAllowed; }
        }
        /**
         * Getter to find out if adding annotations is allowed after signing.
         * @return true if adding annotations is allowed
         */
        virtual public bool AnnotationsAllowed {
            get { return annotationsAllowed; }
        }
        /**
         * Getter for the field lock actions, and fields that are impacted by the action
         * @return an Array with field names
         */
        virtual public List<FieldLock> FieldLocks {
            get { return fieldLocks; }
        }
	}
}
