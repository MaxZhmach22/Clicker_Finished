using System;
using UnityEngine;

namespace Clicker
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
