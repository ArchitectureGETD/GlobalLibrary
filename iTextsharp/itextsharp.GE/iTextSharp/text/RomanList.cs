using System;
using System.Text;
using iTextSharp.GE.text.factories;

namespace iTextSharp.GE.text {

/**
 * 
 * A special-version of <CODE>LIST</CODE> which use roman-letters.
 * 
 * @see com.lowagie.text.List
 * @version 2003-06-22
 * @author Michael Niedermair
 */

    public class RomanList : List {

        /**
        * Initialization
        */
        public RomanList() : base(true) {
        }

        /**
        * Initialization
        * 
        * @param symbolIndent    indent
        */
        public RomanList(int symbolIndent) : base(true, symbolIndent){
        }

        /**
        * Initialization 
        * @param    romanlower        roman-char in lowercase   
        * @param     symbolIndent    indent
        */
        public RomanList(bool romanlower, int symbolIndent) : base(true, symbolIndent) {
            this.lowercase = romanlower;
        }

        /**
        * Adds an <CODE>Object</CODE> to the <CODE>List</CODE>.
        *
        * @param    o    the object to add.
        * @return true if adding the object succeeded
        */
        public override bool Add(IElement o) {
            if (o is ListItem) {
                ListItem item = (ListItem) o;
                Chunk chunk = new Chunk(preSymbol, symbol.Font);
                chunk.Attributes = symbol.Attributes;
                chunk.Append(RomanNumberFactory.GetString(first + list.Count, lowercase));
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
            RomanList clone = new RomanList();
            PopulateProperties(clone);
            return clone;
        }
    }
}
