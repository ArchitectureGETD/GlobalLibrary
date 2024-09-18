using Org.BouncyCastle.X509;

namespace iTextSharp.GE.text.pdf {

    public class PdfPublicKeyRecipient {

        private X509Certificate certificate = null;
    
        private int permission = 0;
  
        protected byte[] cms = null;
            
        public PdfPublicKeyRecipient(X509Certificate certificate, int permission) {
            this.certificate = certificate;
            this.permission = permission;
        }

        virtual public X509Certificate Certificate {
            get {
                return certificate;
            }
        }

        virtual public int Permission {
            get {
                return permission;
            }
        }

        virtual protected internal byte[] Cms {
            set {
                cms = value;
            }
            get {
                return cms;
            }
        }
    }
}
