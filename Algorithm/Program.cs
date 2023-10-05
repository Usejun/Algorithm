using System;
using Algorithm;
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

            public override string ToString()
            {
                return $"x : {x}, y : {y}";
            }
        }

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            PriorityQueue<string, int> pq = new PriorityQueue<string, int>();

            pq.Enqueue("potato", 3);
            pq.Enqueue("감자", 1);
            pq.Enqueue("poteto", 2);

            Util.Print(pq);

            Util.Print(pq.ToArray());
        }
    }
}
