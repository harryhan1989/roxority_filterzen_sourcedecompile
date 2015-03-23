namespace roxority.Shared
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class Trio<T> : Trio<T, T, T>
    {
        protected Trio(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        internal Trio(T item1, T item2, T item3) : base(item1, item2, item3)
        {
        }

        internal virtual T[] GetArray()
        {
            return new T[] { base.Value1, base.Value2, base.Value3 };
        }
    }
}

