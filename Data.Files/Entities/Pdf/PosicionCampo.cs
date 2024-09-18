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
    /// theh PosicionCampo Class
    /// </summary>
    public class PosicionCampo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PosicionCampo"/> class.
        /// </summary>
        public PosicionCampo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PosicionCampo"/> class.
        /// </summary>
        /// <param name="copia">The copia.</param>
        public PosicionCampo(PosicionCampo copia)
        {
            this.Alto = copia.Alto;
            this.Ancho = copia.Ancho;
            this.X = copia.X;
            this.Y = copia.Y;
        }

        /// <summary>
        /// Gets or Sets de X coordinate
        /// </summary>
        /// <value>
        /// The x coordinate
        /// </value>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate
        /// </summary>
        /// <value>
        /// The y coordinate
        /// </value>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the alto.
        /// </summary>
        /// <value>
        /// The alto.
        /// </value>
        public double Alto { get; set; }

        /// <summary>
        /// Gets or sets the ancho.
        /// </summary>
        /// <value>
        /// The ancho.
        /// </value>
        public double Ancho { get; set; }
    }
}
