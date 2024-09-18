
using iTextSharp.GE.xmp.impl;
using iTextSharp.GE.xmp.impl.xpath;

namespace iTextSharp.GE.xmp {
    /// <summary>
    /// Utility services for the metadata object. It has only public static functions, you cannot create
    /// an object. These are all functions that layer cleanly on top of the core XMP toolkit.
    /// <p>
    /// These functions provide support for composing path expressions to deeply nested properties. The
    /// functions <code>XMPMeta</code> such as <code>getProperty()</code>,
    /// <code>getArrayItem()</code> and <code>getStructField()</code> provide easy access to top
    /// level simple properties, items in top level arrays, and fields of top level structs. They do not
    /// provide convenient access to more complex things like fields several levels deep in a complex
    /// struct, or fields within an array of structs, or items of an array that is a field of a struct.
    /// These functions can also be used to compose paths to top level array items or struct fields so
    /// that you can use the binary accessors like <code>getPropertyAsInteger()</code>.
    /// <p>
    /// You can use these functions is to compose a complete path expression, or all but the last
    /// component. Suppose you have a property that is an array of integers within a struct. You can
    /// access one of the array items like this:
    /// <p>
    /// <blockquote>
    /// 
    /// <pre>
    ///      String path = XMPPathFactory.composeStructFieldPath (schemaNs, &quot;Struct&quot;, fieldNs,
    ///          &quot;Array&quot;);
    ///      String path += XMPPathFactory.composeArrayItemPath (schemaNs, &quot;Array&quot; index);
    ///      PropertyInteger result = xmpObj.getPropertyAsInteger(schemaNs, path);
    /// </pre>
    /// 
    /// </blockquote> You could also use this code if you want the string form of the integer:
    /// <blockquote>
    /// 
    /// <pre>
    ///      String path = XMPPathFactory.composeStructFieldPath (schemaNs, &quot;Struct&quot;, fieldNs,
    ///          &quot;Array&quot;);
    ///      PropertyText xmpObj.getArrayItem (schemaNs, path, index);
    /// </pre>
    /// 
    /// </blockquote>
    /// <p>
    /// <em>Note:</em> It might look confusing that the schemaNs is passed in all of the calls above.
    /// This is because the XMP toolkit keeps the top level &quot;schema&quot; namespace separate from
    /// the rest of the path expression.
    /// <em>Note:</em> These methods are much simpler than in the C++-API, they don't check the given
    /// path or array indices.
    /// 
    /// @since 25.01.2006
    /// </summary>
    public static class XmpPathFactory {
        /// <summary>
        /// Compose the path expression for an item in an array.
        /// </summary>
        /// <param name="arrayName"> The name of the array. May be a general path expression, must not be
        ///        <code>null</code> or the empty string. </param>
        /// <param name="itemIndex"> The index of the desired item. Arrays in XMP are indexed from 1.
        /// 		  0 and below means last array item and renders as <code>[last()]</code>.	
        /// </param>
        /// <returns> Returns the composed path basing on fullPath. This will be of the form
        ///         <tt>ns:arrayName[i]</tt>, where &quot;ns&quot; is the prefix for schemaNs and
        ///         &quot;i&quot; is the decimal representation of itemIndex. </returns>
        /// <exception cref="XmpException"> Throws exeption if index zero is used. </exception>
        public static string ComposeArrayItemPath(string arrayName, int itemIndex) {
            if (itemIndex > 0) {
                return arrayName + '[' + itemIndex + ']';
            }
            if (itemIndex == XmpConst.ARRAY_LAST_ITEM) {
                return arrayName + "[last()]";
            }
            throw new XmpException("Array index must be larger than zero", XmpError.BADINDEX);
        }


