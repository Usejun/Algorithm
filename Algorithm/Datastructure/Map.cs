using System;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public class Map<TKey, TValue> : Iterator<Pair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {      
        public Set<TKey> Keys { get; private set; } = new Set<TKey>();
        public Set<TValue> Values { get; private set; } = new Set<TValue>();

        private DoublyLinkedList<Pair<TKey, TValue>>[] table;
        private int capacity;

        public TValue this[TKey key]
        {
            get
            {
                foreach (var pair in table[Hash(key)])
                    if (pair.Key.CompareTo(key) == 0)
                        return pair.Value;

                throw new MapException("This key doesn't exist.");
            }
            set
            {
                foreach (var pair in table[Hash(key)])
                    if (pair.Key.CompareTo(key) == 0)
                    {
                        table[Hash(key)].NodeOfNext(new Pair<TKey, TValue>(key, value)).Value = new Pair<TKey, TValue>(key, value);
                        return;
                    }

                throw new MapException("This key doesn't exist.");
            }
        }

        public Map()
        {
            capacity = 1 << 16;
            Clear();
        }

        public Map(int capacity)
        {
            this.capacity = capacity;
            Clear();
        }

        public Map(params Pair<TKey, TValue>[] pairs)
        {
            capacity = 1 << 16;
            Clear();
            AddRange(pairs);
        }

        public Map(IEnumerable<Pair<TKey, TValue>> pairs)
        {
            capacity = 1 << 16;
            Clear();
            AddRange(pairs);
        }

        public void Add(TKey key, TValue value)
        {
            if (table[Hash(key)].Contains(new Pair<TKey, TValue>(key, value)))
            {
                Values.Add(table[Hash(key)].NodeOfNext(new Pair<TKey, TValue>(key, value)).Value.Value);
                table[Hash(key)].NodeOfNext(new Pair<TKey, TValue>(key, value)).Value = new Pair<TKey, TValue>(key, value);
            }
            else
                table[Hash(key)].AddLast(new Pair<TKey, TValue>(key, value));

            Keys.Add(key);
            Values.Add(value);
        }

        public void Add(Pair<TKey, TValue> pair)
        {
            Add(pair.Key, pair.Value);            
        }

        public void AddRange(params Pair<TKey, TValue>[] pairs)
        {
            foreach (var pair in pairs)
                Add(pair);            
        }

        public void AddRange(IEnumerable<Pair<TKey, TValue>> pairs)
        {
            foreach (var pair in pairs)
                Add(pair);
        }

        public bool Remove(TKey key, TValue value)
        {
            if (table[Hash(key)].Remove(new Pair<TKey, TValue>(key, value)))
            {
                Keys.Remove(key);
                Values.Remove(value);
                return true;
            }

            return false;
        }

        public bool Remove(Pair<TKey, TValue> pair)
        {
            return Remove(pair.Key, pair.Value);
        }

        public bool ContainsKey(TKey key)
        {
            foreach (var pair in table[Hash(key)])
                if (pair.Key.CompareTo(key) == 0)
                    return true;
            return false;            
        }

        public bool ContainsValue(TKey key, TValue value)
        {
            foreach (var pair in table[Hash(key)])
                if (pair.Equals(value))
                    return true;

            return false;            
        }

        public int Hash(TKey key)
        {
            return Math.Abs(key.GetHashCode() % capacity);
        }

        public void Clear()
        {
            Count = 0;
            table = new DoublyLinkedList<Pair<TKey, TValue>>[capacity];

            for (int i = 0; i < capacity; i++)
                table[i] = new DoublyLinkedList<Pair<TKey, TValue>>();            
        }

        public override object Clone()
        {
            return new Map<TKey, TValue>(this);
        }

        public override IEnumerator<Pair<TKey, TValue>> GetEnumerator()
        {
            foreach (var line in table)
                foreach (var item in line)
                    yield return item;                           
        }

        public override Pair<TKey, TValue>[] ToArray()
        {
            Pair<TKey, TValue>[] pairs = new Pair<TKey, TValue>[Count];
            int k = 0;

            foreach (var line in table)
                foreach (var item in line)
                    pairs[k++] = item;

            return pairs;
        }
    }


    [Serializable]
    public class MapException : Exception
    {
        public MapException() { }
        public MapException(string message) : base(message) { }
        public MapException(string message, Exception inner) : base(message, inner) { }
        protected MapException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
