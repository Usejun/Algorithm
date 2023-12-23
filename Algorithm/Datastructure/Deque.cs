using System;

namespace Algorithm.Datastructure
{
    // 데큐 Double-ended Queue
    public class Deque<T> : Collection<T>, IQueue<T>, IStack<T>
    {
        // 연결형 노드
        public class LinkedNode : Node<T>
        {
            public LinkedNode after;
            public LinkedNode before;

            public LinkedNode(T value) : base(value) { }

            public void Connect(LinkedNode node, bool isBefore = true)
            {
                if (isBefore)
                {
                    before = node;
                    node.after = this;
                }
                else if (!isBefore)
                {
                    after = node;
                    node.before = this;
                }
            }

            public void Clear()
            {
                after = null;
                before = null;
            }

            public static implicit operator bool(LinkedNode node)
            {
                return node != null;
            }
        }

        public bool IsEmpty => count == 0;

        LinkedNode front = null;
        LinkedNode back = null;

        public Deque() { }

        public void AddFront(T value)
        {
            LinkedNode node = new LinkedNode(value);

            if (IsEmpty)
            {
                front = node;
                back = node;
            }
            else
            {
                node.Connect(front);
                front = node;
            }

            count++;
        }

        public void AddFrontRange(params T[] values)
        {
            foreach (T value in values)
                AddFront(value);
        }

        public void AddBack(T value)
        {
            LinkedNode node = new LinkedNode(value);

            if (IsEmpty)
            {
                front = node;
                back = node;
            }
            else
            {
                node.Connect(back, false);
                back = node;
            }

            count++;
        }

        public void AddBackRange(params T[] values)
        {
            foreach (T value in values)
                AddBack(value);
        }

        public T RemoveFront()
        {
            if (count == 0)
                throw new CollectionEmptyException("Deque is Empty");

            T item = front.Value;

            front = front.before;
            if (count != 1) front.after = null;

            count--;

            return item;
        }

        public T[] RemoveFrontRange(int repeat)
        {
            T[] values = new T[repeat];

            for (int i = 0; i < repeat; i++)
                values[i] = RemoveFront();

            return values;
        }

        public T RemoveBack()
        {
            if (count == 0)
                throw new CollectionEmptyException("Deque is Empty");

            T item = back.Value;

            back = back?.after;
            if (count != 1) back.before = null;

            count--;

            return item;
        }

        public T[] RemoveBackRange(int repeat)
        {
            T[] values = new T[repeat];

            for (int i = 0; i < repeat; i++)
                values[i] = RemoveBack();

            return values;
        }

        public T PeekFront()
        {
            return front.Value;
        }

        public T PeekBack()
        {
            return back.Value;
        }

        public override void Add(T value)
        {
            AddBack(value);
        }

        public override void Clear()
        {
            front = null;
            back = null;
            count = 0;
        }

        public override bool Remove(T value)
        {
            if (PeekBack().Equals(value)) RemoveBack();
            else if (PeekFront().Equals(value)) RemoveFront();
            else return false;

            return true;
        }

        public override T[] ToArray()
        {
            T[] values = new T[count];
            LinkedNode node = front;

            for (int i = 0; i < count; i++)
            {
                values[i] = node.Value;
                node = node.before;
            }

            return values;
        }

        public void Enqueue(T value)
        {
            AddBack(value);
        }

        public void EnqueueRange(params T[] values)
        {
            AddBackRange(values);
        }

        public T Dequeue()
        {
            return RemoveFront();
        }

        public T[] DequeueRange(int repeat)
        {
            return RemoveFrontRange(repeat);
        }

        public void Push(T value)
        {
            AddBack(value);
        }

        public void PushRange(params T[] values)
        {
            AddBackRange(values);
        }

        public T Pop()
        {
            return RemoveBack();
        }

        public T[] PopRange(int repeat)
        {
            return RemoveBackRange(repeat);
        }
    }
}