        /// <summary>
        /// Compose the path expression for a field in a struct. The result can be added to the
        /// path of 
        /// 
        /// </summary>
        /// <param name="fieldNs"> The namespace URI for the field. Must not be <code>null</code> or the empty
        ///        string. </param>
        /// <param name="fieldName"> The name of the field. Must be a simple XML name, must not be
        ///        <code>null</code> or the empty string. </param>
        /// <returns> Returns the composed path. This will be of the form
        ///         <tt>ns:structName/fNS:fieldName</tt>, where &quot;ns&quot; is the prefix for
        ///         schemaNs and &quot;fNS&quot; is the prefix for fieldNs. </returns>
        /// <exception cref="XmpException"> Thrown if the path to create is not valid. </exception>
        public static string ComposeStructFieldPath(string fieldNs, string fieldName) {
            AssertFieldNs(fieldNs);
            AssertFieldName(fieldName);

            XmpPath fieldPath = XmpPathParser.ExpandXPath(fieldNs, fieldName);
            if (fieldPath.Size() != 2) {
                throw new XmpException("The field name must be simple", XmpError.BADXPATH);
            }

            return '/' + fieldPath.GetSegment((int) XmpPath.STEP_ROOT_PROP).Name;
        }


        /// <summary>
        /// Compose the path expression for a qualifier.
        /// </summary>
        /// <param name="qualNs"> The namespace URI for the qualifier. May be <code>null</code> or the empty
        ///        string if the qualifier is in the XML empty namespace. </param>
        /// <param name="qualName"> The name of the qualifier. Must be a simple XML name, must not be
        ///        <code>null</code> or the empty string. </param>
        /// <returns> Returns the composed path. This will be of the form
        ///         <tt>ns:propName/?qNS:qualName</tt>, where &quot;ns&quot; is the prefix for
        ///         schemaNs and &quot;qNS&quot; is the prefix for qualNs. </returns>
        /// <exception cref="XmpException"> Thrown if the path to create is not valid. </exception>
        public static string ComposeQualifierPath(string qualNs, string qualName) {
            AssertQualNs(qualNs);
            AssertQualName(qualName);

            XmpPath qualPath = XmpPathParser.ExpandXPath(qualNs, qualName);
            if (qualPath.Size() != 2) {
                throw new XmpException("The qualifier name must be simple", XmpError.BADXPATH);
            }

            return "/?" + qualPath.GetSegment((int) XmpPath.STEP_ROOT_PROP).Name;
        }


        /// <summary>
        /// Compose the path expression to select an alternate item by language. The
        /// path syntax allows two forms of &quot;content addressing&quot; that may
        /// be used to select an item in an array of alternatives. The form used in
        /// ComposeLangSelector lets you select an item in an alt-text array based on
        /// the value of its <tt>xml:lang</tt> qualifier. The other form of content
        /// addressing is shown in ComposeFieldSelector. \note ComposeLangSelector
        /// does not supplant SetLocalizedText or GetLocalizedText. They should
        /// generally be used, as they provide extra logic to choose the appropriate
        /// language and maintain consistency with the 'x-default' value.
        /// ComposeLangSelector gives you an path expression that is explicitly and
        /// only for the language given in the langName parameter.
        /// </summary>
        /// <param name="arrayName">
        ///            The name of the array. May be a general path expression, must
        ///            not be <code>null</code> or the empty string. </param>
        /// <param name="langName">
        ///            The RFC 3066 code for the desired language. </param>
        /// <returns> Returns the composed path. This will be of the form
        ///         <tt>ns:arrayName[@xml:lang='langName']</tt>, where
        ///         &quot;ns&quot; is the prefix for schemaNs. </returns>
        public static string ComposeLangSelector(string arrayName, string langName) {
            return arrayName + "[?xml:lang=\"" + Utils.NormalizeLangValue(langName) + "\"]";
        }


