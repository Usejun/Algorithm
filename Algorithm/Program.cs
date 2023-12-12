﻿using System;
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

            (int n, int m) = Two(int.Parse);
            Graph graph = new Graph(n);

            for (int i = 0; i < m; i++)
                graph.Connect(Three(int.Parse));

            Print(Techniques.Bellman(1, graph));
        }   
    }
}
    