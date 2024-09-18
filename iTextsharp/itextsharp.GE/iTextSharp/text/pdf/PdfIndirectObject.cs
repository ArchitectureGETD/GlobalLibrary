using System.IO;
using System.Text;

namespace iTextSharp.GE.text.pdf
{
    /**
     * <CODE>PdfIndirectObject</CODE> is the Pdf indirect object.
     * <P>
     * An <I>indirect object</I> is an object that has been labeled so that it can be referenced by
     * other objects. Any type of <CODE>PdfObject</CODE> may be labeled as an indirect object.<BR>
     * An indirect object consists of an object identifier, a direct object, and the <B>endobj</B>
     * keyword. The <I>object identifier</I> consists of an integer <I>object number</I>, an integer
     * <I>generation number</I>, and the <B>obj</B> keyword.<BR>
     * This object is described in the 'Portable Document Format Reference Manual version 1.3'
     * section 4.10 (page 53).
     *
     * @see        PdfObject
     * @see        PdfIndirectReference
     */
    public class PdfIndirectObject
    {

        // membervariables

        /** The object number */
        protected int number;

        /** the generation number */
        protected int generation = 0;

        internal static byte[] STARTOBJ = DocWriter.GetISOBytes(" obj\n");
        internal static byte[] ENDOBJ = DocWriter.GetISOBytes("\nendobj\n");
        internal static int SIZEOBJ = STARTOBJ.Length + ENDOBJ.Length;
        protected internal PdfObject objecti;
        protected internal PdfWriter writer;

        // constructors

        /**
         * Constructs a <CODE>PdfIndirectObject</CODE>.
         *
         * @param        number            the objecti number
         * @param        objecti            the direct objecti
         */
        public PdfIndirectObject(int number, PdfObject objecti, PdfWriter writer)
            : this(number, 0, objecti, writer)
        {
        }

        public PdfIndirectObject(PdfIndirectReference refi, PdfObject objecti, PdfWriter writer)
            : this(refi.Number, refi.Generation, objecti, writer)
        {
        }

        /**
         * Constructs a <CODE>PdfIndirectObject</CODE>.
         *
         * @param        number            the objecti number
         * @param        generation        the generation number
         * @param        objecti            the direct objecti
         */
        public PdfIndirectObject(int number, int generation, PdfObject objecti, PdfWriter writer)
        {
            this.writer = writer;
            this.number = number;
            this.generation = generation;
            this.objecti = objecti;
            PdfEncryption crypto = null;
            if (writer != null)
                crypto = writer.Encryption;
            if (crypto != null)
                crypto.SetHashKey(number, generation);
        }

        virtual public int Number
        {
            get { return number; }
        }

        virtual public int Generation
        {
            get { return generation; }
        }


        // methods

        /**
         * Returns a <CODE>PdfIndirectReference</CODE> to this <CODE>PdfIndirectObject</CODE>.
         *
         * @return        a <CODE>PdfIndirectReference</CODE>
         */
        public virtual PdfIndirectReference IndirectReference
        {
            get { return new PdfIndirectReference(objecti.Type, number, generation); }
        }

        /**
         * Writes eficiently to a stream
         *
         * @param os the stream to write to
         * @throws IOException on write error
         */
        virtual public void WriteTo(Stream os)
        {
            byte[] tmp = DocWriter.GetISOBytes(number.ToString());
            os.Write(tmp, 0, tmp.Length);
            os.WriteByte((byte)' ');
            tmp = DocWriter.GetISOBytes(generation.ToString());
            os.Write(tmp, 0, tmp.Length);
            os.Write(STARTOBJ, 0, STARTOBJ.Length);
            objecti.ToPdf(writer, os);
            os.Write(ENDOBJ, 0, ENDOBJ.Length);
        }

        public override string ToString() {
            return new StringBuilder().Append(number).Append(' ').Append(generation).Append(" R: ").Append(objecti != null ? objecti.ToString(): "null").ToString();
        }
    }
}
