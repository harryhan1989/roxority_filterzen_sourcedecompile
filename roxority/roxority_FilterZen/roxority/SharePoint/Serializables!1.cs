namespace roxority.SharePoint
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public class Serializables<T> : IEnumerable<T>, IEnumerable, ISerializable
    {
        public readonly T[] Values;

        public Serializables(IEnumerable<T> values)
        {
            this.Values = ((values == null) ? new List<T>() : new List<T>(values)).ToArray();
        }

        public Serializables(params T[] values)
        {
            this.Values = values;
        }

        public Serializables(SerializationInfo info, StreamingContext context)
        {
            int capacity = 0;
            try
            {
                capacity = info.GetInt32("Count");
            }
            catch
            {
            }
            List<T> list = new List<T>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                try
                {
                    object obj2;
                    if ((obj2 = info.GetValue("Item_" + i, typeof(T))) is T)
                    {
                        list.Add((T) obj2);
                    }
                }
                catch
                {
                }
            }
            this.Values = list.ToArray();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new List<T>(this.Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Values.GetEnumerator();
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Count", this.Values.Length);
            for (int i = 0; i < this.Values.Length; i++)
            {
                try
                {
                    info.AddValue("Item_" + i, this.Values[i]);
                }
                catch
                {
                }
            }
        }
    }
}

