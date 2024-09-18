using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.GE.text.pdf.interfaces
{
	public interface IPdfStructureElement {
        PdfObject GetAttribute(PdfName name);
        void SetAttribute(PdfName name, PdfObject obj);
	}
}
