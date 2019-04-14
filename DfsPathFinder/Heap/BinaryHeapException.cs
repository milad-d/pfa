using System;

namespace PathFinder.Heap
{
    class BinaryHeapException : Exception
    {
	    //private static long serialVersionUID = 1L;

	    public BinaryHeapException(String message):base(message)
        {
	    }
    }
}
