using System;
namespace iTextSharp.GE.text.log {

    /**
     * The no-operation logger, it does nothing with the received logging
     * statements. And returns false by default for {@link NoOpLogger#isLogging(Level)}
     *
     * @author redlab_b
     *
     */
    public sealed class NoOpLogger : ILogger {

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#getLogger(java.lang.Class)
         */
        public ILogger GetLogger(Type name) {
            return this;
        }

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#warn(java.lang.String)
         */
        public void Warn(String message) {
        }

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#trace(java.lang.String)
         */
        public void Trace(String message) {
        }

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#debug(java.lang.String)
         */
        public void Debug(String message) {
        }

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#info(java.lang.String)
         */
        public void Info(String message) {
        }

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#error(java.lang.String, java.lang.Exception)
         */
        public void Error(String message, Exception e) {
        }

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#isLogging(com.itextpdf.text.log.Level)
         */
        public bool IsLogging(Level level) {
            return false;
        }

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#error(java.lang.String)
         */
        public void Error(String message) {
        }

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#getLogger(java.lang.String)
         */
        public ILogger GetLogger(String name) {
            return this;
        }
    }
}
