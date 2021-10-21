using UnityEngine;

namespace Clicker
{
    internal interface IEnemy
    {
        float CurrentHp { get; set; }
        float MaxHp { get; }

        Vector3 CurrentPosition { get; }

        int ScorePoints { get; }

        bool IsDead { get; set; }
        void DeathStateInit();
    }
}
