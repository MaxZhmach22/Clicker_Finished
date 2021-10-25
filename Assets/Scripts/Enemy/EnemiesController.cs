using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Clicker
{
    internal sealed class EnemiesController : BaseController, ITickable
    {
        public Subject<int> Score = new Subject<int>();

        private readonly InputTouchPresenter _inputTouchPresenter;
        private readonly Player _player;
        private readonly EnemyMoveModel _enemyMoveModel;
        private readonly GameLevelView _gameLevelView;
        private readonly EnemiesFactory _enemiesFactroy;
        private readonly LevelConfig _levelConfig;

        private readonly List<EnemyBase> _listOfEnemies = new List<EnemyBase>();
        private float _timer;

        public EnemiesController(
            InputTouchPresenter inputTouchPresenter,
            Player player,
            GameLevelView gameLevelView,
            EnemiesFactory enemiesFactroy,
            EnemyMoveModel enemyMoveModel,
            LevelConfig levelConfig)
        {
            _inputTouchPresenter = inputTouchPresenter;
            _player = player;
            _enemyMoveModel = enemyMoveModel;
            _gameLevelView = gameLevelView;
            _enemiesFactroy = enemiesFactroy;
            _levelConfig = levelConfig;
            SubscribeOnSubjects();

            _listOfEnemies = _enemiesFactroy.InstantiateEnemies();

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

            enemy.CurrentHp -= _player.Damage;
            Debug.Log(enemy.CurrentHp);

            if (enemy.CurrentHp <= 0)
            {
                Score.OnNext(enemy.ScorePoints);
                enemy.DeathStateInit();
            }

        }



        public override void Start()
        {
           
        }

        public void Tick()
        {
            _timer += Time.deltaTime;
            if(_timer>= _levelConfig.TimeBetweenReSpawn)
            {
                SpawnEnemies();
                _timer = 0;
            }
        }

        private void SpawnEnemies()
        {
            var count = UnityEngine.Random.Range(0, _levelConfig.EnemiesCountAtOnceSpawn);
            if (_listOfEnemies.Count <= count)
                return;

            for (int i = 0; i < count; i++)
            {
                var enemy = _listOfEnemies
                    .Where(enemy => !enemy.gameObject.activeSelf)
                    .FirstOrDefault();

                enemy.MoveType = _enemyMoveModel.GetRandomMoveTypeValue();
                enemy.SetStartPositioAndDirection(enemy.MoveType, _gameLevelView);
                enemy.GetActive();
                _listOfEnemies.Remove(enemy);
            }
          
        }
    }
}
