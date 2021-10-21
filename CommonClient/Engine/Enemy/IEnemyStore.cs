using System;
using Common.Entity;
using Common.Store;

namespace CommonClient.Engine.Enemy
{
    public interface IEnemyStore: IStore<string, EnemyEntity>
    {
    }
}
