using System;
using System.Collections.Generic;
using System.Text;
using Algorithm;

namespace Algorithm
{   
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            var linkedList = new DataStructure.LinkedList<int>();

            linkedList.AddBack(0);

            linkedList.Insert(1, new int[] { 1, 5, 2, 3, 4, 5, 1, 2, 3, 4 });

            linkedList.RemoveRange(1, 3);

            linkedList.Sort();

            foreach ((int i, int v) in linkedList.ToEnumerate())
            {
                Utility.Print($"{i} {v}");
            }
        }
    }
}
