// Copyright (c) 2024 GlobalExchange
// 
// Este archivo está licenciado bajo la licencia MIT.
// Consulta el archivo LICENSE en la raíz del proyecto para más detalles.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.GE.text;

namespace GlobalLibrary.Data.Files.Entities.Pdf
{
    /// <summary>
    /// Image
    /// </summary>
    /// <seealso cref="iTextSharp.GE.text.Image" />
    public class Image : iTextSharp.GE.text.Image
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public Image(Uri url) : base(url)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="img">The img.</param>
        public Image(iTextSharp.GE.text.Image img) : base(img)
        {
        }       

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>Image</returns>
        public static new Image GetInstance(string filename)
        {
            var img = iTextSharp.GE.text.Image.GetInstance(filename);

            return new Image(img);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static new Image GetInstance(Stream stream)
        {
            var img = iTextSharp.GE.text.Image.GetInstance(stream);

            return new Image(img);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="color">The color.</param>
        /// <param name="forceBW">if set to <c>true</c> [force bw].</param>
        /// <returns>Image</returns>
        public static Image GetInstance(System.Drawing.Image image, BaseColor color, bool? forceBW = null)
        {
            iTextSharp.GE.text.Image img;

            if (forceBW == null)
            {
                img = iTextSharp.GE.text.Image.GetInstance(image, color);                
            }
            else
            {
                img = iTextSharp.GE.text.Image.GetInstance(image, color, forceBW.HasValue);                
            }

            return new Image(img);  
        }
    }
}
