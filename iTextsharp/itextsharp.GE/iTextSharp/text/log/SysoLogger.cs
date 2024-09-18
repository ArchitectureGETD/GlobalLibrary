using System;
using System.Text;
namespace iTextSharp.GE.text.log {

    /**
     * A Simple System.out logger.
     * @author redlab_be
     *
     */
    public class SysoLogger : ILogger {

        private String name;
        private int shorten;

        /**
         * Defaults packageReduce to 1.
         */
        public SysoLogger() : this(1) {
        }
        /**
         * Amount of characters each package name should be reduced with.
         * @param packageReduce
         *
         */
        public SysoLogger(int packageReduce) {
            this.shorten = packageReduce;
        }

        /**
         * @param klass
         * @param shorten
         */
        protected SysoLogger(String klass, int shorten) {
            this.shorten = shorten;
            this.name = klass;
        }

        virtual public ILogger GetLogger(Type klass) {
            return new SysoLogger(klass.FullName, shorten);
        }

        /* (non-Javadoc)
         * @see com.itextpdf.text.log.Logger#getLogger(java.lang.String)
         */
        virtual public ILogger GetLogger(String name) {
            return new SysoLogger("[itext]", 0);
        }

        virtual public bool IsLogging(Level level) {
            return true;
        }

        virtual public void Warn(String message) {
            Console.Out.WriteLine("{0} WARN  {1}", Shorten(name), message);
        }

        /**
         * @param name2
         * @return
         */
        private String Shorten(String className) {
            if (shorten != 0) {
                StringBuilder target = new StringBuilder();
                String name = className;
                int fromIndex = className.IndexOf('.');
                while (fromIndex != -1) {
                    int parseTo = (fromIndex < shorten) ? (fromIndex) : (shorten);
                    target.Append(name.Substring(0, parseTo));
                    target.Append('.');
                    name = name.Substring(fromIndex + 1);
                    fromIndex = name.IndexOf('.');
                }
                target.Append(className.Substring(className.LastIndexOf('.') + 1));
                return target.ToString();
            }
            return className;
        }

        virtual public void Trace(String message) {
            Console.Out.WriteLine("{0} TRACE {1}", Shorten(name), message);
        }

        virtual public void Debug(String message) {
            Console.Out.WriteLine("{0} DEBUG {1}", Shorten(name), message);
        }

        virtual public void Info(String message) {
            Console.Out.WriteLine("{0} INFO  {1}", Shorten(name), message);
        }

        virtual public void Error(String message) {
            Console.Out.WriteLine("{0} ERROR {1}", name, message);
        }

        virtual public void Error(String message, Exception e) {
            Console.Out.WriteLine("{0} ERROR {1}", name, message);
            Console.Out.WriteLine(e.StackTrace);
        }
    }
}
