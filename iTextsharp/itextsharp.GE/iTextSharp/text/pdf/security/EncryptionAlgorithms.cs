using System;
using System.Collections.Generic;
namespace iTextSharp.GE.text.pdf.security {

    /**
     * Class that contains a map with the different encryption algorithms.
     */
    public static class EncryptionAlgorithms {

        /** Maps IDs of encryption algorithms with its human-readable name. */
        private static readonly Dictionary<String, String> algorithmNames = new Dictionary<String, String>();

        static EncryptionAlgorithms() {
            algorithmNames["1.2.840.113549.1.1.1"] = "RSA";
            algorithmNames["1.2.840.10040.4.1"] = "DSA";
            algorithmNames["1.2.840.113549.1.1.2"] = "RSA";
            algorithmNames["1.2.840.113549.1.1.4"] = "RSA";
            algorithmNames["1.2.840.113549.1.1.5"] = "RSA";
            algorithmNames["1.2.840.113549.1.1.14"] = "RSA";
            algorithmNames["1.2.840.113549.1.1.11"] = "RSA";
            algorithmNames["1.2.840.113549.1.1.12"] = "RSA";
            algorithmNames["1.2.840.113549.1.1.13"] = "RSA";
            algorithmNames["1.2.840.10040.4.3"] = "DSA";
            algorithmNames["2.16.840.1.101.3.4.3.1"] = "DSA";
            algorithmNames["2.16.840.1.101.3.4.3.2"] = "DSA";
            algorithmNames["1.3.14.3.2.29"] = "RSA";
            algorithmNames["1.3.36.3.3.1.2"] = "RSA";
            algorithmNames["1.3.36.3.3.1.3"] = "RSA";
            algorithmNames["1.3.36.3.3.1.4"] = "RSA";
            algorithmNames["1.2.643.2.2.19"] = "ECGOST3410";
        }

        /**
        * Gets the algorithm name for a certain id.
        * @param oid    an id (for instance "1.2.840.113549.1.1.1")
        * @return   an algorithm name (for instance "RSA")
        * @since    2.1.6
        */
        public static String GetAlgorithm(String oid) {
            String ret;
            if (algorithmNames.TryGetValue(oid, out ret))
                return ret;
            else
                return oid;
        }
    }
}
