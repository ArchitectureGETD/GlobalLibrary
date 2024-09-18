using System;

namespace iTextSharp.GE.xmp {
    /// <summary>
    /// This exception wraps all errors that occur in the XMP Toolkit.
    /// 
    /// @since   16.02.2006
    /// </summary>
    public class XmpException : Exception {
        /// <summary>
        /// the errorCode of the XMP toolkit </summary>
        private readonly int _errorCode;


        /// <summary>
        /// Constructs an exception with a message and an error code. </summary>
        /// <param name="message"> the message </param>
        /// <param name="errorCode"> the error code </param>
        public XmpException(string message, int errorCode)
            : base(message) {
            _errorCode = errorCode;
        }


        /// <summary>
        /// Constructs an exception with a message, an error code and a <code>Throwable</code> </summary>
        /// <param name="message"> the error message. </param>
        /// <param name="errorCode"> the error code </param>
        /// <param name="t"> the exception source </param>
        public XmpException(string message, int errorCode, Exception t)
            : base(message, t) {
            _errorCode = errorCode;
        }


        /// <returns> Returns the errorCode. </returns>
        public virtual int ErrorCode {
            get { return _errorCode; }
        }
    }
}
