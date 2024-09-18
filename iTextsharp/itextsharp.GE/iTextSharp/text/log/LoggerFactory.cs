using System;
namespace iTextSharp.GE.text.log {

    /**
     * LoggerFactory can be used to set a logger. The logger should be created by
     * implementing {@link Logger}. In the implementation users can choose how they
     * log received messages. Added for developers. For some cases it can be handy
     * to receive logging statements while developing applications with iText
     *
     * @author redlab_b
     *
     */
    public class LoggerFactory {

        static LoggerFactory() {
            myself = new LoggerFactory();
        }

        private static LoggerFactory myself;
        /**
         * Returns the logger set in this LoggerFactory. Defaults to {@link NoOpLogger}
         * @param klass
         * @return the logger.
         */
        public static ILogger GetLogger(Type klass) {
            return myself.logger.GetLogger(klass);
        }
        /**
         * Returns the logger set in this LoggerFactory. Defaults to {@link NoOpLogger}
         * @param name
         * @return the logger.
         */
        public static ILogger GetLogger(String name) {
            return myself.logger.GetLogger(name);
        }
        /**
         * Returns the LoggerFactory
         * @return singleton instance of this LoggerFactory
         */
        public static LoggerFactory GetInstance() {
            return myself;
        }

        private ILogger logger = new NoOpLogger();

        private LoggerFactory() {
        }

        /**
         * Set the global logger to process logging statements with.
         *
         * @param logger the logger
         */
        virtual public void SetLogger(ILogger logger) {
            this.logger = logger;
        }

        /**
         * Get the logger.
         *
         * @return the logger
         */
        virtual public ILogger Logger() {
            return logger;
        }
    }
}
