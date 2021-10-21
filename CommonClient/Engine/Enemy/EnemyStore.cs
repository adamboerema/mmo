using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Entity;

namespace CommonClient.Engine.Enemy
{
    public class EnemyStore: IEnemyStore
    {
        private ConcurrentDictionary<string, EnemyEntity> _enemies
            = new ConcurrentDictionary<string, EnemyEntity>();

        public void Add(EnemyEntity model)
        {
            _enemies[model.Id] = model;
        }

        public EnemyEntity Get(string id)
        {
            return _enemies.ContainsKey(id) ? _enemies[id] : null;
        }

        public IDictionary<string, EnemyEntity> GetAll()
        {
            return _enemies;
        }

        public void Remove(string id)
        {
            _enemies.TryRemove(id, out _);
        }

        public void Update(EnemyEntity model)
        {
            _enemies.TryUpdate(model.Id, model, _enemies[model.Id]);
        }
    }
}
