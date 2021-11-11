using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Component;

namespace Common.Store
{
    public class ComponentStore<T>: IStore<string, T> where T: IComponent
    {
        private ConcurrentDictionary<string, T> _components
            = new ConcurrentDictionary<string, T>();

        public void Add(T component)
        {
            _components[component.Id] = component;
        }

        public T Get(string id)
        {
            return _components.ContainsKey(id) ? _components[id] : default;
        }

        public IDictionary<string, T> GetAll()
        {
            return _components;
        }

        public void Remove(string id)
        {
            _components.TryRemove(id, out _);
        }

        public void Update(T component)
        {
            _components.TryUpdate(component.Id, component, _components[component.Id]);
        }
    }
}
