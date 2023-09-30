using System;
using System.Collections.Generic;
using Algorithm.DataStructure;
using static Algorithm.Util;
using static Algorithm.Mathf;
using System.Collections;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Dynamic;

namespace Algorithm
{
    namespace DataStructure
    {
        // 가변 배열
        public class DynamicArray<T>
        {
            T[] source;

            int count = 0;

            int Length => source.Length;
            bool IsFull => count == Length - 1;

            public DynamicArray(int initializeSize = 100)
            {
                source = new T[initializeSize];
            }

            public void Add(T value)
            {
                if (IsFull)
                    Expand();

                source[count++] = value;
            }

            public void Add(params T[] values)
            {
                foreach (T value in values)
                    Add(value);
            }

            public void Expand()
            {
                T[] newSource = new T[Length * 2];

                for (int i = 0; i < Length; i++)
                    newSource[i] = source[i];

                source = newSource;
            }

            public T this[int index]
            {
                get => source[index];
                set => source[index] = value;
            }
        }

        // 기본 노드
        public class Node<T>
        {
            public T value;
            public Node<T> after;
            public Node<T> before;

            public Node(T value = default, Node<T> after = null, Node<T> before = null)
            {
                this.value = value;
                this.after = after;
                this.before = before;
            }

            public void Clear()
            {
                after = null;
                before = null;
            }
            
            public void Before(Node<T> beforeNode)
            {
                before = beforeNode;
                beforeNode.after = this;
            }

            public void After(Node<T> afterNode)
            {
                after = afterNode;
                afterNode.before = this;
            }
        }

        // 양방향 링크드 리스트
        public class LinkedList<T> : IEnumerable<T>, IList<T>, ICollection<T>, IEnumerable, IExpandArray<T>, IEnumerate<T>
        {
            public int Count => count;
            public bool IsEmpty => count == 0;

            public bool IsReadOnly => false;

            public Func<T[], T[]> sort;

            Node<T> front = null;
            Node<T> back = null;
            int count = 0;            

            public LinkedList()
            {
                sort = Sorts.QuickSort;
            }

            public LinkedList(Func<T[], T[]> sort)
            {
                this.sort = sort;
            }
            
            public void Add(T item)
            {
                AddBack(item);
            }

            public void AddFront(T value)
            {
                Node<T> node = new Node<T>(value);
                if (front == null)
                    front = node;
                else
                {
                    front.After(node);
                    node.Before(front);
                    front = node;
                }

                if (count == 0)
                    back = node;

                count++;
            }

            public void AddFront(params T[] values)
            {
                foreach (T value in values)
                    AddFront(value);              
            }

            public void AddBack(T value)
            {
                Node<T> node = new Node<T>(value);
                if (back is null)
                    back = node;
                else
                {
                    back.Before(node);
                    node.After(back);
                    back = node;
                }

                if (count == 0)
                    front = node;

                count++;
            }

            public void AddBack(params T[] values)
            {
                foreach (T value in values)
                    AddBack(value);
            }

            public void Insert(int index, T value)
            {
                if (index < 0)
                    throw new Exception();
                
                if (AvailableRange(index))
                {
                    AddBack(value);
                    return;
                }

                Node<T> startNode = GetNode(index);
                Node<T> endNode = GetNode(index + 1);
                Node<T> newNode = new Node<T>(value);
                
                startNode.Before(newNode);
                endNode.Before(newNode);

                count++;
            }

            public void Insert(int index, params T[] values)
            {
                if (AvailableRange(index))
                    throw new Exception();                

                if (index == count)
                {
                    AddBack(values);
                    return;
                }

                Node<T> startNode = GetNode(index);
                Node<T> endNode = GetNode(index + 1);

                for (int i = 0; i < values.Length; i++)
                {
                    Node<T> newNode = new Node<T>(values[i]);
                    startNode.Before(newNode);
                    newNode.After(startNode);
                    startNode = newNode;
                }

                startNode.Before(endNode);
                endNode.After(startNode);

                count += values.Length;
            }

            public bool Remove(T item)
            {
                if (front == null || back == null)
                    return false;
                RemoveBack();
                return true;
            }

