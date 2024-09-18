// Copyright (c) 2024 GlobalExchange
// 
// Este archivo está licenciado bajo la licencia MIT.
// Consulta el archivo LICENSE en la raíz del proyecto para más detalles.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalLibrary.Data.Files.Entities.Pdf
{
    /// <summary>
    /// the ReciboPDF Class
    /// </summary>
    public class ReciboPDF
    {
        /// <summary>
        /// Gets or sets the lineas por pagina.
        /// </summary>
        /// <value>
        /// The lineas por pagina.
        /// </value>
        public int LineasPorPagina { get; set; }

        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the campos.
        /// </summary>
        /// <value>
        /// The campos.
        /// </value>
        public List<CampoRecibo> Campos { get; set; }

        /// <summary>
        /// Gets or sets the orientation landscape.
        /// </summary>
        /// <value>
        /// The orientation landscape.
        /// </value>
        public bool? OrientationLandscape { get; set; }


        /// <summary>
        /// Gets or sets the tamanno pagina.
        /// </summary>
        /// <value>
        /// The tamanno pagina.
        /// </value>
        public string TamannoPagina { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public decimal? Height { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public decimal? Width { get; set; }
    }
}
