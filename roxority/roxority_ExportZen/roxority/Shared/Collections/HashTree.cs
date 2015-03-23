namespace roxority.Shared.Collections
{
    using roxority.Shared;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal sealed class HashTree : DictionaryBase
    {
        internal void Add(object value, params object[] keys)
        {
            object obj2;
            this.GetDeepHashtable(keys, 0, true, out obj2).InnerHashtable.Add(obj2, value);
        }

        internal bool Contains(params object[] keys)
        {
            object obj2;
            HashTree tree = this.GetDeepHashtable(keys, 0, false, out obj2);
            return (((tree != null) && (obj2 != null)) && tree.Dictionary.Contains(obj2));
        }

        internal ICollection GetAllValues()
        {
            ArrayList arrayList = new ArrayList(base.Count * 2);
            this.GetAllValues(arrayList);
            return arrayList;
        }

        private void GetAllValues(ArrayList arrayList)
        {
            foreach (object obj2 in base.InnerHashtable.Values)
            {
                HashTree tree = obj2 as HashTree;
                if (tree != null)
                {
                    tree.GetAllValues(arrayList);
                }
                else
                {
                    arrayList.Add(obj2);
                }
            }
        }

        private HashTree GetDeepHashtable(object[] keys, int index, bool create, out object key)
        {
            HashTree tree;
            if (SharedUtil.IsEmpty((ICollection) keys))
            {
                throw new ArgumentNullException("keys");
            }
            key = keys[index];
            if (index == (keys.Length - 1))
            {
                return this;
            }
            if (((tree = base.InnerHashtable[keys[index]] as HashTree) == null) && create)
            {
                base.InnerHashtable[keys[index]] = tree = new HashTree();
            }
            if ((tree != null) && (tree != null))
            {
                return tree.GetDeepHashtable(keys, index + 1, create, out key);
            }
            return null;
        }

        protected override void OnValidate(object key, object value)
        {
            if (value is HashTree)
            {
                throw new ArgumentException(null, "value");
            }
            base.OnValidate(key, value);
        }

        internal void Remove(params object[] keys)
        {
            object obj2;
            HashTree tree = this.GetDeepHashtable(keys, 0, false, out obj2);
            if ((tree != null) && (obj2 != null))
            {
                tree.Dictionary.Remove(obj2);
            }
        }

        internal object this[object[] keys]
        {
            get
            {
                object obj2;
                HashTree tree = this.GetDeepHashtable(keys, 0, false, out obj2);
                if ((tree != null) && (obj2 != null))
                {
                    return tree.InnerHashtable[obj2];
                }
                return null;
            }
            set
            {
                object obj2;
                this.GetDeepHashtable(keys, 0, true, out obj2).InnerHashtable[obj2] = value;
            }
        }
    }
}

