using System;
using System.Collections.Generic;
using Algorithm.DataStructure;
using static Algorithm.Utility;
using static Algorithm.Mathf;
using System.Collections;
using System.Linq;

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
        }

        // 양방향 링크드 리스트
        public class LinkedList<T> : IEnumerable<T>, IList<T>, ICollection<T>, IEnumerable, IExpandArray<T>
        {
            public int Count => count;
            public bool IsEmpty => count == 0;

            public bool IsReadOnly => false;

            public Func<T[], T[]> sort = Sort.QuickSort;

            Node<T> front = null;
            Node<T> back = null;
            int count = 0;

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
                Node<T> node = new Node<T>(value, before: front);
                if (front is null)
                    front = node;
                else
                {
                    front.after = node;
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
                Node<T> node = new Node<T>(value, after: back);
                if (back is null)
                    back = node;
                else
                {
                    back.before = node;
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
                Node<T> beforeNode = GetNode(index);
                Node<T> newNode = new Node<T>(value, after: beforeNode, before: beforeNode.before);

                beforeNode.before.after = newNode;
                beforeNode.before = newNode;

                count++;
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

            Node<T> GetNode(int index)
            {
                Node<T> node = front;
                for (int i = 0; i < index; i++)
                    node = node.before;

                return node;
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (Node<T> i = front; i != null; i = i.before)
                    yield return i.value;              
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                for (int i = arrayIndex; i < array.Length; i++)
                    array[i] = this[i];
            }

            public T[] Sorted()
            {
                return Sort.QuickSort(ToArray());
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

            public T this[int index]
            {
                get => GetNode(index).value;
                set => GetNode(index).value = value;
            }

        }

        // 우선순위 큐의 노드
        public class PriorityQueueNode<T, T1>
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
        }

        // 우선순위 큐 [ 트리 구조 ]
        public class PriorityQueue<T, T1>
        {
            // tree 구조를 가진 List를 구현
            List<PriorityQueueNode<T, T1>> source = new List<PriorityQueueNode<T, T1>>();

            // 노드의 우선도를 비교할 인터페이스
            IComparer<T1> comparer;

            // 큐 안에 들어있는 노드의 갯수
            public int Count => source.Count;

            // 큐의 우선순위 정렬 순서를 정하는 생성자
            public PriorityQueue(bool isReverse = false)
            {
                if (typeof(T1).GetInterface(nameof(IComparable<T1>)) == null)
                    throw new ArgumentException("IComparable가 존재하지 않는 형식입니다.");

                if (isReverse)
                    comparer = new Reverse<T1>(Comparer<T1>.Default);
                else
                    comparer = Comparer<T1>.Default;
            }

            public void Enqueue(T value, T1 priority)
            {
                // List안에 노드를 넣음
                source.Add(new PriorityQueueNode<T, T1>(value, priority));

                int currentIndex = source.Count - 1;

                while (currentIndex > 0)
                {
                    // 현재 노드의 부모 노드를 가져옴
                    int parentIndex = (currentIndex - 1) / 2;

                    // 노드들의 우선도를 비교함
                    if (comparer.Compare(source[currentIndex].Priority, source[parentIndex].Priority) >= 0)
                        break;

                    // 만약 비교한 값이 정렬 순서와 맞지 않다면 부모 노드와 자식 노드를 바꿔줌
                    Swap(currentIndex, parentIndex);

                    // 현재 노드의 인덱스는 부모 노드의 인덱스로 변경함
                    currentIndex = parentIndex;
                }

            }

            public PriorityQueueNode<T, T1> Dequeue()
            {
                if (source.Count == 0)
                    throw new Exception();

                var item = source[0];
                int currentIndex = 0;
                int lastIndex = source.Count - 1;

                source[0] = source[lastIndex];
                source.RemoveAt(lastIndex);

                while (true)
                {
                    int leftChild = currentIndex * 2 + 1;
                    int rightChild = currentIndex * 2 + 2;
                    int smallestChild = currentIndex;

                    if (leftChild < source.Count && comparer.Compare(source[leftChild].Priority, source[smallestChild].Priority) < 0)
                        smallestChild = leftChild;
                    if (rightChild < source.Count && comparer.Compare(source[rightChild].Priority, source[smallestChild].Priority) < 0)
                        smallestChild = rightChild;
                    if (smallestChild == currentIndex)
                        break;

                    Swap(currentIndex, smallestChild);
                    currentIndex = smallestChild;
                }

                return item;
            }

            public PriorityQueueNode<T, T1> Peek()
            {
                if (source.Count == 0)
                    throw new Exception();
                else
                    return source[0];
            }

            public void Swap(int x, int y) => (source[x], source[y]) = (source[y], source[x]);

        }

        // 정렬 순서를 바꿔주는 클래스
        public class Reverse<T> : IComparer<T>
        {
            private readonly IComparer<T> originalComparer;

            public Reverse(IComparer<T> originalComparer) => this.originalComparer = originalComparer;

            public int Compare(T x, T y) => originalComparer.Compare(y, x);

        }

        // 스택 First in Last Out
        public class Stack<T>
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
        }

        // 큐 First in First Out
        public class Queue<T>
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
        }

    }
}
