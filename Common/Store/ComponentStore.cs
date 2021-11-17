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

        public virtual void Add(T component)
        {
            _components[component.Id] = component;
        }

        public virtual T Get(string id)
        {
            return _components.ContainsKey(id) ? _components[id] : default;
        }

        public virtual IDictionary<string, T> GetAll()
        {
            return _components;
        }

        public virtual void Remove(string id)
        {
            _components.TryRemove(id, out _);
        }

        public virtual void Update(T component)
        {
            _components.TryUpdate(component.Id, component, _components[component.Id]);
        }
    }
}
