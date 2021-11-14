using System;
using UnityEngine;


namespace MonsterClicker
{
    internal sealed class TapCatch : IExecute, ITapCatch
    {
        #region Fields

        public event Action<EnemyBase> OnEnemyTouch;
        private Touch _touch;

       

        #endregion


        #region UnityMethods

        public void Execute(float deltaTime)
        {
            if (Input.touchCount == 1)
                ChooseFingerOnScreen(0);
            if (Input.touchCount >= 2)
                ChooseFingerOnScreen(1);
        }

        #endregion


        #region Methods

        private void ChooseFingerOnScreen(int countOfTouch)
        {
            _touch = Input.GetTouch(countOfTouch);
            switch (_touch.phase)
            {
                case TouchPhase.Ended:
                    RayHitEnemy();
                    break;
            }
        }

        private void RayHitEnemy()
        {
            Vector3 touchPoint = _touch.position;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(touchPoint), out RaycastHit hit, 100f))
            {
                if (hit.collider.TryGetComponent<EnemyBase>(out var enemy))
                {
                    enemy.ReturnToPool(enemy.transform);
                    OnEnemyTouch?.Invoke(enemy);
                }
            }
        } 
        #endregion
    }
}
