using System;
using System.Text;

namespace iTextSharp.GE.text {
    /// <summary>
    /// This is an Element that contains
    /// some userdefined meta information about the document.
    /// </summary>
    /// <example>
    /// <code>
    /// <strong>Header header = new Header("inspired by", "William Shakespeare");</strong>
    /// </code>
    /// </example>
    public class Header : Meta {
    
        // membervariables
    
        /// <summary> This is the content of this chunk of text. </summary>
        private StringBuilder name;
    
        // constructors
    
        /// <summary>
        /// Constructs a Header.
        /// </summary>
        /// <param name="name">the name of the meta-information</param>
        /// <param name="content">the content</param>
        public Header(string name, string content) : base(Element.HEADER, content) {
            this.name = new StringBuilder(name);
        }
    
        // methods to retrieve information
    
        /// <summary>
        /// Returns the name of the meta information.
        /// </summary>
        /// <value>a string</value>
        public override string Name {
            get {
                return name.ToString();
            }
        }
    }
}
