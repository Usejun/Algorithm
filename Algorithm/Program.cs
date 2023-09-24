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

            
            
            Utility.Print(string.Join(" ", linkedList));
            Utility.Print(string.Join(" ", linkedList.Sorted()));
        }
    }
}
