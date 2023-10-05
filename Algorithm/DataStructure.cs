using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Algorithm.DataStructure;
using static Algorithm.Util;
using static Algorithm.Mathf;

namespace Algorithm
{
    namespace DataStructure
    {      
        //기본 배열 추상 클래스
        public abstract class Collection<T> : IEnumerate<T>
        {
            public abstract T[] ToArray();
            public virtual List<T> ToList()
            {
                return ToArray().ToList();
            }
            public virtual (int, T)[] ToEnumerate()
            {
                T[] values = ToArray();
                (int, T)[] enumerate = new (int, T)[values.Length];

                for (int i = 0; i < values.Length; i++)
                    enumerate[i] = (i + 1, values[i]);

                return enumerate;
            }
        }

        //기본 노드 추상 클래스
        public abstract class Node<T>
        {
            public T Value;

            public Node(T value)
            {
                Value = value;
            }
        }

        // 가변 배열
        public class DynamicArray<T> : Collection<T>, IEnumerable<T>
        {
            public int Count => count;
            public bool IsFull => count == Length - 1;
            public bool IsReadOnly => false;

            T[] source;
            int count = 0;
            int Length => source.Length;

            public Func<T[], T[]> Sorter { get; set; }

            public T this[int index]
            {
                get
                {
                    if (index < 0 && index >= count)
                        throw new Exception("Out of Range");
                    return source[index];
                }
                set
                {
                    if (index < 0 && index >= count)
                        throw new Exception("Out of Range");
                    source[index] = value;
                }
            }

            public DynamicArray(int initializeSize = 100, Func<T[], T[]> sort = null)
            {
                Sorter = sort ?? Sorts.QuickSort;
                source = new T[initializeSize];
            }

            public void Add(T value)
            {
                if (IsFull)
                    Resize();

                source[count++] = value;
            }

            public void Add(params T[] values)
            {
                foreach (T value in values)
                    Add(value);
            }
        
            void Resize()
            {
                T[] newSource = new T[Length * 2];

                for (int i = 0; i < Length; i++)
                    newSource[i] = source[i];

                source = newSource;
            }

            public override T[] ToArray()
            {
                return source.Take(count).ToArray();
            }

            public override List<T> ToList()
            {
                return ToArray().ToList();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                foreach (T value in source)
                    yield return value;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                foreach (T value in source)
                    yield return value;
            }
        }

        // 기본 노드
        public class LinkedNode<T> : Node<T>
        {
            public LinkedNode<T> after;
            public LinkedNode<T> before;

            public LinkedNode(T value) : base(value) { }
            
            public void Before(LinkedNode<T> beforeNode)
            {
                before = beforeNode;
                beforeNode.after = this;
            }

            public void After(LinkedNode<T> afterNode)
            {
                after = afterNode;
                afterNode.before = this;
            }

            public void Clear()
            {
                after = null;
                before = null;
            }
        }

        // 양방향 링크드 리스트
        public class LinkedList<T> : Collection<T>, ICollection<T>, IList<T>
        {
            public int Count => count;
            public bool IsEmpty => count == 0;
            public bool IsReadOnly => false;

            LinkedNode<T> front = null;
            LinkedNode<T> back = null;

            int count = 0;

            public LinkedList()
            {

            }

            public void Add(T item)
            {
                AddBack(item);
            }

