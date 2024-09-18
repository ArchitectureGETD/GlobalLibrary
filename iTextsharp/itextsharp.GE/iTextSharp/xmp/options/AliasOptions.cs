
namespace iTextSharp.GE.xmp.options {
    using XmpException = XmpException;


    /// <summary>
    /// Options for XMPSchemaRegistryImpl#registerAlias.
    /// 
    /// @since 20.02.2006
    /// </summary>
    public sealed class AliasOptions : XmpOptions {
        /// <summary>
        /// This is a direct mapping. The actual data type does not matter. </summary>
        public const uint PROP_DIRECT = 0;

        /// <summary>
        /// The actual is an unordered array, the alias is to the first element of the array. </summary>
        public const uint PROP_ARRAY = PropertyOptions.ARRAY;

        /// <summary>
        /// The actual is an ordered array, the alias is to the first element of the array. </summary>
        public const uint PROP_ARRAY_ORDERED = PropertyOptions.ARRAY_ORDERED;

        /// <summary>
        /// The actual is an alternate array, the alias is to the first element of the array. </summary>
        public const uint PROP_ARRAY_ALTERNATE = PropertyOptions.ARRAY_ALTERNATE;

        /// <summary>
        /// The actual is an alternate text array, the alias is to the 'x-default' element of the array.
        /// </summary>
        public const uint PROP_ARRAY_ALT_TEXT = PropertyOptions.ARRAY_ALT_TEXT;


        /// <seealso cref= Options#Options() </seealso>
        public AliasOptions() {
            // EMPTY
        }


        /// <param name="options"> the options to init with </param>
        /// <exception cref="XmpException"> If options are not consistant </exception>
        public AliasOptions(uint options)
            : base(options) {
        }


        /// <returns> Returns if the alias is of the simple form. </returns>
        public bool Simple {
            get { return Options == PROP_DIRECT; }
        }


        /// <returns> Returns the option. </returns>
        public bool Array {
            get { return GetOption(PROP_ARRAY); }
            set { SetOption(PROP_ARRAY, value); }
        }


        /// <returns> Returns the option. </returns>
        public bool ArrayOrdered {
            get { return GetOption(PROP_ARRAY_ORDERED); }
            set { SetOption(PROP_ARRAY | PROP_ARRAY_ORDERED, value); }
        }


        /// <returns> Returns the option. </returns>
        public bool ArrayAlternate {
            get { return GetOption(PROP_ARRAY_ALTERNATE); }
            set { SetOption(PROP_ARRAY | PROP_ARRAY_ORDERED | PROP_ARRAY_ALTERNATE, value); }
        }


        /// <returns> Returns the option. </returns>
        public bool ArrayAltText {
            get { return GetOption(PROP_ARRAY_ALT_TEXT); }
            set { SetOption(PROP_ARRAY | PROP_ARRAY_ORDERED | PROP_ARRAY_ALTERNATE | PROP_ARRAY_ALT_TEXT, value); }
        }

        /// <seealso cref= Options#getValidOptions() </seealso>
        protected internal override uint ValidOptions {
            get { return PROP_DIRECT | PROP_ARRAY | PROP_ARRAY_ORDERED | PROP_ARRAY_ALTERNATE | PROP_ARRAY_ALT_TEXT; }
        }


        /// <returns> returns a <seealso cref="PropertyOptions"/>s object </returns>
        /// <exception cref="XmpException"> If the options are not consistant.  </exception>
        public PropertyOptions ToPropertyOptions() {
            return new PropertyOptions(Options);
        }


        /// <seealso cref= Options#defineOptionName(int) </seealso>
        protected internal override string DefineOptionName(uint option) {
            switch (option) {
                case PROP_DIRECT:
                    return "PROP_DIRECT";
                case PROP_ARRAY:
                    return "ARRAY";
                case PROP_ARRAY_ORDERED:
                    return "ARRAY_ORDERED";
                case PROP_ARRAY_ALTERNATE:
                    return "ARRAY_ALTERNATE";
                case PROP_ARRAY_ALT_TEXT:
                    return "ARRAY_ALT_TEXT";
                default:
                    return null;
            }
        }
    }
}
