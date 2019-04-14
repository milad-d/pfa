using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Heap
{
    abstract class BinaryHeapElement
    {
        #region Data Members
        public long binary_heap_index;
        #endregion

        #region Constructor
        public BinaryHeapElement()
        {
            binary_heap_index = 0;
        }
        #endregion

        #region Methods
        public abstract bool LessThanForHeap(BinaryHeapElement e);
        #endregion
    }
}
