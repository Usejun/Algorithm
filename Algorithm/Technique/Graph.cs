using Algorithm.Datastructure;

namespace Algorithm.Technique
{
    public class Graph
    {
        public int Length => length;
        public int Count => count;

        private List<(int to, int edge)>[] node;
        private int length;
        private int count;

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
                throw new GraphException("Out of range");

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
                throw new GraphException("Out of range");

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
                throw new GraphException("Out of range");

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
                    throw new GraphException("Out of range");

                return node[index];
            }
        }
    }

    public class GraphException : System.Exception
    {
        public GraphException() { }
        public GraphException(string message) : base(message) { }
        public GraphException(string message, System.Exception inner) : base(message, inner) { }
        protected GraphException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
