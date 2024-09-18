using iTextSharp.GE.xmp.options;

namespace iTextSharp.GE.xmp.properties {
    /// <summary>
    /// This interface is used to return info about an alias.
    /// 
    /// @since   27.01.2006
    /// </summary>
    public interface IXmpAliasInfo {
        /// <returns> Returns Returns the namespace URI for the base property. </returns>
        string Namespace { get; }


        /// <returns> Returns the default prefix for the given base property.  </returns>
        string Prefix { get; }


        /// <returns> Returns the path of the base property. </returns>
        string PropName { get; }


        /// <returns> Returns the kind of the alias. This can be a direct alias
        ///         (ARRAY), a simple property to an ordered array
        ///         (ARRAY_ORDERED), to an alternate array
        ///         (ARRAY_ALTERNATE) or to an alternate text array
        ///         (ARRAY_ALT_TEXT). </returns>
        AliasOptions AliasForm { get; }
    }
}
