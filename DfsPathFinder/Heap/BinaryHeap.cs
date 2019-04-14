using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

namespace PathFinder.Heap
{
    class BinaryHeap
    {
	    private BinaryHeapElement []heap;
	    private long max_size , next_position;

        private BinaryHeap()
        {
        }

	    public BinaryHeap(int max_size)
        {
		    heap = new BinaryHeapElement[max_size];
		    next_position = 0;
		    this.max_size = max_size;
	    }

	    public void Clear()
        {
		    for(int i = 0 ; i < next_position ; i++)
			    heap[i].binary_heap_index = 0;
		    next_position = 0;
	    }

	    public void Delete(BinaryHeapElement e)
        {
            if (!Has(e))
            {
                throw new BinaryHeapException("The element could not be deleted because it is not in the heap.");
            }
		    long position = (0x7FFFFFFF & e.binary_heap_index);

		    e.binary_heap_index = 0;
		    if(--next_position != position)
            {
			    heap[position] = heap[next_position];
			    heap[position].binary_heap_index = 0x80000000 | position;
			    if(position > 0 && heap[position].LessThanForHeap(heap[(position - 1)/2]))
				    HeapifyUp(position);
			    else
				    HeapifyDown(position);
		    }
	    }

	    public void Insert(BinaryHeapElement e)
        {
		    if(Has(e))
            {
			    long position = (0x7FFFFFFF & e.binary_heap_index);
			    Debug.Assert(position < next_position);
			    if(position > 0 && e.LessThanForHeap(heap[(position - 1)/2]))
				    HeapifyUp(position);
			    else
				    HeapifyDown(position);
		    }
            else
            {
			    if(next_position == max_size) throw new BinaryHeapException("The Binary Heap has exceeded the available space.");
			    e.binary_heap_index = 0x80000000 | next_position;
			    heap[next_position] = e;

			    HeapifyUp(next_position++);
		    }
	    }

	    public bool IsValidHeap()
        {
		    int i = 0 , j;
		    j = 2 * i + 1;
		    while(j < next_position)
            {
			    if(heap[j].LessThanForHeap(heap[i])) return false;
			    if(++j < next_position && heap[j].LessThanForHeap(heap[i])) return false;
			    i++;
			    j = 2 * i + 1;
		    }
		    return true;
	    }

	    public BinaryHeapElement GetElement(int index)
        {
		    if(index >= next_position)
			    throw new BinaryHeapException("There is no element with this index.");
		    return heap[index];
	    }
	    
        public bool Has(BinaryHeapElement e)
        {
		    return (e.binary_heap_index & 0x80000000) != 0;
	    }
	    
        public BinaryHeapElement Peek()
        {
		    if(next_position == 0) throw new BinaryHeapException("The Binary Heap is empty.");
		    return heap[0];
	    }

	    public BinaryHeapElement Pop()
        {
		    if(next_position == 0) throw new BinaryHeapException("The Binary Heap is empty.");
		    BinaryHeapElement min = heap[0];
		    Debug.Assert((0x7FFFFFFF &heap[0].binary_heap_index) == 0);
		    heap[0] = heap[--next_position];
		    HeapifyDown(0);
		    min.binary_heap_index = 0;
		    return min;
	    }

	    public long Size()
        {
		    return next_position;
	    }

	    private void HeapifyDown(long position)
        {
		    long i , j;
		    BinaryHeapElement aux;

		    i = position;
		    j = i * 2 + 1;
		    aux = heap[i];
		    while(j < next_position)
            {
			    if(j + 1 < next_position && heap[j + 1].LessThanForHeap(heap[j])) j++;
			    if(!heap[j].LessThanForHeap(aux)) break;
			    heap[i] = heap[j];
			    heap[i].binary_heap_index = 0x80000000 | i;
			    i = j;
			    j = i * 2 + 1;
		    }
		    heap[i] = aux;
		    heap[i].binary_heap_index = 0x80000000 | i;
	    }

	    private void HeapifyUp(long position)
        {
		    long i = position , j;
		    BinaryHeapElement aux;

		    j = (i - 1) / 2;
		    while(i > 0 && heap[i].LessThanForHeap(heap[j])){
			    aux = heap[i];
			    heap[i] = heap[j];
			    heap[j] = aux;

			    heap[i].binary_heap_index = 0x80000000 | i;
			    heap[j].binary_heap_index = 0x80000000 | j;

			    i = j;
			    j = (i - 1) / 2;
		    }
	    }


    }
}
