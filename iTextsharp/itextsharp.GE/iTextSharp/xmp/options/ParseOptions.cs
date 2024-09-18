
namespace iTextSharp.GE.xmp.options {
    /// <summary>
    /// Options for <seealso cref="XmpMetaFactory.Parse(System.IO.Stream, ParseOptions)"/>.
    /// 
    /// @since 24.01.2006
    /// </summary>
    public sealed class ParseOptions : XmpOptions {
        /// <summary>
        /// Require a surrounding &quot;x:xmpmeta&quot; element in the xml-document. </summary>
        public const uint REQUIRE_XMP_META = 0x0001;

        /// <summary>
        /// Do not reconcile alias differences, throw an exception instead. </summary>
        public const uint STRICT_ALIASING = 0x0004;

        /// <summary>
        /// Convert ASCII control characters 0x01 - 0x1F (except tab, cr, and lf) to spaces. </summary>
        public const uint FIX_CONTROL_CHARS = 0x0008;

        /// <summary>
        /// If the input is not unicode, try to parse it as ISO-8859-1. </summary>
        public const uint ACCEPT_LATIN_1 = 0x0010;

        /// <summary>
        /// Do not carry run the XMPNormalizer on a packet, leave it as it is. </summary>
        public const uint OMIT_NORMALIZATION = 0x0020;

        /// <summary>
        /// Sets the options to the default values.
        /// </summary>
        public ParseOptions() {
            SetOption(FIX_CONTROL_CHARS | ACCEPT_LATIN_1, true);
        }


        /// <returns> Returns the requireXMPMeta. </returns>
        public bool RequireXmpMeta {
            get { return GetOption(REQUIRE_XMP_META); }
            set { SetOption(REQUIRE_XMP_META, value); }
        }

        /// <returns> Returns the strictAliasing. </returns>
        public bool StrictAliasing {
            get { return GetOption(STRICT_ALIASING); }
            set { SetOption(STRICT_ALIASING, value);}
        }

        /// <returns> Returns the strictAliasing. </returns>
        public bool FixControlChars {
            get { return GetOption(FIX_CONTROL_CHARS); }
            set { SetOption(FIX_CONTROL_CHARS, value); }
        }

        /// <returns> Returns the strictAliasing. </returns>
        public bool AcceptLatin1 {
            get { return GetOption(ACCEPT_LATIN_1); }
            set {SetOption(ACCEPT_LATIN_1, value);}
        }

        /// <returns> Returns the option "omit normalization". </returns>
        public bool OmitNormalization {
            get { return GetOption(OMIT_NORMALIZATION); }
            set { SetOption(OMIT_NORMALIZATION, value); }
        }

        /// <seealso cref= Options#getValidOptions() </seealso>
        protected internal override uint ValidOptions {
            get { return REQUIRE_XMP_META | STRICT_ALIASING | FIX_CONTROL_CHARS | ACCEPT_LATIN_1 | OMIT_NORMALIZATION; }
        }

        /// <seealso cref= Options#defineOptionName(int) </seealso>
        protected internal override string DefineOptionName(uint option) {
            switch (option) {
                case REQUIRE_XMP_META:
                    return "REQUIRE_XMP_META";
                case STRICT_ALIASING:
                    return "STRICT_ALIASING";
                case FIX_CONTROL_CHARS:
                    return "FIX_CONTROL_CHARS";
                case ACCEPT_LATIN_1:
                    return "ACCEPT_LATIN_1";
                case OMIT_NORMALIZATION:
                    return "OMIT_NORMALIZATION";
                default:
                    return null;
            }
        }
    }
}
