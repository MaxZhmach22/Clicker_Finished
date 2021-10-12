using UnityEngine;

namespace Clicker
{
    internal sealed class EasyEnemy : EnemyBase
    {
        private int _tapCount = 1;
        public override void ReturnToPool(Transform transform)
        {
            _tapCount --;
            if (_tapCount <= 0)
            {
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.gameObject.SetActive(false);
                _tapCount = 1;
                base.ReturnToPool(transform);
            }
            
        }
    }
}
