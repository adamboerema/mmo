using System;
using Common.Model;
using Common.Store;

namespace CommonClient.Engine.Enemy
{
    public interface IEnemyStore: IStore<string, EnemyModel>
    {
    }
}
