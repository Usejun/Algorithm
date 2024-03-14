using System;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public class Stack<T> : Iterator<T>
    {
        public bool IsFull => Count == items.Length;
        public bool IsEmpty => Count == 0;

        private T[] items;

        private int back;

        public Stack()
        {
            items = new T[1];
        }

        public Stack(int capacity)
        {
            items = new T[capacity];
        }

        public Stack(params T[] items)
        {
            this.items = new T[1];

            PushRange(items);
        }

        public Stack(IEnumerable<T> items)
        {
            this.items = new T[1];

            PushRange(items);
        }

        public void Push(T item)
        {
            if (IsFull)
                Resize();

            items[back++] = item;
            Count++;
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
                throw new StackException("Stack is Empty");

            T item = items[back--];
            Count--;
            return item;
        }

        public T[] PopRange(int length)
        {
            if (length > Count)
                throw new StackException("Length is bigger than stack count");

            T[] items = new T[length];

            for (int i = 0; i < length; i++)
                items[i] = Pop();

            return items;
        }

        public int IndexOf(T value)
        {
            for (int i = 0; i < Count; i++)            
                if (items[i].Equals(value))                
                    return i;                            

            return -1;
        }

        public bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }

        public void Clear()
        {
            back = 0;
            Count = 0;
        }

        protected void Resize()
        {
            T[] resized = new T[items.Length * 9 / 4];

            for (int i = 0; i < Count; i++)
                resized[i] = items[i];

            back = Count;
            items = resized;
        }

        public List<T> ToList()
        {
            return new List<T>(this);
        }

        public override object Clone()
        {
            return new Stack<T>(this);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            for (int i = back; i >= 0; i--)
                yield return items[i];
        }

        public override T[] ToArray()
        {
            T[] array = new T[Count];

            for (int i = 0; i < Count; i++)
                array[i] = items[back - i - 1];

            return array;
        }

        public static Stack<T> operator +(Stack<T> a, Stack<T> b)
        {
            Stack<T> result = new Stack<T>();

            result.PushRange(a);
            result.PushRange(b);

            return result;
        }

        public static Stack<T> operator *(Stack<T> a, int b)
        {
            Stack<T> result = new Stack<T>();
            T[] items = a.ToArray();

            while (b-- > 0)
                result.PushRange(items);

            return result;
        }
    }


    [Serializable]
    public class StackException : Exception
    {
        public StackException() { }
        public StackException(string message) : base(message) { }
        public StackException(string message, Exception inner) : base(message, inner) { }
        protected StackException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
