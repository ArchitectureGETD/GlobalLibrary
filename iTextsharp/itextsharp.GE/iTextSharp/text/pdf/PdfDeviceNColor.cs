using System;
using System.Globalization;
using System.util;
using iTextSharp.GE.text.error_messages;

namespace iTextSharp.GE.text.pdf {
    public class PdfDeviceNColor : ICachedColorSpace, IPdfSpecialColorSpace {

        PdfSpotColor[] spotColors;
        ColorDetails[] colorantsDetails;

        public PdfDeviceNColor(PdfSpotColor[] spotColors) {
            this.spotColors = spotColors;
        }

        public virtual int NumberOfColorants {
            get { return spotColors.Length; }
        }

        public virtual PdfSpotColor[] SpotColors {
            get { return spotColors; }
        }

        public virtual ColorDetails[] GetColorantDetails(PdfWriter writer) {
            if (colorantsDetails == null) {
                colorantsDetails = new ColorDetails[spotColors.Length];
                int i = 0;
                foreach (PdfSpotColor spotColorant in spotColors) {
                    colorantsDetails[i] = writer.AddSimple(spotColorant);
                    i++;
                }
            }
            return colorantsDetails;
        }

        public virtual PdfObject GetPdfObject(PdfWriter writer) {
            PdfArray array = new PdfArray(PdfName.DEVICEN);

            PdfArray colorants = new PdfArray();
            float[] colorantsRanges = new float[spotColors.Length*2];
            PdfDictionary colorantsDict = new PdfDictionary();
            String psFunFooter = "";

            int numberOfColorants = spotColors.Length;
            float[,] CMYK = new float[4, numberOfColorants];
            int i = 0;
            for (; i < numberOfColorants; i++) {
                PdfSpotColor spotColorant = spotColors[i];
                colorantsRanges[2*i] = 0;
                colorantsRanges[2*i + 1] = 1;
                colorants.Add(spotColorant.Name);
                if (colorantsDict.Get(spotColorant.Name) != null)
                    throw new Exception(MessageLocalization.GetComposedMessage("devicen.component.names.shall.be.different"));
                if (colorantsDetails != null)
                    colorantsDict.Put(spotColorant.Name, colorantsDetails[i].IndirectReference);
                else
                    colorantsDict.Put(spotColorant.Name, spotColorant.GetPdfObject(writer));
                BaseColor color = spotColorant.AlternativeCS;
                if (color is ExtendedColor) {
                    int type = ((ExtendedColor) color).Type;
                    switch (type) {
                        case ExtendedColor.TYPE_GRAY:
                            CMYK[0, i] = 0;
                            CMYK[1, i] = 0;
                            CMYK[2, i] = 0;
                            CMYK[3, i] = 1 - ((GrayColor) color).Gray;
                            break;
                        case ExtendedColor.TYPE_CMYK:
                            CMYK[0, i] = ((CMYKColor) color).Cyan;
                            CMYK[1, i] = ((CMYKColor) color).Magenta;
                            CMYK[2, i] = ((CMYKColor) color).Yellow;
                            CMYK[3, i] = ((CMYKColor) color).Black;
                            break;
                        case ExtendedColor.TYPE_LAB:
                            CMYKColor cmyk = ((LabColor) color).ToCmyk();
                            CMYK[0, i] = cmyk.Cyan;
                            CMYK[1, i] = cmyk.Magenta;
                            CMYK[2, i] = cmyk.Yellow;
                            CMYK[3, i] = cmyk.Black;
                            break;
                        default:
                            throw new Exception(
                                MessageLocalization.GetComposedMessage(
                                    "only.rgb.gray.and.cmyk.are.supported.as.alternative.color.spaces"));
                    }
                } else {
                    float r = color.R;
                    float g = color.G;
                    float b = color.B;
                    float computedC = 0, computedM = 0, computedY = 0, computedK = 0;

                    // BLACK
                    if (r == 0 && g == 0 && b == 0) {
                        computedK = 1;
                    } else {
                        computedC = 1 - (r/255);
                        computedM = 1 - (g/255);
                        computedY = 1 - (b/255);

                        float minCMY = Math.Min(computedC,
                            Math.Min(computedM, computedY));
                        computedC = (computedC - minCMY)/(1 - minCMY);
                        computedM = (computedM - minCMY)/(1 - minCMY);
                        computedY = (computedY - minCMY)/(1 - minCMY);
                        computedK = minCMY;
                    }
                    CMYK[0, i] = computedC;
                    CMYK[1, i] = computedM;
                    CMYK[2, i] = computedY;
                    CMYK[3, i] = computedK;
                }
                psFunFooter += "pop ";
            }
            array.Add(colorants);

            String psFunHeader = String.Format(NumberFormatInfo.InvariantInfo, "1.000000 {0} 1 roll ", numberOfColorants + 1);
            array.Add(PdfName.DEVICECMYK);
            psFunHeader = psFunHeader + psFunHeader + psFunHeader + psFunHeader;
            String psFun = "";
            i = numberOfColorants + 4;
            for (; i > numberOfColorants; i--) {
                psFun += String.Format(NumberFormatInfo.InvariantInfo, "{0} -1 roll ", i);
                for (int j = numberOfColorants; j > 0; j--) {
                    psFun += String.Format(NumberFormatInfo.InvariantInfo, "{0} index {1} mul 1.000000 cvr exch sub mul ", j,
                        CMYK[numberOfColorants + 4 - i, numberOfColorants - j]);
                }
                psFun += String.Format(NumberFormatInfo.InvariantInfo, "1.000000 cvr exch sub {0} 1 roll ", i);
            }

            PdfFunction func = PdfFunction.Type4(writer, colorantsRanges, new float[] {0, 1, 0, 1, 0, 1, 0, 1},
                "{ " + psFunHeader + psFun + psFunFooter + "}");
            array.Add(func.Reference);

            PdfDictionary attr = new PdfDictionary();
            attr.Put(PdfName.SUBTYPE, PdfName.NCHANNEL);
            attr.Put(PdfName.COLORANTS, colorantsDict);
            array.Add(attr);

            return array;
        }

        public override bool Equals(Object o) {
            if (this == o) return true;
            if (!(o is PdfDeviceNColor)) return false;

            PdfDeviceNColor that = (PdfDeviceNColor) o;

            if (!Util.ArraysAreEqual(spotColors, that.spotColors)) return false;

            return true;
        }

        public override int GetHashCode() {
            return Util.GetArrayHashCode(spotColors);
        }
    }
}
