using Algorithm.Datastructure;

namespace Algorithm.Technique
{
    //public class Group
    //{
    //    public int Length => length;

    //    readonly Dictionary<int, int> parents;

    //    readonly int length = 0;

    //    public Group(int length)
    //    {
    //        parents = new Dictionary<int, int>();
    //        this.length = length;

    //        for (int i = 1; i <= length; i++)
    //            parents[i] = i;
    //    }

    //    public void Union(int parent, int child)
    //    {
    //        parents[child] = parent;
    //    }

    //    public int Find(int root)
    //    {
    //        if (parents[root] == root) return root;
    //        return parents[root] = Find(parents[root]);
    //    }

    //    public int[] Child(int parent)
    //    {
    //        var childs = new List<int>();

    //        if (Contains(parent))
    //            foreach (int key in parents.Keys)
    //                if (parents[key] == parent)
    //                    childs.Add(key);

    //        return childs.ToArray();
    //    }

    //    public bool Contains(int parent)
    //    {
    //        return parents.ContainsKey(parent);
    //    }

    //    public int Depth(int root, int depth = 0)
    //    {
    //        if (parents[root] == root) return depth;
    //        return Depth(parents[root], depth + 1);
    //    }

    //    public void Sort()
    //    {
    //        foreach (int key in parents.Keys)
    //        {
    //            parents[key] = Find(key);
    //        }
    //    }

    //    public override string ToString()
    //    {
    //        string str = "";

    //        for (int i = 1; i <= length; i++)
    //            str += $"{i} : {parents[i]}\n";

    //        return str;
    //    }
    //}
}
