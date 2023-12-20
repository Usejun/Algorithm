using Algorithm.Datastructure;

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
            int[] distance = Extensions.Convert(Extensions.Range(length), i => INF);
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
            int[] distance = Extensions.Convert(Extensions.Range(length + 1), i => INF);
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

            Extensions.Sort(edges, i => i.edge);

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
}
    