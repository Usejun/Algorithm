using System;
using System.Collections.Generic;

namespace Algorithm
{
    interface IEnumerate<T>
    {
        (int, T)[] ToEnumerate();
    }

    interface ISort<T>
    {
        Func<T[], T[]> Sorter { get; }

        void Sort();

        T[] Sorted();
        
    }
}
