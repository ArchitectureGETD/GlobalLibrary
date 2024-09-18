// Copyright (c) 2024 GlobalExchange
// 
// Este archivo está licenciado bajo la licencia MIT.
// Consulta el archivo LICENSE en la raíz del proyecto para más detalles.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GlobalLibrary.Data.Files.Entities.Pdf
{
    /// <summary>
    /// theCampoReciboClass
    /// </summary>
    [Serializable]
    public class CampoRecibo
    {
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        [XmlElement("Nombre", Namespace = "")]
        public string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [XmlElement("Valor", Namespace = "")]
        public string Valor { get; set; }

        /// <summary>
        /// Gets or sets the fuente.
        /// </summary>
        /// <value>
        /// The fuente.
        /// </value>
        [XmlElement("Fuente", Namespace = "")]
        public FuenteCampo Fuente { get; set; }

        /// <summary>
        /// Gets or sets the alineacion texto.
        /// </summary>
        /// <value>
        /// The alineacion texto.
        /// </value>
        [XmlElement("AlineacionTexto", Namespace = "")]
        public string AlineacionTexto { get; set; } // Enum {Default, Left, Center, Right, Justify}

        /// <summary>
        /// Gets or sets the interlineado.
        /// </summary>
        /// <value>
        /// The interlineado.
        /// </value>
        [XmlElement("Interlineado", Namespace = "")]
        public float? Interlineado { get; set; }

        /// <summary>
        /// Gets or sets the pincel.
        /// </summary>
        /// <value>
        /// The pincel.
        /// </value>
        [XmlElement("Pincel", Namespace = "")]
        public PincelCampo Pincel { get; set; }

        /// <summary>
        /// Gets or sets the posicion.
        /// </summary>
        /// <value>
        /// The posicion.
        /// </value>
        [XmlElement("Posicion", Namespace = "")]
        public PosicionCampo Posicion { get; set; }

        /// <summary>
        /// Gets or sets the tipo campo.
        /// </summary>
        /// <value>
        /// The tipo campo.
        /// </value>
        /// 
        [XmlElement("TipoCampo", Namespace = "")]
        public string TipoCampo { get; set; }

        /// <summary>
        /// Gets or sets the columnas.
        /// </summary>
        /// <value>
        /// The columnas.
        /// </value>
        [XmlArray("Columnas", Namespace = "")]
        public List<CampoRecibo> Columnas { get; set; }

        /// <summary>
        /// Gets or sets the totales.
        /// </summary>
        /// <value>
        /// The totales.
        /// </value>
        [XmlArray("Totales", Namespace = "")]
        public List<CampoRecibo> Totales { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [tiene linea superior].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [tiene linea superior]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("TieneLineaSuperior", Namespace = "")]
        public bool? TieneLineaSuperior { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [tiene linea inferior].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [tiene linea inferior]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("TieneLineaInferior", Namespace = "")]
        public bool? TieneLineaInferior { get; set; }

        /// <summary>
        /// Gets or sets the separador.
        /// </summary>
        /// <value>
        /// The separador.
        /// </value>
        public string Separador { get; set; }

        /// <summary>
        /// Gets or sets the posiciones decimales.
        /// </summary>
        /// <value>
        /// The posiciones decimales.
        /// </value>
        public int? PosicionesDecimales { get; set; }

        /// <summary>
        /// Gets or sets the visibility parameter.
        /// </summary>
        /// <value>
        /// The visibility parameter.
        /// </value>
        public string VisibilityParameter { get; set; }

        /// <summary>
        /// Gets or sets the visibility value.
        /// </summary>
        /// <value>
        /// The visibility value.
        /// </value>
        public string VisibilityValue { get; set; }

        /// <summary>
        /// Gets or sets the formato.
        /// </summary>
        /// <value>
        /// Capitalize | Uppercase | Lowercase | null
        /// </value>
        [XmlElement("Formato", Namespace = "")]
        public string Formato { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CampoRecibo"/> is border.
        /// </summary>
        /// <value>
        ///   <c>true</c> if border; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("Border", Namespace = "")]
        public bool Border { get; set; }

        /// <summary>
        /// Gets or sets the pincel border.
        /// </summary>
        /// <value>
        /// The pincel border.
        /// </value>
        [XmlElement("PincelBorder", Namespace = "")]
        public PincelCampo PincelBorder { get; set; }
    }
}
