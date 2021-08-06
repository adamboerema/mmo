using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Model;

namespace CommonClient.Engine.Enemy
{
    public class EnemyStore: IEnemyStore
    {
        private ConcurrentDictionary<string, EnemyModel> _enemies
            = new ConcurrentDictionary<string, EnemyModel>();

        public void Add(EnemyModel model)
        {
            _enemies[model.Id] = model;
        }

        public EnemyModel Get(string id)
        {
            return _enemies.ContainsKey(id) ? _enemies[id] : null;
        }

        public IDictionary<string, EnemyModel> GetAll()
        {
            return _enemies;
        }

        public void Remove(string id)
        {
            _enemies.TryRemove(id, out _);
        }

        public void Update(EnemyModel model)
        {
            _enemies.TryUpdate(model.Id, model, _enemies[model.Id]);
        }
    }
}
