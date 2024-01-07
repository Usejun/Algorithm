using System;

namespace Algorithm.Datastructure
{
    // 양방향 링크드 리스트
    public class LinkedList<T> : Collection<T>
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
        }

        public bool IsEmpty => count == 0;

        private LinkedNode front = null;
        private LinkedNode back = null;

        public LinkedList() { }

        public override void Add(T item)
        {
            AddBack(item);
        }

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
                front.Connect(node, false);
                front = node;
            }

            count++;
        }

        public void AddRangeFront(params T[] values)
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
                back.Connect(node, true);
                back = node;
            }

            count++;
        }

        public void AddRangeBack(params T[] values)
        {
            foreach (T value in values)
                AddBack(value);
        }

        public void Insert(int index, T value)
        {
            if (OutOfRange(index))
                throw new CollectionIndexException("Out of range");

            if (index >= count)
            {
                AddBack(value);
                return;
            }

            LinkedNode startNode = GetNode(index);
            LinkedNode endNode = GetNode(index + 1);
            LinkedNode newNode = new LinkedNode(value);

            startNode.Connect(newNode, true);
            endNode.Connect(newNode, false);

            count++;
        }

        public void InsertRange(int index, params T[] values)
        {
            if (OutOfRange(index))
                throw new CollectionIndexException("Out of range");

            if (index >= count)
            {
                AddRangeBack(values);
                return;
            }

            LinkedNode startNode = GetNode(index);
            LinkedNode endNode = GetNode(index + 1);

            for (int i = 0; i < values.Length; i++)
            {
                LinkedNode newNode = new LinkedNode(values[i]);
                startNode.Connect(newNode);
                startNode = newNode;
            }

            startNode.Connect(endNode);

            count += values.Length;
        }

        public override bool Remove(T item)
        {
            if (front == null || back == null || !Contains(item))
                return false;
            RemoveAt(IndexOf(item));
            return true;
        }

        public void RemoveFront()
        {
            if (IsEmpty)
                throw new CollectionEmptyException("List is Empty");
            else if (count == 1)
            {
                front = null;
                back = null;
            }
            else
            {
                front = front.before;
                front.after = null;
            }
            count--;
        }

        public void RemoveFront(int count)
        {
            while (count-- > 0)
                RemoveFront();
        }

        public void RemoveBack()
        {
            if (count == 0)
                throw new CollectionIndexException("List is Empty");
            else if (count == 1)
            {
                front = null;
                back = null;
            }
            else
            {
                back = back.after;
                back.before = null;
            }
            count--;
        }

        public void RemoveBack(int count)
        {
            while (count-- > 0)
                RemoveBack();
        }

        public void RemoveAt(int index)
        {
            if (count == 0)
                throw new CollectionIndexException("List is Empty");

            LinkedNode node = GetNode(index);

            node.before.Connect(node.after);
            count--;
        }

        public override int IndexOf(T value)
        {
            int index = 0;
            LinkedNode node = front;

            if (node == null)
                return -1;

            while (!value.Equals(node.Value))
            {
                if (node.after == null)
                    return -1;

                node = node.after;
                index++;
            }

            return index;
        }

        public override bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }

        public override void Clear()
        {
            front = null;
            back = null;
            count = 0;
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

        public LinkedNode GetNode(int index)
        {
            if (OutOfRange(index))
                throw new CollectionExistException("item is not Contains");

            if (index / 2 > count)
            {
                LinkedNode node = back;
                for (int i = 0; i < index - 1; i++)
                {
                    node = node.after;
                    if (node == null) throw new CollectionExistException("item is not Contains");
                }

                return node;
            }
            else
            {
                LinkedNode node = front;
                for (int i = 0; i < index; i++)
                {
                    node = node.before;
                    if (node == null) throw new CollectionExistException("item is not Contains");
                }
                return node;
            }
        }

        private bool OutOfRange(int index)
        {
            return IsEmpty || 0 > index || index > count;
        }

        public T this[int index]
        {
            get => GetNode(index).Value;
            set => GetNode(index).Value = value;
        }

    }
}
