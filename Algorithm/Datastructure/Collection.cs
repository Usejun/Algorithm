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
        public virtual Pair<int, T>[] ToPairs()
        {
            T[] values = ToArray();
            Pair<int, T>[] pairs = new Pair<int, T>[count];

            for (int i = 0; i < count; i++)
                pairs[i] = new Pair<int, T>(i + 1, values[i]);

            return pairs;
        }
        public virtual T1[] Convert<T1>(Func<T, T1> converter)
        {
            T[] values = ToArray();
            T1[] convertedValues = new T1[count];

            for (int i = 0; i < count; i++)
                convertedValues[i] = converter(values[i]);

            return convertedValues;
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
}
