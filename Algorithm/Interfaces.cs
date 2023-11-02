namespace Algorithm
{
    interface IHash<T>
    {   
        int Prime { get; }

        int Hashing(T key);
    }
    
}
