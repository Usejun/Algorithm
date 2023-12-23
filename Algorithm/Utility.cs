using System;
using System.IO;
using System.Text;
using Algorithm.Text;
using System.Threading;
using System.Diagnostics;
using Algorithm.Datastructure;

namespace Algorithm
{
    public static class Util
    {
        public static Stopwatch Timer => sw;
        public static StringBuffer Buffer => sb;

        private static readonly BinaryWriter writer = new BinaryWriter(Console.OpenStandardOutput(), Encoding.Unicode);
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(), Encoding.Unicode);
        private static readonly StringBuffer sb = new StringBuffer();
        private static readonly Stopwatch sw = new Stopwatch();

        public static void Print()
        {
            writer.Write("\n");
            writer.Flush();
        }

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
            Append(string.Join(sep, collection.ToArray()), end);
        }

        public static void Flush()
        {
            Print(sb.ToString(), end:"");
            BufferClear();
        }

        public static void StreamClear()
        {
            Console.Clear();
        }

        public static void BufferClear()
        {
            sb.Clear();
        }

        public static string Input()
        {
            return reader.ReadLine();
        }

        public static T Input<T>(Func<string, T> parser)
        {
            return parser(reader.ReadLine());
        }

        public static string[] Inputs(char sep)
        {
            return Input().Split(sep);
        }

        public static T[] Inputs<T>(Func<string, T> parser, char sep = ' ')
        {
            var input = Inputs(sep);
            var output = new T[input.Length];

            for (int i = 0; i < input.Length; i++)
                output[i] = parser(input[i]);

            return output;            
        }           

        public static void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }     

        public static long Measure(Action code)
        {
            Timer.Restart();

            code();

            Timer.Stop();

            long time = Timer.ElapsedMilliseconds;

            Print(time + "ms");

            return time;
        }

        public static (T, T) Two<T>(Func<string, T> parser)
        {
            T[] input = Inputs(parser);
            return (input[0], input[1]);
        }

        public static (T, T, T) Three<T>(Func<string, T> parser)
        {
            T[] input = Inputs(parser);
            return (input[0], input[1], input[2]);
        }

        public static (T, T, T, T) Four<T>(Func<string, T> parser)
        {
            T[] input = Inputs(parser);
            return (input[0], input[1], input[2], input[3]);
        }

        public static (T, T, T, T, T) Five<T>(Func<string, T> parser)
        {
            T[] input = Inputs(parser);
            return (input[0], input[1], input[2], input[3], input[4]);
        }

        public static (T, T, T, T, T, T) Six<T>(Func<string, T> parser)
        {
            T[] input = Inputs(parser);
            return (input[0], input[1], input[2], input[3], input[4], input[5]);
        }

        public static (T, T, T, T, T, T, T) Seven<T>(Func<string, T> parser)
        {
            T[] input = Inputs(parser);
            return (input[0], input[1], input[2], input[3], input[4], input[5], input[6]);
        }

        public static (T, T, T, T, T, T , T, T) Eight<T>(Func<string, T> parser)
        {
            T[] input = Inputs(parser);
            return (input[0], input[1], input[2], input[3], input[4], input[5], input[6], input[7]);
        }

        public static (T, T, T, T, T, T, T, T, T) Nine<T>(Func<string, T> parser)
        {
            T[] input = Inputs(parser);
            return (input[0], input[1], input[2], input[3], input[4], input[5], input[6], input[7], input[8]);
        }

        public static (T, T, T, T, T, T, T, T, T, T) Ten<T>(Func<string, T> parser)
        {
            T[] input = Inputs(parser);
            return (input[0], input[1], input[2], input[3], input[4], input[5], input[6], input[7], input[8], input[9]);
        }

    }
}
