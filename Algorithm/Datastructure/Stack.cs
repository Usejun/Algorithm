using System;

namespace Algorithm.Datastructure
{
    // 스택 First in Last Out
    public class Stack<T> : List<T>, IStack<T>
    {
        int front = 0;

        public bool IsEmpty => front == 0;
        public override bool IsFull => front == Length;

        public Stack() : base() { }

        public Stack(int capacity = 0, params T[] values) : base(capacity, values) { }

        public void Push(T value)
        {
            if (IsFull)
                Resize();

            source[front++] = value;
            count++;
        }

        public void PushRange(params T[] values)
        {
            foreach (T value in values)
                Push(value);
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new Exception("Stack is Empty");

            T value = source[--front];
            count--;

            return value;
        }

        public T[] PopRange(int repeat)
        {
            T[] values = new T[repeat];

            for (int i = 0; i < repeat; i++)
            {
                if (IsEmpty) break;
                values[i] = Pop();
            }

            return values;
        }

        public T Peek()
        {
            return source[front - 1];
        }

        public override void Add(T value)
        {
            Push(value);
        }

        public override bool Remove(T value)
        {
            if (Peek().Equals(value))
                return false;

            Pop();
            return true;
        }

        public override void Clear()
        {
            source = new T[Length];
            count = 0;
            front = 0;
        }

        public override T[] ToArray()
        {
            T[] values = new T[count];

            for (int i = 0; i < count; i++)
                values[i] = source[i];

            return values;
        }

    }
}
