namespace roxority.Shared
{
    using System;
    using System.Collections.Generic;

    internal class DuoList<T, U> : List<Duo<T, U>>
    {
        internal DuoList()
        {
        }

        internal DuoList(IEnumerable<T> values1, IEnumerable<U> values2) : base(SharedUtil.Enumerate<T, U>(values1, values2))
        {
        }
    }
}

