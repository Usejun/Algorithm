using System;

namespace Algorithm.Datastructure
{
    // 딕셔너리
    public class Dictionary<TKey, TValue> : Set<Pair<TKey, TValue>>, IHash<TKey>
    {
        public TKey[] Keys => Convert(pair => pair.Key).ToArray();
        public TValue[] Values => Convert(pair => pair.Value).ToArray();

        public Dictionary() : base() { }

        public Dictionary(int size) : base(size) { }

        public void Add(TKey key, TValue value)
        {
            int hashCode = Hash(key);
            Pair<TKey, TValue> node = new Pair<TKey, TValue>(key, value);

            if (!ContainsKey(key))
            {
                list[hashCode].Add(node);
                count++;
            }
            else
                this[key] = value;
        }

        public override void Add(Pair<TKey, TValue> pair)
        {
            Add(pair.Key, pair.Value);
        }

        public bool Remove(TKey key, TValue value)
        {
            if (Contains(key, value))
            {
                var removePair = new Pair<TKey, TValue>(key, value);
                count--;
                return list[Hash(key)].Remove(removePair);
            }
            return false;
        }

        public override bool Remove(Pair<TKey, TValue> pair)
        {
            return Remove(pair.Key, pair.Value);
        }

        public bool Contains(TKey key, TValue value)
        {
            return Contains(new Pair<TKey, TValue>(key, value));
        }

        public override bool Contains(Pair<TKey, TValue> pair)
        {
            return ContainsKey(pair.Key) && list[Hash(pair.Key)].Equals(pair.Value);
        }

        public bool ContainsKey(TKey key)
        {
            return Extensions.Contains(Keys, key);
        }

        public bool ContainsValue(TValue value)
        {
            return Extensions.Contains(Values, value);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                    throw new CollectionExistException("does not exist");

                var pairs = list[Hash(key)];

                for (int i = 0; i < count; i++)
                    if (pairs[i].Key.Equals(key))
                        return pairs[i].Value;

                throw new CollectionExistException("does not exist");
            }
            set
            {
                if (!ContainsKey(key))
                {
                    var newPair = new Pair<TKey, TValue>(key, value);

                    var pairs = list[Hash(key)];
                    var oldPair = pairs.GetNode(pairs.IndexOf(newPair));

                    oldPair.Value = newPair;
                }
                
                throw new CollectionExistException("does not exist");
            }
        }

        private int Hash(TKey key)
        {
            int hash = key.GetHashCode();

            hash *= PRIME;

            hash = hash < 0 ? -hash : hash;

            return hash % (size - 1);
        }

        public int Hashing(TKey key)
        {
            return Hash(key);
        }
    }
}
