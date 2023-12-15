using System;
using Algorithm.DataStructure;
using static Algorithm.DataStructure.Extensions;

namespace Algorithm.Technique
{
    public static class Techniques
    {
        public static int INF = 10000001;

        public static void Flyod(int[,] source)
        {
            int length = source.GetLength(0);

            for (int k = 0; k < length; k++)
                for (int i = 0; i < length; i++)
                    for (int j = 0; j < length; j++)
                        if (source[i, j] > source[i, k] + source[k, j])
                            source[i, j] = source[i, k] + source[k, j];
        }

        public static int[] Dijkstra(int start, int[,] source)
        {
            int length = source.GetLength(0);
            int[] distance = new int[length];  
            bool[] visit = new bool[length];

            for (int i = 0; i < length; i++)
                distance[i] = source[start, i];

            distance[start] = 0;
            visit[start] = true;

            for (int i = 0; i < length - 1; i++)
            {
                int node = SmallestNode();
                visit[node] = true;

                for (int j = 0; j < length; j++)
                    if (!visit[j])
                        if (distance[j] > distance[node] + source[node, j])
                            distance[j] = distance[node] + source[node, j];
            }

            return distance;

            int SmallestNode()
            {
                int min = INF + 1;
                int index = -1;

                for (int i = 0; i < length; i++)
                    if (!visit[i] && distance[i] < min)
                    {
                        min = distance[i];
                        index = i;
                    }

                return index;
            }
        }

        public static int[] Dijkstra(int start, Graph graph)
        {
            int length = graph.Length;
            int[] distance = Convert(Range(length), i => INF);
            PriorityQueue<(int n, int v), int> pq = new PriorityQueue<(int n, int v), int>();

            distance[start] = 0;
            pq.Enqueue((start, 0), 0);

            while (!pq.IsEmpty) 
            {
                (int n, int v) = pq.Dequeue().Value;

                if (distance[n] < v) continue;

                foreach ((int next, int nextV) in graph[n])
                {
                    if (nextV + v < distance[next])
                    {
                        distance[next] = nextV + v;
                        pq.Enqueue((next, nextV + v), nextV + v);
                    }
                }
            }

            return distance;
        }

        public static int[] Bellman(int start, Graph graph)
        {
            int length = graph.Length;
            int[] distance = Convert(Range(length + 1), i => INF);
            var edge = graph.AllNode();

            distance[start] = 0;

            for (int i = 0; i < length; i++)
            {
                foreach ((int s, int e, int t) in edge)
                {
                    if (distance[s] != INF && distance[e] > distance[s] + t)
                    {
                        distance[e] = distance[s] + t;
                        if (i == length - 1)                        
                            return new int[] { -1 };                        
                    }
                }
            }
            return distance;                        
        }

        public static Graph Kruskal(Graph graph)
        {
            int length = graph.Length;
            int count = graph.Count;

            Group group = new Group(length);
            Graph mst = new Graph(length);
            var edges = graph.AllNode();            

            Sort(edges, i => i.edge);

            for (int i = 0; i < count; i++)
            {
                (int from, int index, int node) = edges[i];

                if (group.Find(from) == group.Find(index)) continue;

                mst.Connect(from, index, node);

                group.Union(from, index);

                if (mst.Count == length - 1) break;
            }

            return mst;
        }
    }

    public class SegmentTree<T>
            where T : IComparable<T>
    {
        T[] array;
        T[] tree;
        int length;

        public int Length => length;

        public SegmentTree(T[] array)
        {
            tree = new T[array.Length * 4];
            this.array = array;
            length = array.Length;

            Init(0, length - 1, 1);
        }

        T Init(int start, int end, int node)
        {
            if (start == end) return tree[node] = array[start];

            int mid = Mathf.Mid(start, end);

            return tree[node] = (dynamic)Init(start, mid, node * 2) + Init(mid + 1, end, node * 2 + 1);
        }

        public T Sum(int left, int right, int start = 0, int end = -1, int node = 1)
        {
            if (end == -1) return Sum(left, right, start, length - 1, node);

            if (left > end || right < start)
                return default;
            if (left <= start && right >= end)
                return tree[node];

            int mid = Mathf.Mid(start, end);

            return (dynamic)Sum(left, right, start, mid, node * 2) + Sum(left, right, mid + 1, end, node * 2 + 1);
        }

        public void Update(int index, T diff, int start = 0, int end = -1, int node = 1)
        {
            if (end == -1)
            {
                Update(index, diff, start, length - 1, node);
                return;
            }

            if (index < start || index > end)
                return;

            tree[node] += (dynamic)diff;
            if (start == end)
                return;

            int mid = Mathf.Mid(start, end);

            Update(index, diff, start, mid, node);
            Update(index, diff, mid, end, node);
        }
    }

