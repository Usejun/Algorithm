using System;

namespace Algorithm.Datastructure
{
    public struct Pair<TKey, TValue> : IComparable<Pair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public TKey Key;
        public TValue Value;

        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public void Deconstruct(out TKey key, out TValue value)
        {
            key = Key;
            value = Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Pair<TKey, TValue> pair && pair.Key.CompareTo(Key) == 0; 
        }

        public override string ToString()
        {
            return $"({Key}: {Value})";
        }

        public int CompareTo(Pair<TKey, TValue> other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
