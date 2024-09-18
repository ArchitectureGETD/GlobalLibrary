namespace iTextSharp.GE.xmp
{

    /// <summary>
    /// XMP Toolkit Version Information.
    /// <p>
    /// Version information for the XMP toolkit is stored in the jar-library and available through a
    /// runtime call, <seealso cref="XMPMetaFactory#getVersionInfo()"/>,  addition static version numbers are
    /// defined in "version.properties". 
    /// 
    /// @since 23.01.2006
    /// </summary>
    public interface IXmpVersionInfo
    {
        /// <returns> Returns the primary release number, the "1" in version "1.2.3". </returns>
        int Major { get;}


        /// <returns> Returns the secondary release number, the "2" in version "1.2.3". </returns>
        int Minor { get;}


        /// <returns> Returns the tertiary release number, the "3" in version "1.2.3". </returns>
        int Micro { get;}


        /// <returns> Returns a rolling build number, monotonically increasing in a release. </returns>
        int Build { get;}


        /// <returns> Returns true if this is a debug build. </returns>
        bool Debug { get;}


        /// <returns> Returns a comprehensive version information string. </returns>
        string Message { get;}
    }
}
