using System;
using Algorithm;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using Algorithm.DataStructure;

namespace Algorithm
{
    internal class Program
    {
        public class Point
        {
            public int x; 
            public int y;
        }

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            Heap<int> heap = new Heap<int>();
            heap.Add(3);
            heap.Add(1);
            heap.Add(1);
            heap.Add(2);
            heap.Add(3);
            heap.Add(0);

            Util.Print(heap);           
            
        }
    }
}
    