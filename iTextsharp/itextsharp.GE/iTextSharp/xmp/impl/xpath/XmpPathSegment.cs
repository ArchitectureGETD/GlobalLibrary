namespace iTextSharp.GE.xmp.impl.xpath {
    /// <summary>
    /// A segment of a parsed <code>XmpPath</code>.
    ///  
    /// @since   23.06.2006
    /// </summary>
    public class XmpPathSegment {
        /// <summary>
        /// flag if segment is an alias </summary>
        private bool _alias;

        /// <summary>
        /// alias form if applicable </summary>
        private uint _aliasForm;

        /// <summary>
        /// kind of the path segment </summary>
        private uint _kind;

        /// <summary>
        /// name of the path segment </summary>
        private string _name;


        /// <summary>
        /// Constructor with initial values.
        /// </summary>
        /// <param name="name"> the name of the segment </param>
        public XmpPathSegment(string name) {
            _name = name;
        }


        /// <summary>
        /// Constructor with initial values.
        /// </summary>
        /// <param name="name"> the name of the segment </param>
        /// <param name="kind"> the kind of the segment </param>
        public XmpPathSegment(string name, uint kind) {
            _name = name;
            _kind = kind;
        }


        /// <returns> Returns the kind. </returns>
        public virtual uint Kind {
            get { return _kind; }
            set { _kind = value; }
        }


        /// <returns> Returns the name. </returns>
        public virtual string Name {
            get { return _name; }
            set { _name = value; }
        }


        /// <param name="alias"> the flag to set </param>
        public virtual bool Alias {
            set { _alias = value; }
            get { return _alias; }
        }


        /// <returns> Returns the aliasForm if this segment has been created by an alias. </returns>
        public virtual uint AliasForm {
            get { return _aliasForm; }
            set { _aliasForm = value; }
        }


        /// <seealso cref= Object#toString() </seealso>
        public override string ToString() {
            switch (_kind) {
                case XmpPath.STRUCT_FIELD_STEP:
                case XmpPath.ARRAY_INDEX_STEP:
                case XmpPath.QUALIFIER_STEP:
                case XmpPath.ARRAY_LAST_STEP:
                    return _name;
                case XmpPath.QUAL_SELECTOR_STEP:
                case XmpPath.FIELD_SELECTOR_STEP:
                    return _name;

                default:
                    // no defined step
                    return _name;
            }
        }
    }
}
