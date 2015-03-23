namespace roxority.Shared.Collections
{
    using roxority.Shared;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;

    internal class LiveList<T> : List<T>
    {
        internal event EventHandler<EventArgs<T>> AddedItem;

        internal event CancelEventHandler<T> AddingItem;

        internal event EventHandler<EventArgs<int>> ChangedItem;

        internal event CancelEventHandler<Duo<int, T>> ChangingItem;

        internal event EventHandler<EventArgs<T>> RemovedItem;

        internal event CancelEventHandler<T> RemovingItem;

        internal void Add(T item)
        {
            CancelEventArgs<T> e = new CancelEventArgs<T>(item, false);
            this.OnAddingItem(e);
            if (!e.Cancel)
            {
                base.Add(item);
                this.OnAddedItem(new EventArgs<T>(item));
            }
        }

        protected virtual void OnAddedItem(EventArgs<T> e)
        {
            if (this.AddedItem != null)
            {
                this.AddedItem(this, e);
            }
        }

        protected virtual void OnAddingItem(CancelEventArgs<T> e)
        {
            if (this.AddingItem != null)
            {
                this.AddingItem(this, e);
            }
        }

        protected virtual void OnChangedItem(EventArgs<int> e)
        {
            if (this.ChangedItem != null)
            {
                this.ChangedItem(this, e);
            }
        }

        protected virtual void OnChangingItem(CancelEventArgs<Duo<int, T>> e)
        {
            if (this.ChangingItem != null)
            {
                this.ChangingItem(this, e);
            }
        }

        protected virtual void OnRemovedItem(EventArgs<T> e)
        {
            if (this.RemovedItem != null)
            {
                this.RemovedItem(this, e);
            }
        }

        protected virtual void OnRemovingItem(CancelEventArgs<T> e)
        {
            if (this.RemovingItem != null)
            {
                this.RemovingItem(this, e);
            }
        }

        internal void Remove(T item)
        {
            CancelEventArgs<T> e = new CancelEventArgs<T>(item, false);
            this.OnRemovingItem(e);
            if (!e.Cancel)
            {
                base.Remove(item);
                this.OnRemovedItem(new EventArgs<T>(item));
            }
        }

        internal T this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                CancelEventArgs<Duo<int, T>> e = new CancelEventArgs<Duo<int, T>>(new Duo<int, T>(index, value), false);
                this.OnChangingItem(e);
                if (!e.Cancel)
                {
                    base[index] = value;
                    this.OnChangedItem(new EventArgs<int>(index));
                }
            }
        }
    }
}

