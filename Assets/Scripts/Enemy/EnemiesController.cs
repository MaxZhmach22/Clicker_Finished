using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker
{
    internal sealed class EnemiesController : BaseController
    {
        public Subject<int> Score = new Subject<int>();

        private readonly InputTouchPresenter _inputTouchPresenter;
        private readonly Player _player;

        public EnemiesController(InputTouchPresenter inputTouchPresenter, Player player)
        {
            _inputTouchPresenter = inputTouchPresenter;
            _player = player;

            SubscribeOnSubjects();
        }

        private void SubscribeOnSubjects()
        {
            _inputTouchPresenter.Enemy.Subscribe(enemy =>
            {
                if (enemy is IDestroyable destroyable)
                {
                    destroyable.TakeDamageEffectsInit();
                }
                if (enemy != null)
                    DecreaseHealth(enemy);
            });
        }

        public void DecreaseHealth(IEnemy enemy)
        {
            if (enemy.IsDead)
                return;

            if (enemy.CurrentHp <= 0)
            {
                Score.OnNext(enemy.ScorePoints);
                enemy.DeathStateInit();
            }

            enemy.CurrentHp -= _player.Damage;
            Debug.Log(enemy.CurrentHp);
        }

        public override void Start()
        {
           
        }
    }
}
