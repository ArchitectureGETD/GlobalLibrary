using iTextSharp.GE.xmp.options;

namespace iTextSharp.GE.xmp.properties {
    /// <summary>
    /// This interface is used to return a text property together with its and options.
    /// 
    /// @since   23.01.2006
    /// </summary>
    public interface IXmpProperty {
        /// <returns> Returns the value of the property. </returns>
        string Value { get; }


        /// <returns> Returns the options of the property. </returns>
        PropertyOptions Options { get; }


        /// <summary>
        /// Only set by <seealso cref="IXmpMeta.GetLocalizedText(string, string, string, string)"/>. </summary>
        /// <returns> Returns the language of the alt-text item. </returns>
        string Language { get; }
    }
}
