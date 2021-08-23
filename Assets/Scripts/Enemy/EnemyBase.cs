using System;
using UnityEngine;

namespace MonsterClicker
{
    internal abstract class EnemyBase : MonoBehaviour
    {
        public Action OnTapReturnToPool;

        public virtual void ReturnToPool(Transform transform)
        {
            OnTapReturnToPool?.Invoke();
        }
    }
}
