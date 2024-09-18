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
    /// the FuenteCampo Class
    /// </summary>
    public class FuenteCampo
    {
        /// <summary>
        /// Gets or sets the nombre fuente.
        /// </summary>
        /// <value>
        /// The nombre fuente.
        /// </value>
        public string NombreFuente { get; set; }

        /// <summary>
        /// Gets or sets the tamanno.
        /// </summary>
        /// <value>
        /// The tamanno.
        /// </value>
        public float Tamanno { get; set; }

        /// <summary>
        /// Gets or sets the ud tamanno.
        /// </summary>
        /// <value>
        /// The ud tamanno.
        /// </value>
        public string UdTamanno { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FuenteCampo"/> is negrita.
        /// </summary>
        /// <value>
        ///   <c>true</c> if negrita; otherwise, <c>false</c>.
        /// </value>
        public bool Negrita { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FuenteCampo"/> is cursiva.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cursiva; otherwise, <c>false</c>.
        /// </value>
        public bool Cursiva { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FuenteCampo"/> is subrayado.
        /// </summary>
        /// <value>
        ///   <c>true</c> if subrayado; otherwise, <c>false</c>.
        /// </value>
        public bool Subrayado { get; set; }
    }
}
