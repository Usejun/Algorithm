using System.Collections.Generic;

namespace Algorithm
{
    interface IExpandArray<T>
    {
        T[] ToArray();

        List<T> ToList();

        T[] Sorted();
    }
}
