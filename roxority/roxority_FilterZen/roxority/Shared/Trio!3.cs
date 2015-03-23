namespace roxority.Shared
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class Trio<T, U, V> : Duo<T, U>
    {
        internal readonly V Value3;

        protected Trio(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Value3 = (V) info.GetValue("item3", typeof(V));
        }

        internal Trio(T item1, U item2, V item3) : this(item1, item2, item3, null)
        {
            this.Handler = new Get<string>(this.ToStringCore);
        }

        internal Trio(T item1, U item2, V item3, Get<string> handler) : base(item1, item2, handler)
        {
            this.Value3 = item3;
        }

        public override bool Equals(object obj)
        {
            Trio<T, U, V> trio = obj as Trio<T, U, V>;
            if (trio == null)
            {
                return false;
            }
            return (object.ReferenceEquals(this, obj) || ((object.Equals(base.Value1, trio.Value1) && object.Equals(base.Value2, trio.Value2)) && object.Equals(this.Value3, trio.Value3)));
        }

        public override int GetHashCode()
        {
            return SharedUtil.GetHashCode(new object[] { base.Value1, base.Value2, this.Value3 });
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("item3", this.Value3);
        }

        internal override Array ToArray()
        {
            return new object[] { base.Value1, base.Value2, this.Value3 };
        }

        protected override string ToStringCore()
        {
            return string.Format("{0} ; {1}; {2}", base.Value1, base.Value2, this.Value3);
        }
    }
}

