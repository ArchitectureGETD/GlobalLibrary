using System;
using iTextSharp.GE.text.pdf;

namespace iTextSharp.GE.text.pdf.interfaces {

    /**
    * Viewer preferences are described in section 3.6.1 and 8.1 of the
    * PDF Reference 1.7 (Table 3.25 on p139-142 and Table 8.1 on p579-581).
    * They are explained in section 13.1 of the book 'iText in Action'.
    * The values of the different preferences were originally stored
    * in class PdfWriter, but they have been moved to this separate interface
    * for reasons of convenience.
    */

    public interface IPdfViewerPreferences {

        /**
        * Sets the page layout and page mode preferences by ORing one or two of these constants.
        * <p>
        * <ul>
        * <li>The page layout to be used when the document is opened (choose one).
        *   <ul>
        *   <li><b>PAGE_LAYOUT_SINGLE_PAGE</b> - Display one page at a time. (default)
        *   <li><b>PAGE_LAYOUT_ONE_COLUMN</b> - Display the pages in one column.
        *   <li><b>PAGE_LAYOUT_TWO_COLUMN_LEFT</b> - Display the pages in two columns, with
        *       oddnumbered pages on the left.
        *   <li><b>PAGE_LAYOUT_TWO_COLUMN_RIGHT</b> - Display the pages in two columns, with
        *       oddnumbered pages on the right.
        *   <li><b>PAGE_LAYOUT_TWO_PAGE_LEFT</b> - Display the pages two at a time, with
        *       oddnumbered pages on the left.
        *   <li><b>PAGE_LAYOUT_TWO_PAGE_RIGHT</b> - Display the pages two at a time, with
        *       oddnumbered pages on the right.
        *   </ul>
        * <li>The page mode how the document should be displayed
        *     when opened (choose one).
        *   <ul>
        *   <li><b>PAGE_MODE_USE_NONE</b> - Neither document outline nor thumbnail images visible. (default)
        *   <li><b>PAGE_MODE_USE_OUTLINES</b> - Document outline visible.
        *   <li><b>PAGE_MODE_USE_THUMBS</b> - Thumbnail images visible.
        *   <li><b>PAGE_MODE_FULL_SCREEN</b> - Full-screen mode, with no menu bar, window
        *       controls, or any other window visible.
        *   <li><b>PAGE_MODE_USE_OC</b> - Optional content group panel visible
        *   <li><b>PAGE_MODE_USE_ATTACHMENTS</b> - Attachments panel visible
        *   </ul>
        * </ul>
        * For backward compatibility these values are also supported,
        * but it's better to use method <code>addViewerPreference(key, value)</code>
        * if you want to change the following preferences:
        * <ul>
        * <li><b>HIDE_TOOLBAR</b> - A flag specifying whether to hide the viewer application's tool
        *     bars when the document is active.
        * <li><b>HIDE_MENUBAR</b> - A flag specifying whether to hide the viewer application's
        *     menu bar when the document is active.
        * <li><b>HIDE_WINDOW_UI</b> - A flag specifying whether to hide user interface elements in
        *     the document's window (such as scroll bars and navigation controls),
        *     leaving only the document's contents displayed.
        * <li><b>FIT_WINDOW</b> - A flag specifying whether to resize the document's window to
        *     fit the size of the first displayed page.
        * <li><b>CENTER_WINDOW</b> - A flag specifying whether to position the document's window
        *     in the center of the screen.
        * <li><b>DISPLAY_DOC_TITLE</b> - A flag specifying whether to display the document's title
        *     in the top bar.
        * <li>The predominant reading order for text. This entry has no direct effect on the
        *     document's contents or page numbering, but can be used to determine the relative
        *     positioning of pages when displayed side by side or printed <i>n-up</i> (choose one).
        *   <ul>
        *   <li><b>DIRECTION_L2R</b> - Left to right
        *   <li><b>DIRECTION_R2L</b> - Right to left (including vertical writing systems such as
        *       Chinese, Japanese, and Korean)
        *   </ul>
        * <li>The document's page mode, specifying how to display the
        *     document on exiting full-screen mode. It is meaningful only
        *     if the page mode is <b>PageModeFullScreen</b> (choose one).
        *   <ul>
        *   <li><b>NON_FULL_SCREEN_PAGE_MODE_USE_NONE</b> - Neither document outline nor thumbnail images
        *       visible
        *   <li><b>NON_FULL_SCREEN_PAGE_MODE_USE_OUTLINES</b> - Document outline visible
        *   <li><b>NON_FULL_SCREEN_PAGE_MODE_USE_THUMBS</b> - Thumbnail images visible
        *   <li><b>NON_FULL_SCREEN_PAGE_MODE_USE_OC</b> - Optional content group panel visible
        *   </ul>
        * <li><b>PRINT_SCALING_NONE</b> - Indicates that the print dialog should reflect no page scaling.
        * </ul>
        * @param preferences the viewer preferences
	    * @see PdfViewerPreferences#addViewerPreference
        */
        int ViewerPreferences {
            set;
        }
        
