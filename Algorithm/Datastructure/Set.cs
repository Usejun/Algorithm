
using System;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public class Set<T> : Iterator<T>
    {
        private DoublyLinkedList<T>[] table;
        private int capacity;

        public Set() 
        {
            capacity = 1 << 16;     
            Clear();
        }

        public Set(int capacity)
        {
            this.capacity = capacity;
            Clear();
        }

        public Set(params T[] items)
        {
            capacity = 1 << 16;
            Clear();
            Union(items);
        }

        public Set(IEnumerable<T> items)
        {
            capacity = 1 << 16;
            Clear();
            Union(items);
        }        

        public void Add(T item)
        {
            if (Contains(item)) return;

            table[Hash(item)].AddLast(item);
            Count++;
        }

        public bool Remove(T item)
        {
            if (table[Hash(item)].Remove(item))
            {
                Count--;
                return true;    
            }
            return false;
        }

        public bool Contains(T item)
        {
            return table[Hash(item)].Contains(item);
        }

        public void Clear()
        {
            Count = 0;

            table = new DoublyLinkedList<T>[capacity];

            for (int i = 0; i < capacity; i++)
                table[i] = new DoublyLinkedList<T>();            
        }


        public void Union(params T[] items)
        {
            foreach (var item in items)                        
                Add(item);
        }

        public void Union(IEnumerable<T> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public void Intersection(params T[] items)
        {
            List<T> list = new List<T>();

            foreach (var item in items)
                if (Contains(item))
                    list.Add(item);

            Clear();
            Union(list);
        }

        public void Intersection(IEnumerable<T> items)
        {
            List<T> list = new List<T>();

            foreach (var item in items)
                if (Contains(item))
                    list.Add(item);

            Clear();
            Union(list);
        }

        public void Except(params T[] items)
        {
            foreach (var item in items)            
                if (Contains(item))
                    Remove(item);            
        }

        public void Except(IEnumerable<T> items)
        {
            foreach (var item in items)
                if (Contains(item))
                    Remove(item);
        }


        public int Hash(T item)
        {
            return Math.Abs(item.GetHashCode() % capacity);
        }

        public override object Clone()
        {
            return new Set<T>(this);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < capacity; i++)
                foreach (var item in table[i])
                    yield return item;
        }

        public override T[] ToArray()
        {
            T[] items = new T[Count];
            int k = 0;

            for (int i = 0; i < capacity; i++)   
                foreach (var item in table[i])
                     items[k++] = item;

            return items;
        }        

        
        public static Set<T> operator +(Set<T> a, Set<T> b)
        {
            Set<T> result = new Set<T>();

            result.Union(a);
            result.Union(b);

            return result;
        }

        public static Set<T> operator -(Set<T> a, Set<T> b)
        {
            Set<T> result = new Set<T>(a);

            result.Except(b);

            return result;
        }

        public static Set<T> operator ^(Set<T> a, Set<T> b)
        {
            Set<T> result = new Set<T>(a);

            result.Intersection(b);

            return result;
        }

    }
}
