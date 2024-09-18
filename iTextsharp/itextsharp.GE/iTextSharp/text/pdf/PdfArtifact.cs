using System;
using System.Collections.Generic;
using System.util.collections;
using iTextSharp.GE.text.error_messages;
using iTextSharp.GE.text.pdf.interfaces;

namespace iTextSharp.GE.text.pdf
{
    public class PdfArtifact : IAccessibleElement {

        protected PdfName role = PdfName.ARTIFACT;
        protected Dictionary<PdfName, PdfObject> accessibleAttributes = null;
        protected AccessibleElementId id = new AccessibleElementId();

        private static readonly HashSet2<String> allowedArtifactTypes = new HashSet2<string>(new string[] { "Pagination", "Layout", "Page", "Background" }); 

        virtual public PdfObject GetAccessibleAttribute(PdfName key) {
            if (accessibleAttributes != null)
                return accessibleAttributes[key];
            else
                return null;
        }

        virtual public void SetAccessibleAttribute(PdfName key, PdfObject value) {
            if (accessibleAttributes == null)
                accessibleAttributes = new Dictionary<PdfName, PdfObject>();
            accessibleAttributes[key] = value;
        }

        virtual public Dictionary<PdfName, PdfObject> GetAccessibleAttributes() {
            return accessibleAttributes;
        }

        virtual public PdfName Role
        {
            get { return role; }
            set { role = value; }
        }

        virtual public AccessibleElementId ID
        {
            get { return id; }
            set { id = value; }
        }

        public virtual bool IsInline {
            get { return true; }
        }

        virtual public PdfString Type {
            get {
                if (accessibleAttributes == null)
                    return null;
                PdfObject pdfObject;
                accessibleAttributes.TryGetValue(PdfName.TYPE, out pdfObject);
                return (PdfString) pdfObject;
            }
            set {
                if (!allowedArtifactTypes.Contains(value.ToString()))
                    throw new ArgumentException(MessageLocalization.GetComposedMessage("the.artifact.type.1.is.invalid", value));
                SetAccessibleAttribute(PdfName.TYPE, value);
            }
        }

        virtual public void SetType(PdfArtifact.ArtifactType type) {
            PdfString artifactType = null;
            switch (type)
            {
                case ArtifactType.BACKGROUND:
                    artifactType = new PdfString("Background");
                    break;
                case ArtifactType.LAYOUT:
                    artifactType = new PdfString("Layout");
                    break;
                case ArtifactType.PAGE:
                    artifactType = new PdfString("Page");
                    break;
                case ArtifactType.PAGINATION:
                    artifactType = new PdfString("Pagination");
                    break;
            }
            SetAccessibleAttribute(PdfName.TYPE, artifactType);
        }

        virtual public PdfArray BBox {
            get {
                if (accessibleAttributes == null)
                    return null;
                PdfObject pdfObject;
                accessibleAttributes.TryGetValue(PdfName.BBOX, out pdfObject);
                return (PdfArray) pdfObject;
            }
            set {
                SetAccessibleAttribute(PdfName.BBOX, value);
            }
        }

        virtual public PdfArray Attached {
            get {
                if(accessibleAttributes == null)
                    return null;
                PdfObject pdfObject;
                accessibleAttributes.TryGetValue(PdfName.ATTACHED, out pdfObject);
                return (PdfArray)pdfObject;
            }
            set {
                SetAccessibleAttribute(PdfName.ATTACHED, value);
            }
        }

        public enum ArtifactType {
            PAGINATION,
            LAYOUT,
            PAGE,
            BACKGROUND
        }
    }
}
