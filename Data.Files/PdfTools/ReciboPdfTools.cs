// Copyright (c) 2024 GlobalExchange
// 
// Este archivo está licenciado bajo la licencia MIT.
// Consulta el archivo LICENSE en la raíz del proyecto para más detalles.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Xml.Serialization;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.draw;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using Document = iTextSharp.GE.text.Document;
using DrawImagenes = System.Drawing;
using Font = iTextSharp.GE.text.Font;
using PageSize = iTextSharp.GE.text.PageSize;
using Rectangle = iTextSharp.GE.text.Rectangle;
using GlobalLibrary.Data.Files.Entities.Pdf;
using iTextSharp.GE.xtra.iTextSharp.text.pdf.pdfcleanup;
using PdfObject = iTextSharp.GE.text.pdf.PdfObject;
using PdfName = iTextSharp.GE.text.pdf.PdfName;
using PdfNumber = iTextSharp.GE.text.pdf.PdfNumber;
using PdfDictionary = iTextSharp.GE.text.pdf.PdfDictionary;

namespace GlobalLibrary.Data.Files.PdfTools
{
    /// <summary>
    /// The Recibo PDF Tools
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class ReciboPdfTools
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReciboPdfTools" /> class.
        /// </summary>
        public ReciboPdfTools()
        {
            //TODO generalizar filename y path
            this.FuenteDefecto = FontFactory.GetFont("Arial Unicode MS", 12);
        }

        /// <summary>
        /// The Vertical Align Enum Values
        /// </summary>
        public enum VerticalAlign
        {
            /// <summary>
            /// The top
            /// </summary>
            Top,

            /// <summary>
            /// The middle
            /// </summary>
            Middle,

            /// <summary>
            /// The bottom
            /// </summary>
            Bottom
        }

        /// <summary>
        /// The Horizontal Align Enum Values
        /// </summary>
        public enum HorizontalAlign
        {
            /// <summary>
            /// The left
            /// </summary>
            Left,

            /// <summary>
            /// The center
            /// </summary>
            Center,

            /// <summary>
            /// The right
            /// </summary>
            Right
        }

        #region Properties

        /// <summary>
        /// Gets or sets the fuente defecto.
        /// </summary>
        /// <value>
        /// The fuente defecto.
        /// </value>
        public Font FuenteDefecto { get; set; }

        /// <summary>
        /// Gets or sets the pincel defecto.
        /// </summary>
        /// <value>
        /// The pincel defecto.
        /// </value>
        public string PincelDefecto { get; set; } = string.Empty;

        #endregion

