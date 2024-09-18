using System;
using System.Text;
using System.IO;

namespace iTextSharp.GE.text.pdf {

    public class PRIndirectReference : PdfIndirectReference {
    
        protected PdfReader reader;
        // membervariables
    
        // constructors
    
        /**
         * Constructs a <CODE>PdfIndirectReference</CODE>.
         *
         * @param        reader            a <CODE>PdfReader</CODE>
         * @param        number            the object number.
         * @param        generation        the generation number.
         */
    
        public PRIndirectReference(PdfReader reader, int number, int generation) {
            type = INDIRECT;
            this.number = number;
            this.generation = generation;
            this.reader = reader;
        }
    
        /**
         * Constructs a <CODE>PdfIndirectReference</CODE>.
         *
         * @param        reader            a <CODE>PdfReader</CODE>
         * @param        number            the object number.
         */
    
        public PRIndirectReference(PdfReader reader, int number) : this(reader, number, 0) {}
    
        // methods
    
        public override void ToPdf(PdfWriter writer, Stream os) {
            if (writer != null) {
                int n = writer.GetNewObjectNumber(reader, number, generation);
                byte[] b = PdfEncodings.ConvertToBytes(new StringBuilder().Append(n).Append(" ").Append(reader.Appendable ? generation : 0).Append(" R").ToString(), null);
                os.Write(b, 0, b.Length);
            } else {
                base.ToPdf(null, os);
            }
        }

        virtual public PdfReader Reader {
            get {
                return reader;
            }
        }
        
        virtual public void SetNumber(int number, int generation) {
            this.number = number;
            this.generation = generation;
        }
    }
}
