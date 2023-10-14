using System;
using System.Collections.Generic;

namespace Algorithm
{
    interface IEnumerate<T>
    {
        (int, T)[] ToEnumerate();
    }

    interface IHash<T>
    {
        int Prime { get; }

        int Hashing(T key);
    }
}
