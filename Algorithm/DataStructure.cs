using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.DataStructure
{    
    //배열 추가 기능
    public static class Extensions
    {
        public static Action<T[], Func<T, T1>, IComparer> Sorter<T, T1>()
            where T1 : IComparable<T1>
        {
            return Sorts.TimSort;
        }

        public static int[] Range(int length)
        {
            int[] values = new int[length];

            for (int i = 0; i < length; i++)            
                values[i] = i;

            return values;
        }

        public static int[] Range(int min, int max, int step = 1)
        {
            if (step == 0)
                throw new Exception("step is not zero");

            int[] values = new int[(max - min) / Mathf.Abs(step)];

            if (step > 0)
                for (int i = 0; i < values.Length; i++)
                    values[i] = i * step;
            else           
                for (int i = 0; i < values.Length; i++)
                    values[i] = (max - 1) + i * step;      
            
            return values;
        }

        public static int[] Create(int length, int min, int max, bool duplication = false)
        {
            Random r = new Random();
            if (duplication)
            {
                if (max - min < length)
                    throw new Exception("Out of range");

                Set<int> set = new Set<int>();

                while (set.Count != length)
                    set.Add(r.Next(min, max));

                return set.ToArray();
            }
            else
            {                
                int[] array = new int[length];

                for (int i = 0; i < length; i++)
                    array[i] = r.Next(min, max);

                return array;                
            }
        }

        public static T[] Sorted<T, T1>(T[] array, Func<T, T1> order, Action<T[], Func<T, T1>, IComparer> sort = null, IComparer comparer = null)
            where T1 : IComparable<T1>
        {
            T[] list = new T[array.Length];
            Copy(list, array, 0, array.Length);

            sort = sort ?? Sorter<T, T1>();
            sort(list, order, comparer);

            return list;
        }

        public static void Sort<T, T1>(T[] array, Func<T, T1> order, Action<T[], Func<T, T1>, IComparer> sort = null, IComparer comparer = null)
            where T1 : IComparable<T1>
        {
            sort = sort ?? Sorts.TimSort;
            sort(array, order, comparer);
        }

        public static bool Contains<T>(T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)            
                if (array[i].Equals(value))
                    return true;
            return false;
        }

        public static T1[] Convert<T, T1>(T[] array, Func<T, T1> converter)
        {
            T1[] convertedValues = new T1[array.Length];

            for (int i = 0; i < array.Length; i++)
                convertedValues[i] = converter(array[i]);

            return convertedValues;
        }        

        public static Pair<int, T>[] ToPairs<T>(T[] array)
        {
            Pair<int, T>[] pairs = new Pair<int, T>[array.Length];

            for (int i = 0; i < array.Length; i++)
                pairs[i] = new Pair<int, T>(i + 1, array[i]);

            return pairs;
        }

        public static void Copy<T>(T[] baseArray, T[] sourceArray, int index, int length)
        {
            if (sourceArray.Length <= index + length || baseArray.Length <= index + length)
                throw new ArgumentException("Out of range");

            for (int i = 0; i < length; i++)
                baseArray[index + i] = sourceArray[i];
        }

        public static int BinarySearch<T>(T[] array, T value)
            where T : IComparable<T>
        {
            int low = 0, high = array.Length - 1, mid = array.Length;

            while (low <= high)
            {
                mid = Mathf.Mid(low, high);

                if (array[mid].CompareTo(value) == 0)
                    return mid;
                else if (array[mid].CompareTo(value) > 0)
                    high = mid - 1;
                else
                    low = mid + 1;          
            }

            return -1;
        }

        public static int IndexOf<T>(T[] array, T value)
            where T : IComparable<T>
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i].Equals(value))
                    return i;
            return -1;
        }
    }

    // 좌표
    public struct Point
    {
        public int x;
        public int y;
        public int z;

        public Point(int x = 0, int y = 0, int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = this.x;
            y = this.y;
            z = this.z;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.x, a.z + b.z);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.x, a.z - b.z);
        }

        public static Point operator *(Point a, Point b)
        {
            return new Point(a.x * b.x, a.y * b.x, a.z * b.z);
        }

        public static Point operator /(Point a, Point b)
        {
            if (b.x * b.y * b.y == 0)
                throw new DivideByZeroException();

            return new Point(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Point operator %(Point a, Point b)
        {
            return new Point(a.x % b.x, a.y % b.x, a.z % b.z);
        }

        public static bool operator <(Point a, Point b)
        {
            return a.x == b.x ? a.y == b.y ? a.z < b.z : a.y < b.y : a.x < b.x;
        }

        public static bool operator >(Point a, Point b)
        {
            return a.x == b.x ? a.y == b.y ? a.z > b.z : a.y > b.y : a.x > b.x;
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(Point a, Point b)
        {
            return a.x != b.x || a.y != b.y || a.z != b.z;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point v && v == this) return true;
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string str = "";

            var fields = typeof(Point).GetFields();

            foreach (var field in fields)
                str += ($"({field.Name} : {field.GetValue(this)})");

            return str;
        }

        public static Point[] Create(int length, int min, int max)
        {
            Point[] Points = new Point[length];

            Random r = new Random();

            for (int i = 0; i < length; i++)
                Points[i] = new Point(r.Next(min, max), r.Next(min, max), r.Next(min, max));

            return Points;
        }
    }

    //기본 배열 추상 클래스     
    public abstract class Collection<T> : IEnumerable<T>
    {
        public virtual bool IsReadOnly => false;
        public virtual int Count => count;

        protected int count = 0;

        public virtual bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }
        public virtual void CopyTo(T[] array, int length)
        {
            Extensions.Copy(array, ToArray(), 0, length);
        }
        public virtual int IndexOf(T value)
        {
            T[] array = ToArray();

            for (int i = 0; i < count; i++)
                if (value.Equals(array[i]))
                    return i;

            return -1;
        }
        public virtual List<T> ToList()
        {
            return new List<T>(values:ToArray());
        }
        public virtual Pair<int, T>[] ToPairs()
        {
            T[] values = ToArray();
            Pair<int, T>[] pairs = new Pair<int, T>[count];

            for (int i = 0; i < count; i++)
                pairs[i] = new Pair<int, T>(i + 1, values[i]);
            
            return pairs;
        }
        public virtual T1[] Convert<T1>(Func<T, T1> converter)
        {
            T[] values = ToArray();
            T1[] convertedValues = new T1[count];

            for (int i = 0; i < count; i++)
                convertedValues[i] = converter(values[i]);

            return convertedValues;
        }
        public virtual IEnumerator<T> GetEnumerator()
        {
            T[] array = ToArray();
            foreach (var item in array)
                yield return item;
        }

        public abstract T[] ToArray();
        public abstract void Add(T value);
        public abstract bool Remove(T value);
        public abstract void Clear();

        IEnumerator IEnumerable.GetEnumerator()
        {
            T[] array = ToArray();
            foreach (var item in array)
                yield return item;
        }
    }

    //기본 노드 클래스
    public class Node<T>
    {
        public T Value;

        public Node(T value)
        {
            Value = value;
        }
    }

    // Key Value 형식의 구조체
    public struct Pair<TKey, TValue> 
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

        protected int Length => source.Length;

        protected T[] source;
        protected int size;

        public List(int size = 100, params T[] values)
        {
            this.size = size;
            source = new T[size];

            AddRange(values);
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

        public void Insert(int index, T value)
        {
            for (int i = index; i < count; i++)            
                source[i + 1] = source[i];

            source[index] = value;
            count++;
        }

        public override void Clear()
        {
            source = new T[Length];
            count = 0;
        }

        public override bool Remove(T value)
        {
            if (!Extensions.Contains(source, value))
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
                throw new Exception("Out of range");

            for (int i = index; i < count; i++)
                source[i] = source[i + 1];

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
            T[] values = new T[count];

            for (int i = 0; i < count; i++)            
                values[i] = source[i];

            return values;
        }

        public virtual T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new Exception("Out of range");
                return source[index];
            }
            set
            {
                if (index < 0 && index >= count)
                    throw new Exception("Out of range");
                source[index] = value;
            }
        }

        public virtual void Sort<T1>(Func<T, T1> order, Action<T[], Func<T, T1>, IComparer> sort = null, IComparer comparer = null)
            where T1 : IComparable<T1>
        {
            sort = sort ?? Extensions.Sorter<T, T1>();
            T[] array = ToArray();
            sort(array, order, comparer);

            Extensions.Copy(source, array, 0, count);
        }
    }

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
            if (count == 0)
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
            if (count == 0)
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

        public void RemoveAt(int index)
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

            for (int i = 0; i < count; i++)
                values[i] = GetNode(i).Value;

            return values;           
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

        public T this[int index]
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

    // 우선순위 큐, 기본 : 최소 힙
    public class PriorityQueue<TValue, TPriority> : Queue<PriorityQueueNode<TValue, TPriority>>
        where TPriority : IComparable<TPriority>
    {
        public override int Count => heap.Count;
        public PriorityQueueNode<TValue, TPriority> Top => heap.Top;

        Heap<PriorityQueueNode<TValue, TPriority>> heap;
        Dictionary<TPriority, List<TValue>> keyValues;

        readonly bool reverse;

        public PriorityQueue(bool reverse = false)
        {
            this.reverse = reverse;
            Init();
        }

        void Init()
        {
            heap = new Heap<PriorityQueueNode<TValue, TPriority>>(reverse: reverse);
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

        public override void Enqueue(PriorityQueueNode<TValue, TPriority> node)
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

        public override PriorityQueueNode<TValue, TPriority> Dequeue()
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
                for (int i = 0; i < keyValues[priority].Count; i++)
                    array[index++] = new PriorityQueueNode<TValue, TPriority>(keyValues[priority][i], priority);

            return array;
        }
    }

    // 스택 First in Last Out
    public class Stack<T> : List<T>
    {
        int front = 0;

        public bool IsEmpty => front == 0;
        public override bool IsFull => front == Length;

        public Stack(int size = 100)
        {
            this.size = size;
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

            source = array;
        }

        public override T[] ToArray()
        {
            T[] values = new T[count];
            int index = front - 1;

            for (int i = 0; index >= 0 && i < count; i++)
                values[i] = source[index--];

            return values;

        }

    }

    // 큐 First in First Out
    public class Queue<T> : List<T>
    {
        int front = 0;
        int back = 0;

        public bool IsEmpty => count == 0;
        public override bool IsFull => (back + 1) % Length == front;

        public Queue(int size = 100)
        {
            this.size = size;
            source = new T[size];
        }

        public virtual void Enqueue(T value)
        {
            if (IsFull)
                Resize();

            source[back] = value;
            back = (back + 1) % Length;
            count++;
        }

        public virtual T Dequeue()
        {
            if (IsEmpty)
                throw new Exception("Stack is Empty");

            T value = source[front];
            front = (front + 1) % Length;
            count--;

            return value;
        }

        public virtual void DequeueEnqueue()
        {
            Enqueue(Dequeue());
        }

        public virtual T Peek()
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

            source = array;
            front = 0;
            back = i;
            count = i;
        }

        public override void Sort<T1>(Func<T, T1> order, Action<T[], Func<T, T1>, IComparer> sort = null, IComparer comparer = null)
        {
            base.Sort(order, sort, comparer);

            front = 0;
            back = count;
        } 
    }  

    // 딕셔너리
    public class Dictionary<TKey, TValue> : Set<Pair<TKey, TValue>>, IHash<TKey>
    {
        public TKey[] Keys => Convert(pair => pair.Key);
        public TValue[] Values => Convert(pair => pair.Value);

        public Dictionary(int size = 10000)
        {
            this.size = size;
            Init();
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
            return Contains(new Pair<TKey, TValue>(key, value));
        }

        public override bool Contains(Pair<TKey, TValue> pair)
        {
            return ContainsKey(pair.Key) && list[Hash(pair.Key)].Equals(pair.Value);
        }

        public bool ContainsKey(TKey key)
        {
            return Extensions.Contains(Keys, key);
        }

        public bool ContainsValue(TValue value)
        {
            return Extensions.Contains(Values, value);
        }        

        public TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                    throw new Exception("does not exist");

                var pairs = list[Hash(key)];

                for (int i = 0; i < count; i++)
                    if (pairs[i].Key.Equals(key))
                        return pairs[i].Value;

                throw new Exception("does not exist");
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
                    var oldPair = pairs.GetNode(pairs.IndexOf(newPair));

                    oldPair.Value = newPair;
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

        public int Hashing(TKey key)
        {
            return Hash(key);
        }
    }

    // 셋 
    public class Set<T> : Collection<T>, IHash<T>
    {
        protected int size;
        protected const int PRIME = 5413;

        protected LinkedList<T>[] list;

        public int Prime => PRIME;

        protected void Init()
        {
            list = new LinkedList<T>[size];

            for (int i = 0; i < size; i++)
                list[i] = new LinkedList<T>();
        }

        public Set(int size = 10000)
        {
            this.size = size;
            Init();
        }

        public override void Add(T value)
        {
            if (Contains(value))
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
            return list[Hash(value)].Contains(value);
        }

        public override T[] ToArray()
        {
            List<T> array = new List<T>();

            foreach (var linked in list)
                if (!linked.IsEmpty)
                    array.AddRange(linked.ToArray());

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
                else if (!value && Contains(index))
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

    // 힙, 기본 : 최소 힙   
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
                && (!Extensions.Contains(typeof(T).GetInterfaces(), typeof(IComparable)) 
                &&  !Extensions.Contains(typeof(T).GetInterfaces(), typeof(IComparable<T>))))
                throw new Exception("This type does not have ICompable.");

            this.comparer = comparer ?? Comparer<T>.Default;
            this.size = size > values.Length ? size : values.Length;
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

        public void PushRange(params T[] values)
        {
            foreach (T value in values)
                Push(value);
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

                    if (c < i - 1 && Compare(array[c], array[c + 1]) < 0)
                        c++;

                    if (c < i && Compare(array[root], array[c]) < 0)
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

        public int Compare(T x, T y)
        {
           return reverse ? comparer.Compare(y, x) : comparer.Compare(x, y);
        }

        public void Heapify(int index)
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
