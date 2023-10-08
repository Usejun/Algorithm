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
            public virtual T[] Sorted(Func<T[], T[]> sorter = null)
            {
                if (typeof(T).GetInterfaces().Contains(typeof(IComparable<T>))
                 || typeof(T).GetInterfaces().Contains(typeof(IComparable)))
                    throw new Exception("This type does not have ICompable.");

                sorter = sorter ?? Sorts.QuickSort;
                return sorter(ToArray());
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
            public virtual void Sort()
            {

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

        //기본 노드 클래스
        public class Node<T>
        {
            public T Value;

            public Node(T value)            {
                Value = value;
            }

        }

        // 가변 배열
        public class DynamicArray<T> : Collection<T>
        {
            public override int Count => count;
            public bool IsFull => count == Length - 1;

            T[] source;
            int count = 0;
            int Length => source.Length;

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

            public DynamicArray(int initializeSize = 100)
            {
                source = new T[initializeSize];
            }

            public override void Add(T value)
            {
                if (IsFull)
                    Resize();

                source[count++] = value;
            }

            public void AddRange(params T[] values)
            {
                foreach (T value in values)
                    Add(value);
            }

            public void AddRange(IEnumerable<T> values)
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

        // 양방향 링크드 리스트
        public class LinkedList<T> : Collection<T>, IList<T>
        {
            // 연결형 노드
            public class LinkedNode : Node<T>
            {
                public LinkedNode after;
                public LinkedNode before;

                public LinkedNode(T value) : base(value) { }

                public void Before(LinkedNode beforeNode)
                {
                    before = beforeNode;
                    beforeNode.after = this;
                }

                public void After(LinkedNode afterNode)
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

            public override int Count => count;
            public bool IsEmpty => count == 0;

            LinkedNode front = null;
            LinkedNode back = null;

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
                LinkedNode node = new LinkedNode(value);
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

            public void AddRangeFront(params T[] values)
            {
                foreach (T value in values)
                    AddFront(value);              
            }

            public void AddBack(T value)
            {
                LinkedNode node = new LinkedNode(value);
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

            public void AddRangeBack(params T[] values)
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

                LinkedNode startNode = GetNode(index);
                LinkedNode endNode = GetNode(index + 1);
                LinkedNode newNode = new LinkedNode(value);
                
                startNode.Before(newNode);
                endNode.Before(newNode);

                count++;
            }

            public void InsertRange(int index, params T[] values)
            {
                if (AvailableRange(index))
                    throw new Exception();                

                if (index == count)
                {
                    AddRangeBack(values);
                    return;
                }

                LinkedNode startNode = GetNode(index);
                LinkedNode endNode = GetNode(index + 1);

                for (int i = 0; i < values.Length; i++)
                {
                    LinkedNode newNode = new LinkedNode(values[i]);
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

                LinkedNode node = GetNode(index);

                node.before.after = node.after;
                node.after.before = node.before;
                count--;
            }

            public void RemoveRange(int start, int end)
            {
                if (end < start || count - (end - start + 1) < 0)
                    throw new Exception();

                LinkedNode startNode = GetNode(start);
                LinkedNode endNode = GetNode(end);

                startNode.after.before = endNode.before;
                endNode.before.after = startNode.after;

                startNode.Clear();
                endNode.Clear();

                count -= end - start + 1; 
            }

            public int IndexOf(T value)
            {
                int index = 0;
                LinkedNode node = front;

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

            public LinkedNode[] ToNodeArray()
            {
                LinkedNode[] values = new LinkedNode[count];

                for (int i = 0; i < count; i++)
                    values[i] = GetNode(i);

                return values;
                
            }

            public override T[] ToArray()
            {
                return ToNodeArray().Select(node => node.Value).ToArray();
            }

            public LinkedNode GetNode(int index)
            {
                LinkedNode node = front;
                for (int i = 0; i < index; i++)
                    node = node.before;

                return node;
            }

            bool AvailableRange(int index)
            {
                return !IsEmpty && 0 > index && index < count;
            }

            public T this[int index]
            {
                get => GetNode(index).Value;
                set => GetNode(index).Value = value;
            }

        }

        // 우선순위 큐의 노드
        public class PriorityQueueNode<T, T1> : Node<T>, IComparable<PriorityQueueNode<T, T1>>
            where T1 : IComparable<T1>
        {
            // 노드의 우선도
            public T1 Priority;

            public PriorityQueueNode(T value, T1 priority) : base(value)
            {
                Priority = priority;
            }

            public void Deconstruct(out T value, out T1 priority)
            {
                value = Value;
                priority = Priority;
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
        public class Stack<T> : Collection<T>
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

            public override void Sort()
            {
                source = Sorted();
                size = source.Length;
            }
        }

        // 큐 First in First Out
        public class Queue<T> : Collection<T>
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

            public override void Sort()
            {
                source = Sorted();
                size = source.Length;
            }
        }
     
        // Key Value 형식의 구조체
        public struct Pair<TKey, TValue> : IEquatable<Pair<TKey, TValue>>
        {
            public TKey Key => key;
            public TValue Value => value;

            TKey key;
            TValue value;

            public Pair(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }

            public void Deconstruct(out TKey key, out TValue value)
            {
                key = this.key;
                value = this.value;
            }

            public static bool operator ==(Pair<TKey, TValue> l, Pair<TKey, TValue> r)
            {
                return l.Key.Equals(r.Key) && l.Value.Equals(r.Value);
            }

            public static bool operator !=(Pair<TKey, TValue> l, Pair<TKey, TValue> r)
            {
                return !(l.Key.Equals(r.Key)) || !(l.Value.Equals(r.Value));
            }

            public bool Equals(Pair<TKey, TValue> pair)
            {
                return key.Equals(pair.key) && value.Equals(pair.value);
            }

            public override string ToString()
            {
                return $"({key} : {value})";
            }

        }

        // 해쉬테이블 (딕셔너리)
        public class HashTable<TKey, TValue> : Collection<Pair<TKey, TValue>>
            where TKey : IComparable<TKey>
        {
            const int PRIMENUMBER = 5413;

            public override int Count => count;

            int size;

            LinkedList<Pair<TKey, TValue>>[] list;

            int count = 0;

            public HashTable(int initializeSize = 10000)
            {
                size = initializeSize;
                list = new LinkedList<Pair<TKey, TValue>>[size];

                for (int i = 0; i < size; i++)                
                    list[i] = new LinkedList<Pair<TKey, TValue>>();                
            }

            public void Add(TKey key, TValue value)
            {
                int hashCode = Hash(key);
                Pair<TKey, TValue> node = new Pair<TKey, TValue>(key, value);

                if (!ContainsKey(key))
                    list[hashCode].Add(node);
                else
                    this[key] = value;             
            }

            public override void Add(Pair<TKey, TValue> pair)
            {
                Add(pair.Key, pair.Value);
            }

            public bool Remove(TKey key, TValue value)
            {
                if (Contains(key, value))
                {
                    var removePair = new Pair<TKey, TValue>(key, value);
                    return list[Hash(key)].Remove(removePair);
                }
                return false;
            }

            public override bool Remove(Pair<TKey, TValue> pair)
            {
                return Remove(pair.Key, pair.Value);
            }            

            private int Hash(TKey key)
            {
                int hash = key.GetHashCode();

                hash = hash * PRIMENUMBER;

                hash = hash < 0 ? -hash : hash;

                return hash % (list.Length - 1);
            }

            public bool Contains(TKey key, TValue value)
            {
                return ContainsKey(key) && this[key].Equals(value);
            }

            public override bool Contains(Pair<TKey, TValue> pair)
            {
                return Contains(pair.Key, pair.Value);
            }

            public bool ContainsKey(TKey key)
            {
                return ToArray().Select(node => node.Key).Contains(key);
            }

            public bool ContainsValue(TValue value)
            {
                return ToArray().Select(node => node.Value).Contains(value);
            }

            public override void Clear()
            {
                list = new LinkedList<Pair<TKey, TValue>>[size];
                count = 0;
            }
         
            public override Pair<TKey, TValue>[] ToArray()
            {
                DynamicArray<Pair<TKey, TValue>> array = new DynamicArray<Pair<TKey, TValue>>();

                foreach (LinkedList<Pair<TKey, TValue>> linkedList in list)
                    array.AddRange(linkedList);

                return array.ToArray();
            }

            public TKey[] ToKeyArray()
            {
                return ToArray().Select(pair => pair.Key).ToArray();
            } 

            public TValue[] ToValueArray()
            {
                return ToArray().Select(pair => pair.Value).ToArray();
            }

            public TValue this[TKey key]
            {
                get
                {
                    if (!ContainsKey(key))
                        throw new Exception("does not exist");

                    var pairs = list[Hash(key)];

                    foreach (var pair in pairs)
                        if (pair.Key.Equals(key))
                            return pair.Value;

                    return default;
                }
                set
                {
                    if (!ContainsKey(key))
                    {
                        Add(key, value);
                    }
                    else
                    {
                        var newPair = new Pair<TKey, TValue>(key, value);
                        var pairs = list[Hash(key)];
                        var pair = pairs.GetNode(pairs.IndexOf(newPair));

                        pair.Value = newPair;
                    }
                }
            }
        }

    }
}
