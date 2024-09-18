using System;
namespace iTextSharp.GE.text.log
{
    /**
     * Interface that can be implemented if you want to count the number of documents
     * that are being processed by iText.
     * 
     * Implementers may use this method to record actual system usage for licensing purposes
     * (e.g. count the number of documents or the volumne in bytes in the context of a SaaS license).
     */
    public interface ICounter
    {
        /** Gets a Counter instance for a specific class. */
	    ICounter GetCounter(Type klass);
	
	    /**
	        * This method gets triggered if a file is read.
	        * @param l	the length of the file that was written
	        */
	    void Read(long l);
	
	    /**
	        * This method gets triggered if a file is written.
	        * @param l	the length of the file that was written 
	        */
	    void Written(long l); 
    }
}
