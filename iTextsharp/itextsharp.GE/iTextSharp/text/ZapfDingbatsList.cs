using System;

namespace iTextSharp.GE.text
{
    /**
    * 
    * A special-version of <CODE>LIST</CODE> whitch use zapfdingbats-letters.
    * 
    * @see com.lowagie.text.List
    * @author Michael Niedermair and Bruno Lowagie
    */
    public class ZapfDingbatsList : List {
        /**
        * char-number in zapfdingbats
        */
        protected int zn;

        /**
        * Creates a ZapfDingbatsList
        * 
        * @param zn a char-number
        */
        public ZapfDingbatsList(int zn) : base(true) {
            this.zn = zn;
            float fontsize = symbol.Font.Size;
            symbol.Font = FontFactory.GetFont(FontFactory.ZAPFDINGBATS, fontsize, Font.NORMAL);
            postSymbol = " ";
        }

        /**
        * Creates a ZapfDingbatsList
        * 
        * @param zn a char-number
        * @param symbolIndent    indent
        */
        public ZapfDingbatsList(int zn, int symbolIndent) : base(true, symbolIndent) {
            this.zn = zn;
            float fontsize = symbol.Font.Size;
            symbol.Font = FontFactory.GetFont(FontFactory.ZAPFDINGBATS, fontsize, Font.NORMAL);
            postSymbol = " ";
        }

        /**
        * Sets the dingbat's color.
        *
        * @param zapfDingbatColor color for the ZapfDingbat
        */
        virtual public void setDingbatColor(BaseColor zapfDingbatColor)
        {
            float fontsize = symbol.Font.Size;
            symbol.Font = FontFactory.GetFont(FontFactory.ZAPFDINGBATS, fontsize, Font.NORMAL, zapfDingbatColor);
        }

        /**
        * set the char-number 
        * @param zn a char-number
        */
        virtual public int CharNumber {
            set {
                this.zn = value;
            }
            get {
                return this.zn;
            }
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
                chunk.Append(((char)zn).ToString());
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
		    ZapfDingbatsList clone = new ZapfDingbatsList(zn);
		    PopulateProperties(clone);
		    return clone;
	    }
    }
}
