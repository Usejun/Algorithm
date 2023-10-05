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
        public abstract class Collection<T> : IEnumerate<T>, IEnumerable<T>, ICollection<T>
        {
            public virtual bool IsReadOnly => false;
            public virtual int Count => ToArray().Length;

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
            public virtual IEnumerator<T> GetEnumerator()
            {
                foreach (T item in ToArray())
                    yield return item;              
            }
            public virtual bool Contains(T value)
            {
                return ToArray().Contains(value);
            }
            public virtual void CopyTo(T[] array, int index)
            {
                T[] source = ToArray();

                if (source.Length <= index)
                    throw new ArgumentException("Out of Range");

                for (int i = index; i < array.Length; i++)
                    array[i] = source[i];                
            }

            public abstract T[] ToArray();
            public abstract void Add(T value);
            public abstract bool Remove(T value);
            public abstract void Clear();           

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
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
        public class DynamicArray<T> : Collection<T>, ICollection<T>
        {
            public override int Count => count;
            public bool IsFull => count == Length - 1;

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

            public override void Add(T value)
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

            public override void Clear()
            {
                source = new T[Length];
                count = 0;
            }
        
            public override bool Remove(T value)
            {
                if (!source.Contains(value))
                    return false;

                source = source.Except(new T[] { value }).ToArray();
                count--;
                return true;
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
            public override int Count => count;
            public bool IsEmpty => count == 0;

            LinkedNode<T> front = null;
            LinkedNode<T> back = null;

            int count = 0;

            public LinkedList()
            {

            }

            public override void Add(T item)
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

            public override bool Remove(T item)
            {
                if (front == null || back == null || Contains(item))
                    return false;
                RemoveAt(IndexOf(item));
                count--;
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
        public class PriorityQueue<T, T1> : Collection<PriorityQueueNode<T, T1>>
            where T1 : IComparable<T1>
        {
            public override int Count => queue.Count;
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

            public bool ContainsKey(T key)
            {
                return keyValues.ContainsKey(key);
            }

            public PriorityQueueNode<T, T1> Peek()
            {
                return isReverse ? queue.Max : queue.Min;
            }

            public override void Clear()
            {
                queue.Clear();
                keyValues.Clear();                
            }

            public override void Add(PriorityQueueNode<T, T1> value)
            {
                Enqueue(value);
            }

            public override bool Remove(PriorityQueueNode<T, T1> value)
            {
                return queue.Remove(value);
            }

            public override PriorityQueueNode<T, T1>[] ToArray()
            {
                PriorityQueueNode<T, T1>[] array = new PriorityQueueNode<T, T1>[Count];
                int index = 0;

                foreach (var value in keyValues.Keys)
                    foreach (var priority in keyValues[value])
                        array[index++] = new PriorityQueueNode<T, T1>(value, priority);

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

            public override int Count => count;
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

            public override void Add(T value)
            {
                Push(value);
            }

            public override bool Remove(T value)
            {
                if (!ToArray().Contains(value))
                    return false;

                source = source.Except(new T[] { value }).ToArray();
                front--;
                count--;
                return true;
            }

            public override void Clear()
            {
                source = new T[size];
                count = 0;
                front = 0;
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

            public override int Count => count;
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

            public override void Add(T value)
            {
                Enqueue(value);
            }

            public override bool Remove(T value)
            {
                if (!ToArray().Contains(value))
                    return false;

                source = ToArray().Except(new T[] { value }).ToArray();
                front = source.Length;
                back = 0;
                count--;

                return true;
            }

            public override void Clear()
            {
                source = new T[size];
                front = 0;
                back = 0;
                count = 0;
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

        public class HashList<T, T1> : Collection<T1>            
        {
            LinkedList<LinkedList<T1>> list;

            public HashList()
            {
                list = new LinkedList<LinkedList<T1>>();                
            }

            public override T1[] ToArray()
            {
                DynamicArray<T1> array = new DynamicArray<T1>();

                foreach (LinkedList<T1> linkedList in list)
                    array.Add(linkedList.ToArray());

                return array.ToArray();
            }

            public override void Add(T1 value)
            {
                throw new NotImplementedException();
            }

            public override bool Remove(T1 value)
            {
                throw new NotImplementedException();
            }

            public override void Clear()
            {
                throw new NotImplementedException();
            }
        }

    }
}
