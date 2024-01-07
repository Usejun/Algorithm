namespace Algorithm.Datastructure
{
    // Key Value 형식의 구조체
    public struct Pair<TKey, TValue>
    {
        public TKey Key => key;
        public TValue Value => value;

        private TKey key;
        private TValue value;

        public Pair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public void Deconstruct(out TKey key, out TValue value)
        {
            key = this.key;
            value = this.value;
        }

        public bool Equals(Pair<TKey, TValue> pair)
        {
            return key.Equals(pair.key) && value.Equals(pair.value);
        }

        public override string ToString()
        {
            return $"({key} : {value})";
        }

    }
}
