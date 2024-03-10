using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public abstract class Iterator<T> : IEnumerable, IEnumerable<T>
    {
        public int Count { get; protected set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in this)            
                yield return item;                    
        }
  
        public abstract IEnumerator<T> GetEnumerator();
        public abstract T[] ToArray();

        public override string ToString()
        {
            return $"[{string.Join(", ", ToArray())}]";
        }
    }
}
