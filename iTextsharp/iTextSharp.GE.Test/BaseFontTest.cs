using iTextSharp.GE.text.pdf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace iTextSharp.GE.Test
{
    [TestClass]
    public class BaseFontTest
    {
        [TestMethod]
        public void CreateFont_Test()
        {
            try
            {
                var font = BaseFont.CreateFont("C:\\Windows\\Fonts\\ARIALUNI.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }
    }
}
