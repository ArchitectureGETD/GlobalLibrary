using System;
namespace iTextSharp.GE.text.log {

    /**
     * Logger interface
     * {@link LoggerFactory#setLogger(Logger)}.
     *
     * @author redlab_b
     *
     */
    public interface ILogger {

        /**
         * @param klass
         * @return the logger for the given klass
         */
        ILogger GetLogger(Type klass);

        ILogger GetLogger(String name);
        /**
         * @param level
         * @return true if there should be logged for the given level
         */
        bool IsLogging(Level level);
        /**
         * Log a warning message.
         * @param message
         */
        void Warn(String message);

        /**
         * Log a trace message.
         * @param message
         */
        void Trace(String message);

        /**
         * Log a debug message.
         * @param message
         */
        void Debug(String message);

        /**
         * Log an info message.
         * @param message
         */
        void Info(String message);
        /**
         * Log an error message.
         * @param message
         */
        void Error(String message);

        /**
         * Log an error message and exception.
         * @param message
         * @param e
         */
        void Error(String message, Exception e);
    }
}
