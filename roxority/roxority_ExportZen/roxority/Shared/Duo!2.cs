namespace roxority.Shared
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class Duo<T, U> : MultipleBase, ISerializable
    {
        internal readonly T Value1;
        internal readonly U Value2;

        internal Duo(T item1, U item2) : this(item1, item2, null)
        {
            this.Handler = new Get<string>(this.ToStringCore);
        }

        protected Duo(SerializationInfo info, StreamingContext context)
        {
            this.Value1 = (T) info.GetValue("item1", typeof(T));
            this.Value2 = (U) info.GetValue("item2", typeof(U));
        }

        internal Duo(T item1, U item2, Get<string> handler) : base(handler)
        {
            this.Value1 = item1;
            this.Value2 = item2;
        }

        public override bool Equals(object obj)
        {
            Duo<T, U> objB = obj as Duo<T, U>;
            if (objB == null)
            {
                return false;
            }
            return (object.ReferenceEquals(this, objB) || (object.Equals(this.Value1, objB.Value1) && object.Equals(this.Value2, objB.Value2)));
        }

        public override int GetHashCode()
        {
            return SharedUtil.GetHashCode(new object[] { this.Value1, this.Value2 });
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("item1", this.Value1);
            info.AddValue("item2", this.Value2);
        }

        public static bool operator ==(Duo<T, U> one, Duo<T, U> two)
        {
            return ((object.ReferenceEquals(one, null) && object.ReferenceEquals(two, null)) || ((!object.ReferenceEquals(one, null) && !object.ReferenceEquals(two, null)) && one.Equals(two)));
        }

        public static bool operator !=(Duo<T, U> one, Duo<T, U> two)
        {
            return !(one == two);
        }

        internal virtual Array ToArray()
        {
            return new object[] { this.Value1, this.Value2 };
        }

        protected virtual string ToStringCore()
        {
            return string.Format("{0} ; {1}", this.Value1, this.Value2);
        }
    }
}

