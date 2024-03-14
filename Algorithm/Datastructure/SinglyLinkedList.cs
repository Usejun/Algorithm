using System;
using System.Collections.Generic;

namespace Algorithm.Datastructure
{
    public class SinglyLinkedList<T> : Iterator<T>
    {
        public class Node
        {
            public T Value;
            public Node Next;

            public Node(T value)
            {
                Value = value;
            }
        }

        public bool IsEmpty => Count == 0;

        private Node head;

        public SinglyLinkedList() { }

        public SinglyLinkedList(params T[] items)
        {
            AddLastRange(items);
        }

        public SinglyLinkedList(IEnumerable<T> items)
        {
            AddLastRange(items);
        }

        public void AddFirst(T item)
        {
            if (IsEmpty)
                head = new Node(item);
            else
            {
                Node node = new Node(item)
                {
                    Next = head
                };
                head = node;
            }

            Count++;
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
                head = new Node(item);
            else
                head.Next = new Node(item);

            Count++;
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

        public Node Remove(T item)
        {
            Node node = head;

            while (node.Next != null)
            {
                if (node.Next.Value.Equals(item))
                {
                    Node next = node.Next;
                    node.Next = node.Next.Next;
                    Count--;
                    return next;
                }
            }

            return null;
        }

        public Node RemoveFirst()
        {
            if (IsEmpty)
                throw new SinglyLinkedListException("Singly linked list is empty");

            Node node = head;
            head = head.Next;
            Count--;

            return node;
        }

        public Node[] RemoveFirstRange(int length)
        {
            if (length > Count)
                throw new SinglyLinkedListException("Length is bigger than singly linked list count");

            Node[] nodes = new Node[length];

            for (int i = 0; i < length; i++)
                nodes[i] = RemoveFirst();

            return nodes;
        }
        
        public Node RemoveLast()
        {
            if (IsEmpty)
                throw new SinglyLinkedListException("Singly linked list is empty");

            Node node = head;
            while (node.Next != null)            
                node = node.Next;

            Count--;
            return node;
        }

        public Node[] RemoveLastRange(int length)
        {
            if (length > Count)
                throw new SinglyLinkedListException("Length is bigger than singly linked list count");

            Node[] nodes = new Node[length];

            for (int i = 0; i < length; i++)
                nodes[i] = RemoveLast();

            return nodes;
        }

        public void Insert(Node node, T item)
        {
            Node next = node.Next;
            node.Next = new Node(item) { Next = next };
            Count++;
        }

        public bool Contains(T item)
        {
            return NodeOf(item) != null;
        }

        public Node NodeOf(T item)
        {
            Node node = head;

            while (node != null)
            {
                if (node.Value.Equals(item))
                    return node;
                node = node.Next;
            }

            return null;
        }

        public override object Clone()
        {
            return new SinglyLinkedList<T>(this);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            for (Node node = head; node != null; node = node.Next)
                yield return node.Value;
        }

        public override T[] ToArray()
        {
            T[] items = new T[Count];
            Node node = head;

            for (int i = 0; i < Count; i++, node = node.Next)
                items[i] = node.Value;

            return items;
        }

        public static SinglyLinkedList<T> operator +(SinglyLinkedList<T> a, SinglyLinkedList<T> b)
        {
            SinglyLinkedList<T> result = new SinglyLinkedList<T>();

            result.AddLastRange(a);
            result.AddLastRange(b);

            return result;
        }

        public static SinglyLinkedList<T> operator *(SinglyLinkedList<T> a, int b)
        {
            SinglyLinkedList<T> result = new SinglyLinkedList<T>();  
            T[] items = a.ToArray();

            while (b-- > 0)
                result.AddLastRange(items);

            return result;
        }
    }


    [Serializable]
    public class SinglyLinkedListException : Exception
    {
        public SinglyLinkedListException() { }
        public SinglyLinkedListException(string message) : base(message) { }
        public SinglyLinkedListException(string message, Exception inner) : base(message, inner) { }
        protected SinglyLinkedListException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
