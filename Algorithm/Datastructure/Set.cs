namespace Algorithm.Datastructure
{
    // 셋 
    public class Set<T> : Collection<T>, IHash<T>
    {
        protected int size;
        protected const int PRIME = 5413;

        protected LinkedList<T>[] list;

        public int Prime => PRIME;

        public Set()
        {
            size = 10000;
            list = new LinkedList<T>[size];

            list = Extensions.Array<LinkedList<T>>(size);
        }

        public Set(int size)
        {
            this.size = size;
            list = new LinkedList<T>[size];

            list = Extensions.Array<LinkedList<T>>(size);
        }

        public override void Add(T value)
        {
            if (Contains(value))
                return;

            list[Hash(value)].Add(value);
            count++;
        }

        public void AddRange(params T[] values)
        {
            foreach (var value in values)
                Add(value);
        }

        public override void Clear()
        {
            count = 0;

            for (int i = 0; i < size; i++)
                list[i].Clear();
        }

        public override bool Remove(T value)
        {
            if (Contains(value))
            {
                count--;
                return list[Hash(value)].Remove(value);
            }
            else
            {
                return false;
            }
        }

        public override bool Contains(T value)
        {
            return list[Hash(value)].Contains(value);
        }

        public override T[] ToArray()
        {
            List<T> array = new List<T>();

            foreach (var linked in list)
                if (!linked.IsEmpty)
                    array.AddRange(linked.ToArray());

            return array.ToArray();
        }

        public bool this[T index]
        {
            get
            {
                return Contains(index);
            }
            set
            {
                if (value && !Contains(index))
                    Add(index);
                else if (!value && Contains(index))
                    Remove(index);
            }
        }

        private int Hash(T value)
        {
            int hash = value.GetHashCode();

            hash *= PRIME;

            hash = hash < 0 ? -hash : hash;

            return hash % (size - 1);
        }

        public int Hashing(T key)
        {
            return Hash(key);
        }

    }
}
