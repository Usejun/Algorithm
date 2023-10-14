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

            Stack<int> s = new Stack<int>();

            for (int i = 0; i < 10; i++)
            {
                s.Push(i);
            }

            Util.Print(s);

            Mathf.Sort(s);

            Util.Print(s);

        }
    }
}
    