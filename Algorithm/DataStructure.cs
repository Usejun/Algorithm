using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm
{
    namespace DataStructure
    {
        //배열 추가 기능
        public static class Extensions 
        {
            public static int[] Create(int length, int min, int max, bool isSorted = false)
            {
                Random r = new Random();

                var array = Enumerable.Repeat(0, length).Select(i => r.Next(min, max)).ToArray();

                if (isSorted) Sort(array); 

                return array;
            }

            public static T[] Sorted<T>(T[] array, Action<T[]> sorter = null)
                where T : IComparable<T>
            {
                T[] list = new T[array.Length];
                Array.Copy(array, list, array.Length);

                sorter = sorter ?? Sorts.QuickSort;
                sorter(list);

                return list;
            }

            public static T[] Sorted<T>(Collection<T> collection, Action<T[]> sorter = null)
                where T : IComparable<T>
            {
                return Sorted(collection.ToArray(), sorter);
            }

            public static void Sort<T>(T[] array, Action<T[]> sorter = null)
                where T : IComparable<T>
            {
                sorter = sorter ?? Sorts.QuickSort;
                sorter(array);
            }
        }

        //기본 배열 추상 클래스     
        public abstract class Collection<T> : IEnumerate<T>, IEnumerable<T>, ICollection<T>
        { 
            public virtual bool IsReadOnly => false;
            public virtual int Count => count;

            protected int count = 0;

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

        //기본 노드 클래스
        public class Node<T>
        {
            public T Value;

            public Node(T value)            {
                Value = value;
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

            public bool Equals(Pair<TKey, TValue> pair)
            {
                return key.Equals(pair.key) && value.Equals(pair.value);
            }

            public override string ToString()
            {
                return $"({key} : {value})";
            }

        }

        // 가변 배열
        public class List<T> : Collection<T>
        {
            public virtual bool IsFull => count == Length - 1;

            protected T[] source;
            protected int Length => source.Length;

            public virtual T this[int index]
            {
                get
                {
                    if (index < 0 || index >= count)
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

            public List(int initializeSize = 100)
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

                for (int i = 0; i < count; i++)
                    if (source[i].Equals(value))                   
                        for (int j = i + 1; j < count; j++)
                            source[j - 1] = source[j];

                count--;
                return true;
            }

            public virtual void RemoveAt(int index)
            {
                if (index < 0 || index >= count)
                    throw new Exception();  

                for (int i = index; i < count - 1; i++)
                    source[i] = source[i + 1];

                source = source.Take(count - 1).ToArray();
                count--;
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

            public virtual void Sort(Action<T[]> sort = null)
            {
                sort = sort ?? Sorts.QuickSort;
                sort(source);
            }
        }    

        // 양방향 링크드 리스트
        public class LinkedList<T> : List<T>, IList<T>
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
                    front.Connect(node, false);
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
                    back.Connect(node, true);
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
                
                if (Available(index))
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
                if (Available(index))
                    throw new Exception("Out of range");                

                if (count - index - 1 <= 0)
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

            public override void RemoveAt(int index)
            {
                if (count == 0)
                    throw new Exception();
                
                LinkedNode node = GetNode(index);

                node.before.Connect(node.after);
                count--;
            }

            public void RemoveRange(int start, int end)
            {
                if (end < start || count - (end - start + 1) < 0)
                    throw new Exception("Empty List");

                LinkedNode startNode = GetNode(start);
                LinkedNode endNode = GetNode(end);

                startNode.after.Connect(endNode.before);

                startNode.Clear();
                endNode.Clear();

                count -= end - start + 1; 
            }

            public int IndexOf(T value)
            {
                int index = 0;
                LinkedNode node = front;

                if (node == null)
                    return -1;

                while (node.Value is T nodeValue && value.Equals(nodeValue))
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

                if (node == null) throw new Exception("item is not Contains");

                return node;
            }

            bool Available(int index)
            {
                return !IsEmpty && 0 > index && index < count;
            }

            public override T this[int index]
            {
                get => GetNode(index).Value;
                set => GetNode(index).Value = value;
            }

        }

        // 우선순위 큐의 노드
        public class PriorityQueueNode<TValue, TPriority> : Node<TValue>, IComparable<PriorityQueueNode<TValue, TPriority>>
            where TPriority : IComparable<TPriority>
        {
            // 노드의 우선도
            public TPriority Priority;

            public PriorityQueueNode(TValue value, TPriority priority) : base(value)
            {
                Priority = priority;
            }

            public void Deconstruct(out TValue value, out TPriority priority)
            {
                value = Value;
                priority = Priority;
            } 

            public int CompareTo(PriorityQueueNode<TValue, TPriority> other) => CompareTo(other.Priority);

            public int CompareTo(TPriority other) => (Priority as IComparable<TPriority>).CompareTo(other);

            public override string ToString()
            {
                return $"Value : {Value}, Priority : {Priority}";
            }

        }

        // 우선순위 큐
        public class PriorityQueue<TValue, TPriority> : Collection<PriorityQueueNode<TValue, TPriority>>
            where TPriority : IComparable<TPriority>
        {
            public override int Count => heap.Count;
            public PriorityQueueNode<TValue, TPriority> Top => heap.Top;

            Heap<PriorityQueueNode<TValue, TPriority>> heap;
            Dictionary<TPriority, List<TValue>> keyValues;

            readonly bool reverse;

            public PriorityQueue()
            {
                Init();
            }

            public PriorityQueue(bool reverse)
            {
                this.reverse = reverse;
                Init(); 
            }

            void Init()
            {
                heap = new Heap<PriorityQueueNode<TValue, TPriority>>(reverse:reverse);
                keyValues = new Dictionary<TPriority, List<TValue>>();
            }

            public void Enqueue(TValue value, TPriority priority)
            {
                heap.Add(new PriorityQueueNode<TValue, TPriority>(value, priority));
                count++;

                if (!keyValues.ContainsKey(priority))
                {
                    keyValues.Add(priority, new List<TValue>());
                }

                keyValues[priority].Add(value);                    

            }

            public void Enqueue(PriorityQueueNode<TValue, TPriority> node)
            {
                Enqueue(node.Value, node.Priority);
            }

            public void EnqueueRange(params (TValue, TPriority)[] nodes)
            {
                foreach ((TValue value, TPriority priority) in nodes)
                    Enqueue(value, priority);
            }

            public void EnqueueRange(params PriorityQueueNode<TValue, TPriority>[] nodes)
            {
                foreach (PriorityQueueNode<TValue, TPriority> node in nodes)
                    Enqueue(node.Value, node.Priority);
            }

            public PriorityQueueNode<TValue, TPriority> Dequeue()
            {
                PriorityQueueNode<TValue, TPriority> node = heap.Top;

                heap.Pop();
                keyValues[node.Priority].Remove(node.Value);

                count--;

                return node;
            }

            public PriorityQueueNode<TValue, TPriority>[] Dequeue(int count)
            {
                PriorityQueueNode<TValue, TPriority>[] nodes = new PriorityQueueNode<TValue, TPriority>[count];

                for (int i = 0; i < count; i++)                
                    nodes[i] = Dequeue();
                
                return nodes;
            }

            public bool ContainsKey(TPriority priority)
            {
                return keyValues.ContainsKey(priority);
            }

            public override void Clear()
            {
                heap.Clear();
                keyValues.Clear();                
            }

            public override void Add(PriorityQueueNode<TValue, TPriority> value)
            {
                Enqueue(value);
            }

            public override bool Remove(PriorityQueueNode<TValue, TPriority> value)
            {
                return heap.Remove(value);
            }

            public override PriorityQueueNode<TValue, TPriority>[] ToArray()
            {
                PriorityQueueNode<TValue, TPriority>[] array = new PriorityQueueNode<TValue, TPriority>[Count];
                int index = 0;

                foreach (var priority in keyValues.Keys)                
                    foreach (var value in keyValues[priority])
                        array[index++] = new PriorityQueueNode<TValue, TPriority>(value, priority);                

                return array;
            }
        }

        // 스택 First in Last Out
        public class Stack<T> : List<T>
        {
            int front = 0;
            int size = 0;

            public bool IsEmpty => front == 0;
            public override bool IsFull => front == Length;

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
                if (Peek().Equals(value))
                    return false;

                Pop();
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
                T[] array = new T[Length + size];
                int back = Length - 1;

                while (!IsEmpty)
                    array[back--] = Pop();

                source = array.ToArray();
            }

            public override T[] ToArray()
            {
                T[] values = new T[count];
                int index = front - 1;

                for (int i = 0; index >= 0 && i < count; i++)                
                    values[i] = source[index--];
                
                return values;

            }

            public override void Sort(Action<T[]> sort = null)
            {
                sort = sort ?? Sorts.QuickSort;
                T[] array = ToArray();
                sort(array);

                Array.ConstrainedCopy(array, 0, source, 0, count);
            }

        }

        // 큐 First in First Out
        public class Queue<T> : List<T>
        {
            int front = 0;
            int back = 0;
            int size = 0;

            public bool IsEmpty => front == back;
            public override bool IsFull => (back + 1) % Length == front;

            public Queue(int initializeSize = 100)
            {
                size = initializeSize;
                source = new T[size];
            }

            public void Enqueue(T value)
            {
                if (IsFull)
                    Resize();

                source[back] = value;
                back = (back + 1) % Length;
                count++;
            }

            public T Dequeue()
            {
                if (IsEmpty)
                    throw new Exception("Stack is Empty");

                T value = source[front];
                front = (front + 1) % Length;
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
                if (Peek().Equals(value))
                    return false;

                Dequeue();
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

                int i = 0, j = front;

                while (j < back)
                {
                    values[i++] = source[j];
                    j = (j + 1) % Length;
                }

                return values;

            }

            void Resize()
            {
                T[] array = new T[Length + size];
                int i = 0;

                while (!IsEmpty)
                {
                    array[i++] = Dequeue();
                }

                source = array.ToArray();
                front = 0;
                back = i;
                count = i;
            }

            public override void Sort(Action<T[]> sort = null)
            {
                sort = sort ?? Sorts.QuickSort;
                T[] array = ToArray();
                sort(array);

                source = new T[Length];

                Array.ConstrainedCopy(array, 0, source, 0, count);
                front = 0;
                back = count;
            }
        }   
     
        // 딕셔너리
        public class Dictionary<TKey, TValue> : Collection<Pair<TKey, TValue>>, IHash<TKey>
        {
            public TKey[] Keys => ToArray().Select(pair => pair.Key).ToArray();
            public TValue[] Values => ToArray().Select(pair => pair.Value).ToArray();

            int size;
            const int PRIME = 5413;

            public int Prime { get => PRIME; }

            LinkedList<Pair<TKey, TValue>>[] list;

            void Init(int size)
            {
                list = new LinkedList<Pair<TKey, TValue>>[size];

                for (int i = 0; i < size; i++)
                    list[i] = new LinkedList<Pair<TKey, TValue>>();
            }

            public Dictionary(int initializeSize = 10000)
            {
                size = initializeSize;
                Init(size);               
            }

            public void Add(TKey key, TValue value)
            {
                int hashCode = Hash(key);
                Pair<TKey, TValue> node = new Pair<TKey, TValue>(key, value);

                if (!ContainsKey(key))
                {
                    list[hashCode].Add(node);
                    count++;
                }
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
                    count--;
                    return list[Hash(key)].Remove(removePair);
                }
                return false;
            }

            public override bool Remove(Pair<TKey, TValue> pair)
            {
                return Remove(pair.Key, pair.Value);
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
                Init(size);
            }
         
            public override Pair<TKey, TValue>[] ToArray()
            {
                List<Pair<TKey, TValue>> array = new List<Pair<TKey, TValue>>();

                foreach (LinkedList<Pair<TKey, TValue>> linkedList in list)
                    array.AddRange(linkedList);

                return array.ToArray();
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

            int Hash(TKey key)
            {
                int hash = key.GetHashCode();

                hash *= PRIME;

                hash = hash < 0 ? -hash : hash;

                return hash % (size - 1);
            }

            int IHash<TKey>.Hashing(TKey key)
            {
                return Hash(key);
            }
        }

        // 셋 
        public class Set<T> : Collection<T>, IHash<T>
        {
            int size;
            const int PRIME = 5413;

            LinkedList<T>[] list;

            public int Prime => PRIME;

            void Init()
            {
                list = new LinkedList<T>[size];

                for (int i = 0; i < size; i++)
                    list[i] = new LinkedList<T>();
            }

            public Set(int initializeSize = 1000)
            {
                size = initializeSize;
                Init();
            }

            public override void Add(T value)
            {
                if (this[value])
                    return;

                list[Hash(value)].Add(value);
                count++;
            }

            public void AddRange(params T[] values)
            {
                foreach (var value in values)
                    Add(value);
            }

            public override void Clear()
            {
                Init();
            }

            public override bool Remove(T value)
            {
                if (Contains(value))
                {
                    count--;
                    return list[Hash(value)].Remove(value);
                }
                else
                {
                    return false;
                }
            }

            public override bool Contains(T value)
            {
                int hash = Hash(value);

                return list[hash].Count != 0 && list[hash].Contains(value);
            }

            public override T[] ToArray()
            {
                List<T> array = new List<T>();

                foreach (var linked in list)
                    if (!linked.IsEmpty)
                        array.AddRange(linked);

                return array.ToArray();             
            }

            public bool this[T index]
            {
                get 
                {
                    return Contains(index);
                }
                set
                {
                    if (value && !Contains(index))
                        Add(index);
                    else if (!value)
                        Remove(index);
                }
            }

            int Hash(T value)
            {
                int hash = value.GetHashCode();

                hash *= PRIME;

                hash = hash < 0 ? -hash : hash;

                return hash % (size - 1);
            }

            public int Hashing(T key)
            {
                return Hash(key);
            }

        }

        // 힙
        public class Heap<T> : Collection<T>
        {
            public T Top => heap[0];
            
            List<T> heap;

            int size;
            bool reverse;

            IComparer<T> comparer;

            public Heap(int size = 100, IComparer<T> comparer = null, bool reverse = false, params T[] values)
            {
                if (comparer == null
                    && !typeof(T).GetInterfaces().Contains(typeof(IComparable))
                    && !typeof(T).GetInterfaces().Contains(typeof(IComparable<T>)))
                    throw new Exception("This type does not have ICompable.");                           

                this.comparer = comparer ?? Comparer<T>.Default;
                this.size = size;
                heap = new List<T>(size);
                this.reverse = reverse;

                foreach (T value in values)
                    Push(value);
            }

            public void Push(T value)
            {
                heap.Add(value);

                int now = heap.Count - 1;

                while (now != 0)
                {
                    int next = (now - 1) / 2;

                    if (Compare(heap[now], heap[next]) > 0)
                        (heap[now], heap[next]) = (heap[next], heap[now]);

                    now = next;
                }

                count++;
            }

            public T Pop()
            {
                if (count == 0)
                    throw new Exception("Empty Heap");                

                T value = heap[0];

                int last = heap.Count - 1;
                heap[0] = heap[last];
                heap.RemoveAt(last);
                count--;

                Heapify(0);

                return value;
            }

            public override void Add(T value)
            {
                Push(value);
            }            

            public override bool Remove(T value)
            {
                if (Top.Equals(value))
                {
                    Pop();
                    count--;
                    return true;
                }
                return false;
            }

            public override void Clear()
            {
                heap = new List<T>(size);
                count = 0;
            }

            public override T[] ToArray()
            {
                T[] array = heap.ToArray();

                for (int i = array.Length - 1; i >= 0; i--)
                {
                    (array[0], array[i]) = (array[i], array[0]);
                    int root = 0, c = 1;

                    do
                    {
                        c = 2 * root + 1;

                        if (c < i - 1 && comparer.Compare(array[c], array[c + 1]) < 0)
                            c++;

                        if (c < i && comparer.Compare(array[root], array[c]) < 0)
                            (array[root], array[c]) = (array[c], array[root]);

                        root = c;
                    } while (c < i);
                }

                return array;
            }

            public override bool Contains(T value)
            {
                return heap.Contains(value);
            }

            /// <returns>0 보다 작다. : x가 y보다 작다.<para>0 : x와 y가 같다.</para><para>0 보다 크다. : x가 y보다 크다.</para> 만약 reverse가 true라면 결과 값 또한 반대로 출력된다.</returns>
            int Compare(T x, T y) => reverse ? comparer.Compare(y, x) : comparer.Compare(x, y);

            void Heapify(int index)
            {
                int c = 2 * index + 1;

                if (c < count - 1 && Compare(heap[c], heap[c + 1]) < 0)
                    c++;
                if (c < count && Compare(heap[c], heap[index]) > 0)
                    (heap[c], heap[index]) = (heap[index], heap[c]);

                if (c < count / 2) 
                    Heapify(c);
            }
        }
    }
}
