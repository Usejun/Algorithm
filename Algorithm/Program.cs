using System;
using Algorithm;
using System.Text;
using Algorithm.DataStructure;
using System.Linq;

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
            Stack<Point> stack = new Stack<Point>();
            Queue<int> queue = new Queue<int>();

            int[,,] v = new int[,,] { { { 0, 0 }, { 1, 1 }, { 2, 2 } }, { { 0, 0 }, { 1, 1 }, { 2, 2 } }, { { 0, 0 }, { 1, 1 }, { 2, 2 } } };

            pq.Enqueue("potato", 3);
            pq.Enqueue("감자", 1);
            pq.Enqueue("poteto", 2);

            stack.Push(new Point() { x = 1, y = 2 });
            stack.Push(new Point() { x = 2, y = 3 });
            stack.Push(new Point() { x = 3, y = 4 });

            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(2);
            queue.Enqueue(1);
            queue.Enqueue(8);
           
            Util.Print(stack.ToArray());

            Util.Print(stack.ToEnumerate(), sep:"\n");

            Util.Print(v, sep:" ");

            Util.Print(pq.Dequeue().Value);
            Util.Print(pq.Dequeue().Value);
            Util.Print(pq.Dequeue().Value);

            Util.Print(Util.Sort(queue));
        }
    }
}
