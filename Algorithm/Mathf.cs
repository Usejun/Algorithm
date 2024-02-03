using System;
using System.Collections;

namespace Algorithm
{
    public static class Mathf
    {
        public static double PI = 3.141592;

        public static int Sum(int[] array)
        {
            int sum = 0;
            foreach (int i in array)
                sum += i;

            return sum;
        }

        public static long Sum(long[] array)
        {
            long sum = 0;
            foreach (long i in array)
                sum += i;

            return sum;
        }

        public static float Sum(float[] array)
        {
            float sum = 0;
            foreach (float i in array)
                sum += i;
            return sum;
        }

        public static double Sum(double[] array)
        {
            double sum = 0;
            foreach (double i in array)
                sum += i;
            return sum;
        }

        public static int Mid(int a, int b)
        {
            if (a > b) (a, b) = (b, a);
            return a + ((b - a) / 2);
        }

        public static long Mid(long a, long b)
        {
            if (a > b) (a, b) = (b, a);
            return a + ((b - a) / 2);
        }

        public static float Mid(float a, float b)
        {
            if (a > b) (a, b) = (b, a);
            return a + ((b - a) / 2);
        }

        public static double Mid(double a, double b)
        {
            if (a > b) (a, b) = (b, a);
            return a + ((b - a) / 2);
        }

        public static T Max<T>(T a, T b, IComparer comparer = null)
        {
            comparer = comparer ?? Comparer.Default;

            return comparer.Compare(a, b) > 0 ? a : b;  
        }

        public static T Min<T>(T a, T b, IComparer comparer = null)
        {
            comparer = comparer ?? Comparer.Default;

            return comparer.Compare(a, b) < 0 ? a : b;
        }

        public static T Max<T>(IComparer comparer = null, params T[] values)
        {
            T max = values[0];

            foreach (T t in values)
                max = Max(max, t, comparer);

            return max;
        }

        public static T Min<T>(IComparer comparer = null, params T[] values)
        {
            T min = values[0];

            foreach (T t in values)
                min = Min(min, t, comparer);

            return min;
        }

        public static double Pow(double a, double b)
        {
            if (b == 0) return 0;
            if (b == 1) return a;
            if (b%2==1) return a * Pow(a, b - 1);
            double p = Pow(a, b / 2);
            return p * p;
        }

        public static double Pow(double a, double b, double m)
        {
            if (b == 0) return 0;
            if (b == 1) return a % m;
            if (b % 2 == 1) return a * Pow(a, b - 1) % m;
            double p = Pow(a, b / 2);
            return p * p % m;
        }

        public static int Abs(int a)
        {
            return a < 0 ? -a : a;
        }

        public static long Abs(long a)
        {
            return a < 0 ? -a : a;
        }

        public static float Abs(float a)
        {
            return a < 0 ? -a : a;
        }

        public static double Abs(double a)
        {
            return a < 0 ? -a : a;
        }

        public static double Gcd(int a, int b) => b == 0 ? a : Gcd(b, a % b);

        public static double Round(double a, int digits)
        {
            if (digits < 0 || digits > 15)
                return 0;

            double pow = Pow(10, digits);

            a *= pow;
            a -= a % 1;
            a /= pow;

            return a;
        }

        public static long Permutation(int n, int k)
        {
            if (n < k) return 0;
            if (n == k || k == 0) return 1;

            k = n - k;

            long sum = 1;

            for (int i = k + 1; i <= n; i++)
                sum *= i;

            return sum;
        }

        public static long Combination(int n, int k)
        {
            if (n < k) return 0;
            if (n == k || k == 0) return 1;
            
            long sum = Permutation(n, k);
            for (int i = 2; i <= k; i++)
                sum /= i;

            return sum;                 
        }

        public static long Factorial(int n)
        {
            int sum = 1;

            for (int i = 2; i <= n; i++)
                sum *= i;

            return sum;
        }
    }
}