        /**
        * Adds a viewer preference.
        * <ul>
        * <li>In case the key is one of these values:
        * 		<ul>
        * 			<li>PdfName.<b>HIDETOOLBAR</b>
        * 			<li>PdfName.<b>HIDEMENUBAR</b>
        * 			<li>PdfName.<b>HIDEWINDOWUI</b>
        * 			<li>PdfName.<b>FITWINDOW</b>
        * 			<li>PdfName.<b>CENTERWINDOW</b>
        * 			<li>PdfName.<b>DISPLAYDOCTITLE</b>
        * 		</ul>
        * The value must be a of type PdfBoolean (true or false).
        * <li>In case the key is PdfName.<b>NONFULLSCREENPAGEMODE</b>,
        * the value must be one of these names:
        * 		<ul>
        * 			<li>PdfName.<b>USENONE</b>
        * 			<li>PdfName.<b>USEOUTLINES</b>
        * 			<li>PdfName.<b>USETHUMBS</b>
        * 			<li>PdfName.<b>USEOC</b>
        * 		</ul>
        * <li>In case the key is PdfName.DIRECTION,
        * the value must be one of these names:
        * 		<ul>
        * 			<li>PdfName.<b>L2R</b>
        * 			<li>PdfName.<b>R2L</b>
        * 		</ul>
        * <li>In case the key is one of these values:
        * 		<ul>
        * 			<li>PdfName.<b>VIEWAREA</b>
        * 			<li>PdfName.<b>VIEWCLIP</b>
        * 			<li>PdfName.<b>PRINTAREA</b>
        * 			<li>PdfName.<b>PRINTCLIP</b>
        * 		</ul>
        * 	The value must be one of these names:
        * 		<ul>
        * 			<li>PdfName.<b>MEDIABOX</b>
        * 			<li>PdfName.<b>CROPBOX</b>
        * 			<li>PdfName.<b>BLEEDBOX</b>
        * 			<li>PdfName.<b>TRIMBOX</b>
        * 			<li>PdfName.<b>ARTBOX</b>
        * 		</ul>
        * <li>In case the key is PdfName.<b>PRINTSCALING</b>, the value can be
        * 		<ul>
        * 			<li>PdfName.<b>APPDEFAULT</b>
        * 			<li>PdfName.<b>NONE</b>
        * 		</ul>
        * <li>In case the key is PdfName.<b>DUPLEX</b>, the value can be:
        * 		<ul>
        * 			<li>PdfName.<b>SIMPLEX</b>
        * 			<li>PdfName.<b>DUPLEXFLIPSHORTEDGE</b>
        * 			<li>PdfName.<b>DUPLEXFLIPLONGEDGE</b>
        * 		</ul>
        * <li>In case the key is PdfName.<b>PICKTRAYBYPDFSIZE</b>, the value must be of type PdfBoolean.
        * <li>In case the key is PdfName.<b>PRINTPAGERANGE</b>, the value must be of type PdfArray.
        * <li>In case the key is PdfName.<b>NUMCOPIES</b>, the value must be of type PdfNumber.
        * <ul>
        * </ul>
        * @param key	the name of the viewer preference
        * @param value	the value of the viewer preference
	    * @see PdfViewerPreferences#setViewerPreferences
        */
        void AddViewerPreference(PdfName key, PdfObject value);    	
    }
}
