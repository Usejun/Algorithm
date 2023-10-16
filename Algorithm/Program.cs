using System;
using System.Linq;
using System.Text;
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

            var heap = new Heap<int>(reverse:true) { 1, 4, 6, 3, 2, 1, 5, 8, 2, 4, 6, 1};

            while (heap.Count > 0)
            {
                Util.Print(heap.Pop());
            };
            
        }
    }
}
    