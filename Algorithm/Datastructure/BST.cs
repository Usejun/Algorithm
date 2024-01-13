using System;

namespace Algorithm.Datastructure
{
    // 이진탐색트리 노드 방향
    public enum NodeType
    {
        Parent,
        Left,
        Right,
        End
    }
    
    // 이진탐색트리 노드
    public class BSTNode<TKey, TValue> : Node<TValue>, IComparable<BSTNode<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        private TKey key;
        private BSTNode<TKey, TValue>[] nodes = new BSTNode<TKey, TValue>[(int)NodeType.End];

        public TKey Key => key;
        public BSTNode<TKey, TValue>[] Nodes => nodes;

        public BSTNode(TKey key, TValue value) : base(value) 
        { 
            this.key = key; 
        }

        public void Deconstruct(out TKey key, out TValue value)
        {
            key = this.key;
            value = Value;
        }

        public int CompareTo(BSTNode<TKey, TValue> other)
        {
            return key.CompareTo(other.key);
        }

        public override string ToString()
        {
            return $"({key}, {Value})";
        }
    }

    // 이진탐색트리 BST
    public class BST<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private BSTNode<TKey, TValue> root;
        private int count;

        public int Count => count;

        public BST()
        {
            root = null;
            count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            if (root == null)
            {
                root = new BSTNode<TKey, TValue>(key, value);
            }
            else
            {
                var parent = root;
                var node = new BSTNode<TKey, TValue>(key, value);
                var type = NodeType.End;

                while (true)
                {
                    type = root.CompareTo(node) <= 0 ? NodeType.Left : NodeType.Right;

                    if (parent.Nodes[(int)type] == null)
                    {
                        node.Nodes[(int)NodeType.Parent] = parent;
                        parent.Nodes[(int)type] = node;
                        break;
                    }
                    else
                    {
                        parent = parent.Nodes[(int)type];
                    }
                }
            }
        }

        public bool Find(TKey key, TValue value)
        {
            return FindRoute(key, value) != null;
        }

        public BSTNode<TKey, TValue>[] FindRoute(TKey key, TValue value)
        {
            int depth = 0;
            var parent = root;
            var node = new BSTNode<TKey, TValue>(key, value);
            var type = NodeType.End;

            while (true)
            {
                type = root.CompareTo(node) <= 0 ? NodeType.Left : NodeType.Right;

                if (parent.Key.CompareTo(node.Key) == 0)                
                    break;                
                else
                {
                    parent = parent.Nodes[(int)type];                
                    depth++;
                }
            }

            BSTNode<TKey, TValue>[] nodes = new BSTNode<TKey, TValue>[depth + 2];

            for (int i = 0; i <= depth; i++)
            {
                nodes[depth - i] = parent;
                parent = parent.Nodes[(int)NodeType.Parent];
            }

            return nodes;
        }

    }
}
