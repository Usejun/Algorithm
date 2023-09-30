using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections;

namespace Algorithm
{
    public static class Utility
    {
        static readonly BinaryWriter writer = new BinaryWriter(Console.OpenStandardOutput(), Encoding.Unicode);

        public static void Print(object text = default, string end = "\n")
        {
            writer.Write(text?.ToString() ?? "");
            writer.Write(end);
            writer.Flush();
        }

        public static void Print(string text, string end = "\n")
        {
            writer.Write(text);
            writer.Write(end);
            writer.Flush();
        }

        public static void Print(Array array, string end = "\n", string separator = " ")
        {
            Put(0, new int[array.Rank]);

            void Put(int dimension, int[] indices)
            {
                if (dimension == array.Rank - 1)
                {
                    for (int i = 0; i < array.GetLength(dimension); i++)
                    {
                        indices[dimension] = i;
                        Print(array.GetValue(indices), end: separator);
                    }
                    Print(end: end);
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

        public static void Print(IEnumerable enumerable, string end = "\n", string separator = " ")
        {
            Print(string.Join(separator, enumerable), end:end);
        }

        public static string Input()
        {
            return Console.ReadLine();            
        }

        public static string[] Inputs(char separator = ' ')
        {
            return Input().Split(separator);
        }

        public static T[] Inputs<T>(Func<string, T> parser, char separator = ' ')
        {
            return Inputs(separator).Select(parser).ToArray();
        }   
    }
}
