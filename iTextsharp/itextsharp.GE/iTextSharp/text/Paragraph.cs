using System;
using System.Collections.Generic;
using iTextSharp.GE.text.api;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.interfaces;

namespace iTextSharp.GE.text {
    /// <summary>
    /// A Paragraph is a series of Chunks and/or Phrases.
    /// </summary>
    /// <remarks>
    /// A Paragraph has the same qualities of a Phrase, but also
    /// some additional layout-parameters:
    /// <UL>
    /// <LI/>the indentation
    /// <LI/>the alignment of the text
    /// </UL>
    /// </remarks>
    /// <example>
    /// <code>
    /// <strong>Paragraph p = new Paragraph("This is a paragraph",
    ///                FontFactory.GetFont(FontFactory.HELVETICA, 18, Font.BOLDITALIC, new BaseColor(0, 0, 255)));</strong>
    ///    </code>
    /// </example>
    /// <seealso cref="T:iTextSharp.GE.text.Element"/>
    /// <seealso cref="T:iTextSharp.GE.text.Phrase"/>
    /// <seealso cref="T:iTextSharp.GE.text.ListItem"/>
    public class Paragraph : Phrase, IIndentable, ISpaceable, IAccessibleElement {
    
        // membervariables
    
        ///<summary> The alignment of the text. </summary>
        protected int alignment = Element.ALIGN_UNDEFINED;
        
        ///<summary> The indentation of this paragraph on the left side. </summary>
        protected float indentationLeft;
    
        ///<summary> The indentation of this paragraph on the right side. </summary>
        protected float indentationRight;
    
        /**
        * Holds value of property firstLineIndent.
        */
        private float firstLineIndent = 0;

    /** The spacing before the paragraph. */
        protected float spacingBefore;
        
    /** The spacing after the paragraph. */
        protected float spacingAfter;
        
        
        /**
        * Holds value of property extraParagraphSpace.
        */
        private float extraParagraphSpace = 0;
        
        ///<summary> Does the paragraph has to be kept together on 1 page. </summary>
        protected bool keeptogether = false;
        protected float paddingTop;
        protected PdfName role = PdfName.P;
        protected Dictionary<PdfName, PdfObject> accessibleAttributes = null;
        private AccessibleElementId id = new AccessibleElementId();

        // constructors
    
        /// <summary>
        /// Constructs a Paragraph.
        /// </summary>
        public Paragraph() : base() {}
    
        /// <summary>
        /// Constructs a Paragraph with a certain leading.
        /// </summary>
        /// <param name="leading">the leading</param>
        public Paragraph(float leading) : base(leading) {}
    
        /// <summary>
        /// Constructs a Paragraph with a certain Chunk.
        /// </summary>
        /// <param name="chunk">a Chunk</param>
        public Paragraph(Chunk chunk) : base(chunk) {}
    
        /// <summary>
        /// Constructs a Paragraph with a certain Chunk
        /// and a certain leading.
        /// </summary>
        /// <param name="leading">the leading</param>
        /// <param name="chunk">a Chunk</param>
        public Paragraph(float leading, Chunk chunk) : base(leading, chunk) {}
    
        /// <summary>
        /// Constructs a Paragraph with a certain string.
        /// </summary>
        /// <param name="str">a string</param>
        public Paragraph(string str) : base(str) {}
    
        /// <summary>
        /// Constructs a Paragraph with a certain string
        /// and a certain Font.
        /// </summary>
        /// <param name="str">a string</param>
        /// <param name="font">a Font</param>
        public Paragraph(string str, Font font) : base(str, font) {}
    
        /// <summary>
        /// Constructs a Paragraph with a certain string
        /// and a certain leading.
        /// </summary>
        /// <param name="leading">the leading</param>
        /// <param name="str">a string</param>
        public Paragraph(float leading, string str) : base(leading, str) {}
    
