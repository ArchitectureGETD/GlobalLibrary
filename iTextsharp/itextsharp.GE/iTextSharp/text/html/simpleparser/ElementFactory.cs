using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.util;
using iTextSharp.GE.text;
using iTextSharp.GE.text.html;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.pdf.draw;
namespace iTextSharp.GE.text.html.simpleparser {
    /**
     * Factory that produces iText Element objects,
     * based on tags and their properties.
     * @author blowagie
     * @author psoares
     * @since 5.0.6 (renamed)
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public class ElementFactory {

        /**
         * The font provider that will be used to fetch fonts.
         * @since   iText 5.0   This used to be a FontFactoryImp
         */
        private IFontProvider provider = FontFactory.FontImp;

        /**
         * Creates a new instance of FactoryProperties.
         */
        public ElementFactory() {
        }

        /**
         * Setter for the font provider
         * @param provider
         * @since   5.0.6 renamed from setFontImp
         */
        virtual public IFontProvider FontProvider {
            set {
                provider = value;
            }
            get {
                return provider;
            }
        }
        
        /**
         * Creates a Font object based on a chain of properties.
         * @param   chain   chain of properties
         * @return  an iText Font object
         */
        virtual public Font GetFont(ChainedProperties chain) {
            
            // [1] font name
            
            String face = chain[HtmlTags.FACE];
            // try again, under the CSS key.  
            //ISSUE: If both are present, we always go with face, even if font-family was  
            //  defined more recently in our ChainedProperties.  One solution would go like this: 
            //    Map all our supported style attributes to the 'normal' tag name, so we could   
            //    look everything up under that one tag, retrieving the most current value.
            if (face == null || face.Trim().Length == 0) {
                face = chain[HtmlTags.FONTFAMILY];
            }
            // if the font consists of a comma separated list,
            // take the first font that is registered
            if (face != null) {
                StringTokenizer tok = new StringTokenizer(face, ",");
                while (tok.HasMoreTokens()) {
                    face = tok.NextToken().Trim();
                    if (face.StartsWith("\""))
                        face = face.Substring(1);
                    if (face.EndsWith("\""))
                        face = face.Substring(0, face.Length - 1);
                    if (provider.IsRegistered(face))
                        break;
                }
            }
            
            // [2] encoding
            String encoding = chain[HtmlTags.ENCODING];
            if (encoding == null)
                encoding = BaseFont.WINANSI;
            
            // [3] embedded
            
            // [4] font size
            String value = chain[HtmlTags.SIZE];
            float size = 12;
            if (value != null)
                size = float.Parse(value, CultureInfo.InvariantCulture);
            
            // [5] font style
            int style = 0;
            
            // text-decoration
            String decoration = chain[HtmlTags.TEXTDECORATION];
            if (decoration != null && decoration.Trim().Length != 0) {
              if (HtmlTags.UNDERLINE.Equals(decoration)) {
                style |= Font.UNDERLINE;
              } else if (HtmlTags.LINETHROUGH.Equals(decoration)) {
                style |= Font.STRIKETHRU;
              }
            }
            // italic
            if (chain.HasProperty(HtmlTags.I))
                style |= Font.ITALIC;
            // bold
            if (chain.HasProperty(HtmlTags.B))
                style |= Font.BOLD;
            // underline
            if (chain.HasProperty(HtmlTags.U))
                style |= Font.UNDERLINE;
            // strikethru
            if (chain.HasProperty(HtmlTags.S))
                style |= Font.STRIKETHRU;
            
            // [6] Color
            BaseColor color = HtmlUtilities.DecodeColor(chain[HtmlTags.COLOR]);
            
            // Get the font object from the provider
            return provider.GetFont(face, encoding, true, size, style, color);
        }
        
        
        /**
         * Creates an iText Chunk
         * @param content the content of the Chunk
         * @param chain the hierarchy chain
         * @return a Chunk
         */
        virtual public Chunk CreateChunk(String content, ChainedProperties chain) {
            Font font = GetFont(chain);
            Chunk ck = new Chunk(content, font);
            if (chain.HasProperty(HtmlTags.SUB))
                ck.SetTextRise(-font.Size / 2);
            else if (chain.HasProperty(HtmlTags.SUP))
                ck.SetTextRise(font.Size / 2);
            ck.SetHyphenation(GetHyphenation(chain));
            return ck;
        }

