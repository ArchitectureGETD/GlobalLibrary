// Copyright (c) 2024 GlobalExchange
// 
// Este archivo está licenciado bajo la licencia MIT.
// Consulta el archivo LICENSE en la raíz del proyecto para más detalles.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.GE.text.pdf.parser;
using Vector = iTextSharp.GE.text.pdf.parser.Vector;

namespace GlobalLibrary.Data.Files.PdfTools
{
    // <summary>
    /// MyLocationTextExtractionStrategy
    /// </summary>
    /// <seealso cref="iTextSharp.text.pdf.parser.LocationTextExtractionStrategy" />
    public class CustomLocationTextExtractionStrategy : LocationTextExtractionStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLocationTextExtractionStrategy"/> class.
        /// </summary>
        /// <param name="textToSearchFor">The text to search for.</param>
        /// <param name="compareOptions">The compare options.</param>
        public CustomLocationTextExtractionStrategy(string textToSearchFor, System.Globalization.CompareOptions compareOptions = System.Globalization.CompareOptions.None)
        {
            this.ResultPositions = new List<iTextSharp.GE.text.Rectangle>();
            this.TextToSearchFor = textToSearchFor;
            this.CompareOptions = compareOptions;
        }

        /// <summary>
        /// My points
        /// </summary>
        public List<iTextSharp.GE.text.Rectangle> ResultPositions { get; set; }

        /// <summary>
        /// Gets or sets the text to search for.
        /// </summary>
        /// <value>
        /// The text to search for.
        /// </value>
        public string TextToSearchFor { get; set; }

        /// <summary>
        /// Gets or sets the compare options.
        /// </summary>
        /// <value>
        /// The compare options.
        /// </value>
        public System.Globalization.CompareOptions CompareOptions { get; set; }

        /// <summary>
        /// RenderText
        /// </summary>
        /// <param name="renderInfo">renderInfo</param>
        /// @see com.itextpdf.text.pdf.parser.RenderListener#renderText(com.itextpdf.text.pdf.parser.TextRenderInfo)
        public override void RenderText(TextRenderInfo renderInfo)
        {
            base.RenderText(renderInfo);

            //Se obtiene busca el texto en el elemento actual a tratar
            var startPosition = System.Globalization.CultureInfo.CurrentCulture.CompareInfo.Compare(renderInfo.GetText(), this.TextToSearchFor, this.CompareOptions);
            if (startPosition != 0)
            {
                return;
            }

            //Para determinar su posición se emplean su primer y último carácter
            var chars = renderInfo.GetCharacterRenderInfos().Skip(startPosition).Take(this.TextToSearchFor.Length).ToList();
            var firstChar = chars.First();
            var lastChar = chars.Last();
            var bottomLeft = firstChar.GetDescentLine().GetStartPoint();
            var topRight = lastChar.GetAscentLine().GetEndPoint();

            //Generamos un rectángulo y lo añadimos a la colección de puntos.
            var rect = new iTextSharp.GE.text.Rectangle(
                                                    bottomLeft[Vector.I1],
                                                    bottomLeft[Vector.I2],
                                                    topRight[Vector.I1],
                                                    topRight[Vector.I2]);

            this.ResultPositions.Add(rect);
        }
    }
}