        /// <summary>
        /// Constructs a Paragraph with a certain leading, string
        /// and Font.
        /// </summary>
        /// <param name="leading">the leading</param>
        /// <param name="str">a string</param>
        /// <param name="font">a Font</param>
        public Paragraph(float leading, string str, Font font) : base(leading, str, font) {}
    
        /// <summary>
        /// Constructs a Paragraph with a certain Phrase.
        /// </summary>
        /// <param name="phrase">a Phrase</param>
        public Paragraph(Phrase phrase) : base(phrase) {
            if (phrase is Paragraph) {
                Paragraph p = (Paragraph)phrase;
                Alignment = p.Alignment;
                IndentationLeft = p.IndentationLeft;
                IndentationRight = p.IndentationRight;
                FirstLineIndent = p.FirstLineIndent;
                SpacingAfter = p.SpacingAfter;
                SpacingBefore = p.SpacingBefore;
                ExtraParagraphSpace = p.ExtraParagraphSpace;
                Role = p.role;
                id = p.ID;
                if (p.accessibleAttributes != null)
                    accessibleAttributes = new Dictionary<PdfName, PdfObject>(p.accessibleAttributes);
            }
        }

        /**
         * Creates a shallow clone of the Paragraph.
         * @return
         */
        public virtual Paragraph CloneShallow(bool spacingBefore) {
            Paragraph copy = new Paragraph();
            PopulateProperties(copy, spacingBefore);
    	    return copy;
        }

        protected void PopulateProperties(Paragraph copy, bool spacingBefore) {
            copy.Font = Font;
            copy.Alignment = Alignment;
            copy.SetLeading(Leading, multipliedLeading);
            copy.IndentationLeft = IndentationLeft;
            copy.IndentationRight = IndentationRight;
            copy.FirstLineIndent = FirstLineIndent;
            copy.SpacingAfter = SpacingAfter;
            if (spacingBefore)
                copy.SpacingBefore = SpacingBefore;
            copy.ExtraParagraphSpace = ExtraParagraphSpace;
            copy.Role = Role;
            copy.id = ID;
            if (accessibleAttributes != null)
                copy.accessibleAttributes = new Dictionary<PdfName, PdfObject>(accessibleAttributes);
            copy.TabSettings = this.TabSettings;
            copy.KeepTogether = this.KeepTogether;
        }

        /**
         * Creates a shallow clone of the Paragraph.
         * @return
         */
        [Obsolete]
        public virtual Paragraph cloneShallow(bool spacingBefore) {
            return CloneShallow(spacingBefore);
        }

        /**
         * Breaks this Paragraph up in different parts, separating paragraphs, lists and tables from each other.
         * @return
         */
        virtual public IList<IElement> BreakUp() {
            IList<IElement> list = new List<IElement>();
            Paragraph tmp = null;
            foreach (IElement e in this) {
                if (e.Type == Element.LIST || e.Type == Element.PTABLE || e.Type == Element.PARAGRAPH) {
                    if (tmp != null && tmp.Count > 0) {
                        tmp.SpacingAfter = 0;
                        list.Add(tmp);
                        tmp = CloneShallow(false);
                    }
                    if (list.Count == 0) {
                        switch (e.Type) {
                            case Element.PTABLE:
                                ((PdfPTable)e).SpacingBefore = SpacingBefore;
                                break;
                            case Element.PARAGRAPH:
                                ((Paragraph)e).SpacingBefore = SpacingBefore;
                                break;
                            case Element.LIST:
                                ListItem firstItem = ((List)e).GetFirstItem();
                                if (firstItem != null) {
                                    firstItem.SpacingBefore = SpacingBefore;
                                }
                                break;
                        }
                    }
                    list.Add(e);
                }
                else {
                    if (tmp == null) {
                        tmp = CloneShallow(list.Count == 0);
                    }
                    tmp.Add(e);
                }
            }
            if (tmp != null && tmp.Count > 0) {
                list.Add(tmp);
            }
            if (list.Count != 0) {
                IElement lastElement = list[list.Count - 1];
                switch (lastElement.Type) {
                    case Element.PTABLE:
                        ((PdfPTable)lastElement).SpacingAfter = SpacingAfter;
                        break;
                    case Element.PARAGRAPH:
                        ((Paragraph)lastElement).SpacingAfter = SpacingAfter;
                        break;
                    case Element.LIST:
                        ListItem lastItem = ((List)lastElement).GetLastItem();
                        if (lastItem != null) {
                            lastItem.SpacingAfter = SpacingAfter;
                        }
                        break;
                }
            }
            return list;
        }

