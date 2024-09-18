
namespace iTextSharp.GE.xmp.options {
    /// <summary>
    /// Options for <code>XMPIterator</code> construction.
    /// 
    /// @since 24.01.2006
    /// </summary>
    public sealed class IteratorOptions : XmpOptions {
        /// <summary>
        /// Just do the immediate children of the root, default is subtree. </summary>
        public const uint JUST_CHILDREN = 0x0100;

        /// <summary>
        /// Just do the leaf nodes, default is all nodes in the subtree.
        ///  Bugfix #2658965: If this option is set the Iterator returns the namespace 
        ///  of the leaf instead of the namespace of the base property. 
        /// </summary>
        public const uint JUST_LEAFNODES = 0x0200;

        /// <summary>
        /// Return just the leaf part of the path, default is the full path. </summary>
        public const uint JUST_LEAFNAME = 0x0400;

        //	/** Include aliases, default is just actual properties. <em>Note:</em> Not supported. 
        //	 *  @deprecated it is commonly preferred to work with the base properties */
        //	public static final int INCLUDE_ALIASES = 0x0800;
        /// <summary>
        /// Omit all qualifiers. </summary>
        public const uint OMIT_QUALIFIERS = 0x1000;


        /// <returns> Returns whether the option is set. </returns>
        public bool JustChildren {
            get { return GetOption(JUST_CHILDREN); }
            set { SetOption(JUST_CHILDREN, value); }
        }


        /// <returns> Returns whether the option is set. </returns>
        public bool JustLeafname {
            get { return GetOption(JUST_LEAFNAME); }
            set { SetOption(JUST_LEAFNAME, value); }
        }


        /// <returns> Returns whether the option is set. </returns>
        public bool JustLeafnodes {
            get { return GetOption(JUST_LEAFNODES); }
            set { SetOption(JUST_LEAFNODES, value); }
        }


        /// <returns> Returns whether the option is set. </returns>
        public bool OmitQualifiers {
            get { return GetOption(OMIT_QUALIFIERS); }
            set { SetOption(OMIT_QUALIFIERS, value); }
        }

        /// <seealso cref= Options#getValidOptions() </seealso>
        protected internal override uint ValidOptions {
            get { return JUST_CHILDREN | JUST_LEAFNODES | JUST_LEAFNAME | OMIT_QUALIFIERS; }
        }


        /// <seealso cref= Options#defineOptionName(int) </seealso>
        protected internal override string DefineOptionName(uint option) {
            switch (option) {
                case JUST_CHILDREN:
                    return "JUST_CHILDREN";
                case JUST_LEAFNODES:
                    return "JUST_LEAFNODES";
                case JUST_LEAFNAME:
                    return "JUST_LEAFNAME";
                case OMIT_QUALIFIERS:
                    return "OMIT_QUALIFIERS";
                default:
                    return null;
            }
        }
    }
}
