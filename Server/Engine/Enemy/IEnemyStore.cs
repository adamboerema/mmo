using System;
using Common.Model;
using Common.Store;

namespace Server.Engine.Enemy
{
    public interface IEnemyStore: IStore<string, EnemyModel>
    {
    }
}