            public void RemoveFront()
            {
                if (IsEmpty)
                    throw new Exception();
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

            public void RemoveFront(int n)
            {
                while (n-- > 0)
                    RemoveFront();
            }

            public void RemoveBack()
            {
                if (count == 0)
                    throw new Exception();
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

            public void RemoveBack(int n)
            {
                while (n-- > 0)
                    RemoveBack();
            }

            public void RemoveAt(int index)
            {
                if (count == 0)
                    throw new Exception();

                Node<T> node = GetNode(index);

                node.before.after = node.after;
                node.after.before = node.before;
                count--;
            }

            public void RemoveRange(int start, int end)
            {
                if (end < start || count - (end - start + 1) < 0)
                    throw new Exception();

                Node<T> startNode = GetNode(start);
                Node<T> endNode = GetNode(end);

                startNode.after.before = endNode.before;
                endNode.before.after = startNode.after;

                startNode.Clear();
                endNode.Clear();

                count -= end - start + 1; 
            }

            public int IndexOf(T value)
            {
                int index = 0;
                Node<T> node = front;

                while (value != (dynamic)node.value)
                {
                    if (node.after == null)
                        return -1;

                    node = node.after;
                    index++;
                }

                return index;
            }

            public bool Contains(T value)
            {
                return IndexOf(value) != -1;
            }

            public void Clear()
            {
                front = null;
                back = null;
                count = 0;
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (Node<T> i = front; i != null; i = i.before)
                    yield return i.value;              
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                for (int i = arrayIndex; i < array.Length; i++)
                    array[i] = this[i];
            }

            public T[] Sorted()
            {
                return Sorts.QuickSort(ToArray());
            }

            public T[] ToArray()
            {
                T[] array = new T[Count];

                for (int i = 0; i < Count; i++)
                    array[i] = this[i];

                return array;
            }

            public List<T> ToList()
            {
                return ToArray().ToList();               
            }

            public Tuple<int, T>[] ToEnumerate()
            {
                Tuple<int, T>[] tuples = new Tuple<int, T>[Count];

                for (int i = 0; i < Count; i++)
                    tuples[i] = Tuple.Create(i, this[i]);

                return tuples;
            }

            public void Sort()
            {
                T[] sorted = Sorted();
                Clear();
                AddBack(sorted);
            }

            bool AvailableRange(int index)
            {
                return !IsEmpty && 0 > index && index < count;
            }

            Node<T> GetNode(int index)
            {
                Node<T> node = front;
                for (int i = 0; i < index; i++)
                    node = node.before;

                return node;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            Tuple<int, T>[] IEnumerate<T>.ToEnumerate()
            {
                return ToEnumerate();
            }

            void IExpandArray<T>.Sort()
            {
                Sort();
            }

            public T this[int index]
            {
                get => GetNode(index).value;
                set => GetNode(index).value = value;
            }

        }

        // 우선순위 큐의 노드
        public class PriorityQueueNode<T, T1> : IComparable<PriorityQueueNode<T, T1>>
            where T1 : IComparable<T1>
        {
            // 노드의 값
            public T Value;
            // 노드의 우선도
            public T1 Priority;

            public PriorityQueueNode(T value, T1 priority)
            {
                Value = value;
                Priority = priority;
            }

            public int CompareTo(PriorityQueueNode<T, T1> other) => CompareTo(other.Priority);

            public int CompareTo(T1 other) => (Priority as IComparable<T1>).CompareTo(other);

            public override string ToString()
            {
                return $"Value : {Value}, Priority : {Priority}";
            }
        }

        // 우선순위 큐
        public class PriorityQueue<T, T1> : IEnumerable<PriorityQueueNode<T, T1>>
            where T1 : IComparable<T1>
        {
            public int Count => queue.Count;
            public PriorityQueueNode<T, T1> Max => queue.Max;
            public PriorityQueueNode<T, T1> Min => queue.Min;

            public bool IsReadOnly => false;

            public PriorityQueueNode<T, T1> this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            SortedSet<PriorityQueueNode<T, T1>> queue;
            Dictionary<T, List<T1>> keyValues;

            readonly bool isReverse = false;

            public PriorityQueue()
            {
                Init();
            }

            public PriorityQueue(bool isReverse)
            {
                this.isReverse = isReverse;
                Init();
            }

            private void Init()
            {
                queue = new SortedSet<PriorityQueueNode<T, T1>>();
                keyValues = new Dictionary<T, List<T1>>();
            }

            public void Enqueue(T key, T1 priority)
            {
                queue.Add(new PriorityQueueNode<T, T1>(key, priority));

                if (!keyValues.ContainsKey(key))
                    keyValues.Add(key, new List<T1>());

                keyValues[key].Add(priority);
            }

            public void Enqueue(PriorityQueueNode<T, T1> node)
            {
                queue.Add(node);
            }

            public PriorityQueueNode<T, T1> Dequeue()
            {
                PriorityQueueNode<T, T1> node = isReverse ? queue.Max : queue.Min;

                queue.Remove(isReverse ? queue.Max : queue.Min);
                keyValues[node.Value].Remove(node.Priority);

                return node;
            }

            public bool Contains(PriorityQueueNode<T, T1> node)
            {
                return queue.Contains(node);
            }

            public bool ContainsKey(T key)
            {
                return keyValues.ContainsKey(key);
            }

            public PriorityQueueNode<T, T1> Peek()
            {
                return isReverse ? queue.Max : queue.Min;
            }

            public void Clear()
            {
                queue.Clear();
                keyValues.Clear();                
            }

            public void Add(PriorityQueueNode<T, T1> node)
            {
                Enqueue(node);
            }

            public bool Remove(PriorityQueueNode<T, T1> node)
            {
                return queue.Remove(node);
            }

            public IEnumerator<PriorityQueueNode<T, T1>> GetEnumerator()
            {
                var pq = isReverse ? queue.Reverse().ToArray() : queue.ToArray();                             

                foreach (var node in pq)
                    yield return node;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        // 스택 First in Last Out
        public class Stack<T> : IEnumerable<T>
            where T : IComparable<T>
        {
            T[] source;

            int front = 0;
            int count = 0;
            int size = 0;

            public int Count => count;
            public bool IsEmpty => front == 0;
            public bool IsFull => front == Length - 1;

            int Length => source.Length;

            public Stack(int initializeSize = 100)
            {
                size = initializeSize;
                source = new T[size];
            }

            public void Push(T value)
            {
                if (IsFull)
                    Expand();

                source[front++] = value;
                count++;
            }

            public T Pop()
            {
                if (IsEmpty)
                    throw new Exception("Stack is Empty");

                T value = source[--front];
                count--;

                return value;
            }

            public T Peek()
            {
                return source[front - 1];
            }

            void Expand()
            {
                T[] newArray = new T[Length + size];
                int newFront = Length - 1;

                while (!IsEmpty)
                    newArray[--newFront] = Pop();

                front = Length - 1;
                source = newArray;
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (int i = front - 1; i >= 0; i--)
                    yield return source[i];
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        // 큐 First in First Out
        public class Queue<T> : IEnumerable<T>
            where T : IComparable<T>
        {
            T[] source;

            int front = 0;
            int back = 0;
            int count = 0;
            int size = 0;

            public int Count => count;
            public bool IsEmpty => front == back;
            public bool IsFull => (front + 1) % Length == back;
            int Length => source.Length;

            public Queue(int initializeSize = 100)
            {
                size = initializeSize;
                source = new T[size];
            }

            public void Enqueue(T value)
            {
                if (IsFull)
                    Expand();

                source[front] = value;
                front = (front + 1) % Length;
                count++;
            }

            public T Dequeue()
            {
                if (IsEmpty)
                    throw new Exception("Stack is Empty");

                T value = source[back];
                back = (back + 1) % Length;
                count--;

                return value;
            }

            public T Peek()
            {
                return source[front];
            }

            void Expand()
            {
                T[] newArray = new T[Length + size];
                int newFront = 0;

                while (!IsEmpty)
                {
                    newArray[newFront] = Dequeue();
                    newFront = (newFront + 1) % newArray.Length;
                }

                source = newArray;
                front = newFront;
                back = 0;
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (int i = back; i != front; i = (i + 1) % Length)
                {
                    yield return source[i]; 
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

    }
}
