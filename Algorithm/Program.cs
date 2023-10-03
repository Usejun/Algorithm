using System;
using Algorithm;
using System.Text;
using System.Collections;
using Algorithm.DataStructure;
using System.Collections.Generic;

namespace Algorithm
{   
    internal class Program
    {
        public class Point
        {
            public int x;
            public int y;

            public override string ToString()
            {
                return $"x : {x}, y : {y}";
            }
        }

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            DynamicArray<int> array = new DynamicArray<int>(10);

            for (int i = 0; i < 100; i++)
            {
                array.Add(i);
            }

            Console.WriteLine(string.Join(" ", array.ToArray()));

        }
    }
}
