using Algorithm.Sort;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public class List<T> : Iterator<T>, IList<T>, ICloneable, IComparable<List<T>>
    {
        public bool IsFull => items.Length <= Count;
        public bool IsReadOnly => true;

        private T[] items;

        public T this[int index] 
        {
            get 
            {
                if (OutOfRange(index))
                    throw new IndexOutOfRangeException();

                return items[index];    
            } 
            set
            {
                if (OutOfRange(index))
                    throw new IndexOutOfRangeException();

                items[index] = value;
            }
        }

        public List() 
        {
            items = new T[1];
        }

        public List(int capacity)
        {
            items = new T[capacity];
        }

        public List(params T[] items)
        {
            this.items = new T[1];
            AddRange(items);
        }

        public List(IEnumerable<T> items)
        {
            this.items = new T[1];
            AddRange(items);       
        }

        public void Add(T item)
        {
            if (IsFull) Resize();

            items[Count++] = item;          
        }

        public void AddRange(params T[] items)
        {
            foreach (T item in items)
                Add(item);            
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
                Add(item);            
        }
        
        public void Insert(int index, T item)
        {
            if (IsFull) Resize();

            for (int i = index + 1; i < Count; i++)            
                items[i] = items[i - 1];
            items[index] = item;

            Count++;    
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
                if (items[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (OutOfRange(index)) return;

            for (int i = index; i < Count - 1; i++)            
                items[i] = items[i + 1];     
            Count--;
        }

        public bool Contains(T item) => IndexOf(item) != -1;

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
                if (items[i].Equals(item))
                    return i;

            return -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; arrayIndex + i < array.Length; i++)            
                array[arrayIndex + i] = items[i];            
        }

        public override T[] ToArray()
        {
            T[] result = new T[Count];

            for (int i = 0; i < Count; i++)
                result[i] = items[i];

            return result;            
        }

        public void Clear()
        {
            Count = 0;
        }

        public void Sort<T1>(Func<T, T1> order)
            where T1 : IComparable<T1>
        {
            T[] items = ToArray();

            Sorts.TimSort(items, order);

            for (int i = 0; i < Count; i++)
                this.items[i] = items[i];            
        }

        public List<T> Range(int left = 0, int right = -1, int step = 1)
        { 
            right = right == -1 ? Count : right;

            List<T> result = new List<T>();
            if (step == 0 || left < 0 || right < 0) throw new IndexOutOfRangeException();
            else if (step > 0)
            {
                if (left > right)
                    (left, right) = (right, left);

                for (int i = left; i < right; i += step)
                    result.Add(this[i]);
            }
            else
            {
                if (left < right)
                    (left, right) = (right, left);

                for (int i = left; i >= right; i += step)
                    result.Add(this[i]);
            }

            return result;
        }

        public int Match(Func<T, bool> match = null)
        {
            match = match is null ? i => true : match;
            int count = 0;

            for (int i = 0; i < Count; i++)
                if (match(this[i]))
                    count++;

            return count;
        }

        protected bool OutOfRange(int index)
        {
            return 0 > index || index >= Count;
        }

        protected void Resize()
        {
            T[] resized = new T[Count * 9 / 4];

            for (int i = 0; i < Count; i++)            
                resized[i] = items[i];

            items = resized;
        }

        public object Clone()
        {
            return new List<T>(this);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)            
                yield return items[i];            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return items[i];
        }

        public int CompareTo(List<T> other)
        {
            return Count - other.Count;
        }

        public static List<T> operator +(List<T> a, List<T> b)
        {
            List<T> result = new List<T>();

            result.AddRange(a);
            result.AddRange(b);

            return result;
        }

        public static List<T> operator *(List<T> a, int b)
        {
            List<T> result = new List<T>();

            while(b-- > 0)
                result.AddRange(a);           

            return result;
        }

        public static List<T> operator *(List<T> a, long b)
        {
            List<T> result = new List<T>();

            while (b-- > 0)
                result.AddRange(a);

            return result;
        }

        public static List<T> operator *(List<T> a, float b)
        {
            List<T> result = new List<T>();

            while (b-- > 0)
                result.AddRange(a);

            return result;
        }

        public static List<T> operator *(List<T> a, double b)
        {
            List<T> result = new List<T>();

            while (b-- > 0)
                result.AddRange(a);

            return result;
        }    
    }


    [Serializable]
    public class EnumerableEmptyException : Exception
    {
        public EnumerableEmptyException() { }
        public EnumerableEmptyException(string message) : base(message) { }
        public EnumerableEmptyException(string message, Exception inner) : base(message, inner) { }
        protected EnumerableEmptyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
