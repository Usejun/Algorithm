using Algorithm;
using Algorithm.Sort;
using Algorithm.Technique;
using Algorithm.Text.JSON;
using Algorithm.Datastructure;

namespace Algorithm
{
    internal class Program
    {       
        public static void Init()
        {
            System.Console.InputEncoding = System.Text.Encoding.Unicode;
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        public static void Main()
        {
            Init();

            PriorityQueue<int, int> pq = new PriorityQueue<int, int>(true);

            pq.Enqueue(12, 2);
            pq.Enqueue(2, 3);
            pq.Enqueue(3, 4);
            pq.Enqueue(5, 1);
            pq.Enqueue(-1, 2);
            pq.Enqueue(-12, 3);

            while (!pq.IsEmpty)
                Util.Print(pq.Dequeue().Value);
           
        }    
    }
}
    