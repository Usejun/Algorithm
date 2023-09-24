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
            linkedList.AddBack(1);
            linkedList.AddBack(3);
            linkedList.AddBack(2);

            Utility.Print(string.Join(" ", linkedList));
            Utility.Print(string.Join(" ", linkedList.Sorted()));
        }
    }
}
