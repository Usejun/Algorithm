using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public class Queue<T> : Iterator<T>, IEnumerable<T>, IEnumerable, ICloneable, IComparable<Queue<T>>
    {
        public bool IsFull => Count == items.Length;

        private T[] items;

        private int front;
        private int back;

        public Queue()
        {
            items = new T[1];
        }

        public Queue(int capacity)
        {
            items = new T[capacity];
        }

        public Queue(params T[] items)
        {
            this.items = new T[1];

            EnqueueRange(items);
        }

        public Queue(IEnumerable<T> items)
        {
            this.items = new T[1];

            EnqueueRange(items);
        }

        public void Enqueue(T item)
        {
            if (IsFull)
                Resize();

            items[back] = item;
            back = (back + 1) % items.Length;
            Count++;
        }

        public void EnqueueRange(params T[] items)
        {
            foreach (var item in items)
                Enqueue(item);            
        }

        public void EnqueueRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                Enqueue(item);
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new EnumerableEmptyException("Queue is Empty");

            T item = items[front];
            front = (front + 1) % items.Length;
            Count--;
            return item;
        }

        public T[] DequeueRange(int length)
        {
            T[] items = new T[length];

            for (int i = 0; i < length; i++)
                items[i] = Dequeue();

            return items;            
        }

        public int IndexOf(T value)
        {
            int front = this.front, back = this.back;

            for (int i = 0; i < Count; i++)
            {
                if (items[front].Equals(value))
                    return front;
                front = (front + 1) % items.Length;
            }

            return -1;
        }

        public bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }

        protected void Resize()
        {
            T[] resized = new T[items.Length * 9 / 4];
            int front = this.front, back = this.back;

            for (int i = 0; i < Count; i++)
            {
                resized[i] = items[front];
                front = (front + 1) % items.Length;
            }

            this.front = 0;
            this.back = Count;
            items = resized;
        }

        public List<T> ToList()
        {
            return new List<T>(this);
        }

        public object Clone()
        {
            return new Queue<T>(this);
        }

        public int CompareTo(Queue<T> other)
        {
            return Count - other.Count;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            int front = this.front;

            for (int i = 0; i < Count; i++)
            {
                yield return items[front];
                front = (front + 1) % items.Length;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            int front = this.front;

            for (int i = 0; i < Count; i++)
            {
                yield return items[front];
                front = (front + 1) % items.Length;
            }
        }

        public override T[] ToArray()
        {
            T[] array = new T[Count];

            for (int i = 0; i < Count; i++)
            {
                array[i] = items[front];
                front = (front + 1) % items.Length;
            }

            return array;
        }

        public static Queue<T> operator +(Queue<T> a, Queue<T> b)
        {
            Queue<T> result = new Queue<T>();

            result.EnqueueRange(a);
            result.EnqueueRange(b);

            return result;
        }

        public static Queue<T> operator *(Queue<T> a, int b)
        {
            Queue<T> result = new Queue<T>();

            while (b-- > 0)
                result.EnqueueRange(a);

            return result;
        }

        public static Queue<T> operator *(Queue<T> a, long b)
        {
            Queue<T> result = new Queue<T>();

            while (b-- > 0)
                result.EnqueueRange(a);

            return result;
        }

        public static Queue<T> operator *(Queue<T> a, float b)
        {
            Queue<T> result = new Queue<T>();

            while (b-- > 0)
                result.EnqueueRange(a);

            return result;
        }

        public static Queue<T> operator *(Queue<T> a, double b)
        {
            Queue<T> result = new Queue<T>();

            while (b-- > 0)
                result.EnqueueRange(a);

            return result;
        }
    }
}
