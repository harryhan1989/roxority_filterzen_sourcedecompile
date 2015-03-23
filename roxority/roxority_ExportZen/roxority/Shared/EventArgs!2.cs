namespace roxority.Shared
{
    using System;

    internal class EventArgs<T, U> : EventArgs<T>
    {
        private U value2;

        internal EventArgs(T value, U value2) : base(value)
        {
            this.value2 = value2;
        }

        internal U Value2
        {
            get
            {
                return this.value2;
            }
            set
            {
                this.value2 = value;
            }
        }
    }
}

