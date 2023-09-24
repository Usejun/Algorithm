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

            var linkedList = new DataStructure.LinkedList<int>
            {
                0, 2, 4, 2, 3, 1, 5, 7, 3, 2, 7, 1, 0              
            };

            Utility.Print(string.Join(" ", linkedList));
            Utility.Print(string.Join(" ", linkedList.Sorted()));
        }
    }
}
