namespace roxority.Data
{
    using System;
    using System.Collections.Generic;

    public class DataSourceCache
    {
        internal readonly List<string> cachedProperties = new List<string>();
        internal bool caching;
        internal int recacheHere;
        internal readonly List<CachedRecord> recordCache = new List<CachedRecord>();

        public void Clear()
        {
            this.recordCache.Clear();
            this.recacheHere = 0;
            this.caching = false;
            this.cachedProperties.Clear();
        }
    }
}

