using System;
using System.Text;
using Algorithm.Technique;
using Algorithm.DataStructure;

namespace Algorithm
{
    internal class Program
    {
        public class Point
        { 
            public int x; 
            public int y;
            public int z;

            public Point(int x = 0, int y = 0, int z = 0)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public void Deconstruct(out int x, out int y, out int z)
            {
                x = this.x;
                y = this.y;
                z = this.z;
            }
            
            public static Point operator +(Point a, Point b)
            {
                return new Point(a.x + b.x, a.y + b.x, a.z + b.z);
            }
            
            public static Point operator -(Point a, Point b)
            {
                return new Point(a.x - b.x, a.y - b.x, a.z - b.z);
            }

            public static Point operator *(Point a, Point b)
            {
                return new Point(a.x * b.x, a.y * b.x, a.z * b.z);
            }

            public static Point operator /(Point a, Point b)
            {
                if (b.x * b.y * b.y == 0)
                    throw new DivideByZeroException();

                return new Point(a.x / b.x, a.y / b.y, a.z / b.z);
            }

            public static Point operator %(Point a, Point b)
            {
                return new Point(a.x % b.x, a.y % b.x, a.z % b.z);
            }

            public static bool operator <(Point a, Point b)
            {
                return a.x == b.x ? a.y == b.y ? a.z < b.z : a.y < b.y : a.x < b.x; 
            }

            public static bool operator >(Point a, Point b)
            {
                return a.x == b.x ? a.y == b.y ? a.z > b.z : a.y > b.y : a.x > b.x;
            }

            public static bool operator ==(Point a, Point b)
            {
                return a.x == b.x && a.y == b.y && a.z == b.z; 
            }

            public static bool operator !=(Point a, Point b)
            {
                return a.x != b.x || a.y != b.y || a.z != b.z;
            }

            public override bool Equals(object obj)
            {
                return this == obj as Point;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                string str = "";               

                var fields = typeof(Point).GetFields();          

                foreach (var field in fields)
                   str += ($"({field.Name} : {field.GetValue(this)})");

                return str;
            }

            public static Point[] Create(int length, int min, int max)
            {
                Point[] points = new Point[length];

                Random r = new Random();

                for (int i = 0; i < length; i++)
                    points[i] = new Point(r.Next(min, max), r.Next(min, max), r.Next(min, max));

                return points;
            }
        }

        public static void Init()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
        }

        static void Main(string[] args)
        {
            Init();
            int length = 1000000;


        }
    }
}
    