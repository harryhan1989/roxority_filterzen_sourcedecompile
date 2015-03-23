namespace roxority.Shared.Collections
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class DisposableHashtable : DisposableDictionaryBase
    {
        internal virtual void Add(object key, object value)
        {
            base.Dictionary.Add(key, value);
        }

        internal bool Contains(object key)
        {
            return this.ContainsKey(key);
        }

        internal virtual bool ContainsKey(object key)
        {
            return base.Dictionary.Contains(key);
        }

        internal virtual bool ContainsValue(object value)
        {
            return base.InnerHashtable.ContainsValue(value);
        }

        internal virtual void Remove(object key)
        {
            base.Dictionary.Remove(key);
        }

        internal virtual object this[object key]
        {
            get
            {
                return base.Dictionary[key];
            }
            set
            {
                base.Dictionary[key] = value;
            }
        }

        internal virtual ICollection Keys
        {
            get
            {
                return base.Dictionary.Keys;
            }
        }

        internal virtual object SyncRoot
        {
            get
            {
                return base.Dictionary.SyncRoot;
            }
        }
    }
}

