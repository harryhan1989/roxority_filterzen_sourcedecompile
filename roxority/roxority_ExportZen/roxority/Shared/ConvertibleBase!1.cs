namespace roxority.Shared
{
    using System;

    public abstract class ConvertibleBase<T>
    {
        private Get<T> handler;

        protected ConvertibleBase() : this(null)
        {
        }

        protected ConvertibleBase(Get<T> handler)
        {
            this.Handler = handler;
        }

        internal virtual Get<T> Handler
        {
            get
            {
                return this.handler;
            }
            set
            {
                this.handler = value;
            }
        }
    }
}

