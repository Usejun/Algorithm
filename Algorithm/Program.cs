﻿using System;
using System.Linq;
using System.Text;
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

            var arr = Extensions.Create(100, -120, 123);
            Sorts.QuickSort(arr);
            Util.Print(arr, sep:"\n");
            
        }
    }
}
    