using System;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public class Heap<T> : Iterator<T>
        where T : IComparable<T>
    {
        public override int Count => items.Count;
        public bool IsEmpty => Count == 0;
        public T Top => items[0];

        private bool reverse = false;
        private List<T> items;

        public Heap()
        {
            items = new List<T>();
        }

        public Heap(int capacity)
        {
            items = new List<T>(capacity);
        }

        public Heap(params T[] items)
        {
            this.items = new List<T>();
            PushRange(items);
        }

        public Heap(IEnumerable<T> items)
        {
            this.items = new List<T>();
            PushRange(items);
        }

        public Heap(bool reverse)
        {
            items = new List<T>();
            this.reverse = reverse;
        }

        public Heap(bool reverse, int capacity)
        {
            items = new List<T>(capacity);
            this.reverse = reverse;
        }

        public Heap(bool reverse, params T[] items)
        {
            this.items = new List<T>();
            this.reverse = reverse;
            PushRange(items);
        }

        public Heap(bool reverse, IEnumerable<T> items)
        {
            this.items = new List<T>();
            this.reverse = reverse;      
            PushRange(items);
        }

        public void Push(T item)
        {
            items.Add(item);
            HeapifyUp(Count - 1);
        }

        public void PushRange(params T[] items)
        {
            foreach (var item in items)
                Push(item);            
        }

        public void PushRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                Push(item);            
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new HeapException("Heap is Empty");

            T item = items[0];
            items[0] = items[Count - 1];
            items.RemoveAt(Count - 1);
            HeapifyDown(0);

            return item;
        }

        public T[] PopRange(int length)
        {
            if (length > Count)
                throw new HeapException("Length is bigger than heap count");

            T[] values = new T[length];

            for (int i = 0; i < length; i++)
                values[i] = Pop();

            return values;
        }

        public void Clear()
        {
            items.Clear();           
        }

        private void HeapifyUp(int child)
        {
            while (child != 0)
            {
                int parent = (child - 1) / 2;

                if (Compare(parent, child))
                    Swap(parent, child);

                child = parent;
            }
        }

        private void HeapifyDown(int parent)
        {
            while (Count / 2 > parent)
            {
                int index = parent, left = 2 * parent + 1, right = 2 * parent + 2;

                if (right < Count && Compare(index, right)) 
                    index = right;
                if (left < Count && Compare(index, left))
                    index = left;

                Swap(parent, index);

                if (parent == index) break;

                parent = index;
            }
        }

        private bool Compare(int a, int b) => Compare(items[a], items[b]);
        
        private bool Compare(T a, T b) => reverse ? a.CompareTo(b) < 0 : a.CompareTo(b) > 0;

        private void Swap(int a, int b) => (items[a], items[b]) = (items[b], items[a]);        

        public override object Clone()
        {
            return new Heap<T>(reverse, items);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            Heap<T> heap = new Heap<T>(reverse, items);

            while (!heap.IsEmpty)
                yield return heap.Pop();
        }

        public override T[] ToArray()
        {
            Heap<T> heap = (Heap<T>)Clone();
            T[] items = new T[Count];

            for (int i = 0; i < Count; i++)
                items[i] = heap.Pop();

            return items;
        }

        public static Heap<T> operator +(Heap<T> a, Heap<T> b)
        {
            Heap<T> result = new Heap<T>();

            result.PushRange(a);
            result.PushRange(b);

            return result;
        }

        public static Heap<T> operator *(Heap<T> a, int b)
        {
            Heap<T> result = new Heap<T>();
            T[] items = a.ToArray();

            while (b-- > 0)
                result.PushRange(items);

            return result;
        }
    }

    [Serializable]
    public class HeapException : Exception
    {
        public HeapException() { }
        public HeapException(string message) : base(message) { }
        public HeapException(string message, Exception inner) : base(message, inner) { }
        protected HeapException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
