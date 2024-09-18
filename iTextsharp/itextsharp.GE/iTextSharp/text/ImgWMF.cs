using System;
using System.IO;
using System.Net;
using iTextSharp.GE.text.error_messages;

using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.codec.wmf;

namespace iTextSharp.GE.text {
    /**
     * An ImgWMF is the representation of a windows metafile
     * that has to be inserted into the document
     *
     * @see        Element
     * @see        Image
     * @see        Gif
     * @see        Png
     */
    /// <summary>
    /// An ImgWMF is the representation of a windows metafile
    /// that has to be inserted into the document
    /// </summary>
    public class ImgWMF : Image {
    
        // Constructors
        /// <summary>
        /// Constructs an ImgWMF-object
        /// </summary>
        /// <param name="image">a Image</param>
        public ImgWMF(Image image) : base(image) {}
    
        /// <summary>
        /// Constructs an ImgWMF-object, using an url.
        /// </summary>
        /// <param name="url">the URL where the image can be found</param>
        public ImgWMF(Uri url) : base(url) {
            ProcessParameters();
        }

        /// <summary>
        /// Constructs an ImgWMF-object, using a filename.
        /// </summary>
        /// <param name="filename">a string-representation of the file that contains the image.</param>
        public ImgWMF(string filename) : this(Utilities.ToURL(filename)) {}
    
        /// <summary>
        /// Constructs an ImgWMF-object from memory.
        /// </summary>
        /// <param name="img">the memory image</param>
        public ImgWMF(byte[] img) : base((Uri)null) {
            rawData = img;
            originalData = img;
            ProcessParameters();
        }
    
        /// <summary>
        /// This method checks if the image is a valid WMF and processes some parameters.
        /// </summary>
        private void ProcessParameters() {
            type = Element.IMGTEMPLATE;
            originalType = ORIGINAL_WMF;
            Stream istr = null;
            try {
                string errorID;
                if (rawData == null){
                    WebRequest w = WebRequest.Create(url);
                    w.Credentials = CredentialCache.DefaultCredentials;
                    istr = w.GetResponse().GetResponseStream();
                    errorID = url.ToString();
                }
                else{
                    istr = new MemoryStream(rawData);
                    errorID = "Byte array";
                }
                InputMeta im = new InputMeta(istr);
                if (im.ReadInt() != unchecked((int)0x9AC6CDD7))    {
                    throw new BadElementException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.placeable.windows.metafile", errorID));
                }
                im.ReadWord();
                int left = im.ReadShort();
                int top = im.ReadShort();
                int right = im.ReadShort();
                int bottom = im.ReadShort();
                int inch = im.ReadWord();
                dpiX = 72;
                dpiY = 72;
                scaledHeight = (float)(bottom - top) / inch * 72f;
                this.Top =scaledHeight;
                scaledWidth = (float)(right - left) / inch * 72f;
                this.Right = scaledWidth;
            }
            finally {
                if (istr != null) {
                    istr.Close();
                }
                plainWidth = this.Width;
                plainHeight = this.Height;
            }
        }
    
        /// <summary>
        /// Reads the WMF into a template.
        /// </summary>
        /// <param name="template">the template to read to</param>
        virtual public void ReadWMF(PdfTemplate template) {
            TemplateData = template;
            template.Width = this.Width;
            template.Height = this.Height;
            Stream istr = null;
            try {
                if (rawData == null){
                    WebRequest w = WebRequest.Create(url);
                    w.Credentials = CredentialCache.DefaultCredentials;
                    istr = w.GetResponse().GetResponseStream();
                }
                else{
                    istr = new MemoryStream(rawData);
                }
                MetaDo meta = new MetaDo(istr, template);
                meta.ReadAll();
            }
            finally {
                if (istr != null) {
                    istr.Close();
                }
            }
        }
    }
}
