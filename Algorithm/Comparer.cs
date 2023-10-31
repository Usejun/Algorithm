using System;

namespace Algorithm
{
    public class Comparer : IComparer
    {
        public static IComparer Default = new Comparer();

        public virtual int Compare(object x, object y)
        {
            if (x.GetType().Equals(y.GetType()) && x is IComparable X && y is IComparable Y)
                return X.CompareTo(Y);
            
            throw new Exception("비교 할 수 없는 타입입니다.");
        }
    }

    public class Comparer<T> : Comparer, IComparer<T>
    {
        Func<T, T, int> comparer;
        
        public Comparer(Func<T, T, int> comparer = null)
        {
            comparer = comparer ?? Compare;
            this.comparer = comparer;
        }

        public int Compare(T x, T y)
        {
            if (x is IComparable X && y is IComparable Y)
                return X.CompareTo(Y);

            throw new Exception("비교 할 수 없는 타입입니다.");
        }

        public override int Compare(object x, object y)
        {
            if (x is T X && y is T Y)
                return comparer(X, Y);

            throw new Exception("비교 할 수 없는 타입입니다.");
        }
    }
}
