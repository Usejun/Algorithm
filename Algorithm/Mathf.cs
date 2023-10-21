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

        public static T Max<T>(T a, T b, IComparer comp = null)
        {
            if (!(typeof(T).GetInterfaces().Contains(typeof(IComparable))))
                throw new Exception("IComapar is not implemented.");

            comp = comp ?? Comparer.Default;

            return comp.Compare(a, b) > 0 ? a : b;  
        }

        public static T Min<T>(T a, T b, IComparer comp)
        {
            comp = comp ?? Comparer.Default;

            return comp.Compare(a, b) < 0 ? a : b;
        }

        public static T Max<T>(IComparer comp = null, params T[] values)
        {
            T max = values[0];

            foreach (T t in values)
                max = Max(max, t, comp);

            return max;
        }

        public static T Min<T>(IComparer comp = null, params T[] values)
        {
            T min = values[0];

            foreach (T t in values)
                min = Min(min, t, comp);

            return min;
        }        

    }
}
