using System;
using System.Collections.Generic;

namespace CommonClient.Store
{
    public interface IStore<K, V>
    {
        public V Get(K index);

        public void Add(K index, V value);

        public void Update(K index, V value);

        public void Remove(K index);
    }
}
