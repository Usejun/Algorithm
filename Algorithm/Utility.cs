using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Algorithm.DataStructure;

namespace Algorithm
{
    public static class Util
    {
        static readonly BinaryWriter writer = new BinaryWriter(Console.OpenStandardOutput(), Encoding.Unicode);

        static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(), Encoding.Unicode);
        
        static readonly StringBuilder sb = new StringBuilder();

        static readonly Stopwatch sw = new Stopwatch();

        public static void Print(string text, string end = "\n")
        {
            writer.Write(text);
            writer.Write(end);
            writer.Flush();
        }

        public static void Print(object value, string end = "\n")
        {
            Print(value?.ToString() ?? "", end: end);
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

        public static void Append(string text, string end = "\n")
        {
            sb.Append(text);
            sb.Append(end);
        }

        public static void Append(object value, string end = "\n")
        {
            Append(value?.ToString() ?? "", end: end);
        }

        public static void Append(Array array, string end = "\n", string sep = " ")
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
                        Append(array.GetValue(indices), end: count == 0 ? "" : sep);
                    }
                    Append("", end);
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

        public static void Append<T>(Collection<T> collection, string end = "\n", string sep = " ")
        {
            sb.Append(string.Join(sep, collection.ToArray()));
            sb.Append(end);
        }

        public static void Flush()
        {
            Print(sb, end:"");
            Clear();
        }

        public static void Clear()
        {
            sb.Clear();
        }

        public static string Input()
        {
            return reader.ReadLine();
        }

        public static string[] Input(char sep)
        {
            return Input().Split(sep);
        }

        public static T[] Input<T>(Func<string, T> parser, char sep = ' ')
        {
            return Input(sep).Select(parser).ToArray();
        }           

        public static void Start()
        {
            sw.Restart();
        }

        public static long Stop()
        {
            sw.Stop();

            return sw.ElapsedMilliseconds;                     
        }

        public static void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }     

    }
}
