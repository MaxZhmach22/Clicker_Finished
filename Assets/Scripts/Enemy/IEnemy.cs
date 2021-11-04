using UnityEngine;

namespace Clicker
{
    internal interface IEnemy : IScorable, IDestroyable
    {
        float CurrentHp { get; set; }
        float MaxHp { get; }
        Vector3 CurrentPosition { get; }
        bool IsDead { get; set; }
        void DeathStateInit();

        void TakeDamage();
    }
}
