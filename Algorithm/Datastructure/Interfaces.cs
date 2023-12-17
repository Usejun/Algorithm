namespace Algorithm.Datastructure
{
    public interface IHash<T>
    {   
        int Prime { get; }

        int Hashing(T key);
    }

    public interface IQueue<T>
    {
        bool IsEmpty { get; }
        void Enqueue(T value);
        void EnqueueRange(params T[] values);
        T Dequeue();
        T[] DequeueRange(int repeat);
    }

    public interface IStack<T>
    {
        bool IsEmpty { get; }
        void Push(T value);
        void PushRange(params T[] values);
        T Pop();
        T[] PopRange(int repeat);
    }
    
}
