namespace iTextSharp.GE.text {
    /// <summary>
    /// Interface for a text element to which other objects can be added.
    /// </summary>
    /// <seealso cref="T:iTextSharp.GE.text.Phrase"/>
    /// <seealso cref="T:iTextSharp.GE.text.Paragraph"/>
    /// <seealso cref="T:iTextSharp.GE.text.Section"/>
    /// <seealso cref="T:iTextSharp.GE.text.ListItem"/>
    /// <seealso cref="T:iTextSharp.GE.text.Chapter"/>
    /// <seealso cref="T:iTextSharp.GE.text.Anchor"/>
    /// <seealso cref="T:iTextSharp.GE.text.Cell"/>
    public interface ITextElementArray : IElement {
        /// <summary>
        /// Adds an object to the TextElementArray.
        /// </summary>
        /// <param name="o">an object that has to be added</param>
        /// <returns>true if the addition succeeded; false otherwise</returns>
        bool Add(IElement o);
    }
}
