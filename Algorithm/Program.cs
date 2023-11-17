using System;
using System.Text;
using System.Numerics;
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
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
        }

        static void Main(string[] args)
        {
            Init();

            foreach ((int i, int v) in ToPairs(Range(0, 200, -2)))
            {
                Print((i, v));
            }

            Random

            

        }    
    }
}
    