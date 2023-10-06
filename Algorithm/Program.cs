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
            Stack<int> s = new Stack<int>();

            s.Push(1);
            s.Push(2);
            s.Push(3);
            s.Push(4);

            pq.Enqueue("potato", 3);
            pq.Enqueue("감자", 1);
            pq.Enqueue("poteto", 2);

            Util.Print(pq, sep:"\n");
            Util.Print(s);

            var (name, priority) = pq.Dequeue();
        }
    }
}
