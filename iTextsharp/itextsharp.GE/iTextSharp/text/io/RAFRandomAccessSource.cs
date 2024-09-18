using System;
using System.IO;
namespace iTextSharp.GE.text.io {

    /**
     * 
     * A RandomAccessSource that uses a {@link RandomAccessFile} as it's source
     * Note: Unlike most of the RandomAccessSource implementations, this class is not thread safe
     */
    class RAFRandomAccessSource : IRandomAccessSource {
	    /**
	     * The source
	     */
	    private readonly FileStream raf;
    	
	    /**
	     * The length of the underling RAF.  Note that the length is cached at construction time to avoid the possibility
	     * of IOExceptions when reading the length.
	     */
	    private readonly long length;
    	
	    /**
	     * Creates this object
	     * @param raf the source for this RandomAccessSource
	     * @throws IOException if the RAF can't be read
	     */
	    public RAFRandomAccessSource(FileStream raf) {
		    this.raf = raf;
		    length = raf.Length;
	    }

	    /**
	     * {@inheritDoc}
	     */
	    // TODO: test to make sure we are handling the length properly (i.e. is raf.length() the last byte in the file, or one past the last byte?)
	    public virtual int Get(long position) {
		    if (position > length)
			    return -1;
    		
		    // Not thread safe!
            if(raf.Position != position)
		        raf.Seek(position, SeekOrigin.Begin);
    		
		    return raf.ReadByte();
	    }

	    /**
	     * {@inheritDoc}
	     */
	    public virtual int Get(long position, byte[] bytes, int off, int len) {
		    if (position > length)
			    return -1;
    		
		    // Not thread safe!
            if (raf.Position != position)
		        raf.Seek(position, SeekOrigin.Begin);

		    int n = raf.Read(bytes, off, len);
            return n == 0 ? -1 : n; //in .NET Streams return 0 on EOF, not -1
	    }

	    /**
	     * {@inheritDoc}
	     * Note: the length is determined when the {@link RAFRandomAccessSource} is constructed.  If the file length changes
	     * after construction, that change will not be reflected in this call.
	     */
	    public virtual long Length {
            get {
		        return length;
            }
	    }

	    /**
	     * Closes the underlying RandomAccessFile
	     */
	    public virtual void Close() {
		    raf.Close();
	    }

        virtual public void Dispose() {
            Close();
        }
    }
}
