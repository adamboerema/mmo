using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Entity;
using Server.Component.Enemy;

namespace Server.Engine.Enemy
{
    public class EnemyStore: IEnemyStore
    {
        private ConcurrentDictionary<string, EnemyComponent> _enemies
            = new ConcurrentDictionary<string, EnemyComponent>();

        public void Add(EnemyComponent component)
        {
            _enemies[component.Id] = component;
        }

        public EnemyComponent Get(string id)
        {
            return _enemies.ContainsKey(id) ? _enemies[id] : null;
        }

        public IDictionary<string, EnemyComponent> GetAll()
        {
            return _enemies;
        }

        public void Remove(string id)
        {
            _enemies.TryRemove(id, out _);
        }

        public void Update(EnemyComponent model)
        {
            _enemies.TryUpdate(model.Id, model, _enemies[model.Id]);
        }
    }
}
