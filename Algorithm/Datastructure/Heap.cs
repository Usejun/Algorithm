using System.Collections.Generic;
using System;

namespace Algorithm.Datastructure
{
    // 힙, 기본 : 최소 힙   
    public class Heap<T> : Collection<T>
    {
        public T Top => heap[0];

        List<T> heap;
        int capacity;
        bool reverse;
        IComparer<T> comparer;

        public Heap()
        {
            if ((!Extensions.Contains(typeof(T).GetInterfaces(), typeof(IComparable))
                && !Extensions.Contains(typeof(T).GetInterfaces(), typeof(IComparable<T>))))
                throw new Exception("This type does not have ICompable.");

            comparer = Comparer<T>.Default;
            capacity = 0;
            reverse = false;
        }

        public Heap(IComparer<T> comparer = null, bool reverse = false, int capacity = 0, params T[] values)
        {
            if (comparer == null
                && (!Extensions.Contains(typeof(T).GetInterfaces(), typeof(IComparable))
                && !Extensions.Contains(typeof(T).GetInterfaces(), typeof(IComparable<T>))))
                throw new Exception("This type does not have ICompable.");

            this.comparer = comparer ?? Comparer<T>.Default;
            this.capacity = capacity;
            this.reverse = reverse;

            Clear();

            foreach (T value in values)
                Push(value);
        }

        public void Push(T value)
        {
            heap.Add(value);

            int now = heap.Count - 1;

            while (now != 0)
            {
                int next = (now - 1) / 2;

                if (Compare(heap[now], heap[next]) > 0)
                    (heap[now], heap[next]) = (heap[next], heap[now]);

                now = next;
            }

            count++;
        }

        public void PushRange(params T[] values)
        {
            foreach (T value in values)
                Push(value);
        }

        public T Pop()
        {
            if (count == 0)
                throw new Exception("Empty Heap");

            T value = heap[0];

            int last = heap.Count - 1;
            heap[0] = heap[last];
            heap.RemoveAt(last);
            count--;

            Heapify(0);

            return value;
        }

        public override void Add(T value)
        {
            Push(value);
        }

        public override bool Remove(T value)
        {
            if (Top.Equals(value))
            {
                Pop();
                return true;
            }
            return false;
        }

        public override void Clear()
        {
            heap = new List<T>(capacity: capacity);
            count = 0;
        }

        public override T[] ToArray()
        {
            T[] array = heap.ToArray();

            for (int i = array.Length - 1; i >= 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                int root = 0, c = 1;

                do
                {
                    c = 2 * root + 1;

                    if (c < i - 1 && Compare(array[c], array[c + 1]) < 0)
                        c++;

                    if (c < i && Compare(array[root], array[c]) < 0)
                        (array[root], array[c]) = (array[c], array[root]);

                    root = c;
                } while (c < i);
            }

            return array;
        }

        public override bool Contains(T value)
        {
            return heap.Contains(value);
        }

        private int Compare(T x, T y)
        {
            return reverse ? comparer.Compare(y, x) : comparer.Compare(x, y);
        }

        private void Heapify(int index)
        {
            int c = 2 * index + 1;

            if (c < count - 1 && Compare(heap[c], heap[c + 1]) < 0)
                c++;
            if (c < count && Compare(heap[c], heap[index]) > 0)
                (heap[c], heap[index]) = (heap[index], heap[c]);

            if (c < count / 2)
                Heapify(c);
        }
    }
}