    public class Group
    {
        public int Length => length;

        readonly Dictionary<int, int> parents;
        
        readonly int length = 0;

        public Group(int length)
        {
            parents = new Dictionary<int, int>();
            this.length = length;

            for (int i = 1; i <= length; i++)
                parents[i] = i;          
        }

        public void Union(int parent, int child)
        {
            parents[child] = parent;
        }

        public int Find(int root)
        {
            if (parents[root] == root) return root;
            return parents[root] = Find(parents[root]);
        }

        public int[] Child(int parent)
        {
            var childs = new List<int>();

            if (Contains(parent))
                foreach (int key in parents.Keys)
                    if (parents[key] == parent)
                        childs.Add(key);

            return childs.ToArray();
        }

        public bool Contains(int parent)
        {
            return parents.ContainsKey(parent);
        }

        public int Depth(int root, int depth = 0)
        {
            if (parents[root] == root) return depth;
            return Depth(parents[root], depth + 1);
        }

        public void Sort()
        {
            foreach (int key in parents.Keys)
            {
                parents[key] = Find(key);
            }
        }

        public override string ToString()
        {
            string str = "";

            for (int i = 1; i <= length; i++)
                str += $"{i} : {parents[i]}\n";

            return str;
        }
    }

    public class Graph
    {
        public int Length => length;
        public int Count => count;

        List<(int to, int edge)>[] node;       
        int length;
        int count;

        public Graph(int length)
        {
            this.length = length;

            node = new List<(int, int)>[length + 1];

            for (int i = 1; i < length + 1; i++)            
                node[i] = new List<(int, int)>();            
        }

        public void Connect(int from, int to, int edge)
        {
            Connect(from, (to, edge));
        }

        public void Connect(int from, (int to, int edge) info)
        {
            Connect((from, info.to, info.edge));
        }

        public void Connect((int from, int to, int edge) info)
        {
            if (info.from < 0 || length < info.from ||
                info.to < 0 || length < info.to)
                throw new Exception("Out of range");

            if (!Update(info.from, (info.to, info.edge)))
            {
                node[info.from].Add((info.to, info.edge));
                count++;
            }
        }

        public bool IsConnected(int from, int to, int edge)
        {
            return IsConnected(from, (to, edge));
        }
        
        public bool IsConnected(int from, (int to, int edge) info)
        {
            if (from < 0 || length < from ||
                info.to < 0 || length < info.to)
                throw new Exception("Out of range");

            return node[from].Contains(info);
        }

        public bool Disconnect(int from, int to, int edge)
        {
            return Disconnect(from, (to, edge));
        }

        public bool Disconnect(int from, (int to, int edge) info)
        {
            if (from < 0 || length < from ||
                info.to < 0 || length < info.to)
                throw new Exception("Out of range");

            return node[from].Remove(info);
        }

        public bool Update(int from, (int to, int edge) info)
        {
            for (int i = 0; i < node[from].Count; i++)
            {
                if (node[from][i].to != info.to)
                    continue;

                if (node[from][i].edge < info.edge)
                {
                    node[from][i] = info;
                    return true;
                }
            }

            return false;
        }     

        public (int from, int to, int edge)[] AllNode()
        {
            (int from, int to, int edge)[] nodes = new (int from, int to, int edge)[count];
            int k = 0;

            for (int i = 1; i < length; i++)
                for (int j = 0; j < node[i].Count; j++)
                    nodes[k++] = (i, node[i][j].to, node[i][j].edge);

            return nodes;
        }

        public List<(int, int)> this[int index]
        {
            get
            {
                if (index < 0 || length <= index)
                    throw new Exception("Out of range");

                return node[index];
            }
        }
    }
}
    