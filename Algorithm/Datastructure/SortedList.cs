using System;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    // 오름차순으로 정렬된 리스트
    public class SortedList<T> : List<T>
    {
        IComparer<T> comparer;

        public SortedList(IComparer<T> comparer = null)
        {
            if (comparer == null
                && (!Extensions.Contains(typeof(T).GetInterfaces(), typeof(IComparable))
                && !Extensions.Contains(typeof(T).GetInterfaces(), typeof(IComparable<T>))))
                throw new Exception("This type does not have ICompable.");

            this.comparer = comparer ?? Comparer<T>.Default;
            source = new T[0];
        }

        public override void Add(T value)
        {
            if (IsFull)
                Resize();

            Insert(BinarySearch(value), value);
        }

        int BinarySearch(T value)
        {
            int low = 0, mid = count - 1, high = count - 1;

            while (low <= high)
            {
                mid = Mathf.Mid(low, high);

                if (comparer.Compare(source[mid], value) > 0)
                    high = mid - 1;
                else
                    low = mid + 1;
            }

            return low;
        }
    }
}
