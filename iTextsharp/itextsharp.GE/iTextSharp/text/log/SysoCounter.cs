using System;

namespace iTextSharp.GE.text.log
{
    public class SysoCounter : ICounter
    {
      	/**
	     * The name of the class for which the Counter was created
	     * (or iText if no name is available)
	     */
	    protected String name;
	
	    /**
	     * Empty SysoCounter constructor.
	     */
	    public SysoCounter() {
		    name = "iText";
	    }
	
	    /**
	     * Constructs a SysoCounter for a specific class.
	     * @param klass
	     */
	    protected SysoCounter(Type klass) {
		    name = klass.FullName;
	    }
	
	    /**
	     * @see com.itextpdf.text.log.Counter#getCounter(java.lang.Class)
	     */
	    virtual public ICounter GetCounter(Type klass) {
		    return new SysoCounter(klass);
	    }

	    /**
	     * @see com.itextpdf.text.log.Counter#read(long)
	     */
	    virtual public void Read(long l) {
		    System.Console.WriteLine("[{0}] {1} bytes read", name, l);
	    }

	    /**
	     * @see com.itextpdf.text.log.Counter#written(long)
	     */
	    virtual public void Written(long l) {
            System.Console.WriteLine("[{0}] {1} bytes written", name, l);
	    }
    }
}
