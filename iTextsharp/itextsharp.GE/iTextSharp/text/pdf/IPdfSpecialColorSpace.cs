namespace iTextSharp.GE.text.pdf {
    public interface IPdfSpecialColorSpace {
        ColorDetails[] GetColorantDetails(PdfWriter writer);
    }
}
