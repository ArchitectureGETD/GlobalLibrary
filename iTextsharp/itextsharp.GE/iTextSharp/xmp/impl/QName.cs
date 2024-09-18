
namespace iTextSharp.GE.xmp.impl {
    /// <summary>
    /// @since   09.11.2006
    /// </summary>
    public class QName {
        /// <summary>
        /// XML localname </summary>
        private readonly string _localName;

        /// <summary>
        /// XML namespace prefix </summary>
        private readonly string _prefix;


        /// <summary>
        /// Splits a qname into prefix and localname. </summary>
        /// <param name="qname"> a QName </param>
        public QName(string qname) {
            int colon = qname.IndexOf(':');

            if (colon >= 0) {
                _prefix = qname.Substring(0, colon);
                _localName = qname.Substring(colon + 1);
            }
            else {
                _prefix = "";
                _localName = qname;
            }
        }


        /// <summary>
        /// Constructor that initializes the fields </summary>
        /// <param name="prefix"> the prefix </param>
        /// <param name="localName"> the name </param>
        public QName(string prefix, string localName) {
            _prefix = prefix;
            _localName = localName;
        }


        /// <returns> the localName </returns>
        public virtual string LocalName {
            get { return _localName; }
        }


        /// <returns> the prefix </returns>
        public virtual string Prefix {
            get { return _prefix; }
        }

        /// <returns> Returns whether the QName has a prefix. </returns>
        public virtual bool HasPrefix() {
            return !string.IsNullOrEmpty(_prefix);
        }
    }
}
