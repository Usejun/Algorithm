using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public static class Iterator
    {
        public static int[] Range(int left, int right, int step = 1)
        {
            if (step == 0 || left < 0 || right < 0) throw new IndexOutOfRangeException();

            int index = 0;

            if (step > 0)
            {
                if (left > right) (left, right) = (right, left);

                int[] result = new int[right - left];

                for (int i = left; i < right; i += step, index++)
                    result[index] = i;

                return result;
            }
            else
            {
                if (left < right) (left, right) = (right, left);

                int[] result = new int[left - right];

                for (int i = left; i > right; i += step, index++)
                    result[index] = i;

                return result;
            }

        }        
    }

    public abstract class Iterator<T> : IEnumerable, IEnumerable<T>, ICloneable, IComparable<Iterator<T>>
    {
        public virtual int Count { get; protected set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in this)            
                yield return item;                    
        }
         
        public abstract object Clone();
        public abstract IEnumerator<T> GetEnumerator();
        public abstract T[] ToArray();      

        public override string ToString()
        {
            return $"[{string.Join(", ", ToArray())}]";
        }

        public int CompareTo(Iterator<T> other)
        {
            return Count.CompareTo(other.Count);
        }        
    }   
}
