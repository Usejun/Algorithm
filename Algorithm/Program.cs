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

            var ll = new LinkedList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            while (!ll.IsEmpty)
            {
                Print(ll[ll.Count - 1]);
                ll.RemoveBack();
            }

        }   
    }
}
    