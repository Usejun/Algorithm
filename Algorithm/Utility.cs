using System;
using System.Text;
using System.IO;
using System.Linq;

namespace Algorithm
{
    public static class Utility
    {
        static readonly StringBuilder writer = new StringBuilder();

        public static void Print(object text = default, char end = '\n')
        {
            writer.Append(text?.ToString());
            writer.Append(end);
            Console.Write(writer);
            writer.Clear();
        }

        public static void Print(Array array, char end = '\n', char separator = ' ')
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

        public static string Input()
        {
            return Console.ReadLine();            
        }

        public static string[] Inputs(char separator = '\n')
        {
            return Input().Split(separator);
        }

        public static T[] Inputs<T>(Func<string, T> parser, char separator = '\n')
        {
            return Inputs().Select(parser).ToArray();
        }   
    }
}
