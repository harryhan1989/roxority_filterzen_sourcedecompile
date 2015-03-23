using System.Collections;
using System.Collections.Generic;
using System;

namespace DsLib
{
    public class GenericCompare2<T> : IEqualityComparer, IEqualityComparer<T>
    {
        
        public new bool Equals(object x, object y)
        {
            return x == y;
        }
       
        public int GetHashCode(object obj)
        {
            return int.Parse(obj.ToString());
        }

        public bool Equals(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return EqualityComparer<T>.Default.GetHashCode(obj);
            //return int.Parse(obj.ToString());
        }
    }


    public static class GenericCompare
    {
        public static T Max<T>(T x, T y)
        {
            return (Comparer<T>.Default.Compare(x, y) > 0) ? x : y;
        }

        public static bool IsLessThan<T>(T x, T y)
        {
            return (Comparer<T>.Default.Compare(x, y) < 0) ? true : false;
        }

        public static bool IsEqual<T>(T x, T y)
        {
            return (Comparer<T>.Default.Compare(x, y) == 0) ? true : false;
        }

        public static bool IsGreaterThanEqual<T>(T x, T y)
        {
            return (Comparer<T>.Default.Compare(x, y) >= 0) ? true : false;
        }
    }

    public class GenericCompare1<T>
    {
        IComparer<T> _comparer;

        public GenericCompare1(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public GenericCompare1()
        {
            _comparer = Comparer<T>.Default;
        }

        public T Max(T x, T y)
        {
            return _comparer.Compare(x, y) > 0 ? x : y;
        }

        public bool IsLessThan(T x, T y)
        {
            return _comparer.Compare(x, y) < 0 ? true : false;
        }

        public bool IsEqual(T x, T y)
        {
            return _comparer.Compare(x, y) == 0 ? true : false;
        }

        public bool IsGreaterThanEqual(T x, T y)
        {
            return _comparer.Compare(x, y) >= 0 ? true : false;
        }

        public int Compare(T x, T y)
        {
            return _comparer.Compare(x, y);
        }
    }
}
