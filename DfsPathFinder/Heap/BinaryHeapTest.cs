using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder.Heap
{
    class MyInt : BinaryHeapElement 
    {
	    public int value;

	    public MyInt(int value)
        {
		    this.value = value;
	    }

	    public override bool LessThanForHeap (BinaryHeapElement e)
        {
		    return value < ((MyInt)e).value;
	    }
    }

    class BinaryHeapTest
    {
        public void Test() //public BinaryHeapTest()
        {
		    int seed = (int)System.DateTime.Now.Ticks;
		    Random random = new Random(seed);
		    int SIZE = 50;
		    int MAX_PLUS_ONE = 1000;
		    BinaryHeap heap = new BinaryHeap(SIZE);
		    List<MyInt> v = new List<MyInt>();


		    System.Console.WriteLine(seed);

		    for(int i = 0 ; i < SIZE ; i++)     
            {
			    MyInt myint = new MyInt(random.Next(MAX_PLUS_ONE));
			    heap.Insert(myint);
			    v.Add(myint);
		    }

		    for(int i = 0 ; i < SIZE * 0.8f ; i++)
            {
			    MyInt myint = v[random.Next(v.Count)];
			    myint.value = random.Next(MAX_PLUS_ONE);
			    heap.Insert(myint);
		    }

		    for(int i = 0 ; i < SIZE * 0.8f ; i++)
            {
			    int a = random.Next(v.Count);
			    MyInt myint = v[a];
			    v.RemoveAt(a);
			    heap.Delete(myint);
		    }


		    {
			    int smallest = 0;
			    while(heap.Size() > 0)
                            {
				    MyInt myint = (MyInt)heap.Pop();
				    if(myint.value < smallest)
                                    {
					    System.Console.WriteLine("Falha!");
					    return;
				    }
				    smallest = myint.value;
				    System.Console.WriteLine(myint.value);
			    }
		    }
	    }
    }
}
