using UnityEngine;

namespace MonsterClicker
{
    internal sealed class HardEnemy : EnemyBase
    {
        private int _tapCount = 3;
        public override void ReturnToPool(Transform transform)
        {
            _tapCount--;
            if(_tapCount == 2)
            {
                transform.localScale = new Vector3(2, 2, 2);
            }
            if(_tapCount == 1)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            if (_tapCount <= 0)
            {
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.gameObject.SetActive(false);
                _tapCount = 2;
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                base.ReturnToPool(transform);
            }

        }
    }
}
