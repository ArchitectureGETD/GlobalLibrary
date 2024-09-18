using iTextSharp.GE.text.pdf.languages;

namespace com.itextpdf.text.pdf.languages
{

/**
 * Implementation of the IndicLigaturizer for Devanagari.
 *
 * Warning: this is an incomplete and experimental implementation of Devanagari. This implementation should not be used in production.
 */

    public class DevanagariLigaturizer : IndicLigaturizer
    {

        // Devanagari characters
        public const char DEVA_MATRA_AA = '\u093E';
        public const char DEVA_MATRA_I = '\u093F';
        public const char DEVA_MATRA_E = '\u0947';
        public const char DEVA_MATRA_AI = '\u0948';
        public const char DEVA_MATRA_HLR = '\u0962';
        public const char DEVA_MATRA_HLRR = '\u0963';
        public const char DEVA_LETTER_A = '\u0905';
        public const char DEVA_LETTER_AU = '\u0914';
        public const char DEVA_LETTER_KA = '\u0915';
        public const char DEVA_LETTER_HA = '\u0939';
        public const char DEVA_HALANTA = '\u094D';

        /**
        * Constructor for the IndicLigaturizer for Devanagari.
        */

        public DevanagariLigaturizer() {
            langTable = new char[11];
            langTable[MATRA_AA] = DEVA_MATRA_AA;
            langTable[MATRA_I] = DEVA_MATRA_I;
            langTable[MATRA_E] = DEVA_MATRA_E;
            langTable[MATRA_AI] = DEVA_MATRA_AI;
            langTable[MATRA_HLR] = DEVA_MATRA_HLR;
            langTable[MATRA_HLRR] = DEVA_MATRA_HLRR;
            langTable[LETTER_A] = DEVA_LETTER_A;
            langTable[LETTER_AU] = DEVA_LETTER_AU;
            langTable[LETTER_KA] = DEVA_LETTER_KA;
            langTable[LETTER_HA] = DEVA_LETTER_HA;
            langTable[HALANTA] = DEVA_HALANTA;
        }
    }
}
