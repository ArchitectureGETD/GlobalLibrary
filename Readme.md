# GlobalLibrary.Data.Files

The **ReciboPdfTools** class provides a set of tools for creating and manipulating PDF documents, specifically for handling receipts. This class leverages the iTextSharp and PdfSharp libraries to perform various PDF operations.

## Dependencies

-   iTextSharp
-   PdfSharp

## Supported Platforms

- Windows

## Properties

- Font FuenteDefecto: Default font used in the PDF.
- string PincelDefecto: Default brush used in the PDF.

## Methods

### Public Methods

> **MemoryStream Create(IEnumerable<KeyValuePair<string, object>> datosComunes, IEnumerable<IEnumerable<KeyValuePair<string, object>>> datosLineas, ReciboPDF plantilla, Entities.Pdf.Image imagenFondo, Entities.Pdf.Image firmaAsesor, bool esDigital, Entities.Pdf.Image reciboCaraB, Entities.Pdf.Image qr, Func<ReciboPdfTools, int, object, object, List<CampoRecibo>, List<CampoRecibo>, List<CampoRecibo>, bool> fillDocumentFileAction)**
> - Creates a PDF document based on the provided data and template.
>
> **MemoryStream CreateBill(IEnumerable<KeyValuePair<string, object>> datosFactura, IEnumerable<IEnumerable<KeyValuePair<string, object>>> datosServicios, IEnumerable<IEnumerable<KeyValuePair<string, object>>> datosImpuestos, string filename, string fondoname, string path, string fileExtension)**
> - Creates a bill PDF document based on the provided data.
>
> **void AnnadirPaginaFromObject(object documento, Entities.Pdf.Image imagen, string tamannoPagina, decimal? height, decimal? width)**
> - Adds a page to the document from an object.
>
> **bool EscribirSobreCampoFromObject(CampoRecibo campo, object? value, object writer, object doc, string? cadenaConcatenar = null)**
> - Writes data to a specific field in the document from an object.
>
> **bool CheckVisibility(CampoRecibo campo, IEnumerable<KeyValuePair<string, object>> datos)**
> - Checks the visibility of a field based on the provided data.
>
> **static void AddImageToDocumentObject(object documento, CampoRecibo campo, Entities.Pdf.Image imagen)**
> - Adds an image to the document.
>
> **static Entities.Pdf.Image MappingImage(Entities.Pdf.Image imagen, bool isMask)**
> - Maps an image with optional masking.
>
> **static void AddImageTable(Entities.Pdf.Image image, object documentoFinal, object writer, CampoRecibo campo)**
> - Adds an image to a table in the document.
>
> **static byte[] IncrustarFirma(byte[] inputFile, byte[] firmaBytes, byte[] huellaBytes)**
> - Embeds a signature into the PDF document.
>
> **static byte[] CortarDocumento(byte[] inputFile)**
> - Cuts the PDF document.
>
> **static byte[] SelectPDFPages(byte[] pdf)**
> - Selects specific pages from the PDF document.
>
> **static byte[] UnirRecibos(List<byte[]> recibos)**
> - Merges multiple receipt PDFs into one.
>
> **static byte[] EliminarTexto(byte[] inputFile, string searchText)**
> - Removes specific text from the PDF document.
>
> **static byte[] EliminarFondo(byte[] inputFile, bool firma, Entities.Pdf.Image fondoImprimir)**
> - Removes the background from the PDF document.
>
> **static byte[] ObtenerReciboCompleto(decimal margen, out byte[] salida, byte[] docSuperior, byte[] docInferior, bool esCompletoSimple)**
> - Obtains a complete receipt by merging the top and bottom parts of the document.


## Getting Started

This guide will help you get started with the NuGet packages located in the `\GlobalLibrary\NuGets\[Version]` folder.

### Prerequisites

- Development ide for .NET project
- .NET SDK

### Installation

1. Open your project in Visual Studio.
2. Right-click on your solution in the Solution Explorer and select `Manage NuGet Packages for Solution...`.
3. Click on the `Settings` icon (gear) in the top right corner of the NuGet Package Manager.
4. Add a new package source:
    - Name: `GlobalLibrary`
    - Source: `\GlobalLibrary\NuGets\[Version]`
5. Click `OK` to save the new package source.
6. Go back to the `Browse` tab in the NuGet Package Manager.
7. Select the `GlobalLibrary` package source from the drop-down menu.
8. Search for the desired package and click `Install`.


## Usage
To use the ReciboPdfTools class, instantiate it and call the desired methods with the appropriate parameters. Below is an example of how to create a receipt PDF:

```csharp
    var reciboPdfTools = new ReciboPdfTools();
    var datosComunes = new List<KeyValuePair<string, object>> { /* populate with data */ };
    var datosLineas = new List<IEnumerable<KeyValuePair<string, object>>> { /* populate with data */ };
    var plantilla = new ReciboPDF();
    var imagenFondo = iTextSharp.GE.text.Image.GetInstance("path/to/image");
    var firmaAsesor = iTextSharp.GE.text.Image.GetInstance("path/to/signature");
    var reciboCaraB = iTextSharp.GE.text.Image.GetInstance("path/to/reciboCaraB");
    var qr = iTextSharp.GE.text.Image.GetInstance("path/to/qr");

    using (var pdfStream = reciboPdfTools.Create(datosComunes, datosLineas, plantilla, imagenFondo, firmaAsesor, true, reciboCaraB, qr))
    {
        // Save or use the PDF stream
    }
```

### Contributing

If you want to contribute to the GlobalLibrary, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add new feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Create a new Pull Request.

### License

This project is licensed under the MIT License - see the [LICENSE](/GlobalLibrary/LICENSE.md) file for details.

