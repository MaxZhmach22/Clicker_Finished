using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker
{
    internal sealed class EnemyModelTest
    {

        private InputTouchPresenter _inputTouchPresenter;

        public EnemyModelTest(InputTouchPresenter inputTouchPresenter)
        {
            _inputTouchPresenter = inputTouchPresenter;
            _inputTouchPresenter.Enemy.Subscribe(enemy =>
            { 
                if(enemy != null)
                DecreaseHealth(enemy); 
            });
        }

        public void DecreaseHealth( IEnemy enemy)
        {
            enemy.CurrentHp -= 35;
            Debug.Log(enemy.CurrentHp);
        }
    }
}
