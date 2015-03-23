namespace roxority.Shared
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class CancelledException : Exception
    {
        internal CancelledException()
        {
        }

        internal CancelledException(string message) : base(message)
        {
        }

        protected CancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        internal CancelledException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

