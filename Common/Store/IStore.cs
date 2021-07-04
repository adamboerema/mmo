using System;
using System.Collections.Generic;

namespace Common.Store
{
    public interface IStore<K, V>
    {
        public V Get(K id);

        public ICollection<KeyValuePair<K, V>> GetAll();

        public void Add(V model);

        public void Update(V model);

        public void Remove(K id);
    }
}
