using System;

namespace iTextSharp.GE.text.log
{
    /**
     * Factory that creates a counter for every reader or writer class.
     * You can implement your own counter and declare it like this:
     * <code>CounterFactory.getInstance().setCounter(new SysoCounter());</code>
     * SysoCounter is just an example of a Counter implementation.
     * It writes info about files being read and written to the System.out.
     * 
     * This functionality can be used to create metrics in a SaaS context.
     */
    public class CounterFactory {

	    /** The singleton instance. */
	    private static CounterFactory myself;

	    static CounterFactory() {
		    myself = new CounterFactory();
	    }
	
	    /** The current counter implementation. */
        private ICounter counter = new DefaultCounter();

	    /** The empty constructor. */
	    private CounterFactory() {}
	
	    /** Returns the singleton instance of the factory. */
	    public static CounterFactory getInstance() {
		    return myself;
	    }
	
	    /** Returns a counter factory. */
	    public static ICounter GetCounter(Type klass) {
		    return myself.counter.GetCounter(klass);
	    }
	
	    /**
	     * Getter for the counter.
	     */
	    virtual public ICounter GetCounter() {
		    return counter;
	    }
	
	    /**
	     * Setter for the counter.
	     */
	    virtual public void SetCounter(ICounter counter) {
		    this.counter = counter;
	    }
    }
}
