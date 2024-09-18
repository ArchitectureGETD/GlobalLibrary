using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.GE.text.error_messages;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.fonts.cmaps {

    /**
     *
     * @author psoares
     */
    public class CMapParserEx {
        
        private static readonly PdfName CMAPNAME = new PdfName("CMapName");
        private const String DEF = "def";
        private const String ENDCIDRANGE = "endcidrange";
        private const String ENDCIDCHAR = "endcidchar";
        private const String ENDBFRANGE = "endbfrange";
        private const String ENDBFCHAR = "endbfchar";
        private const String USECMAP = "usecmap";
        private const int MAXLEVEL = 10;
        
        public static void ParseCid(String cmapName, AbstractCMap cmap, ICidLocation location) {
            ParseCid(cmapName, cmap, location, 0);
        }
        
        private static void ParseCid(String cmapName, AbstractCMap cmap, ICidLocation location, int level) {
            if (level >= MAXLEVEL)
                return;
            PRTokeniser inp = location.GetLocation(cmapName);
            try {
                List<PdfObject> list = new List<PdfObject>();
                PdfContentParser cp = new PdfContentParser(inp);
                int maxExc = 50;
                while (true) {
                    try {
                        cp.Parse(list);
                    }
                    catch {
                        if (--maxExc < 0)
                            break;
                        continue;
                    }
                    if (list.Count == 0)
                        break;
                    String last = list[list.Count - 1].ToString();
                    if (level == 0 && list.Count == 3 && last.Equals(DEF)) {
                        PdfObject key = list[0];
                        if (PdfName.REGISTRY.Equals(key))
                            cmap.Registry = list[1].ToString();
                        else if (PdfName.ORDERING.Equals(key))
                            cmap.Ordering = list[1].ToString();
                        else if (CMAPNAME.Equals(key))
                            cmap.Name = list[1].ToString();
                        else if (PdfName.SUPPLEMENT.Equals(key)) {
                            try {
                                cmap.Supplement = ((PdfNumber)list[1]).IntValue;
                            }
                            catch {}
                        }
                    }
                    else if ((last.Equals(ENDCIDCHAR) || last.Equals(ENDBFCHAR)) && list.Count >= 3) {
                        int lmax = list.Count - 2;
                        for (int k = 0; k < lmax; k += 2) {
                            if (list[k] is PdfString) {
                                cmap.AddChar((PdfString)list[k], list[k + 1]);
                            }
                        }
                    }
                    else if ((last.Equals(ENDCIDRANGE) || last.Equals(ENDBFRANGE)) && list.Count >= 4) {
                        int lmax = list.Count - 3;
                        for (int k = 0; k < lmax; k += 3) {
                            if (list[k] is PdfString && list[k + 1] is PdfString) {
                                cmap.AddRange((PdfString)list[k], (PdfString)list[k + 1], list[k + 2]);
                            }
                        }
                    }
                    else if (last.Equals(USECMAP) && list.Count == 2 && list[0] is PdfName) {
                        ParseCid(PdfName.DecodeName(list[0].ToString()), cmap, location, level + 1);
                    }
                }
            }
            finally {
                inp.Close();
            }
        }
    }
}