        /**
         * Creates an iText Paragraph object using the properties
         * of the different tags and properties in the hierarchy chain.
         * @param   chain   the hierarchy chain
         * @return  a Paragraph without any content
         */
        virtual public Paragraph CreateParagraph(ChainedProperties chain) {
            Paragraph paragraph = new Paragraph();
            UpdateElement(paragraph, chain);
            return paragraph;
        }

        /**
         * Creates an iText Paragraph object using the properties
         * of the different tags and properties in the hierarchy chain.
         * @param   chain   the hierarchy chain
         * @return  a ListItem without any content
         */
        virtual public ListItem CreateListItem(ChainedProperties chain) {
            ListItem item = new ListItem();
            UpdateElement(item, chain);
            return item;
        }
        
        /**
         * Method that does the actual Element creating for
         * the createParagraph and createListItem method.
         * @param paragraph
         * @param chain
         */
        virtual protected void UpdateElement(Paragraph paragraph, ChainedProperties chain) {
            // Alignment
            String value = chain[HtmlTags.ALIGN];
            paragraph.Alignment = HtmlUtilities.AlignmentValue(value);
            // hyphenation
            paragraph.Hyphenation = GetHyphenation(chain);
            // leading
            SetParagraphLeading(paragraph, chain[HtmlTags.LEADING]);
            // spacing before
            value = chain[HtmlTags.AFTER];
            if (value != null) {
                try {
                    paragraph.SpacingBefore = float.Parse(value, CultureInfo.InvariantCulture);
                } catch {
                }
            }
            // spacing after
            value = chain[HtmlTags.AFTER];
            if (value != null) {
                try {
                    paragraph.SpacingAfter = float.Parse(value, CultureInfo.InvariantCulture);
                } catch {
                }
            }
            // extra paragraph space
            value = chain[HtmlTags.EXTRAPARASPACE];
            if (value != null) {
                try {
                    paragraph.ExtraParagraphSpace = float.Parse(value, CultureInfo.InvariantCulture);
                } catch {
                }
            }
            // indentation
            value = chain[HtmlTags.INDENT];
            if (value != null) {
                try {
                    paragraph.IndentationLeft = float.Parse(value, CultureInfo.InvariantCulture);
                } catch {
                }
            }
        }

        /**
         * Sets the leading of a Paragraph object.
         * @param   paragraph   the Paragraph for which we set the leading
         * @param   leading     the String value of the leading
         */
        protected static void SetParagraphLeading(Paragraph paragraph, String leading) {
            // default leading
            if (leading == null) {
                paragraph.SetLeading(0, 1.5f);
                return;
            }
            try {
                StringTokenizer tk = new StringTokenizer(leading, " ,");
                // absolute leading
                String v = tk.NextToken();
                float v1 = float.Parse(v, CultureInfo.InvariantCulture);
                if (!tk.HasMoreTokens()) {
                    paragraph.SetLeading(v1, 0);
                    return;
                }
                // relative leading
                v = tk.NextToken();
                float v2 = float.Parse(v, CultureInfo.InvariantCulture);
                paragraph.SetLeading(v1, v2);
            } catch {
                // default leading
                paragraph.SetLeading(0, 1.5f);
            }
        }


        /**
         * Gets a HyphenationEvent based on the hyphenation entry in
         * the hierarchy chain.
         * @param   chain   the hierarchy chain
         * @return  a HyphenationEvent
         * @since   2.1.2
         */
        virtual public IHyphenationEvent GetHyphenation(ChainedProperties chain) {
            String value = chain[HtmlTags.HYPHENATION];
            // no hyphenation defined
            if (value == null || value.Length == 0) {
                return null;
            }
            // language code only
            int pos = value.IndexOf('_');
            if (pos == -1) {
                return new HyphenationAuto(value, null, 2, 2);
            }
            // language and country code
            String lang = value.Substring(0, pos);
            String country = value.Substring(pos + 1);
            // no leftMin or rightMin
            pos = country.IndexOf(',');
            if (pos == -1) {
                return new HyphenationAuto(lang, country, 2, 2);
            }
            // leftMin and rightMin value
            int leftMin;
            int rightMin = 2;
            value = country.Substring(pos + 1);
            country = country.Substring(0, pos);
            pos = value.IndexOf(',');
            if (pos == -1) {
                leftMin = int.Parse(value);
            } else {
                leftMin = int.Parse(value.Substring(0, pos));
                rightMin = int.Parse(value.Substring(pos + 1));
            }
            return new HyphenationAuto(lang, country, leftMin, rightMin);
        }
        
