using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Org.BouncyCastle.Asn1;
using System.Text;
using Org.BouncyCastle.X509;
using iTextSharp.GE.text.error_messages;
namespace iTextSharp.GE.text.pdf.security {

//import java.io.ByteArrayInputStream;
//import java.io.IOException;
//import java.security.cert.X509Certificate;
//import java.util.ArrayList;
//import java.util.Enumeration;
//import java.util.HashMap;
//import java.util.List;
//import java.util.Map;

//import org.bouncycastle.asn1.ASN1InputStream;
//import org.bouncycastle.asn1.ASN1ObjectIdentifier;
//import org.bouncycastle.asn1.ASN1Primitive;
//import org.bouncycastle.asn1.ASN1Sequence;
//import org.bouncycastle.asn1.ASN1Set;
//import org.bouncycastle.asn1.ASN1String;
//import org.bouncycastle.asn1.ASN1TaggedObject;

//import com.itextpdf.text.ExceptionConverter;
//import com.itextpdf.text.error_messages.MessageLocalization;

    /**
     * Class containing static methods that allow you to get information from
     * an X509 Certificate: the issuer and the subject.
     */
    public static class CertificateInfo {

        // Inner classes
        
        /**
         * a class that holds an X509 name
         */
        public class X509Name {
            /**
            * country code - StringType(SIZE(2))
            */
            public static DerObjectIdentifier C = new DerObjectIdentifier("2.5.4.6");

            /**
            * organization - StringType(SIZE(1..64))
            */
            public static DerObjectIdentifier O = new DerObjectIdentifier("2.5.4.10");

            /**
            * organizational unit name - StringType(SIZE(1..64))
            */
            public static DerObjectIdentifier OU = new DerObjectIdentifier("2.5.4.11");

            /**
            * Title
            */
            public static DerObjectIdentifier T = new DerObjectIdentifier("2.5.4.12");

            /**
            * common name - StringType(SIZE(1..64))
            */
            public static DerObjectIdentifier CN = new DerObjectIdentifier("2.5.4.3");

            /**
            * device serial number name - StringType(SIZE(1..64))
            */
            public static DerObjectIdentifier SN = new DerObjectIdentifier("2.5.4.5");

            /**
            * locality name - StringType(SIZE(1..64))
            */
            public static DerObjectIdentifier L = new DerObjectIdentifier("2.5.4.7");

            /**
            * state, or province name - StringType(SIZE(1..64))
            */
            public static DerObjectIdentifier ST = new DerObjectIdentifier("2.5.4.8");

            /** Naming attribute of type X520name */
            public static DerObjectIdentifier SURNAME = new DerObjectIdentifier("2.5.4.4");
            /** Naming attribute of type X520name */
            public static DerObjectIdentifier GIVENNAME = new DerObjectIdentifier("2.5.4.42");
            /** Naming attribute of type X520name */
            public static DerObjectIdentifier INITIALS = new DerObjectIdentifier("2.5.4.43");
            /** Naming attribute of type X520name */
            public static DerObjectIdentifier GENERATION = new DerObjectIdentifier("2.5.4.44");
            /** Naming attribute of type X520name */
            public static DerObjectIdentifier UNIQUE_IDENTIFIER = new DerObjectIdentifier("2.5.4.45");

            /**
            * Email address (RSA PKCS#9 extension) - IA5String.
            * <p>Note: if you're trying to be ultra orthodox, don't use this! It shouldn't be in here.
            */
            public static DerObjectIdentifier EmailAddress = new DerObjectIdentifier("1.2.840.113549.1.9.1");

            /**
            * email address in Verisign certificates
            */
            public static DerObjectIdentifier E = EmailAddress;

            /** object identifier */
            public static DerObjectIdentifier DC = new DerObjectIdentifier("0.9.2342.19200300.100.1.25");

            /** LDAP User id. */
            public static DerObjectIdentifier UID = new DerObjectIdentifier("0.9.2342.19200300.100.1.1");

            /** A Hashtable with default symbols */
            public static readonly Dictionary<DerObjectIdentifier,string> DefaultSymbols = new Dictionary<DerObjectIdentifier,string>();
            
            static X509Name(){
                DefaultSymbols[C] = "C";
                DefaultSymbols[O] = "O";
                DefaultSymbols[T] = "T";
                DefaultSymbols[OU] = "OU";
                DefaultSymbols[CN] = "CN";
                DefaultSymbols[L] = "L";
                DefaultSymbols[ST] = "ST";
                DefaultSymbols[SN] = "SN";
                DefaultSymbols[EmailAddress] = "E";
                DefaultSymbols[DC] = "DC";
                DefaultSymbols[UID] = "UID";
                DefaultSymbols[SURNAME] = "SURNAME";
                DefaultSymbols[GIVENNAME] = "GIVENNAME";
                DefaultSymbols[INITIALS] = "INITIALS";
                DefaultSymbols[GENERATION] = "GENERATION";
            }
            /** A Hashtable with values */
            public Dictionary<string,List<string>> values = new Dictionary<string,List<string>>();

