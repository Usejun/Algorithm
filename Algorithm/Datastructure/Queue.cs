using System;
using System.Collections;

namespace Algorithm.Datastructure
{
    // 큐 First in First Out
    public class Queue<T> : List<T>, IQueue<T>
    {
        int front = 0;
        int back = 0;

        public bool IsEmpty => count == 0;
        public override bool IsFull => back == front;

        public Queue() : base() { }

        public Queue(int capacity = 0, params T[] values) : base(capacity, values) { }

        public virtual void Enqueue(T value)
        {
            if (IsFull)
                Resize();

            source[back] = value;
            back = (back + 1) % Length;
            count++;
        }

        public virtual void EnqueueRange(params T[] values)
        {
            foreach (T value in values)
                Enqueue(value);
        }

        public virtual T Dequeue()
        {
            if (IsEmpty)
                throw new Exception("Stack is Empty");

            T value = source[front];
            front = (front + 1) % Length;
            count--;

            return value;
        }

        public virtual T[] DequeueRange(int repeat)
        {
            T[] values = new T[repeat];

            for (int i = 0; i < repeat; i++)
            {
                if (IsEmpty) break;
                values[i] = Dequeue();
            }

            return values;
        }

        public virtual void DequeueEnqueue()
        {
            Enqueue(Dequeue());
        }

        public virtual T Peek()
        {
            return source[front];
        }

        public override void Add(T value)
        {
            Enqueue(value);
        }

        public override bool Remove(T value)
        {
            if (Peek().Equals(value))
                return false;

            Dequeue();
            return true;
        }

        public override void Clear()
        {
            source = new T[Length];
            front = 0;
            back = 0;
            count = 0;
        }

        public override T[] ToArray()
        {
            T[] values = new T[count];

            int i = front, j = count, k = 0;

            while (j-- > 0)
            {
                values[k++] = source[i];
                i = (i + 1) % Length;
            }

            return values;

        }

        protected override void Resize()
        {
            base.Resize();

            front = 0;
            back = count;
        }

        public override void Sort<T1>(Func<T, T1> order, Action<T[], Func<T, T1>, IComparer> sort = null, IComparer comparer = null)
        {
            base.Sort(order, sort, comparer);

            front = 0;
            back = count;
        }
    }
}
