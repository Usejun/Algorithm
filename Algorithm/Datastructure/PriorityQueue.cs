using System;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public class PriorityQueue<TKey, TValue> : Iterator<Pair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public override int Count => heap.Count;
        public bool IsEmpty => heap.IsEmpty;
        public Pair<TKey, TValue> Top => heap.Top;

        private Heap<Pair<TKey, TValue>> heap;

        public PriorityQueue()
        {
            heap = new Heap<Pair<TKey, TValue>>();
        }

        public PriorityQueue(params Pair<TKey, TValue>[] pairs)
        {
            heap = new Heap<Pair<TKey, TValue>>(pairs);
        }

        public PriorityQueue(IEnumerable<Pair<TKey, TValue>> pairs)
        {
            heap = new Heap<Pair<TKey, TValue>>(pairs);
        }

        public PriorityQueue(bool reverse, params Pair<TKey, TValue>[] pairs)
        {
            heap = new Heap<Pair<TKey, TValue>>(reverse, pairs);
        }

        public PriorityQueue(bool reverse, IEnumerable<Pair<TKey, TValue>> pairs)
        {
            heap = new Heap<Pair<TKey, TValue>>(reverse, pairs);
        }

        public void Enqueue(TKey key, TValue value)
        {
            Enqueue(new Pair<TKey, TValue>(key, value));
        }

        public void Enqueue(Pair<TKey, TValue> pair)
        {
            heap.Push(pair);
        }

        public void EnqueueRange(params Pair<TKey, TValue>[] items)
        {
            foreach (var item in items)
                Enqueue(item);            
        }

        public void EnqueueRange(IEnumerable<Pair<TKey, TValue>> items)
        {
            foreach (var item in items)
                Enqueue(item);
        }

        public Pair<TKey, TValue> Dequeue()
        {
            if (IsEmpty) throw new PriorityQueueException("Priority queue is empty");
            return heap.Pop();
        }

        public Pair<TKey, TValue>[] Dequeue(int length)
        {
            if (Count > length)
                throw new PriorityQueueException("Priority queue is empty");

            Pair<TKey, TValue>[] items = new Pair<TKey, TValue>[length];

            for (int i = 0; i < length; i++)
                items[i] = Dequeue();

            return items;
        }

        public override object Clone()
        {
            return new PriorityQueue<TKey, TValue>(this);
        }

        public override IEnumerator<Pair<TKey, TValue>> GetEnumerator()
        {
            Pair<TKey, TValue>[] items = heap.ToArray();

            foreach (var item in items)
                yield return item;            
        }

        public override Pair<TKey, TValue>[] ToArray()
        {
            return heap.ToArray();
        }
    }

    [Serializable]
    public class PriorityQueueException : Exception
    {
        public PriorityQueueException() { }
        public PriorityQueueException(string message) : base(message) { }
        public PriorityQueueException(string message, Exception inner) : base(message, inner) { }
        protected PriorityQueueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
