namespace iTextSharp.GE.xmp.properties {
    /// <summary>
    /// This interface is used to return a property together with its path and namespace.
    /// It is returned when properties are iterated with the <code>XMPIterator</code>.
    /// 
    /// @since   06.07.2006
    /// </summary>
    public interface IXmpPropertyInfo : IXmpProperty {
        /// <returns> Returns the namespace of the property </returns>
        string Namespace { get; }


        /// <returns> Returns the path of the property, but only if returned by the iterator. </returns>
        string Path { get; }
    }
}
