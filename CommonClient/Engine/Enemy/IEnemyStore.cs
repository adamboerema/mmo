using System;
using Common.Base;
using Common.Store;

namespace CommonClient.Engine.Enemy
{
    public interface IEnemyStore: IStore<string, EnemyModel>
    {
    }
}
