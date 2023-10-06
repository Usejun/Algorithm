using System;
using Algorithm.DataStructure;

namespace Algorithm
{
    public static class Mathf
    {
        public static int Mid(int a, int b) => a / 2 + b / 2;

        public static long Mid(long a, long b) => a / 2 + b / 2;

        public static float Mid(float a, float b) => a / 2 + b / 2;

        public static double Mid(double a, double b) => a / 2 + b / 2;

        public static T[] Sorted<T>(Collection<T> collection, Func<T[], T[]> sorter = null)
            where T : IComparable<T>
        {
            sorter = sorter ?? Sorts.QuickSort;
            return sorter(collection.ToArray());
        }
    }
}
