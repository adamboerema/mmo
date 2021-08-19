using System;
using Common.Base;
using Common.Store;

namespace Server.Engine.Enemy
{
    public interface IEnemyStore: IStore<string, EnemyModel>
    {
    }
}
