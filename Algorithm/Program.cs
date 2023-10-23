using System;
using System.Linq;
using System.Text;
using Algorithm.DataStructure;
using static Algorithm.Technique;

namespace Algorithm
{
    internal class Program
    {
        public class Point
        {
            public int x; 
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public void Deconstruct(out int x, out int y)
            {
                x = this.x;
                y = this.y;
            }
            
            public static Point operator +(Point a, Point b)
            {
                return new Point(a.x + b.x, a.y + b.x);
            }
            
            public static Point operator -(Point a, Point b)
            {
                return new Point(a.x - b.x, a.y - b.x);
            }

            public static Point operator *(Point a, Point b)
            {
                return new Point(a.x * b.x, a.y * b.x);
            }

            public static Point operator /(Point a, Point b)
            {
                if (b.x == 0 || b.y == 0)
                    throw new DivideByZeroException();

                return new Point(a.x / b.x, a.y / b.x);
            }

            public static Point operator %(Point a, Point b)
            {
                return new Point(a.x % b.x, a.y % b.x);
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();               

                var fields = typeof(Point).GetFields();          

                foreach (var field in fields)
                    sb.AppendLine($"({field.Name} : {field.GetValue(this)})");

                return sb.ToString();
            }
        }

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            Heap<int> heap = new Heap<int>(values: Extensions.Create(1000000, 0, 1000));
            var arr1 = Extensions.Create(1000000, 0, 1000);
            var arr2 = Extensions.Create(1000000, 0, 1000);

            Util.Start();

            heap.ToArray();

            Util.Print(Util.Stop() + "ms");

            Sorts.Measure(Sorts.HeapSort, arr1);
            Sorts.Measure(Sorts.HeapSort, arr2);
        }
    }
}
    