            public void AddFront(T value)
            {
                LinkedNode<T> node = new LinkedNode<T>(value);
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
                LinkedNode<T> node = new LinkedNode<T>(value);
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

                LinkedNode<T> startNode = GetNode(index);
                LinkedNode<T> endNode = GetNode(index + 1);
                LinkedNode<T> newNode = new LinkedNode<T>(value);
                
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

                LinkedNode<T> startNode = GetNode(index);
                LinkedNode<T> endNode = GetNode(index + 1);

                for (int i = 0; i < values.Length; i++)
                {
                    LinkedNode<T> newNode = new LinkedNode<T>(values[i]);
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

            public void RemoveFront(int count)
            {
                while (count-- > 0)
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

            public void RemoveBack(int count)
            {
                while (count-- > 0)
                    RemoveBack();
            }

            public void RemoveAt(int index)
            {
                if (count == 0)
                    throw new Exception();

                LinkedNode<T> node = GetNode(index);

                node.before.after = node.after;
                node.after.before = node.before;
                count--;
            }

            public void RemoveRange(int start, int end)
            {
                if (end < start || count - (end - start + 1) < 0)
                    throw new Exception();

                LinkedNode<T> startNode = GetNode(start);
                LinkedNode<T> endNode = GetNode(end);

                startNode.after.before = endNode.before;
                endNode.before.after = startNode.after;

                startNode.Clear();
                endNode.Clear();

                count -= end - start + 1; 
            }

            public int IndexOf(T value)
            {
                int index = 0;
                LinkedNode<T> node = front;

                while (value != (dynamic)node.Value)
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
                for (LinkedNode<T> i = front; i != null; i = i.before)
                    yield return i.Value;              
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                for (int i = arrayIndex; i < array.Length; i++)
                    array[i] = this[i];
            }

            public LinkedNode<T>[] ToNodeArray()
            {
                LinkedNode<T>[] values = new LinkedNode<T>[count];

                for (int i = 0; i < count; i++)
                    values[i] = GetNode(i);

                return values;
                
            }

            public override T[] ToArray()
            {
                return ToNodeArray().Select(node => node.Value).ToArray();
            }

            bool AvailableRange(int index)
            {
                return !IsEmpty && 0 > index && index < count;
            }

            LinkedNode<T> GetNode(int index)
            {
                LinkedNode<T> node = front;
                for (int i = 0; i < index; i++)
                    node = node.before;

                return node;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public T this[int index]
            {
                get => GetNode(index).Value;
                set => GetNode(index).Value = value;
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
        public class PriorityQueue<T, T1> : Collection<T>
            where T1 : IComparable<T1>
        {
            public int Count => queue.Count;
            public PriorityQueueNode<T, T1> Max => queue.Max;
            public PriorityQueueNode<T, T1> Min => queue.Min;

            SortedSet<PriorityQueueNode<T, T1>> queue;
            Dictionary<T, List<T1>> keyValues;

            readonly bool isReverse;

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

            public void Enqueue(T value, T1 priority)
            {
                queue.Add(new PriorityQueueNode<T, T1>(value, priority));

                if (!keyValues.ContainsKey(value))
                    keyValues.Add(value, new List<T1>());

                keyValues[value].Add(priority);
            }

            public void Enqueue(PriorityQueueNode<T, T1> node)
            {
                queue.Add(node);
            }

            public void Enqueue(params (T, T1)[] nodes)
            {
                foreach ((T value, T1 priority) in nodes)
                    Enqueue(value, priority);
            }

            public void Enqueue(params PriorityQueueNode<T, T1>[] nodes)
            {
                foreach (PriorityQueueNode<T, T1> node in nodes)
                    Enqueue(node);
            }

            public PriorityQueueNode<T, T1> Dequeue()
            {
                PriorityQueueNode<T, T1> node = isReverse ? queue.Max : queue.Min;

                queue.Remove(isReverse ? queue.Max : queue.Min);
                keyValues[node.Value].Remove(node.Priority);

                return node;
            }

            public PriorityQueueNode<T, T1>[] Dequeue(int count)
            {
                PriorityQueueNode<T, T1>[] nodes = new PriorityQueueNode<T, T1>[count];

                for (int i = 0; i < count; i++)                
                    nodes[i] = Dequeue();
                
                return nodes;
            }

            public void DequeueEnqueue()
            {
                Enqueue(Dequeue());
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

            public override T[] ToArray()
            {
                return ToValueArray();
            }

            public T[] ToValueArray()
            {
                T[] array = new T[Count];
                int index = 0;

                foreach (var key in keyValues.Keys)
                        array[index++] = key;

                return array;
            }

            public T1[] ToPriorityArray()
            {
                T1[] array = new T1[Count];
                int index = 0;

                foreach (var values in keyValues.Values)
                    foreach (var value in values)
                        array[index++] = value;
                
                return array;
            }
        }

        // 스택 First in Last Out
        public class Stack<T> : Collection<T>, IEnumerable<T>
        {
            T[] source;

            int front = 0;
            int count = 0;
            int size = 0;

            public int Count => count;
            public bool IsEmpty => front == 0;
            public bool IsFull => front == Length - 1;
            public Func<T[], T[]> Sorter { get; private set; }

            int Length => source.Length;

            public Stack(int initializeSize = 100)
            {
                size = initializeSize;
                source = new T[size];
            }

            public void Push(T value)
            {
                if (IsFull)
                    Resize();

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

            void Resize()
            {
                T[] newArray = new T[Length + size];
                int newFront = Length - 1;

                while (!IsEmpty)
                    newArray[--newFront] = Pop();

                front = Length - 1;
                source = newArray;
            }

            public override T[] ToArray()
            {
                T[] values = new T[count];
                int index = front - 1;

                for (int i = 0; index >= 0 && i < count; i++)                
                    values[i] = source[index--];
                
                return values;

            }

            public override List<T> ToList()
            {
                return ToArray().ToList();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                foreach (T value in source.Take(count))
                    yield return value;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                foreach (T value in source.Take(count))
                    yield return value;
            }
        }

        // 큐 First in First Out
        public class Queue<T> : Collection<T>, IEnumerable<T>
        {
            T[] source;

            int front = 0;
            int back = 0;
            int count = 0;
            int size = 0;

            public int Count => count;
            public bool IsEmpty => front == back;
            public bool IsFull => (front + 1) % Length == back;
            public Func<T[], T[]> Sorter { get; private set; }

            int Length => source.Length;

            public Queue(int initializeSize = 100)
            {
                size = initializeSize;
                source = new T[size];
            }

            public void Enqueue(T value)
            {
                if (IsFull)
                    Resize();

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

            public override T[] ToArray()
            {
                T[] values = new T[count];

                int i = 0, j = back;

                while (i < count)
                {
                    values[i++] = source[j];
                    j = (j + 1) % Length;
                }

                return values;

            }

            public override List<T> ToList()
            {
                return ToArray().ToList();
            }

            void Resize()
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

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                for (int i = back; i != front; i = (i + 1) % Length)
                {
                    yield return source[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                for (int i = back; i != front; i = (i + 1) % Length)
                {
                    yield return source[i];
                }
            }
        }

        public class Dictionary<T, T1> : Collection<T1>
        {

        }

    }
}
