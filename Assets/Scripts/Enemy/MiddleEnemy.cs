using UnityEngine;

namespace Clicker
{
    internal sealed class MiddleEnemy : EnemyBase
    {
        private int _tapCount = 2;
        public override void ReturnToPool(Transform transform)
        {
            _tapCount --;
            transform.localScale = new Vector3(1, 1, 1);
            if (_tapCount <= 0)
            {
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.gameObject.SetActive(false);
                _tapCount = 2;
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                base.ReturnToPool(transform);
            }

        }
    }
}
