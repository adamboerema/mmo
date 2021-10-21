using System;
using Common.Entity;
using Common.Store;

namespace Server.Engine.Enemy
{
    public interface IEnemyStore: IStore<string, EnemyEntity>
    {
    }
}
