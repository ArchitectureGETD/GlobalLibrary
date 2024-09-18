using System;
using System.IO;
using iTextSharp.GE.text.xml.simpleparser;
using iTextSharp.GE.text.xml.xmp;
using iTextSharp.GE.xmp.options;

namespace iTextSharp.GE.xmp.impl {
    /// <summary>
    /// Serializes the <code>XMPMeta</code>-object to an <code>OutputStream</code> according to the
    /// <code>SerializeOptions</code>. 
    /// 
    /// @since   11.07.2006
    /// </summary>
    public class XmpSerializerHelper {
        /// <summary>
        /// Static method to Serialize the metadata object. For each serialisation, a new XMPSerializer
        /// instance is created, either XMPSerializerRDF or XMPSerializerPlain so thats its possible to 
        /// serialialize the same XMPMeta objects in two threads.
        /// </summary>
        /// <param name="xmp"> a metadata implementation object </param>
        /// <param name="out"> the output stream to Serialize to </param>
        /// <param name="options"> serialization options, can be <code>null</code> for default. </param>
        /// <exception cref="XmpException"> </exception>
        public static void Serialize(XmpMetaImpl xmp, Stream @out, SerializeOptions options) {
            options = options ?? new SerializeOptions();

            // sort the internal data model on demand
            if (options.Sort) {
                xmp.Sort();
            }
            (new XmpSerializerRdf()).Serialize(xmp, @out, options);
        }


        /// <summary>
        /// Serializes an <code>XMPMeta</code>-object as RDF into a string.
        /// <em>Note:</em> Encoding is forced to UTF-16 when serializing to a
        /// string to ensure the correctness of &quot;exact packet size&quot;.
        /// </summary>
        /// <param name="xmp"> a metadata implementation object </param>
        /// <param name="options"> Options to control the serialization (see
        ///            <seealso cref="SerializeOptions"/>). </param>
        /// <returns> Returns a string containing the serialized RDF. </returns>
        /// <exception cref="XmpException"> on serializsation errors. </exception>
        public static string SerializeToString(XmpMetaImpl xmp, SerializeOptions options) {
            // forces the encoding to be UTF-16 to get the correct string length
            options = options ?? new SerializeOptions();
            options.EncodeUtf16Be = true;

            MemoryStream @out = new MemoryStream(2048);
            Serialize(xmp, @out, options);

            try {
                return new EncodingNoPreamble(IanaEncodings.GetEncodingEncoding(options.Encoding)).GetString(@out.GetBuffer());
            }
            catch (Exception) {
                // cannot happen as UTF-8/16LE/BE is required to be implemented in
                // Java
                return GetString(@out.GetBuffer());
            }
        }


        /// <summary>
        /// Serializes an <code>XMPMeta</code>-object as RDF into a byte buffer.
        /// </summary>
        /// <param name="xmp"> a metadata implementation object </param>
        /// <param name="options"> Options to control the serialization (see <seealso cref="SerializeOptions"/>). </param>
        /// <returns> Returns a byte buffer containing the serialized RDF. </returns>
        /// <exception cref="XmpException"> on serializsation errors. </exception>
        public static byte[] SerializeToBuffer(XmpMetaImpl xmp, SerializeOptions options) {
            MemoryStream @out = new MemoryStream(2048);
            Serialize(xmp, @out, options);
            return @out.GetBuffer();
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

    }
}
