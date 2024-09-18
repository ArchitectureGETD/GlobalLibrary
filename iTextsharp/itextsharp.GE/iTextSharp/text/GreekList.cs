using System;
using iTextSharp.GE.text.factories;

namespace iTextSharp.GE.text
{
    /**
    * 
    * A special-version of <CODE>LIST</CODE> whitch use greek-letters.
    * 
    * @see com.lowagie.text.List
    */
    public class GreekList : List {
        /**
        * Initialization
        * 
        * @param symbolIndent   indent
        */
        public GreekList() : base(true) {
            SetGreekFont();
        }

        /**
        * Initialisierung
        * 
        * @param symbolIndent   indent
        */
        public GreekList(int symbolIndent) : base(true, symbolIndent) {
            SetGreekFont();
        }

        /**
        * Initialisierung 
        * @param    greeklower      greek-char in lowercase   
        * @param    symbolIndent    indent
        */
        public GreekList(bool greeklower, int symbolIndent) : base(true, symbolIndent) {
            lowercase = greeklower;
            SetGreekFont();
        }

        /**
        * change the font to SYMBOL
        */
        virtual protected void SetGreekFont() {
            float fontsize = symbol.Font.Size;
            symbol.Font = FontFactory.GetFont(FontFactory.SYMBOL, fontsize, Font.NORMAL);
        }

        /**
        * Adds an <CODE>Object</CODE> to the <CODE>List</CODE>.
        *
        * @param    o   the object to add.
        * @return true if adding the object succeeded
        */
        public override bool Add(IElement o) {
            if (o is ListItem) {
                ListItem item = (ListItem) o;
                Chunk chunk = new Chunk(preSymbol, symbol.Font);
                chunk.Attributes = symbol.Attributes;
                chunk.Append(GreekAlphabetFactory.GetString(first + list.Count, lowercase));
                chunk.Append(postSymbol);
                item.ListSymbol = chunk;
                item.SetIndentationLeft(symbolIndent, autoindent);
                item.IndentationRight = 0;
                list.Add(item);
                return true;
            } else if (o is List) {
                List nested = (List) o;
                nested.IndentationLeft = nested.IndentationLeft + symbolIndent;
                first--;
                list.Add(nested);
                return true;
            }
            return false;
        }

	    public override List CloneShallow() {
		    GreekList clone = new GreekList();
		    PopulateProperties(clone);
		    return clone;
	    }
    }
}
