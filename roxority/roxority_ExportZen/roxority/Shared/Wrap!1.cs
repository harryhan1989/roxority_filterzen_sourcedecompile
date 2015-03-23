namespace roxority.Shared
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class Wrap<T> : ISerializable where T: class
    {
        internal readonly T Value;

        internal Wrap(T value)
        {
            this.Value = value;
        }

        protected Wrap(SerializationInfo info, StreamingContext context)
        {
            this.Value = (T) info.GetValue("Wrap_Value", typeof(T));
        }

        public override bool Equals(object obj)
        {
            if (!SharedUtil.Equals(this, obj))
            {
                return SharedUtil.Equals(this.Value, obj);
            }
            return true;
        }

        public override int GetHashCode()
        {
            if (this.Value != null)
            {
                return this.Value.GetHashCode();
            }
            return 0;
        }

        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Wrap_Value", this.Value);
        }

        public static implicit operator Wrap<T>(T value)
        {
            return new Wrap<T>(value);
        }

        public static implicit operator T(Wrap<T> value)
        {
            return value.Value;
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(info, context);
        }

        public override string ToString()
        {
            if (this.Value != null)
            {
                return this.Value.ToString();
            }
            return null;
        }
    }
}

