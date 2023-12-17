using System;
using System.Collections;

namespace Algorithm.Datastructure
{
    // 가변 배열
    public class List<T> : Collection<T>
    {
        public virtual bool IsFull => count == Length;

        protected int Length => source.Length;

        protected T[] source;

        public List()
        {
            source = new T[0];
        }

        public List(int capacity = 0, params T[] values)
        {
            source = new T[capacity];

            AddRange(values);
        }

        public override void Add(T value)
        {
            if (IsFull)
                Resize();

            source[count] = value;
            count++;
        }

        public void AddRange(params T[] values)
        {
            foreach (T value in values)
                Add(value);
        }

        public virtual void Insert(int index, T value)
        {
            if (IsFull)
                Resize();

            for (int i = count; i > index; i--)
                source[i + 1] = source[i];

            source[index] = value;
            count++;
        }

        public override void Clear()
        {
            source = new T[Length];
            count = 0;
        }

        public override bool Remove(T value)
        {
            if (!Extensions.Contains(source, value))
                return false;

            for (int i = 0; i < count; i++)
                if (source[i].Equals(value))
                    for (int j = i + 1; j < count; j++)
                        source[j - 1] = source[j];

            count--;
            return true;
        }

        public virtual void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new Exception("Out of range");

            for (int i = index; i < count; i++)
            {
                if (index + 1 >= count) break;
                source[i] = source[i + 1];
            }

            count--;
        }

        protected virtual void Resize()
        {
            T[] array = new T[Length == 0 ? 4 : Length * 2];

            CopyTo(array, count);

            source = array;
        }

        public override T[] ToArray()
        {
            T[] values = new T[count];

            for (int i = 0; i < count; i++)
                values[i] = source[i];

            return values;
        }

        public virtual T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new Exception("Out of range");
                return source[index];
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new Exception("Out of range");
                source[index] = value;
            }
        }

        public virtual void Sort<T1>(Func<T, T1> order, Action<T[], Func<T, T1>, IComparer> sort = null, IComparer comparer = null)
            where T1 : IComparable<T1>
        {
            sort = sort ?? Extensions.Sorter<T, T1>();
            T[] array = ToArray();
            sort(array, order, comparer);

            Extensions.Copy(source, array, 0, count);
        }
    }
}
