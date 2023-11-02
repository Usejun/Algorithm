using System.Collections;

namespace Algorithm
{
    public static class Mathf
    {
        public static double PI = 3.141592;

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

        public static double Pow(int a, int b)
        {
            if (b == 0)
                return 1;

            double pow = a;

            for (int i = 1; i < b; i++)
                pow *= a;
            
            return pow;
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
    }
}
