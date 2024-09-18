using System.Collections.Generic;

namespace iTextSharp.GE.text {
    /// <summary>
    /// Interface for a text element.
    /// </summary>
    /// <seealso cref="T:iTextSharp.GE.text.Anchor"/>
    /// <seealso cref="T:iTextSharp.GE.text.Cell"/>
    /// <seealso cref="T:iTextSharp.GE.text.Chapter"/>
    /// <seealso cref="T:iTextSharp.GE.text.Chunk"/>
    /// <seealso cref="T:iTextSharp.GE.text.Gif"/>
    /// <seealso cref="T:iTextSharp.GE.text.Graphic"/>
    /// <seealso cref="T:iTextSharp.GE.text.Header"/>
    /// <seealso cref="T:iTextSharp.GE.text.Image"/>
    /// <seealso cref="T:iTextSharp.GE.text.Jpeg"/>
    /// <seealso cref="T:iTextSharp.GE.text.List"/>
    /// <seealso cref="T:iTextSharp.GE.text.ListItem"/>
    /// <seealso cref="T:iTextSharp.GE.text.Meta"/>
    /// <seealso cref="T:iTextSharp.GE.text.Paragraph"/>
    /// <seealso cref="T:iTextSharp.GE.text.Phrase"/>
    /// <seealso cref="T:iTextSharp.GE.text.Rectangle"/>
    /// <seealso cref="T:iTextSharp.GE.text.Row"/>
    /// <seealso cref="T:iTextSharp.GE.text.Section"/>
    /// <seealso cref="T:iTextSharp.GE.text.Table"/>
    public interface IElement {

        // methods
    
        /// <summary>
        /// Processes the element by adding it (or the different parts) to an
        /// IElementListener.
        /// </summary>
        /// <param name="listener">an IElementListener</param>
        /// <returns>true if the element was processed successfully</returns>
        bool Process(IElementListener listener);
    
        /// <summary>
        /// Gets the type of the text element.
        /// </summary>
        /// <value>a type</value>
        int Type {
            get;
        }
    
        /**
        * Checks if this element is a content object.
        * If not, it's a metadata object.
        * @since    iText 2.0.8
        * @return   true if this is a 'content' element; false if this is a 'medadata' element
        */
        
        bool IsContent();
        
        /**
        * Checks if this element is nestable.
        * @since    iText 2.0.8
        * @return   true if this element can be nested inside other elements.
        */
        
        bool IsNestable();
        
        /// <summary>
        /// Gets all the chunks in this element.
        /// </summary>
        /// <value>an ArrayList</value>
        IList<Chunk> Chunks {
            get;
        }
    
        /// <summary>
        /// Gets the content of the text element.
        /// </summary>
        /// <returns>the content of the text element</returns>
        string ToString();
    }
}
