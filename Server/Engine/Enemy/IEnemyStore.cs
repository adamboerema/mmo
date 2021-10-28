using System;
using Common.Entity;
using Common.Store;
using Server.Component.Enemy;

namespace Server.Engine.Enemy
{
    public interface IEnemyStore: IStore<string, EnemyComponent>
    {
    }
}
