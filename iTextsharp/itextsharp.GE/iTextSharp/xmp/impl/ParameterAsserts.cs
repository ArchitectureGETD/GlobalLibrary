
namespace iTextSharp.GE.xmp.impl {
    /// <summary>
    /// @since   11.08.2006
    /// </summary>
    internal class ParameterAsserts : XmpConst {
        /// <summary>
        /// private constructor
        /// </summary>
        private ParameterAsserts() {
            // EMPTY
        }


        /// <summary>
        /// Asserts that an array name is set. </summary>
        /// <param name="arrayName"> an array name </param>
        /// <exception cref="XmpException"> Array name is null or empty </exception>
        public static void AssertArrayName(string arrayName) {
            if (string.IsNullOrEmpty(arrayName)) {
                throw new XmpException("Empty array name", XmpError.BADPARAM);
            }
        }


        /// <summary>
        /// Asserts that a property name is set. </summary>
        /// <param name="propName"> a property name or path </param>
        /// <exception cref="XmpException"> Property name is null or empty </exception>
        public static void AssertPropName(string propName) {
            if (string.IsNullOrEmpty(propName)) {
                throw new XmpException("Empty property name", XmpError.BADPARAM);
            }
        }


        /// <summary>
        /// Asserts that a schema namespace is set. </summary>
        /// <param name="schemaNs"> a schema namespace </param>
        /// <exception cref="XmpException"> Schema is null or empty </exception>
        public static void AssertSchemaNs(string schemaNs) {
            if (string.IsNullOrEmpty(schemaNs)) {
                throw new XmpException("Empty schema namespace URI", XmpError.BADPARAM);
            }
        }


        /// <summary>
        /// Asserts that a prefix is set. </summary>
        /// <param name="prefix"> a prefix </param>
        /// <exception cref="XmpException"> Prefix is null or empty </exception>
        public static void AssertPrefix(string prefix) {
            if (string.IsNullOrEmpty(prefix)) {
                throw new XmpException("Empty prefix", XmpError.BADPARAM);
            }
        }


        /// <summary>
        /// Asserts that a specific language is set. </summary>
        /// <param name="specificLang"> a specific lang </param>
        /// <exception cref="XmpException"> Specific language is null or empty </exception>
        public static void AssertSpecificLang(string specificLang) {
            if (string.IsNullOrEmpty(specificLang)) {
                throw new XmpException("Empty specific language", XmpError.BADPARAM);
            }
        }


        /// <summary>
        /// Asserts that a struct name is set. </summary>
        /// <param name="structName"> a struct name </param>
        /// <exception cref="XmpException"> Struct name is null or empty </exception>
        public static void AssertStructName(string structName) {
            if (string.IsNullOrEmpty(structName)) {
                throw new XmpException("Empty array name", XmpError.BADPARAM);
            }
        }


        /// <summary>
        /// Asserts that any string parameter is set. </summary>
        /// <param name="param"> any string parameter </param>
        /// <exception cref="XmpException"> Thrown if the parameter is null or has length 0. </exception>
        public static void AssertNotNull(object param) {
            if (param == null) {
                throw new XmpException("Parameter must not be null", XmpError.BADPARAM);
            }
            if ((param is string) && ((string) param).Length == 0) {
                throw new XmpException("Parameter must not be null or empty", XmpError.BADPARAM);
            }
        }


        /// <summary>
        /// Asserts that the xmp object is of this implemention
        /// (<seealso cref="XmpMetaImpl"/>). </summary>
        /// <param name="xmp"> the XMP object </param>
        /// <exception cref="XmpException"> A wrong implentaion is used. </exception>
        public static void AssertImplementation(IXmpMeta xmp) {
            if (xmp == null) {
                throw new XmpException("Parameter must not be null", XmpError.BADPARAM);
            }
            if (!(xmp is XmpMetaImpl)) {
                throw new XmpException("The XMPMeta-object is not compatible with this implementation",
                                       XmpError.BADPARAM);
            }
        }
    }
}
