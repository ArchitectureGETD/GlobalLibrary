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
    /// the PincelCampo class
    /// </summary>
    public class PincelCampo
    {
        /// <summary>
        /// Gets or sets alpha value.
        /// </summary>
        /// <value>
        /// alpha value.
        /// </value>
        public int A { get; set; }

        /// <summary>
        /// Gets or sets Red Value
        /// </summary>
        /// <value>
        /// The red value.
        /// </value>
        public int R { get; set; }

        /// <summary>
        /// Gets or sets Green Value
        /// </summary>
        /// <value>
        /// The gren value
        /// </value>
        public int G { get; set; }

        /// <summary>
        /// Gets or sets Blue Value
        /// </summary>
        /// <value>
        /// The blue value
        /// </value>
        public int B { get; set; }
    }
}
