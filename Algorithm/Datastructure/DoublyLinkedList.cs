using System;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public class DoublyLinkedList<T> : Iterator<T>
    {
        public class Node
        {
            public T Value;
            public Node Prev;
            public Node Next;

            public Node(T value)
            {
                Value = value;
            }
        }

        public bool IsEmpty => Count == 0;

        private Node root = new Node(default);

        public DoublyLinkedList() { }
        
        public DoublyLinkedList(params T[] items)
        {
            AddLastRange(items);
        }

        public DoublyLinkedList(IEnumerable<T> items)
        {
            AddLastRange(items);
        }

        public void AddFirst(T item)
        {
            if (IsEmpty)
            {
                Node node = new Node(item)
                {
                    Next = root,
                    Prev = root
                };

                root.Next = node;
                root.Prev = node;

                Count++;
            }
            else
            {
                InsertNext(root, item);
            }
        }

        public void AddFirstRange(params T[] items)
        {
            foreach (var item in items)
                AddFirst(item);
        }

        public void AddFirstRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                AddFirst(item);
        }

        public void AddLast(T item)
        {
            if (IsEmpty)
            {
                Node node = new Node(item)
                {
                    Next = root,
                    Prev = root
                };

                root.Next = node;
                root.Prev = node;

                Count++;
            }
            else
            {
                InsertPrev(root, item);
            }
        }

        public void AddLastRange(params T[] items)
        {
            foreach (var item in items)
                AddLast(item);
        }

        public void AddLastRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                AddLast(item);
        }

        public void InsertPrev(Node node, T item)
        {
            Node newNode = new Node(item)
            {
                Prev = node.Prev,
                Next = node
            };

            node.Prev.Next = newNode;
            node.Prev = newNode;

            Count++;
        }

        public void InsertNext(Node node, T item)
        {
            Node newNode = new Node(item)
            {
                Next = node.Next,
                Prev = node
            };

            node.Next.Prev = newNode;
            node.Next = newNode;

            Count++;
        }

        public Node RemoveFirst()
        {
            if (IsEmpty)
                throw new DoublyLinkedListException("Doubly linked list is empty");

            Node node = root.Next;

            node.Next.Prev = root;
            root.Next = node.Next;
            Count--;

            return node;
        }

        public Node[] RemoveFirstRange(int length)
        {
            if (length > Count)
                throw new DoublyLinkedListException("Length is bigger than doubly linked list count");

            Node[] result = new Node[length];

            for (int i = 0; i < length; i++)
                result[i] = RemoveFirst();

            return result;
        }

        public Node RemoveLast()
        {
            if (IsEmpty)
                throw new DoublyLinkedListException("Doubly linked list is empty");

            Node node = root.Next;

            node.Prev.Next = root;
            root.Prev = node.Prev;
            Count--;

            return node;

        }

        public Node[] RemoveLastRange(int length)
        {
            if (length > Count)
                throw new DoublyLinkedListException("Length is bigger than doubly linked list count");

            Node[] result = new Node[length];

            for (int i = 0; i < length; i++)
                result[i] = RemoveLast();

            return result;
        }

        public bool Remove(T item)
        {
            return Remove(NodeOfNext(item));
        }

        public bool Remove(Node node)
        {
            if (node == null)
                return false;

            node.Next.Prev = node.Prev;
            node.Prev.Next = node.Next;
            Count--;
            return true;
        }

        public Node NodeOfNext(T item)
        {
            Node node = root.Next;

            while (node != root && node != null)
            {
                if (node.Value.Equals(item))                
                    return node;
                node = node.Next;
            }

            return null;
        }

        public Node NodeOfPrev(T item)
        {
            Node node = root.Prev;

            while (node != root)
            {
                if (node.Value.Equals(item))
                    return node;
                node = node.Prev;
            }

            return null;
        }

        public bool Contains(T item)
        {
            return NodeOfNext(item) != null;
        }

        public override object Clone()
        {
            return new DoublyLinkedList<T>(this);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            Node node = root.Next;

            while (node != root && node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        public override T[] ToArray()
        {
            T[] items = new T[Count];
            Node node = root.Next;

            for (int i = 0; i < Count; i++)
            {
                items[i] = node.Value;
                node = node.Next;
            }

            return items;
        }

        public static DoublyLinkedList<T> operator +(DoublyLinkedList<T> a, DoublyLinkedList<T> b)
        {
            DoublyLinkedList<T> result = new DoublyLinkedList<T>();

            result.AddLastRange(a);
            result.AddLastRange(b);

            return result;
        }

        public static DoublyLinkedList<T> operator *(DoublyLinkedList<T> a, int b)
        {
            DoublyLinkedList<T> result = new DoublyLinkedList<T>();
            T[] items = a.ToArray();

            while (b-- > 0)
                result.AddLastRange(items);

            return result;
        }
    }


    [Serializable]
    public class DoublyLinkedListException : System.Exception
    {
        public DoublyLinkedListException() { }
        public DoublyLinkedListException(string message) : base(message) { }
        public DoublyLinkedListException(string message, System.Exception inner) : base(message, inner) { }
        protected DoublyLinkedListException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
