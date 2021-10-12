using System;
using UnityEngine;

namespace Clicker
{
    internal sealed class TapCatch : IExecute
    {

        private Touch _touch;
        public Action<EnemyBase> OnEnemyReturn;
        public Action<int> OnEnemyTap;
        private int tapInt = 100;

        void IExecute.Execute(float deltaTime)
        {
            if (Input.touchCount == 1)
            {
                
                _touch = Input.GetTouch(0);
                switch (_touch.phase)
                {
                    case TouchPhase.Ended:
                        RayHitEnemy();
                        break;
                    default:
                        break;
                }
                
             
            }
            if (Input.touchCount >= 2)
            {
                _touch = Input.GetTouch(1);
                switch (_touch.phase)
                {
                    case TouchPhase.Ended:
                        RayHitEnemy();
                        break;
                    default:
                        break;
                }
               
            }
        }
        private void RayHitEnemy()
        {

            Vector3 touchPoint = _touch.position;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(touchPoint), out RaycastHit hit, 100f))
            {
                var enemy = hit.collider.GetComponent<EnemyBase>();
                {
                    if (enemy != null)
                    {
                        enemy.ReturnToPool(enemy.transform);
                        OnEnemyReturn?.Invoke(enemy);
                        OnEnemyTap?.Invoke(tapInt);
                    }

                }
            }
 
        }
    }
}
