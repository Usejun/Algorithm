using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    //기본 배열 추상 클래스     
    public abstract class Collection<T> : IEnumerable<T>, ICollection<T>
    {
        public virtual bool IsReadOnly => false;
        public virtual int Count => count;

        protected int count = 0;

        public virtual bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }
        public virtual void CopyTo(T[] array, int length)
        {
            Extensions.Copy(array, ToArray(), 0, length);
        }
        public virtual int IndexOf(T value)
        {
            T[] array = ToArray();

            for (int i = 0; i < count; i++)
                if (value.Equals(array[i]))
                    return i;

            return -1;
        }
        public virtual List<T> ToList()
        {
            return new List<T>(values: ToArray());
        }
        public virtual List<Pair<int, T>> ToPairs()
        {
            return new List<Pair<int, T>>(values: Extensions.ToPairs(ToArray()));
        }
        public virtual List<T1> Convert<T1>(Func<T, T1> converter)
        {
            return new List<T1>(values: Extensions.Convert(ToArray(), converter));
        }
        public virtual int Counting(Func<T, bool> match = null)
        {
            match = match ?? (i => true);
            return Extensions.Count(ToArray(), match);
        }
        public virtual IEnumerator<T> GetEnumerator()
        {
            T[] array = ToArray();
            foreach (var item in array)
                yield return item;
        }
        public override string ToString()
        {
            return string.Join(" ", ToArray());
        }

        public abstract T[] ToArray();
        public abstract void Add(T value);
        public abstract bool Remove(T value);
        public abstract void Clear();

        IEnumerator IEnumerable.GetEnumerator()
        {
            T[] array = ToArray();
            foreach (var item in array)
                yield return item;
        }
    }

    public class CollectionIndexException : Exception
    {
        public CollectionIndexException() { }
        public CollectionIndexException(string message) : base(message) { }
        public CollectionIndexException(string message, Exception inner) : base(message, inner) { }
        protected CollectionIndexException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class CollectionEmptyException : Exception
    {
        public CollectionEmptyException() { }
        public CollectionEmptyException(string message) : base(message) { }
        public CollectionEmptyException(string message, Exception inner) : base(message, inner) { }
        protected CollectionEmptyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class CollectionComparableException : Exception
    {
        public CollectionComparableException() { }
        public CollectionComparableException(string message) : base(message) { }
        public CollectionComparableException(string message, Exception inner) : base(message, inner) { }
        protected CollectionComparableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class CollectionExistException : Exception
    {
        public CollectionExistException() { }
        public CollectionExistException(string message) : base(message) { }
        public CollectionExistException(string message, Exception inner) : base(message, inner) { }
        protected CollectionExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
