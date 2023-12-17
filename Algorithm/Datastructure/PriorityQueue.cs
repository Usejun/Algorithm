using System;

namespace Algorithm.Datastructure
{
    // 우선순위 큐의 노드
    public class PriorityQueueNode<TValue, TPriority> : Node<TValue>, IComparable<PriorityQueueNode<TValue, TPriority>>
        where TPriority : IComparable<TPriority>
    {
        // 노드의 우선도
        public TPriority Priority;

        public PriorityQueueNode(TValue value, TPriority priority) : base(value)
        {
            Priority = priority;
        }

        public void Deconstruct(out TValue value, out TPriority priority)
        {
            value = Value;
            priority = Priority;
        }

        public int CompareTo(PriorityQueueNode<TValue, TPriority> other) => CompareTo(other.Priority);

        public int CompareTo(TPriority other) => (Priority as IComparable<TPriority>).CompareTo(other);

        public override string ToString()
        {
            return $"Value : {Value}, Priority : {Priority}";
        }

    }

    // 우선순위 큐, 기본 : 최소 힙
    public class PriorityQueue<TValue, TPriority> : Collection<PriorityQueueNode<TValue, TPriority>>, IQueue<PriorityQueueNode<TValue, TPriority>>
        where TPriority : IComparable<TPriority>
    {
        public bool IsEmpty => heap.Count == 0;
        public override int Count => heap.Count;
        public PriorityQueueNode<TValue, TPriority> Top => heap.Top;

        private Heap<PriorityQueueNode<TValue, TPriority>> heap;
        private Dictionary<TPriority, List<TValue>> keyValues;

        public PriorityQueue()
        {
            heap = new Heap<PriorityQueueNode<TValue, TPriority>>();
            keyValues = new Dictionary<TPriority, List<TValue>>();
        }

        public PriorityQueue(bool reverse = false)
        {
            heap = new Heap<PriorityQueueNode<TValue, TPriority>>(reverse: reverse);
            keyValues = new Dictionary<TPriority, List<TValue>>();
        }

        public void Enqueue(TValue value, TPriority priority)
        {
            heap.Add(new PriorityQueueNode<TValue, TPriority>(value, priority));
            count++;

            if (!keyValues.ContainsKey(priority))
            {
                keyValues.Add(priority, new List<TValue>());
            }

            keyValues[priority].Add(value);

        }

        public void Enqueue(PriorityQueueNode<TValue, TPriority> node)
        {
            Enqueue(node.Value, node.Priority);
        }

        public void EnqueueRange(params (TValue, TPriority)[] nodes)
        {
            foreach ((TValue value, TPriority priority) in nodes)
                Enqueue(value, priority);
        }

        public void EnqueueRange(params PriorityQueueNode<TValue, TPriority>[] nodes)
        {
            foreach (PriorityQueueNode<TValue, TPriority> node in nodes)
                Enqueue(node);
        }

        public PriorityQueueNode<TValue, TPriority> Dequeue()
        {
            PriorityQueueNode<TValue, TPriority> node = heap.Top;

            heap.Pop();
            keyValues[node.Priority].Remove(node.Value);

            count--;

            return node;
        }

        public PriorityQueueNode<TValue, TPriority>[] DequeueRange(int repeat)
        {
            PriorityQueueNode<TValue, TPriority>[] nodes = new PriorityQueueNode<TValue, TPriority>[repeat];

            for (int i = 0; i < repeat; i++)
            {
                if (IsEmpty) break;
                nodes[i] = Dequeue();
            }

            return nodes;
        }

        public bool ContainsKey(TPriority priority)
        {
            return keyValues.ContainsKey(priority);
        }

        public override void Clear()
        {
            heap.Clear();
            keyValues.Clear();
        }

        public override void Add(PriorityQueueNode<TValue, TPriority> value)
        {
            Enqueue(value);
        }

        public override bool Remove(PriorityQueueNode<TValue, TPriority> value)
        {
            return heap.Remove(value);
        }

        public override PriorityQueueNode<TValue, TPriority>[] ToArray()
        {
            PriorityQueueNode<TValue, TPriority>[] array = new PriorityQueueNode<TValue, TPriority>[Count];
            int index = 0;

            foreach (var priority in keyValues.Keys)
                for (int i = 0; i < keyValues[priority].Count; i++)
                    array[index++] = new PriorityQueueNode<TValue, TPriority>(keyValues[priority][i], priority);

            return array;
        }
    }
}