        /// <summary>
        /// Creates the specified datos comunes.
        /// </summary>
        /// <param name="datosComunes">The datos comunes.</param>
        /// <param name="datosLineas">The datos lineas.</param>
        /// <param name="plantilla">The plantilla.</param>
        /// <param name="imagenFondo">The imagen fondo.</param>
        /// <param name="firmaAsesor">The firma asesor.</param>
        /// <param name="esDigital">if set to <c>true</c> [es digital].</param>
        /// <param name="reciboCaraB">The recibo cara b.</param>
        /// <param name="qr">The qr.</param>
        /// <param name="fillDocumentFileAction">The fill document file action.</param>
        /// <returns>
        /// el Recibo en PDF
        /// </returns>        
        public MemoryStream Create(IEnumerable<KeyValuePair<string, object>> datosComunes, IEnumerable<IEnumerable<KeyValuePair<string, object>>> datosLineas, ReciboPDF plantilla, Entities.Pdf.Image imagenFondo, Entities.Pdf.Image firmaAsesor, bool esDigital, Entities.Pdf.Image reciboCaraB, Entities.Pdf.Image qr, Func<ReciboPdfTools, int, object, object, List<CampoRecibo>, List<CampoRecibo>, List<CampoRecibo>, bool> fillDocumentFileAction)
        {
            ArgumentNullException.ThrowIfNull(fillDocumentFileAction, nameof(fillDocumentFileAction));

            // A partir de aquí generamos el contenido del recibo
            string lineaTotal = string.Empty;
            int numLineas = datosLineas.Count();
            int numPaginas = numLineas / plantilla.LineasPorPagina;

            // si el resto de la división es distinto de 0, se necesita una página más para las lineas sobrantes
            if (numLineas % plantilla.LineasPorPagina != 0)
            {
                numPaginas++;
            }

            using (Document documentoFinal = new Document())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter wri = PdfWriter.GetInstance(documentoFinal, memoryStream);
                    documentoFinal.Open();

                    List<CampoRecibo> camposComunes = new List<CampoRecibo>();
                    List<CampoRecibo> camposLineas = new List<CampoRecibo>();
                    List<CampoRecibo> camposLineasImp = new List<CampoRecibo>();

                    fillDocumentFileAction(this, numPaginas, documentoFinal, wri, camposComunes, camposLineas, camposLineasImp);

                    documentoFinal.Close();

                    return memoryStream;
                }
            }
        }       

        /// <summary>
        /// Creates the bill.
        /// </summary>
        /// <param name="datosFactura">The datos factura.</param>
        /// <param name="datosServicios">The datos servicios.</param>
        /// <param name="datosImpuestos">The datos impuestos.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="fondoname">The fondoname.</param>
        /// <param name="path">The path.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>
        /// la factura en PDF
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Exception</exception>
        public MemoryStream CreateBill(IEnumerable<KeyValuePair<string, object>> datosFactura, IEnumerable<IEnumerable<KeyValuePair<string, object>>> datosServicios, IEnumerable<IEnumerable<KeyValuePair<string, object>>> datosImpuestos, string filename, string fondoname, string path, string fileExtension)
        {
            try
            {
                string rutaPlantilla = string.Empty;
                bool existeRuta = false;

                //Comprobamos la existencia de la ruta de las plantillas
                existeRuta = Directory.Exists(path);

                //Si no hemos encontrado una ruta, devolvemos excepcion porque no existe la plantilla del pais solicitado
                if (!existeRuta)
                {
                    throw new InvalidOperationException(string.Format("No existe plantilla o ruta a la misma"));
                }

                // Una vez tenemos la carpeta más especifica para las plantillas, generamos la ruta a la plantilla segun el tipo de recibo solicitado
                rutaPlantilla = path + Path.DirectorySeparatorChar;

                ReciboPDF plantilla = this.GetPlantillaFromXML(rutaPlantilla + filename + fileExtension);

                // A partir de aquí generamos el contenido del recibo
                string lineaTotal = string.Empty;
                int numPaginas = 1;

                PdfSharp.Pdf.PdfDocument documentoFinal = new PdfSharp.Pdf.PdfDocument();

                List<CampoRecibo> camposBase = new List<CampoRecibo>();
                List<CampoRecibo> camposComunes = new List<CampoRecibo>();
                List<CampoRecibo> camposImpuestos = new List<CampoRecibo>();
                List<CampoRecibo> camposRect = new List<CampoRecibo>();
                List<CampoRecibo> camposServicios = new List<CampoRecibo>();
                List<CampoRecibo> camposTotales = new List<CampoRecibo>();

                Regex matchBase = new Regex(@".*Base.*");
                Regex matchImpuestos = new Regex(@".+LY");
                Regex matchRect = new Regex(@"cuadro\d+");
                Regex matchServicios = new Regex(@".+LX");
                Regex matchTotales = new Regex(@".+Total.*");
                camposBase.AddRange(plantilla.Campos.Where(x => matchBase.IsMatch(x.Nombre) == true));
                camposComunes.AddRange(plantilla.Campos.Where(x => matchServicios.IsMatch(x.Nombre) == false && matchImpuestos.IsMatch(x.Nombre) == false && matchRect.IsMatch(x.Nombre) == false && matchTotales.IsMatch(x.Nombre) == false && matchBase.IsMatch(x.Nombre) == false));
                camposImpuestos.AddRange(plantilla.Campos.Where(x => matchImpuestos.IsMatch(x.Nombre) == true));
                camposRect.AddRange(plantilla.Campos.Where(x => matchRect.IsMatch(x.Nombre) == true));
                camposServicios.AddRange(plantilla.Campos.Where(x => matchServicios.IsMatch(x.Nombre) == true));
                camposTotales.AddRange(plantilla.Campos.Where(x => matchTotales.IsMatch(x.Nombre) == true));

                for (int iter = 0; iter < numPaginas; iter++)
                {
                    PdfSharp.Pdf.PdfPage pagina = this.AnnadirPagina(documentoFinal, rutaPlantilla + Path.DirectorySeparatorChar + fondoname, false);

                    foreach (CampoRecibo campo in camposComunes)
                    {
                        object value = null;
                        try
                        {
                            if (string.IsNullOrEmpty(campo.Valor))
                            {
                                value = datosFactura.Where(x => x.Key == campo.Nombre).SingleOrDefault().Value;
                            }
                            else
                            {
                                value = campo.Valor;
                            }
                        }
                        catch (Exception ex)
                        {
                            Exception result = ex;
                            throw result;
                        }

                        if (value != null)
                        {
                            if (value.GetType().Equals(typeof(string)))
                            {
                                try
                                {
                                    this.EscribirSobreCampo(campo, (string)value, pagina);
                                }
                                catch (Exception ex)
                                {
                                    Exception result = ex;
                                    throw result;
                                }
                            }
                        }
                    }

                    int i = 0;
                    foreach (var servicio in datosServicios)
                    {
                        foreach (CampoRecibo campo in camposServicios)
                        {
                            string value = string.Empty;

                            try
                            {
                                value = servicio.Where(x => x.Key == campo.Nombre).SingleOrDefault().Value.ToString();
                            }
                            catch (Exception ex)
                            {
                                Exception result = ex;
                                throw result;
                            }

                            try
                            {
                                if (i != 0)
                                {
                                    campo.Posicion.Y += campo.Posicion.Alto;
                                }
                                this.EscribirSobreCampo(campo, value, pagina);
                            }
                            catch (Exception ex)
                            {
                                Exception result = ex;
                                throw result;
                            }
                        }

                        i++;
                    }

                    foreach (CampoRecibo campo in camposBase)
                    {
                        if (datosServicios.Count() > 1)
                        {
                            campo.Posicion.Y += (datosServicios.Count() - 1) * camposServicios[0].Posicion.Alto;
                        }

                        if (matchRect.IsMatch(campo.Nombre))
                        {
                            this.PintarRectangulo(campo.Posicion, campo.PincelBorder, pagina);
                        }
                        else
                        {
                            object value = null;
                            try
                            {
                                if (string.IsNullOrEmpty(campo.Valor))
                                {
                                    value = datosFactura.Where(x => x.Key == campo.Nombre).SingleOrDefault().Value;
                                }
                                else
                                {
                                    value = campo.Valor;
                                }
                            }
                            catch (Exception ex)
                            {
                                Exception result = ex;
                                throw result;
                            }

                            if (value != null)
                            {
                                if (value.GetType().Equals(typeof(string)))
                                {
                                    try
                                    {
                                        this.EscribirSobreCampo(campo, (string)value, pagina);
                                    }
                                    catch (Exception ex)
                                    {
                                        Exception result = ex;
                                        throw result;
                                    }
                                }
                            }
                        }
                    }

                    i = 0;
                    foreach (var porcentaje in datosImpuestos)
                    {
                        foreach (CampoRecibo campo in camposImpuestos)
                        {
                            string value = string.Empty;

                            try
                            {
                                value = porcentaje.Where(x => x.Key == campo.Nombre).SingleOrDefault().Value.ToString();
                            }
                            catch (Exception ex)
                            {
                                Exception result = ex;
                                throw result;
                            }

                            try
                            {
                                if (i != 0)
                                {
                                    campo.Posicion.Y += campo.Posicion.Alto;
                                }
                                else if (datosServicios.Count() > 1)
                                {
                                    campo.Posicion.Y += (datosServicios.Count() - 1) * camposServicios[0].Posicion.Alto;
                                }

                                this.EscribirSobreCampo(campo, value, pagina);
                            }
                            catch (Exception ex)
                            {
                                Exception result = ex;
                                throw result;
                            }
                        }

                        i++;
                    }

                    foreach (CampoRecibo campo in camposTotales)
                    {
                        if (datosServicios.Count() > 1)
                        {
                            campo.Posicion.Y += (datosServicios.Count() - 1) * camposServicios[0].Posicion.Alto;
                        }
                        if (datosImpuestos.Count() > 1)
                        {
                            campo.Posicion.Y += (datosImpuestos.Count() - 1) * camposImpuestos[0].Posicion.Alto;
                        }

                        if (matchRect.IsMatch(campo.Nombre))
                        {
                            this.PintarRectangulo(campo.Posicion, campo.PincelBorder, pagina);
                        }

                        object value = null;
                        try
                        {
                            if (string.IsNullOrEmpty(campo.Valor))
                            {
                                value = datosFactura.Where(x => x.Key == campo.Nombre).SingleOrDefault().Value;
                            }
                            else
                            {
                                value = campo.Valor;
                            }
                        }
                        catch (Exception ex)
                        {
                            Exception result = ex;
                            throw result;
                        }

                        if (value != null)
                        {
                            if (value.GetType().Equals(typeof(string)))
                            {
                                try
                                {
                                    this.EscribirSobreCampo(campo, (string)value, pagina);
                                }
                                catch (Exception ex)
                                {
                                    Exception result = ex;
                                    throw result;
                                }
                            }
                        }
                    }

                    foreach (var rect in camposRect)
                    {
                        try
                        {
                            this.PintarRectangulo(rect.Posicion, rect.PincelBorder, pagina);
                        }
                        catch (Exception ex)
                        {
                            Exception result = ex;
                            throw result;
                        }
                    }
                }                

                using (MemoryStream ms = new MemoryStream())
                {
                    documentoFinal.Save(ms);

                    return ms;      
                }                
            }
            catch (Exception ex)
            {
                Exception result = ex;
                throw result;
            }
        }

        /// <summary>
        /// Annadirs the pagina.
        /// </summary>
        /// <param name="documento">The documento.</param>
        /// <param name="imagen">The imagen.</param>
        /// <param name="tamannoPagina">The tamanno pagina.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <exception cref="System.InvalidOperationException">El documento no es de tipo Document</exception>
        public void AnnadirPaginaFromObject(object documento, Entities.Pdf.Image imagen, string tamannoPagina, decimal? height, decimal? width)
        {
            if (documento.GetType() != typeof(Document))
            {                           
                throw new InvalidOperationException("Document is not of type Document");
            }

            this.AnnadirPagina((Document)documento, imagen, tamannoPagina, height, width);
        }

        /// <summary>
        /// Escribirs the sobre campo from object.
        /// </summary>
        /// <param name="campo">The campo.</param>
        /// <param name="value">The value.</param>
        /// <param name="writer">The wri.</param>
        /// <param name="doc">The document.</param>
        /// <param name="cadenaConcatenar">The cadena concatenar.</param>
        /// <returns>bool</returns>
        public bool EscribirSobreCampoFromObject(CampoRecibo campo, object? value, object writer, object doc, string? cadenaConcatenar = null)
        {
            if (doc.GetType() != typeof(Document) || writer.GetType() != typeof(PdfWriter))
            {
                throw new InvalidOperationException("Document is not of type Document or writer is not of type PdfWriter");
            }

            return this.EscribirSobreCampo(campo, value, (PdfWriter)writer, (Document)doc, cadenaConcatenar);
        }

        /// <summary>
        /// Checks the visibility.
        /// </summary>
        /// <param name="campo">The campo.</param>
        /// <param name="datos">The datos.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool CheckVisibility(CampoRecibo campo, IEnumerable<KeyValuePair<string, object>> datos)
        {
            bool result = true;
            if (campo.VisibilityParameter != null)
            {
                if (datos.Where(x => x.Key == campo.VisibilityParameter).Any())
                {
                    var condition = datos.Where(x => x.Key == campo.VisibilityParameter).FirstOrDefault();
                    if (campo.VisibilityValue != null && condition.Value != null)
                    {
                        if (condition.Value.GetType() == typeof(bool))
                        {
                            result = (bool)condition.Value == bool.Parse(campo.VisibilityValue);
                        }
                        else if (condition.Value.GetType() == typeof(decimal))
                        {
                            result = (string)condition.Value == campo.VisibilityValue;
                        }
                        else if (condition.Value.GetType() == typeof(string))
                        {
                            result = (string)condition.Value == campo.VisibilityValue;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        return condition.Value == null;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Adds the image to document object.
        /// </summary>
        /// <param name="documento">The documento final.</param>
        /// <param name="campo">The campo.</param>
        /// <param name="imagen">The imagen.</param>
        public static void AddImageToDocumentObject(object documento, CampoRecibo campo, Entities.Pdf.Image imagen)
        {
            if (documento.GetType() != typeof(Document))
            {
                throw new InvalidOperationException("Document is not of type Document");
            }

            imagen.SetAbsolutePosition((float)campo.Posicion.X, (float)(((Document)documento).PageSize.Height - campo.Posicion.Y - campo.Posicion.Alto));
            imagen.ScaleToFit((float)campo.Posicion.Ancho, (float)campo.Posicion.Alto);
            ((Document)documento).Add(imagen);
        }

        /// <summary>
        /// Mappings the image.
        /// </summary>
        /// <param name="imagen">The imagen.</param>
        /// <param name="isMask">if set to <c>true</c> [is mask].</param>
        /// <returns>Entities.Pdf.Image</returns>
        public static Entities.Pdf.Image MappingImage(Entities.Pdf.Image imagen, bool isMask)
        {
            DrawImagenes.Bitmap bmp = new DrawImagenes.Bitmap((int)imagen.Width, (int)imagen.Height);
            DrawImagenes.Graphics g = DrawImagenes.Graphics.FromImage(bmp);
            g.Clear(System.Drawing.Color.Transparent);
            g.Flush();

            if (isMask)
            {
                var mask = Entities.Pdf.Image.GetInstance(bmp, new BaseColor(0, 0, 0, 255), true);
                mask.MakeMask();
                imagen.ImageMask = mask;
            }
            else
            {
                imagen = Entities.Pdf.Image.GetInstance(bmp, new BaseColor(255, 255, 255, 255));
            }

            return imagen;
        }

        /// <summary>
        /// Adds the image table.
        /// </summary>
        /// <param name="image">Image to add.</param>
        /// <param name="documentoFinal">The documento final.</param>
        /// <param name="writer">The wri.</param>
        /// <param name="campo">The campo.</param>
        public static void AddImageTable(Entities.Pdf.Image image, object documentoFinal, object writer, CampoRecibo campo)
        {
            if (documentoFinal.GetType() != typeof(Document) || writer.GetType() != typeof(PdfWriter))
            {
                throw new InvalidOperationException("Document is not of type Document or writer is not of type PdfWriter");
            }            

            image.SetAbsolutePosition((float)campo.Posicion.X, (float)(((Document)documentoFinal).PageSize.Height - campo.Posicion.Y - campo.Posicion.Alto));
            image.ScaleToFit((float)campo.Posicion.Ancho, (float)campo.Posicion.Alto);
            PdfPTable table = new PdfPTable(1);
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.SetTotalWidth(new float[] { (float)campo.Posicion.Ancho });
            table.AddCell(image);
            table.WriteSelectedRows(0, -1, (float)campo.Posicion.X, ((Document)documentoFinal).PageSize.Height - (float)campo.Posicion.Y, ((PdfWriter)writer).DirectContent);
        }       

        /// <summary>
        /// Incrustars the firma.
        /// </summary>
        /// <param name="inputFile">The input file.</param>
        /// <param name="firmaBytes">The firma bytes.</param>
        /// <param name="huellaBytes">The huella bytes.</param>
        /// <returns>
        /// byte[]
        /// </returns>
        public static byte[] IncrustarFirma(byte[] inputFile, byte[] firmaBytes, byte[] huellaBytes)
        {
            if (firmaBytes == null && huellaBytes == null)
            {
                return inputFile;
            }

            int j = 1;
            PdfReader pdf = new PdfReader(inputFile);
            int n = pdf.NumberOfPages;
            MemoryStream baos = new MemoryStream();
            PdfStamper stp = new PdfStamper(pdf, baos);
            Dictionary<PdfObject, PdfDictionary> listaImagenes = new Dictionary<PdfObject, PdfDictionary>();
            PdfWriter writer = stp.Writer;
            while (j <= n)
            {
                PdfDictionary pg = pdf.GetPageN(j);

                //Para cada página del recibo, se obtienen las imágenes que contiene
                PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));
                PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));

                if (xobj != null)
                {
                    foreach (var it in xobj.Keys)
                    {
                        PdfObject obj = xobj.Get(it);
                        PdfDictionary tg = obj.IsIndirect() ? (PdfDictionary)PdfReader.GetPdfObject(obj) : null;
                        if (tg != null && tg.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE))
                        {
                            listaImagenes.Add(obj, tg);
                        }
                    }
                }
                j++;
            }
            if (listaImagenes.Any())
            {
                DrawImagenes.ImageConverter converter = new DrawImagenes.ImageConverter();
                var objHuella = listaImagenes.Where(im => im.Value.Get(PdfName.MASK) != null &&
                                im.Value.Get(PdfName.COLORSPACE) != PdfName.DEVICEGRAY && im.Value.Get(PdfName.FILTER) == PdfName.FLATEDECODE).SingleOrDefault();
                var objFirma = listaImagenes.Where(im => im.Value.Get(PdfName.IMAGEMASK) == null && im.Value.Get(PdfName.MASK) == null && im.Value.Get(PdfName.SMASK) == null &&
                                im.Value.Get(PdfName.COLORSPACE) != PdfName.DEVICEGRAY && im.Value.Get(PdfName.FILTER) == PdfName.FLATEDECODE).SingleOrDefault();
                if (objHuella.Value != null && huellaBytes != null)
                {
                    //se inserta la huella
                    PdfReader.KillIndirect(objHuella.Key);
                    iTextSharp.GE.text.Image img2 = iTextSharp.GE.text.Image.GetInstance(huellaBytes);
                    if (img2.ImageMask != null)
                    {
                        writer.AddDirectImageSimple(img2.ImageMask);
                    }
                    writer.AddDirectImageSimple(img2, (PRIndirectReference)objHuella.Key);
                }

                if (objFirma.Value != null && firmaBytes != null)
                {
                    //se inserta la firma
                    PdfReader.KillIndirect(objFirma.Key);
                    iTextSharp.GE.text.Image img2 = iTextSharp.GE.text.Image.GetInstance(firmaBytes);
                    if (img2.ImageMask != null)
                    {
                        writer.AddDirectImageSimple(img2.ImageMask);
                    }
                    writer.AddDirectImageSimple(img2, (PRIndirectReference)objFirma.Key);
                }
            }
            stp.Close();
            pdf.Close();
            return baos.ToArray();
        }

        /// <summary>
        /// Cortars the documento.
        /// </summary>
        /// <param name="inputFile">The input file.</param>
        /// <returns>
        /// byte
        /// </returns>
        public static byte[] CortarDocumento(byte[] inputFile)
        {
            PdfReader pdf = new PdfReader(inputFile);
            MemoryStream baos = new MemoryStream();

            iTextSharp.GE.text.Rectangle mediabox = null;
            if (EsPaginaReciboCompleta(pdf.GetPageSize(1).Height, pdf.GetPageSize(1).Width))
            {
                return inputFile;
            }

            // Se obtiene tamaño de pdf
            if (pdf.GetPageSizeWithRotation(1).Width == PageSize.A5.Rotate().Width)
            {
                mediabox = new iTextSharp.GE.text.Rectangle(PageSize.A5.Rotate());
            }
            else if (pdf.GetPageSizeWithRotation(1).Width == PageSize.HALFLETTER.Rotate().Width)
            {
                mediabox = new iTextSharp.GE.text.Rectangle(PageSize.HALFLETTER.Rotate());
            }
            iTextSharp.GE.text.Rectangle tamPdf = new iTextSharp.GE.text.Rectangle(pdf.GetPageSizeWithRotation(1));
            Document document = new Document(mediabox);
            PdfWriter writer = PdfWriter.GetInstance(document, baos);

            document.Open();
            PdfContentByte content = writer.DirectContent;
            PdfImportedPage page;
            int n = pdf.NumberOfPages;
            var dif = tamPdf.Height - mediabox.Height;
            for (int i = 1; i <= n; i++)
            {
                page = writer.GetImportedPage(pdf, i);

                //Para coger el tamaño correcto, se le resta a la parte inferior del recibo el tamaño 
                //adicional que pueda tener, para convertirlo en A5/halfLetter exacto.
                if (pdf.GetPageSizeWithRotation(1).Rotation == 90 || pdf.GetPageSizeWithRotation(1).Rotation == 270)
                {
                    content.AddTemplate(page, 0, -1f, 1f, 0, 0, tamPdf.Height - dif);
                }
                else
                {
                    content.AddTemplate(page, 1f, 0, 0, 1f, 0, -dif);
                }
                document.NewPage();
                document.SetPageSize(mediabox);
                document.NewPage();
            }
            document.Close();
            pdf.Close();
            return baos.ToArray();
        }

        /// <summary>
        /// Selects the PDF pages.
        /// </summary>
        /// <param name="pdf">The PDF.</param>
        /// <returns>Page pdf</returns>
        public static byte[] SelectPDFPages(byte[] pdf)
        {
            //Función para eliminar las páginas pares de un pdf, en este caso se utilizará 
            //en aquellos casos en los que se haya guardado un recibo con cara B, y sea
            //necesario eliminarlo para su impresión.
            PdfReader reader = new PdfReader(pdf);
            IEnumerable<int> pagesToKeep = Enumerable.Range(1, reader.NumberOfPages).Where(x => x % 2 != 0);
            reader.SelectPages(string.Join(",", pagesToKeep));
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfStamper stamper = new PdfStamper(reader, ms))
                {
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Unirs the recibos.
        /// </summary>
        /// <param name="recibos">The recibos.</param>
        /// <returns>
        /// byte
        /// </returns>
        public static byte[] UnirRecibos(List<byte[]> recibos)
        {
            byte[] salida;
            using (var destinationDocumentStream = new MemoryStream())
            {
                var pdfConcat = new PdfConcatenate(destinationDocumentStream);
                pdfConcat.Open();

                recibos.ForEach(x =>
                {
                    PdfReader reader = new PdfReader(new MemoryStream(x));
                    /*reader.SelectPages();*/
                    pdfConcat.AddPages(reader);

                    reader.Close();
                });
                pdfConcat.Close();

                salida = destinationDocumentStream.ToArray();
            }
            return salida;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <param name="inputFile">The input file.</param>
        /// <param name="searchText">The texto eliminar.</param>
        /// <returns>
        /// byte[]
        /// </returns>
        public static byte[] EliminarTexto(byte[] inputFile, string searchText)
        {
            var reader = new PdfReader(inputFile);
            MemoryStream baos = new MemoryStream();
            PRStream stream;
            int n = reader.XrefSize;

            // Look for image and manipulate image stream
            for (int i = 0; i < n; i++)
            {
                var imagepdf = reader.GetPdfObject(i);
                if (imagepdf == null || !imagepdf.IsStream())
                {
                    continue;
                }
                stream = (PRStream)imagepdf;
                PdfObject pdfsubtype = stream.Get(PdfName.SUBTYPE);
                if (pdfsubtype != null && pdfsubtype.ToString().Equals(PdfName.IMAGE.ToString()))
                {
                    stream.Put(PdfName.BITSPERCOMPONENT, new PdfNumber(8));
                }
            }

            int j = 1;
            int fn = reader.NumberOfPages;
            PdfStamper stamper = new PdfStamper(reader, baos);
            while (j <= fn)
            {
                //buscador de la cadena proporcionada
                var textExtractor = new CustomLocationTextExtractionStrategy(searchText);
                var ex = iTextSharp.GE.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, j, textExtractor);

                //para cada una de las coincidencias obtenidas se genera un objeto de tipo PdfCleanUpLocation que se rellenará de blanco para eliminar el contenido.
                List<PdfCleanUpLocation> cleanUpLocations = new List<PdfCleanUpLocation>();

                foreach (var rect in textExtractor.ResultPositions)
                {
                    cleanUpLocations.Add(new PdfCleanUpLocation(j, new iTextSharp.GE.text.Rectangle(rect), BaseColor.WHITE));
                    PdfCleanUpProcessor cleaner = new PdfCleanUpProcessor(cleanUpLocations, stamper);
                    cleaner.CleanUp();
                }
                j++;
            }
            stamper.Close();
            reader.Close();
            return baos.ToArray();
        }

        /// <summary>/// <summary>
        /// Eliminars the fondo.
        /// </summary>
        /// <param name="inputFile">The input file.</param>
        /// <param name="firma">if set to <c>true</c> [firma].</param>
        /// <param name="fondoImprimir">The fondo imprimir.</param>
        /// <returns>
        /// byte
        /// </returns>
        public static byte[] EliminarFondo(byte[] inputFile, bool firma, Entities.Pdf.Image fondoImprimir)
        {
            if (inputFile == null)
            {
                return null;
            }

            //se genera una imágen transparente que reemplazará fondo/firma
            DrawImagenes.Bitmap bmp = new DrawImagenes.Bitmap(1, 1);
            DrawImagenes.Graphics g = DrawImagenes.Graphics.FromImage(bmp);
            g.Clear(System.Drawing.Color.Transparent);
            g.Flush();

            //ELIMINACIÓN DE FONDO DE RECIBO
            int j = 1;
            PdfReader pdf = new PdfReader(inputFile);
            int n = pdf.NumberOfPages;
            MemoryStream baos = new MemoryStream();
            PdfStamper stp = new PdfStamper(pdf, baos);
            Dictionary<PdfObject, PdfDictionary> listaImagenes = new Dictionary<PdfObject, PdfDictionary>();
            PdfWriter writer = stp.Writer;
            while (j <= n)
            {
                PdfDictionary pg = pdf.GetPageN(j);

                //Para cada página del recibo, se obtienen las imágenes que contiene
                PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));
                PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));

                if (xobj != null)
                {
                    foreach (var it in xobj.Keys)
                    {
                        PdfObject obj = xobj.Get(it);
                        ////La imagen de fondo en un pdf es un objeto de tipo indirecto                       
                        PdfDictionary tg = obj.IsIndirect() ? (PdfDictionary)PdfReader.GetPdfObject(obj) : null;
                        if (tg != null && tg.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE))
                        {
                            listaImagenes.Add(obj, tg);
                        }
                    }
                }
                j++;
            }

            DrawImagenes.ImageConverter converter = new DrawImagenes.ImageConverter();

            iTextSharp.GE.text.Image img = null;

            if (fondoImprimir != null)
            {
                img = fondoImprimir;
            }
            else
            {
                img = iTextSharp.GE.text.Image.GetInstance((byte[])converter.ConvertTo(bmp, typeof(byte[])));
            }

            if (listaImagenes.Any())
            {
                ////Fondo: se seleccionan las imágenes sin ningún tipo de máscara y de tipo jpg
                var objFondo = listaImagenes.Where(im => im.Value.Get(PdfName.SMASK) == null && im.Value.Get(PdfName.MASK) == null && im.Value.Get(PdfName.IMAGEMASK) == null &&
                                im.Value.Get(PdfName.FILTER) == PdfName.DCTDECODE);
                if (objFondo.Any())
                {
                    foreach (var fondo in objFondo)
                    {
                        PdfReader.KillIndirect(fondo.Key);
                        ////se inserta la transparente que reemplazará al fondo

                        if (img.ImageMask != null)
                        {
                            writer.AddDirectImageSimple(img.ImageMask);
                        }
                        writer.AddDirectImageSimple(img, (PRIndirectReference)fondo.Key);
                    }
                }
                else
                {
                    if (fondoImprimir != null)
                    {
                        ////se inserta la transparente que reemplazará al fondo
                        j = 1;
                        while (j <= n)
                        {
                            var pdfContentByte = stp.GetOverContent(j);
                            var size = pdf.GetPageSize(1);
                            img.SetAbsolutePosition(0, 0);
                            img.ScaleAbsolute(size.Width, size.Height);
                            stp.GetUnderContent(j).AddImage(img);
                            j++;
                        }
                    }
                }

                //Eliminación de la firma
                if (!firma)
                {
                    foreach (var obj in listaImagenes.Where(o => o.Value.Get(PdfName.SMASK) != null))
                    {
                        //se inserta la imagen transparente que reemplazará la firma
                        PdfReader.KillIndirect(obj.Key);
                        iTextSharp.GE.text.Image img2 = iTextSharp.GE.text.Image.GetInstance((byte[])converter.ConvertTo(bmp, typeof(byte[])));
                        if (img2.ImageMask != null)
                        {
                            writer.AddDirectImageSimple(img2.ImageMask);
                        }
                        writer.AddDirectImageSimple(img2, (PRIndirectReference)obj.Key);
                    }
                }
            }
            else
            {
                if (fondoImprimir != null)
                {
                    ////se inserta la transparente que reemplazará al fondo
                    j = 1;
                    while (j <= n)
                    {
                        var pdfContentByte = stp.GetOverContent(j);
                        var size = pdf.GetPageSize(1);
                        img.SetAbsolutePosition(0, 0);
                        img.ScaleAbsolute(size.Width, size.Height);
                        stp.GetUnderContent(j).AddImage(img);
                        j++;
                    }
                }
            }
            stp.Close();
            pdf.Close();
            return baos.ToArray();
        }

        public static byte[] ObtenerReciboCompleto(decimal margen, out byte[] salida, byte[] docSuperior, byte[] docInferior, bool esCompletoSimple)
        {
            PdfReader reader = new PdfReader(docSuperior);
            Document inputDoc = new Document(reader.GetPageSizeWithRotation(1));

            //para el ajuste de la parte inferior, se incluye un margen adicional
            decimal tamannoMargen = margen;

            using (MemoryStream fs = new MemoryStream())
            {
                PdfWriter outputWriter = PdfWriter.GetInstance(inputDoc, fs);
                inputDoc.Open();
                PdfContentByte cb1 = outputWriter.DirectContent;
                PdfReader overlayReader = new PdfReader(docInferior);
                int overlayRotation = overlayReader.GetPageRotation(1);
                int n = reader.NumberOfPages;

                //si es un documento definitivo de una filial configurada para recibo digital se imprime tal y como se almacenó a excepción del fondo.
                //En formato A5 y en vertical para su correcta impresión.
                if (esCompletoSimple)
                {
                    int i = 1;
                    while (i <= n)
                    {
                        PdfImportedPage overLay = outputWriter.GetImportedPage(overlayReader, i);
                        var size = overlayReader.GetPageSizeWithRotation(i);

                        // Se obtiene tamaño de pdf
                        if (size.Width == PageSize.A5.Rotate().Width)
                        {
                            inputDoc.SetPageSize(PageSize.A5);
                        }
                        else if (size.Width == PageSize.HALFLETTER.Rotate().Width)
                        {
                            inputDoc.SetPageSize(PageSize.HALFLETTER);
                        }

                        inputDoc.NewPage();
                        PdfImportedPage page = outputWriter.GetImportedPage(overlayReader, i);
                        int rotation = reader.GetPageRotation(i);
                        if (overlayRotation == 90 || overlayRotation == 270)
                        {
                            cb1.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                        else
                        {
                            cb1.AddTemplate(overLay, 1f, 0, 0, 1f, 0, 0);
                        }
                        i++;
                    }
                }
                else
                {
                    if (EsPaginaReciboCompleta(overlayReader.GetPageSize(1).Height, overlayReader.GetPageSize(1).Width))
                    {
                        int i = 1;
                        while (i <= n)
                        {
                            var size = reader.GetPageSizeWithRotation(i);
                            PdfImportedPage page = outputWriter.GetImportedPage(reader, i);
                            PdfImportedPage overLay = outputWriter.GetImportedPage(overlayReader, i);
                            inputDoc.SetPageSize(new iTextSharp.GE.text.Rectangle(size.Width, size.Height));
                            inputDoc.NewPage();
                            cb1.AddTemplate(overLay, 0, 0);
                            inputDoc.NewPage();
                            cb1.AddTemplate(page, 0, 0);
                            i++;
                        }
                    }
                    else
                    {
                        int i = 1;
                        while (i <= n)
                        {
                            //se generará el recibo superponiendo dos capas
                            PdfImportedPage overLay = outputWriter.GetImportedPage(overlayReader, i);

                            //El tamaño deberá ser el doble al del recibo existente.
                            overLay.Height = overLay.Height * 2;
                            var size = reader.GetPageSizeWithRotation(i);
                            inputDoc.SetPageSize(new iTextSharp.GE.text.Rectangle(size.Width, size.Height * 2));
                            inputDoc.NewPage();

                            PdfImportedPage page = outputWriter.GetImportedPage(reader, i);
                            int rotation = reader.GetPageRotation(i);

                            //Inserción parte superior recibo (con firma)
                            if (overlayRotation == 90 || overlayRotation == 270)
                            {
                                cb1.AddTemplate(overLay, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height * 2);
                            }
                            else
                            {
                                cb1.AddTemplate(overLay, 1f, 0, 0, 1f, 0, 0);
                            }

                            //Inserción parte inferior recibo (sin firma)
                            if (rotation == 90 || rotation == 270)
                            {
                                cb1.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height + (float)tamannoMargen);
                            }
                            else
                            {
                                cb1.AddTemplate(page, 1f, 0, 0, 1f, 0, reader.GetPageSizeWithRotation(i).Height + (float)tamannoMargen);
                            }

                            i++;
                        }
                    }
                }
                inputDoc.Close();
                overlayReader.Close();
                salida = fs.ToArray();
            }
            reader.Close();
            return salida;
        }

        #region Private Methods

        /// <summary>
        /// Eses the pagina recibo completa.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <returns>
        /// bool
        /// </returns>
        private static bool EsPaginaReciboCompleta(float height, float width)
        {
            return (height == PageSize.A4.Height && width == PageSize.A4.Width) || (height == PageSize.LETTER.Height && width == PageSize.LETTER.Width);
        }

        /// <summary>
        /// Annadirs the pagina.
        /// </summary>
        /// <param name="documento">The documento.</param>
        /// <param name="imagen">The imagen.</param>
        /// <param name="tamannoPagina">The tamanno pagina.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        private void AnnadirPagina(Document documento, iTextSharp.GE.text.Image imagen, string tamannoPagina, decimal? height, decimal? width)
        {
            Rectangle tamanno = null;
            if (documento != null)
            {
                if (height.HasValue && width.HasValue)
                {
                    tamanno = new Rectangle((float)width, (float)height);
                }
                else
                {
                    // se crea el rectangulo pasando a la X la altura y a la Y la anchura para que salga horizontal
                    tamanno = string.IsNullOrEmpty(tamannoPagina) ? new Rectangle(PageSize.A5.Height, PageSize.A5.Width) : (tamannoPagina == "HalfLetter" ? new Rectangle(PageSize.HALFLETTER.Height, PageSize.HALFLETTER.Width) : new Rectangle(PageSize.A5.Height, PageSize.A5.Width));
                }

                documento.SetPageSize(tamanno);
                documento.NewPage();
                if (imagen != null)
                {
                    imagen.SetAbsolutePosition(0, 0);
                    /*imagen.ScaleToFit(tamanno);*/
                    imagen.ScaleAbsolute(tamanno.Width, tamanno.Height);
                    documento.Add(imagen);
                }
            }
        }

        /// <summary>
        /// Determines whether the specified value is arabic.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is arabic; otherwise, <c>false</c>.
        /// </returns>
        private bool IsArabic(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            Regex regex = new Regex("[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]");
            return regex.IsMatch(value);
        }        

        /// <summary>
        /// Reemplazars the separador decimal.
        /// </summary>
        /// <param name="valor">The valor.</param>
        /// <param name="posiciones">The posiciones.</param>
        /// <returns>
        /// string
        /// </returns>
        private string ReemplazarSeparadorDecimal(decimal valor, int? posiciones)
        {
            int posicionesDecimal = posiciones.HasValue ? posiciones.Value : 2;

            decimal step = (decimal)Math.Pow(10, posicionesDecimal);
            decimal tmp = Math.Truncate(step * valor);
            decimal truncado = tmp / step;

            string formato = posicionesDecimal >= 2 ? "#,0.00" + string.Empty.PadRight(posicionesDecimal - 2, '0') : "#,0." + string.Empty.PadRight(posicionesDecimal, '#');
            string salida = truncado.ToString(formato);

            return salida;
        }

        /// <summary>
        /// Gets the plantilla from XML.
        /// </summary>
        /// <param name="plantillaPath">The plantilla path.</param>
        /// <returns>ReciboPDF</returns>
        private ReciboPDF GetPlantillaFromXML(string plantillaPath)
        {
            ReciboPDF recibo = null;
            XmlSerializer ser = new XmlSerializer(typeof(ReciboPDF));
            TextReader reader = new StreamReader(plantillaPath);

            recibo = (ReciboPDF)ser.Deserialize(reader);

            return recibo;
        }

        /// <summary>
        /// Annadirs the pagina.
        /// </summary>
        /// <param name="documento">The documento.</param>
        /// <param name="imagePath">The image path.</param>
        /// <param name="orientacion">The orientacion.</param>
        /// <returns>
        /// PdfPage
        /// </returns>
        private PdfSharp.Pdf.PdfPage AnnadirPagina(PdfSharp.Pdf.PdfDocument documento, string imagePath, bool? orientacion)
        {
            return this.AnnadirPagina(documento, imagePath, orientacion, true);
        }

        /// <summary>
        /// Annadirs the pagina.
        /// </summary>
        /// <param name="documento">The documento.</param>
        /// <param name="imagePath">The image path.</param>
        /// <param name="orientacion">The orientacion.</param>
        /// <param name="incluirFondo">if set to <c>true</c> [incluir fondo].</param>
        /// <returns>PdfPage</returns>
        private PdfSharp.Pdf.PdfPage AnnadirPagina(PdfSharp.Pdf.PdfDocument documento, string imagePath, bool? orientacion, bool incluirFondo)
        {
            PdfSharp.Pdf.PdfPage pagina = null;

            if (documento != null)
            {
                pagina = documento.AddPage();
                XImage imagen = XImage.FromFile(imagePath);
                pagina.Orientation = !orientacion.HasValue || orientacion.Value ? PageOrientation.Landscape : PageOrientation.Portrait;

                XImageFormat formato = imagen.Format;

                if (pagina.Orientation == PageOrientation.Landscape)
                {
                    pagina.Size = PdfSharp.PageSize.A5;
                }
                else
                {
                    pagina.Size = PdfSharp.PageSize.A4;
                }
                if (incluirFondo)
                {
                    using (XGraphics xgraphics = XGraphics.FromPdfPage(pagina))
                    {
                        //xgraphics.DrawImage(imagen, 500,500, pagina.Width.Point, (pagina.Width.Point / imagen.PointWidth)* imagen.PointHeight);
                        xgraphics.DrawImage(imagen, new XRect(0, 0, pagina.Width, pagina.Height));

                        //xgraphics.DrawImage(imagen, new XRect(0, 0, pagina.Width, pagina.Height * (imagen.Width / imagen.Height)));
                    }
                }
            }

            return pagina;
        }

        /// <summary>
        /// Escribirs the sobre campo.
        /// </summary>
        /// <param name="campo">The campo.</param>
        /// <param name="value">The value.</param>
        /// <param name="wri">The pagina.</param>
        /// <param name="doc">The document.</param>
        /// <param name="cadenaConcatenar">The cadena concatenar.</param>
        /// <returns>
        /// true if all worked ok
        /// </returns>
        private bool EscribirSobreCampo(CampoRecibo campo, object? value, PdfWriter wri, iTextSharp.GE.text.Document doc, string? cadenaConcatenar = null)
        {
            bool funcionamientoOK = false;
            if (campo.Nombre.Equals("LineaSeparadorField"))
            {
                PdfContentByte contentByte = wri.DirectContent;
                contentByte.SetLineWidth(1);
                contentByte.MoveTo(10, doc.PageSize.Height - campo.Posicion.Y);
                contentByte.LineTo(doc.PageSize.Width - 10, doc.PageSize.Height - campo.Posicion.Y);
                contentByte.Stroke();

                return true;
            }
            if (value != null)
            {
                if (!string.IsNullOrEmpty(campo.Formato) && value.GetType().Equals(typeof(string)))
                {
                    if (campo.Formato == "Capitalize")
                    {
                        value = char.ToUpper(value.ToString()[0]) + value.ToString().Substring(1).ToLower();
                    }
                    else if (campo.Formato == "Uppercase")
                    {
                        value = value.ToString().ToUpper();
                    }
                    else if (campo.Formato == "Lowercase")
                    {
                        value = value.ToString().ToLower();
                    }
                }

                BaseFont bf = BaseFont.CreateFont("C:\\Windows\\Fonts\\ARIALUNI.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font f = new Font(
                    bf,
                    campo.Fuente.Tamanno,
                    campo.Fuente.Negrita && campo.Fuente.Cursiva ? Font.BOLDITALIC :
                    (campo.Fuente.Negrita ? Font.BOLD :
                    (campo.Fuente.Cursiva ? Font.ITALIC :
                    (campo.Fuente.Subrayado ? Font.STRIKETHRU :
                    Font.NORMAL))),
                    new BaseColor(campo.Pincel.R, campo.Pincel.G, campo.Pincel.B, campo.Pincel.A));

                int posiciones = campo.PosicionesDecimales.HasValue ? campo.PosicionesDecimales.Value : 2;
                string valor = value.GetType().Equals(typeof(decimal)) ? this.ReemplazarSeparadorDecimal((decimal)value, posiciones) : value.ToString();

                value = string.IsNullOrEmpty(cadenaConcatenar) ? valor : valor + " " + cadenaConcatenar;

                Paragraph p = new Paragraph(string.IsNullOrEmpty(value.ToString()) ? string.Empty : value.ToString(), f);

                ColumnText ct = new ColumnText(wri.DirectContent);

                if (this.IsArabic(value.ToString()))
                {
                    ct.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                }
                else
                {
                    ct.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                }
                p.Alignment = ct.Alignment = campo.AlineacionTexto == "Right" ? Element.ALIGN_RIGHT : (campo.AlineacionTexto == "Center" ? Element.ALIGN_CENTER : (campo.AlineacionTexto == "Justified" ? Element.ALIGN_JUSTIFIED : Element.ALIGN_LEFT));
                if (campo.Interlineado != null)
                {
                    p.SetLeading(campo.Interlineado.Value, 0f);
                }
                ct.SetSimpleColumn(
                Convert.ToSingle(campo.Posicion.X),
                Convert.ToSingle(doc.PageSize.Height - campo.Posicion.Y),
                Convert.ToSingle(campo.Posicion.X + campo.Posicion.Ancho),
                    Convert.ToSingle(doc.PageSize.Height - campo.Posicion.Y - campo.Posicion.Alto));
                ct.AddElement(p);
                ct.Go();
            }

            return funcionamientoOK;
        }

        /// <summary>
        /// Escribirs the sobre campo.
        /// </summary>
        /// <param name="campo">The campo.</param>
        /// <param name="value">The value.</param>
        /// <param name="pagina">The pagina.</param>
        /// <returns>
        /// true if all worked ok
        /// </returns>
        private bool EscribirSobreCampo(CampoRecibo campo, string value, PdfSharp.Pdf.PdfPage pagina)
        {
            bool funcionamientoOK = false;

            //XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);

            XStringFormat formato = new XStringFormat();
            ////formato.FormatFlags = new XStringFormatFlags();

            //options.Unicode;
            if (pagina != null)
            {
                using (XGraphics xgraphics = XGraphics.FromPdfPage(pagina))
                {
                    XTextFormatter textformatter = new XTextFormatter(xgraphics);
                    XRect posicion = this.XrectFromField(campo.Posicion);

                    textformatter.Alignment = string.IsNullOrEmpty(campo.AlineacionTexto) ? XParagraphAlignment.Left : (XParagraphAlignment)Enum.Parse(typeof(XParagraphAlignment), campo.AlineacionTexto);

                    XFontStyle style = XFontStyle.Regular;
                    if (campo.Fuente.Cursiva)
                    {
                        style = style | XFontStyle.Italic;
                    }
                    if (campo.Fuente.Negrita)
                    {
                        style = style | XFontStyle.Bold;
                    }
                    if (campo.Fuente.Subrayado)
                    {
                        style = style | XFontStyle.Underline;
                    }

                    if (campo.Border)
                    {
                        PosicionCampo posRect = new PosicionCampo(campo.Posicion);
                        posicion.Y += 4;

                        if (textformatter.Alignment == XParagraphAlignment.Left)
                        {
                            posicion.X += 4;
                        }
                        else if (textformatter.Alignment == XParagraphAlignment.Right)
                        {
                            posicion.X -= 4;
                        }
                        this.PintarRectangulo(posRect, campo.PincelBorder, pagina, xgraphics);
                    }

                    XFont fuenteCampo = new XFont(campo.Fuente.NombreFuente, campo.Fuente.Tamanno, style);
                    XSolidBrush pincelCampo = new XSolidBrush(XColor.FromArgb(campo.Pincel.A, campo.Pincel.R, campo.Pincel.G, campo.Pincel.B));

                    textformatter.DrawString(string.IsNullOrEmpty(value) ? string.Empty : value, fuenteCampo, pincelCampo, posicion);
                }
            }

            return funcionamientoOK;
        }

        /// <summary>
        /// Pintars the rectangulo.
        /// </summary>
        /// <param name="posicion">The posicion.</param>
        /// <param name="pincel">The pincel.</param>
        /// <param name="pagina">The pagina.</param>
        /// <param name="xgraphics">The xgraphics.</param>
        /// <returns>
        /// true if all work ok
        /// </returns>
        private bool PintarRectangulo(PosicionCampo posicion, PincelCampo pincel, PdfSharp.Pdf.PdfPage pagina, XGraphics xgraphics = null)
        {
            bool funcionamientoOK = false;
            XRect posRect = this.XrectFromField(posicion);

            if (xgraphics == null)
            {
                using (XGraphics xgraphicsAux = XGraphics.FromPdfPage(pagina))
                {
                    XSolidBrush pincelCampo = new XSolidBrush(XColor.FromArgb(pincel.A, pincel.R, pincel.G, pincel.B));
                    XPen pen = new XPen(pincelCampo.Color);
                    xgraphicsAux.DrawRectangle(pen, posRect);
                }
            }
            else
            {
                XSolidBrush pincelCampo = new XSolidBrush(XColor.FromArgb(pincel.A, pincel.R, pincel.G, pincel.B));
                XPen pen = new XPen(pincelCampo.Color);
                xgraphics.DrawRectangle(pen, posRect);
            }

            return funcionamientoOK;
        }

        /// <summary>
        /// Xrects from field.
        /// </summary>
        /// <param name="posicionCampo">The posicion campo.</param>
        /// <returns>
        /// XRect
        /// </returns>
        private XRect XrectFromField(PosicionCampo posicionCampo)
        {
            XRect posicion = new XRect(posicionCampo.X, posicionCampo.Y, posicionCampo.Ancho, posicionCampo.Alto);
            return posicion;
        } 
        #endregion
    }
}
