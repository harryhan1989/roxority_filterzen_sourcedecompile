namespace roxority.Shared
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class Exception<T> : Exception
    {
        internal readonly T Value;

        internal Exception(T value)
        {
            this.Value = value;
        }

        protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Value = (T) info.GetValue("Value", typeof(T));
        }

        internal Exception(string message, T value) : base(message)
        {
            this.Value = value;
        }

        internal Exception(string message, T value, Exception inner) : base(message, inner)
        {
            this.Value = value;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", this.Value);
            base.GetObjectData(info, context);
        }
    }
}

