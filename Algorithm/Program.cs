using System;
using Algorithm.Technique;
using Algorithm.DataStructure;

using static Algorithm.Util;
using static Algorithm.DataStructure.Extensions;

namespace Algorithm
{
    internal class Program
    {       
        public static void Init()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        static void Main(string[] args)
        {
            Init();

            (int v, int e) = Two(int.Parse);
            Graph graph = new Graph(v + 1);

            for (int i = 0; i < e; i++)
            {
                (int f, int t, int n) = Three(int.Parse);
                graph.AddNode(f, t, n);
            }

            Graph graph2 = Techniques.Kruskal(graph);

            Print(Mathf.Sum(Convert(graph2.AllNode(), i => i.edge)));
        }   
    }
}
    