        /// <summary>
        /// Compose the path expression to select an alternate item by a field's value. The path syntax
        /// allows two forms of &quot;content addressing&quot; that may be used to select an item in an
        /// array of alternatives. The form used in ComposeFieldSelector lets you select an item in an
        /// array of structs based on the value of one of the fields in the structs. The other form of
        /// content addressing is shown in ComposeLangSelector. For example, consider a simple struct
        /// that has two fields, the name of a city and the URI of an FTP site in that city. Use this to
        /// create an array of download alternatives. You can show the user a popup built from the values
        /// of the city fields. You can then get the corresponding URI as follows:
        /// <p>
        /// <blockquote>
        /// 
        /// <pre>
        ///      String path = composeFieldSelector ( schemaNs, &quot;Downloads&quot;, fieldNs, 
        ///          &quot;City&quot;, chosenCity ); 
        ///      XMPProperty prop = xmpObj.getStructField ( schemaNs, path, fieldNs, &quot;URI&quot; );
        /// </pre>
        /// 
        /// </blockquote>
        /// </summary>
        /// <param name="arrayName"> The name of the array. May be a general path expression, must not be
        ///        <code>null</code> or the empty string. </param>
        /// <param name="fieldNs"> The namespace URI for the field used as the selector. Must not be
        ///        <code>null</code> or the empty string. </param>
        /// <param name="fieldName"> The name of the field used as the selector. Must be a simple XML name, must
        ///        not be <code>null</code> or the empty string. It must be the name of a field that is
        ///        itself simple. </param>
        /// <param name="fieldValue"> The desired value of the field. </param>
        /// <returns> Returns the composed path. This will be of the form
        ///         <tt>ns:arrayName[fNS:fieldName='fieldValue']</tt>, where &quot;ns&quot; is the
        ///         prefix for schemaNs and &quot;fNS&quot; is the prefix for fieldNs. </returns>
        /// <exception cref="XmpException"> Thrown if the path to create is not valid. </exception>
        public static string ComposeFieldSelector(string arrayName, string fieldNs, string fieldName, string fieldValue) {
            XmpPath fieldPath = XmpPathParser.ExpandXPath(fieldNs, fieldName);
            if (fieldPath.Size() != 2) {
                throw new XmpException("The fieldName name must be simple", XmpError.BADXPATH);
            }

            return arrayName + '[' + fieldPath.GetSegment((int) XmpPath.STEP_ROOT_PROP).Name + "=\"" + fieldValue +
                   "\"]";
        }


        /// <summary>
        /// ParameterAsserts that a qualifier namespace is set. </summary>
        /// <param name="qualNs"> a qualifier namespace </param>
        /// <exception cref="XmpException"> Qualifier schema is null or empty </exception>
        private static void AssertQualNs(string qualNs) {
            if (string.IsNullOrEmpty(qualNs)) {
                throw new XmpException("Empty qualifier namespace URI", XmpError.BADSCHEMA);
            }
        }


        /// <summary>
        /// ParameterAsserts that a qualifier name is set. </summary>
        /// <param name="qualName"> a qualifier name or path </param>
        /// <exception cref="XmpException"> Qualifier name is null or empty </exception>
        private static void AssertQualName(string qualName) {
            if (string.IsNullOrEmpty(qualName)) {
                throw new XmpException("Empty qualifier name", XmpError.BADXPATH);
            }
        }


        /// <summary>
        /// ParameterAsserts that a struct field namespace is set. </summary>
        /// <param name="fieldNs"> a struct field namespace </param>
        /// <exception cref="XmpException"> Struct field schema is null or empty </exception>
        private static void AssertFieldNs(string fieldNs) {
            if (string.IsNullOrEmpty(fieldNs)) {
                throw new XmpException("Empty field namespace URI", XmpError.BADSCHEMA);
            }
        }


        /// <summary>
        /// ParameterAsserts that a struct field name is set. </summary>
        /// <param name="fieldName"> a struct field name or path </param>
        /// <exception cref="XmpException"> Struct field name is null or empty </exception>
        private static void AssertFieldName(string fieldName) {
            if (string.IsNullOrEmpty(fieldName)) {
                throw new XmpException("Empty f name", XmpError.BADXPATH);
            }
        }
    }
}
