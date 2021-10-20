using UnityEngine;

namespace Clicker
{
    internal interface IEnemy
    {
        float CurrentHp { get; set; }
        float MaxHp { get; }
    }
}
