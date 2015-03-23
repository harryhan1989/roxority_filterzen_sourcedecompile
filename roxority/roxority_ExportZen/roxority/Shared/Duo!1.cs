namespace roxority.Shared
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class Duo<T> : Duo<T, T>
    {
        internal Duo(T item1, T item2) : base(item1, item2)
        {
        }

        protected Duo(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        internal virtual T[] GetArray()
        {
            return new T[] { base.Value1, base.Value2 };
        }
    }
}