        /**
         * Creates a LineSeparator.
         * @since 5.0.6
         */
        virtual public LineSeparator CreateLineSeparator(IDictionary<String, String> attrs, float offset) {
            // line thickness
            float lineWidth = 1;
            String size;
            attrs.TryGetValue(HtmlTags.SIZE, out size);
            if (size != null) {
                float tmpSize = HtmlUtilities.ParseLength(size, HtmlUtilities.DEFAULT_FONT_SIZE);
                if (tmpSize > 0)
                    lineWidth = tmpSize;
            }
            // width percentage
            String width;
            attrs.TryGetValue(HtmlTags.WIDTH, out width);
            float percentage = 100;
            if (width != null) {
                float tmpWidth = HtmlUtilities.ParseLength(width, HtmlUtilities.DEFAULT_FONT_SIZE);
                if (tmpWidth > 0) percentage = tmpWidth;
                if (!width.EndsWith("%"))
                    percentage = 100; // Treat a pixel width as 100% for now.
            }
            // line color
            BaseColor lineColor = null;
            // alignment
            string aligns;
            attrs.TryGetValue(HtmlTags.ALIGN, out aligns);
            int align = HtmlUtilities.AlignmentValue(aligns);
            return new LineSeparator(lineWidth, percentage, lineColor, align, offset);
        }
        
        virtual public Image CreateImage(
                String src,
                IDictionary<String, String> attrs,
                ChainedProperties chain,
                IDocListener document,
                IImageProvider img_provider,
                Dictionary<String, Image> img_store,
                String img_baseurl) {
            Image img = null;
            // getting the image using an image provider
            if (img_provider != null)
                img = img_provider.GetImage(src, attrs, chain, document);
            // getting the image from an image store
            if (img == null && img_store != null) {
                Image tim;
                img_store.TryGetValue(src, out tim);
                if (tim != null)
                    img = Image.GetInstance(tim);
            }
            if (img != null)
                return img;
            // introducing a base url
            // relative src references only
            if (!src.StartsWith("http") && img_baseurl != null) {
                src = img_baseurl + src;
            }
            else if (img == null && !src.StartsWith("http")) {
                String path = chain[HtmlTags.IMAGEPATH];
                if (path == null)
                    path = "";
                src = Path.Combine(path, src);
            }
            img = Image.GetInstance(src);
            if (img == null)
                return null;
            
            float actualFontSize = HtmlUtilities.ParseLength(
                chain[HtmlTags.SIZE],
                HtmlUtilities.DEFAULT_FONT_SIZE);
            if (actualFontSize <= 0f)
                actualFontSize = HtmlUtilities.DEFAULT_FONT_SIZE;
            String width;
            attrs.TryGetValue(HtmlTags.WIDTH, out width);
            float widthInPoints = HtmlUtilities.ParseLength(width, actualFontSize);
            String height;
            attrs.TryGetValue(HtmlTags.HEIGHT, out height);
            float heightInPoints = HtmlUtilities.ParseLength(height, actualFontSize);
            if (widthInPoints > 0 && heightInPoints > 0) {
                img.ScaleAbsolute(widthInPoints, heightInPoints);
            } else if (widthInPoints > 0) {
                heightInPoints = img.Height * widthInPoints
                        / img.Width;
                img.ScaleAbsolute(widthInPoints, heightInPoints);
            } else if (heightInPoints > 0) {
                widthInPoints = img.Width * heightInPoints
                        / img.Height;
                img.ScaleAbsolute(widthInPoints, heightInPoints);
            }
            
            String before = chain[HtmlTags.BEFORE];
            if (before != null)
                img.SpacingBefore = float.Parse(before, CultureInfo.InvariantCulture);
            String after = chain[HtmlTags.AFTER];
            if (after != null)
                img.SpacingAfter = float.Parse(after, CultureInfo.InvariantCulture);
            img.WidthPercentage = 0;
            return img;
        }
        
        virtual public List CreateList(String tag, ChainedProperties chain) {
            List list;
            if (Util.EqualsIgnoreCase(HtmlTags.UL, tag)) {
                list = new List(List.UNORDERED);
                list.SetListSymbol("\u2022 ");
            }
            else {
                list = new List(List.ORDERED);
            }
            try{
                list.IndentationLeft = float.Parse(chain[HtmlTags.INDENT], CultureInfo.InvariantCulture);
            }catch  {
                list.Autoindent = true;
            }
            return list;
        }
    }
}
