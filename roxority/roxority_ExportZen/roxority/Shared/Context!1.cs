namespace roxority.Shared
{
    using System;

    internal class Context<T> : IContext, IDisposable
    {
        internal readonly T ContextObject;
        internal readonly Action<T> DisposeAction;

        internal Context(T contextObject, Action<T> disposeAction)
        {
            this.ContextObject = contextObject;
            this.DisposeAction = disposeAction;
        }

        public void Dispose()
        {
            if ((this.DisposeAction != null) && (this.ContextObject != null))
            {
                this.DisposeAction(this.ContextObject);
            }
            else if (this.ContextObject is IDisposable)
            {
                ((IDisposable) this.ContextObject).Dispose();
            }
        }

        object IContext.ContextObject
        {
            get
            {
                return this.ContextObject;
            }
        }
    }
}

