using Algorithm;
using Algorithm.Technique;
using Algorithm.Text.JSON;
using Algorithm.Datastructure;
using Algorithm.Net;

namespace Algorithm
{
    internal class Program
    {       
        public static void Init()
        {
            System.Console.InputEncoding = System.Text.Encoding.Unicode;
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        static void Main(string[] args)
        {
            Init();

            Queue<int> q = new Queue<int>(capacity:10000);

            q.EnqueueRange(Extensions.Create(100, 0, 1000));

            while (!q.IsEmpty)
            {
                Util.Print(q[0]);
            }

        }
    }
}
    