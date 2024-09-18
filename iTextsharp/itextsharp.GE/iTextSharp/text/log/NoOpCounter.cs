using System;

namespace iTextSharp.GE.text.log
{
    /**
     * Implementation of the Counter interface that doesn't do anything.
     */
    public class NoOpCounter : ICounter
    {
        /**
         * @param klass The Class asking for the Counter
         * @return the Counter instance
         * @see com.itextpdf.text.log.Counter#getCounter(java.lang.Class)
         */
        virtual public ICounter GetCounter(Type klass) {
            return this;
        }

        /**
         * @see com.itextpdf.text.log.Counter#read(long)
         */
        virtual public void Read(long l) {

        }

        /**
         * @see com.itextpdf.text.log.Counter#written(long)
         */
        virtual public void Written(long l) {
        
        }
    }
}
