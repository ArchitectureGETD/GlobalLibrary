using System;
using System.Text;

namespace iTextSharp.GE.text.pdf.hyphenation {


    public class Hyphen {
        public String preBreak;
        public String noBreak;
        public String postBreak;

        internal Hyphen(String pre, String no, String post) {
            preBreak = pre;
            noBreak = no;
            postBreak = post;
        }

        internal Hyphen(String pre) {
            preBreak = pre;
            noBreak = null;
            postBreak = null;
        }

        public override String ToString() {
            if (noBreak == null 
                    && postBreak == null 
                    && preBreak != null
                    && preBreak.Equals("-")) {
                return "-";
                    }
            StringBuilder res = new StringBuilder("{");
            res.Append(preBreak);
            res.Append("}{");
            res.Append(postBreak);
            res.Append("}{");
            res.Append(noBreak);
            res.Append('}');
            return res.ToString();
        }
    }
}
