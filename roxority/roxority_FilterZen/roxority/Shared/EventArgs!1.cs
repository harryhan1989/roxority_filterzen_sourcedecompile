namespace roxority.Shared
{
    using System;

    internal class EventArgs<T> : EventArgs
    {
        private T value;

        internal EventArgs(T value)
        {
            this.value = value;
        }

        internal EventArgs<T> SetValue(T value)
        {
            this.Value = value;
            return (EventArgs<T>) this;
        }

        internal T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}

