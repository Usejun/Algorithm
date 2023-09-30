using System;

namespace Algorithm
{
    public static class Trees
    {
        public class SegmentTree<T> 
            where T : IComparable<T>
        {
            T[] array;
            T[] tree;
            int length;

            public int Length => length;

            public SegmentTree(T[] array)
            {
                tree = new T[array.Length * 4];
                this.array = array;
                length = array.Length;

                Init(0, length - 1, 1);
            }

            T Init(int start, int end, int node)
            {
                if (start == end) return tree[node] = array[start];

                int mid = Mathf.Mid(start, end);

                return tree[node] = (dynamic)Init(start, mid, node * 2) + Init(mid + 1, end, node * 2 + 1);
            }

            public T Sum(int left, int right, int start = 0, int end = -1, int node = 1)
            {
                if (end == -1) return Sum(left, right, start, length - 1, node);

                if (left > end || right < start)
                    return default;
                if (left <= start && right >= end)
                    return tree[node];

                int mid = Mathf.Mid(start, end);

                return (dynamic)Sum(left, right, start, mid, node * 2) + Sum(left, right, mid + 1, end, node * 2 + 1);
            }

            public void Update(int index, T diff, int start = 0, int end = -1, int node = 1)
            {
                if (end == -1)
                {
                    Update(index, diff, start, length - 1, node);
                    return;
                }

                if (index < start || index > end)
                    return;

                tree[node] += (dynamic)diff;
                if (start == end)
                    return;

                int mid = Mathf.Mid(start, end);

                Update(index, diff, start, mid, node);
                Update(index, diff, mid, end, node);
            }
        }
    }
}