        /**
         * Breaks this Paragraph up in different parts, separating paragraphs, lists and tables from each other.
         * @return
         */
        [Obsolete]
        public IList<IElement> breakUp() {
            return BreakUp();
        }


        // implementation of the Element-methods
    
        /// <summary>
        /// Gets the type of the text element.
        /// </summary>
        /// <value>a type</value>
        public override int Type {
            get {
                return Element.PARAGRAPH;
            }
        }
    
        // methods
    
        /// <summary>
        /// Adds an Object to the Paragraph.
        /// </summary>
        /// <param name="o">the object to add</param>
        /// <returns>a bool</returns>
        public override bool Add(IElement o) {
            if (o is List) {
                List list = (List) o;
                list.IndentationLeft = list.IndentationLeft + indentationLeft;
                list.IndentationRight = indentationRight;
                base.Add(list);
                return true;
            }
            else if (o is Image) {
                base.AddSpecial((Image) o);
                return true;
            }
            else if (o is Paragraph) {
                base.AddSpecial(o);
                return true;
            }
            base.Add(o);
            return true;
        }
    
        // setting the membervariables
        
    
        /// <summary>
        /// Get/set the alignment of this paragraph.
        /// </summary>
        /// <value>a integer</value>
        virtual public int Alignment{
            get {
                return alignment;
            }
            set {
                this.alignment = value;
            }
        }
    
        /// <summary>
        /// Get/set the indentation of this paragraph on the left side.
        /// </summary>
        /// <value>a float</value>
        virtual public float IndentationLeft {
            get {
                return indentationLeft;
            }

            set {
                this.indentationLeft = value;
            }
        }
    
        /// <summary>
        /// Get/set the indentation of this paragraph on the right side.
        /// </summary>
        /// <value>a float</value>
        virtual public float IndentationRight {
            get {
                return indentationRight;
            }
            
            set {
                this.indentationRight = value;
            }
        }
    
        virtual public float SpacingBefore {
            get {
                return spacingBefore;
            }
            set {
                spacingBefore = value;
            }
        }

        virtual public float SpacingAfter {
            get {
                return spacingAfter;
            }
            set {
                spacingAfter = value;
            }
        }

        /// <summary>
        /// Set/get if this paragraph has to be kept together on one page.
        /// </summary>
        /// <value>a bool</value>
        virtual public bool KeepTogether {
            get {
                return keeptogether;
            }
            set {
                this.keeptogether = value;
            }
        }    

        virtual public float FirstLineIndent {
            get {
                return this.firstLineIndent;
            }
            set {
                this.firstLineIndent = value;
            }
        }

        virtual public float ExtraParagraphSpace {
            get {
                return this.extraParagraphSpace;
            }
            set {
                this.extraParagraphSpace = value;
            }
        }

        virtual public PdfObject GetAccessibleAttribute(PdfName key) {
            if (accessibleAttributes != null) {
                PdfObject value;
                accessibleAttributes.TryGetValue(key, out value);
                return value;
            } else
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

        virtual public PdfName Role {
            get { return role; }
            set { this.role = value; }
        }

        virtual public AccessibleElementId ID {
            get
            {
                if (id == null)
                    id = new AccessibleElementId();
                return id;
            }
            set { id = value; }
        }

        public virtual bool IsInline {
            get { return false; }
        }

        public virtual float PaddingTop
        {
            get { return paddingTop; }
            set { paddingTop = value; } 
        }
    }
}
