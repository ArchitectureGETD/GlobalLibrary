using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text
{
    public class TabSplitCharacter : ISplitCharacter
    {
        public static readonly ISplitCharacter TAB = new TabSplitCharacter();

        virtual public bool IsSplitCharacter(int start, int current, int end, char[] cc, PdfChunk[] ck)
        {
            return true;
        }
    }
}
