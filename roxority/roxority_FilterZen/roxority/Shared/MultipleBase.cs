namespace roxority.Shared
{
    using System;

    public abstract class MultipleBase : ConvertibleBase<string>
    {
        protected MultipleBase() : base(null)
        {
        }

        protected MultipleBase(Get<string> handler) : base(handler)
        {
        }

        public override string ToString()
        {
            return this.Handler();
        }

        internal override Get<string> Handler
        {
            get
            {
                return base.Handler;
            }
            set
            {
                base.Handler = (value == null) ? new Get<string>(this.ToString) : value;
            }
        }
    }
}

