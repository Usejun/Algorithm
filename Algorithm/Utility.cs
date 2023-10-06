using System;
using System.IO;
using Algorithm;
using System.Linq;
using System.Text;
using Algorithm.DataStructure;
using System.Collections.Generic;

namespace Algorithm
{
    public static class Util
    {
        static readonly BinaryWriter writer = new BinaryWriter(Console.OpenStandardOutput(), Encoding.Unicode);
        
        public static void Print(string text, string end = "\n")
        {
            writer.Write(text);
            writer.Write(end);
            writer.Flush();
        }

        public static void Print(object value, string end = "\n")
        {
            writer.Write(value?.ToString() ?? "");
            writer.Write(end);
            writer.Flush();
        }

        public static void Print(Array array, string end = "\n", string sep = " ")
        {
            int count = array.Length;

            Put(0, new int[array.Rank]);

            void Put(int dimension, int[] indices)
            {
                if (dimension == array.Rank - 1)
                {
                    for (int i = 0; i < array.GetLength(dimension); i++)
                    {
                        indices[dimension] = i;
                        count--;
                        Print(array.GetValue(indices), end: count == 0 ? "" : sep);
                    }
                    Print("", end);
                }
                else
                {
                    for (int i = 0; i < array.GetLength(dimension); i++)
                    {
                        indices[dimension] = i;
                        Put(dimension + 1, indices);
                    }
                }
            }
        }

        public static void Print<T>(Collection<T> collection, string end = "\n", string sep = " ")
        {
            Print(collection.ToArray(), end, sep);
        }

        public static string Input()
        {
            return Console.ReadLine();            
        }

        public static string[] Inputs(char sep = ' ')
        {
            return Input().Split(sep);
        }

        public static T[] Inputs<T>(Func<string, T> parser, char sep = ' ')
        {
            return Inputs(sep).Select(parser).ToArray();
        }           
    }
}
