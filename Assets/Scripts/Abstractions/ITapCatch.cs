using System;

namespace MonsterClicker
{
    internal interface ITapCatch
    {
        event Action<EnemyBase> OnEnemyTouch;
    }
}