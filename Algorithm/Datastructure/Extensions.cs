using System;
using System.Collections;

using Algorithm.Sort;

namespace Algorithm.Datastructure
{
    //배열 추가 기능
    public static class Extensions
    {
        public static Action<T[], Func<T, T1>, IComparer> Sorter<T, T1>()
            where T1 : IComparable<T1>
        {
            return Sorts.TimSort;
        }

        public static int[] Range(int length)
        {
            int[] values = new int[length];

            for (int i = 0; i < length; i++)
                values[i] = i;

            return values;
        }

        public static int[] Range(int min, int max, int step = 1)
        {
            if (step == 0)
                throw new Exception("step is not zero");

            int[] values = new int[(max - min + 1) / Mathf.Abs(step)];

            if (step > 0)
                for (int i = 0; i < values.Length; i++)
                    values[i] = i * step;
            else
                for (int i = 0; i < values.Length; i++)
                    values[i] = (max - 1) + i * step;

            return values;
        }

        public static int[] Create(int length, int min, int max, bool duplication = false)
        {
            Random r = new Random();
            if (duplication)
            {
                if (max - min < length)
                    throw new Exception("Out of range");

                Set<int> set = new Set<int>();

                while (set.Count != length)
                    set.Add(r.Next(min, max));

                return set.ToArray();
            }
            else
            {
                int[] array = new int[length];

                for (int i = 0; i < length; i++)
                    array[i] = r.Next(min, max);

                return array;
            }
        }

        public static T[] Sorted<T, T1>(T[] array, Func<T, T1> order, Action<T[], Func<T, T1>, IComparer> sort = null, IComparer comparer = null)
            where T1 : IComparable<T1>
        {
            T[] list = new T[array.Length];
            Copy(list, array, 0, array.Length);

            Sort(list, order, sort, comparer);

            return list;
        }

        public static void Sort<T, T1>(T[] array, Func<T, T1> order, Action<T[], Func<T, T1>, IComparer> sort = null, IComparer comparer = null)
            where T1 : IComparable<T1>
        {
            sort = sort ?? Sorter<T, T1>();
            sort(array, order, comparer);
        }

        public static bool Contains<T>(T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i].Equals(value))
                    return true;
            return false;
        }

        public static T1[] Convert<T, T1>(T[] array, Func<T, T1> converter)
        {
            T1[] convertedValues = new T1[array.Length];

            for (int i = 0; i < array.Length; i++)
                convertedValues[i] = converter(array[i]);

            return convertedValues;
        }

        public static T[] FilterAnd<T>(T[] array, params Func<T, bool>[] conditions)
        {
            List<T> list = new List<T>();

            foreach (T t in array)
            {
                bool flag = true;
                foreach (var condition in conditions)
                    if (!condition(t))
                    {
                        flag = false;
                        break;
                    }

                if (flag) list.Add(t);
            }
                      
                
            return list.ToArray();            
        }

        public static T[] FilterOr<T>(T[] array, params Func<T, bool>[] conditions)
        {
            List<T> list = new List<T>();

            foreach (T t in array)         
                foreach (var condition in conditions)
                    if (condition(t))
                    {
                        list.Add(t);
                        break;
                    }          

            return list.ToArray();
        }

        public static Pair<int, T>[] ToPairs<T>(T[] array)
        {
            Pair<int, T>[] pairs = new Pair<int, T>[array.Length];

            for (int i = 0; i < array.Length; i++)
                pairs[i] = new Pair<int, T>(i + 1, array[i]);

            return pairs;
        }

        public static void Copy<T>(T[] baseArray, T[] sourceArray, int index, int length)
        {
            if (sourceArray.Length < index + length || baseArray.Length <= index + length)
                throw new ArgumentException("Out of range");

            for (int i = 0; i < length; i++)
                baseArray[index + i] = sourceArray[i];
        }

        public static int BinarySearch<T>(T[] array, T value)
            where T : IComparable<T>
        {
            int low = 0, high = array.Length - 1, mid = array.Length;

            while (low <= high)
            {
                mid = Mathf.Mid(low, high);

                if (array[mid].CompareTo(value) > 0)
                    high = mid - 1;
                else
                    low = mid + 1;
            }

            return high;
        }

        public static int IndexOf<T>(T[] array, T value)
            where T : IComparable<T>
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i].Equals(value))
                    return i;
            return -1;
        }

        public static int Count<T>(T[] array, Func<T, bool> match)
        {
            int count = 0;

            for (int i = 0; i < array.Length; i++)
                if (match(array[i]))
                    count++;

            return count;
        }

        public static T[] Array<T>(int length)
            where T : new()
        {
            T[] values = new T[length];

            for (int i = 0; i < length; i++)
                values[i] = new T();

            return values;

        }

        public static T[,] Array<T>(int row, int column)
            where T : new()
        {
            T[,] values = new T[column, row];

            for (int i = 0; i < column; i++)
                for (int j = 0; j < row; j++)
                    values[i, j] = new T();

            return values;

        }
    }
}
