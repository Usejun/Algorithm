namespace Algorithm
{
    interface IHash<T>
    {   
        int Prime { get; }

        int Hashing(T key);
    }

    public interface IComparer<T>
    {
        int Compare(T x, T y);
    }

    public interface IComparer
    {
        int Compare(object x, object y);
    }
}
