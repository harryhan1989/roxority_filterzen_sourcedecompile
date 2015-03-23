namespace roxority.Shared.Collections
{
    using System;
    using System.Collections;

    internal abstract class DisposableDictionaryBase : DictionaryBase, IDisposable
    {
        protected DisposableDictionaryBase()
        {
        }

        public void Dispose()
        {
            foreach (DictionaryEntry entry in this)
            {
                IDisposable disposable = entry.Value as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}

