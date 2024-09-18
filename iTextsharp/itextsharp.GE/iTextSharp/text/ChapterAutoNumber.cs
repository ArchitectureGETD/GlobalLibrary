using System;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text {

    /**
    * Chapter with auto numbering.
    *
    * @author Michael Niedermair
    */
    public class ChapterAutoNumber : Chapter {

        /**
        * Is the chapter number already set?
        * @since	2.1.4
        */
        protected bool numberSet = false;

        /**
        * Create a new object.
        *
        * @param para     the Chapter title (as a <CODE>Paragraph</CODE>)
        */
        public ChapterAutoNumber(Paragraph para) : base(para, 0) {
        }

        /**
        * Create a new objet.
        * 
        * @param title     the Chapter title (as a <CODE>String</CODE>)
        */
        public ChapterAutoNumber(String title) : base(title, 0) {
        }

        /**
        * Create a new section for this chapter and ad it.
        *
        * @param title  the Section title (as a <CODE>String</CODE>)
        * @return Returns the new section.
        */
        public override Section AddSection(String title) {
    	    if (AddedCompletely) {
    		    throw new InvalidOperationException(MessageLocalization.GetComposedMessage("this.largeelement.has.already.been.added.to.the.document"));
    	    }
            return AddSection(title, 2);
        }

        /**
        * Create a new section for this chapter and add it.
        *
        * @param title  the Section title (as a <CODE>Paragraph</CODE>)
        * @return Returns the new section.
        */
        public override Section AddSection(Paragraph title) {
    	    if (AddedCompletely) {
    		    throw new InvalidOperationException(MessageLocalization.GetComposedMessage("this.largeelement.has.already.been.added.to.the.document"));
    	    }
            return AddSection(title, 2);
        }

        /**
        * Changes the Chapter number.
        * @param	number	the new chapter number
        * @since 2.1.4
        */
        virtual public int SetAutomaticNumber(int number) {
    	    if (!numberSet) {
        	    number++;
        	    base.SetChapterNumber(number);
        	    numberSet = true;
    	    }
		    return number;
        }
    }
}
