using System;
using System.Collections.Generic;

namespace iTextSharp.GE.text.html {

    /**
     * Static final values of supported HTML tags and attributes.
     * @since 5.0.6
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public static class HtmlTags {
    
	    // tag names
    	
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String A = "a";
	    /** name of a tag */
	    public const String B = "b";
	    /** name of a tag */
	    public const String BODY = "body";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String BLOCKQUOTE = "blockquote";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String BR = "br";
	    /** name of a tag */
	    public const String DIV = "div";
	    /** name of a tag */
	    public const String EM = "em";
	    /** name of a tag */
	    public const String FONT = "font";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String H1 = "h1";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String H2 = "h2";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String H3 = "h3";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String H4 = "h4";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String H5 = "h5";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String H6 = "h6";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String HR = "hr";
	    /** name of a tag */
	    public const String I = "i";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String IMG = "img";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String LI = "li";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String OL = "ol";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String P = "p";
	    /** name of a tag */
	    public const String PRE = "pre";
	    /** name of a tag */
	    public const String S = "s";
	    /** name of a tag */
	    public const String SPAN = "span";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String STRIKE = "strike";
	    /** name of a tag */
	    public const String STRONG = "strong";
	    /** name of a tag */
	    public const String SUB = "sub";
	    /** name of a tag */
	    public const String SUP = "sup";
	    /** name of a tag */
	    public const String TABLE = "table";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String TD = "td";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String TH = "th";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String TR = "tr";
	    /** name of a tag */
	    public const String U = "u";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const String UL = "ul";

	    // attributes (some are not real HTML attributes!)

	    /** name of an attribute */
	    public const String ALIGN = "align";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const String BGCOLOR = "bgcolor";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const String BORDER = "border";
	    /** name of an attribute */
	    public const String CELLPADDING = "cellpadding";
	    /** name of an attribute */
	    public const String COLSPAN = "colspan";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const String EXTRAPARASPACE = "extraparaspace";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const String ENCODING = "encoding";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const String FACE = "face";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const String HEIGHT = "height";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const String HREF = "href";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const String HYPHENATION = "hyphenation";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const String IMAGEPATH = "image_path";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const String INDENT = "indent";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const String LEADING = "leading";
	    /** name of an attribute */
	    public const String ROWSPAN = "rowspan";
	    /** name of an attribute */
	    public const String SIZE = "size";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const String SRC = "src";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const String VALIGN = "valign";
	    /** name of an attribute */
	    public const String WIDTH = "width";
    	
	    // attribute values

	    /** the possible value of an alignment attribute */
	    public const String ALIGN_LEFT = "left";
	    /** the possible value of an alignment attribute */
	    public const String ALIGN_CENTER = "center";
	    /** the possible value of an alignment attribute */
	    public const String ALIGN_RIGHT = "right";
	    /** 
	     * The possible value of an alignment attribute.
	     * @since 5.0.6
	     */
	    public const String ALIGN_JUSTIFY = "justify";
	    /** 
	     * The possible value of an alignment attribute.
	     * @since 5.0.6
	     */
        public const String ALIGN_JUSTIFIED_ALL = "JustifyAll";
	    /** the possible value of an alignment attribute */
	    public const String ALIGN_TOP = "top";
	    /** the possible value of an alignment attribute */
	    public const String ALIGN_MIDDLE = "middle";
	    /** the possible value of an alignment attribute */
	    public const String ALIGN_BOTTOM = "bottom";
	    /** the possible value of an alignment attribute */
	    public const String ALIGN_BASELINE = "baseline";
    	
	    // CSS
    	
	    /** This is used for inline css style information */
	    public const String STYLE = "style";
	    /**
	     * Attribute for specifying externally defined CSS class.
	     * @since 5.0.6
	     */
	    public const String CLASS = "class";
	    /** the CSS tag for text color */
	    public const String COLOR = "color";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const String FONTFAMILY = "font-family";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const String FONTSIZE = "font-size";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const String FONTSTYLE = "font-style";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const String FONTWEIGHT = "font-weight";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const String LINEHEIGHT = "line-height";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const String PADDINGLEFT = "padding-left";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const String TEXTALIGN = "text-align";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const String TEXTDECORATION = "text-decoration";
	    /** the CSS tag for text decorations */
	    public const String VERTICALALIGN = "vertical-align";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const String BOLD = "bold";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const String ITALIC = "italic";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const String LINETHROUGH = "line-through";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const String NORMAL = "normal";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const String OBLIQUE = "oblique";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const String UNDERLINE = "underline";

	    /**
	     * A possible attribute.
	     * @since 5.0.6
	     */
	    public const String AFTER = "after";
	    /**
	     * A possible attribute.
	     * @since 5.0.6
	     */
	    public const String BEFORE = "before";
    }
}