            /**
            * Constructs an X509 name
            * @param seq an Asn1 Sequence
            */
            public X509Name(Asn1Sequence seq) {
                IEnumerator e = seq.GetEnumerator();
                
                while (e.MoveNext()) {
                    Asn1Set sett = (Asn1Set)e.Current;
                    
                    for (int i = 0; i < sett.Count; i++) {
                        Asn1Sequence s = (Asn1Sequence)sett[i];
                        String id;
                        if (!(s[0] is DerObjectIdentifier) || !DefaultSymbols.TryGetValue((DerObjectIdentifier)s[0], out id))
                            continue;
                        List<string> vs;
                        if (!values.TryGetValue(id, out vs)) {
                            vs = new List<string>();
                            values[id] = vs;
                        }
                        vs.Add(((DerStringBase)s[1]).GetString());
                    }
                }
            }
            /**
            * Constructs an X509 name
            * @param dirName a directory name
            */
            public X509Name(String dirName) {
                X509NameTokenizer   nTok = new X509NameTokenizer(dirName);
                
                while (nTok.HasMoreTokens()) {
                    String  token = nTok.NextToken();
                    int index = token.IndexOf('=');
                    
                    if (index == -1) {
                        throw new ArgumentException(MessageLocalization.GetComposedMessage("badly.formated.directory.string"));
                    }
                    
                    String id = token.Substring(0, index).ToUpper(System.Globalization.CultureInfo.InvariantCulture);
                    String value = token.Substring(index + 1);
                    List<string> vs;
                    if (!values.TryGetValue(id, out vs)) {
                        vs = new List<string>();
                        values[id] = vs;
                    }
                    vs.Add(value);
                }                
            }
            
            virtual public String GetField(String name) {
                List<string> vs;
                if (values.TryGetValue(name, out vs))
                    return vs.Count == 0 ? null : vs[0];
                else
                    return null;
            }

            /**
            * gets a field array from the values Hashmap
            * @param name
            * @return an ArrayList
            */
            virtual public List<string> GetFieldArray(String name) {
                List<string> vs;
                if (values.TryGetValue(name, out vs))
                    return vs;
                else
                    return null;
            }
            
            /**
            * getter for values
            * @return a Hashtable with the fields of the X509 name
            */
            virtual public Dictionary<string,List<string>> GetFields() {
                return values;
            }
            
            /**
            * @see java.lang.Object#toString()
            */
            public override String ToString() {
                return values.ToString();
            }
        }

        /**
        * class for breaking up an X500 Name into it's component tokens, ala
        * java.util.StringTokenizer. We need this class as some of the
        * lightweight Java environment don't support classes like
        * StringTokenizer.
        */
        public class X509NameTokenizer {
            private String          oid;
            private int             index;
            private StringBuilder    buf = new StringBuilder();
            
            public X509NameTokenizer(
            String oid) {
                this.oid = oid;
                this.index = -1;
            }
            
            virtual public bool HasMoreTokens() {
                return (index != oid.Length);
            }
            
            virtual public String NextToken() {
                if (index == oid.Length) {
                    return null;
                }
                
                int     end = index + 1;
                bool quoted = false;
                bool escaped = false;
                
                buf.Length = 0;
                
                while (end != oid.Length) {
                    char    c = oid[end];
                    
                    if (c == '"') {
                        if (!escaped) {
                            quoted = !quoted;
                        }
                        else {
                            buf.Append(c);
                        }
                        escaped = false;
                    }
                    else {
                        if (escaped || quoted) {
                            buf.Append(c);
                            escaped = false;
                        }
                        else if (c == '\\') {
                            escaped = true;
                        }
                        else if (c == ',') {
                            break;
                        }
                        else {
                            buf.Append(c);
                        }
                    }
                    end++;
                }
                
                index = end;
                return buf.ToString().Trim();
            }
        }

        // Certificate issuer
        
        /**
        * Get the issuer fields from an X509 Certificate
        * @param cert an X509Certificate
        * @return an X509Name
        */
        public static X509Name GetIssuerFields(X509Certificate cert) {
            return new X509Name((Asn1Sequence)GetIssuer(cert.GetTbsCertificate()));
        }

        /**
        * Get the "issuer" from the TBSCertificate bytes that are passed in
        * @param enc a TBSCertificate in a byte array
        * @return a DERObject
        */
        public static Asn1Object GetIssuer(byte[] enc) {
            Asn1InputStream inp = new Asn1InputStream(new MemoryStream(enc));
            Asn1Sequence seq = (Asn1Sequence)inp.ReadObject();
            return (Asn1Object)seq[seq[0] is Asn1TaggedObject ? 3 : 2];
        }
        
        // Certificate Subject

        /**
        * Get the subject fields from an X509 Certificate
        * @param cert an X509Certificate
        * @return an X509Name
        */
        public static X509Name GetSubjectFields(X509Certificate cert) {
            if (cert != null)
                return new X509Name((Asn1Sequence)GetSubject(cert.GetTbsCertificate()));
            return null;
        }

        /**
        * Get the "subject" from the TBSCertificate bytes that are passed in
        * @param enc A TBSCertificate in a byte array
        * @return a DERObject
        */
        private static Asn1Object GetSubject(byte[] enc) {
            Asn1InputStream inp = new Asn1InputStream(new MemoryStream(enc));
            Asn1Sequence seq = (Asn1Sequence)inp.ReadObject();
            return (Asn1Object)seq[seq[0] is Asn1TaggedObject ? 5 : 4];
        }
    }
}
