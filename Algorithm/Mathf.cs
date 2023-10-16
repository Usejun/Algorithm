﻿using System;
using System.Collections;
using System.Linq;
using Algorithm.DataStructure;

namespace Algorithm
{
    public static class Mathf
    {
        public static int Mid(int a, int b) => a / 2 + b / 2;

        public static long Mid(long a, long b) => a / 2 + b / 2;

        public static float Mid(float a, float b) => a / 2 + b / 2;

        public static double Mid(double a, double b) => a / 2 + b / 2;

        public static T Max<T>(T a, T b, IComparer comp)
        {
            comp = comp ?? Comparer.Default;

            return comp.Compare(a, b) > 0 ? a : b;  
        }

        public static T Min<T>(T a, T b, IComparer comp)
        {
            comp = comp ?? Comparer.Default;

            return comp.Compare(a, b) < 0 ? a : b;
        }

        public static T Max<T>(T a, T b)
            where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        public static T Min<T>(T a, T b)
            where T : IComparable<T>
        {
            return a.CompareTo(b) < 0 ? a : b;
        }

        public static T Max<T>(params T[] array)
            where T : IComparable<T>
        {
            T max = array[0];

            foreach (T t in array)
                max = Max(max, t);

            return max;
        }

        public static T Min<T>(params T[] array)
            where T : IComparable<T>
        {
            T min = array[0];

            foreach (T t in array)
                min = Min(min, t);

            return min;
        }

        public static T Max<T>(IComparer comp, params T[] array)
        {
            T max = array[0];

            foreach (T t in array)
                max = Max(max, t, comp);

            return max;
        }

        public static T Min<T>(IComparer comp, params T[] array)
        {
            T min = array[0];

            foreach (T t in array)
                min = Min(min, t, comp);

            return min;
        }

        public static T[] Sorted<T>(T[] array, Action<T[]> sorter = null)
            where T : IComparable<T>
        {
            T[] list = new T[array.Length];
            Array.Copy(array, list, array.Length);

            sorter = sorter ?? Sorts.QuickSort;
            sorter(list);

            return list;
        }

        public static T[] Sorted<T>(Collection<T> collection, Action<T[]> sorter = null)
            where T : IComparable<T>
        {
            return Sorted(collection.ToArray(), sorter);
        }

        public static void Sort<T>(T[] array, Action<T[]> sorter = null)
            where T : IComparable<T>
        {
            sorter = sorter ?? Sorts.QuickSort;
            sorter(array);
        }

        public static int[] CreateArray(int length, int min, int max)
        {
            Random r = new Random();

            return Enumerable.Repeat(0, length).Select(i => r.Next(min, max)).ToArray();
        }
    }
}
