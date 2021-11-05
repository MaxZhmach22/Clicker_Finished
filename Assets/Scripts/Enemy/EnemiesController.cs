using System;
using UniRx;
using UnityEngine;
using Zenject;
using System.Linq;
using System.Collections.Generic;

namespace Clicker
{
    internal sealed class EnemiesController : BaseController, ITickable, IFixedTickable
    {
        public Subject<int> Score = new Subject<int>();
        public Subject<IEnemy> EnemyToExplose = new Subject<IEnemy>();
        public Subject<IEnemy> EnemyToShoot = new Subject<IEnemy>();

        private readonly InputTouchPresenter _inputTouchPresenter;
        private readonly MeteorEnemiesPool _enemiesPool;
        private readonly EnemyMoveModel _enemyMoveModel;
        private readonly LevelConfig _levelConfig;
        private readonly LevelHelper _level;
        private readonly Player _player;

        private float _timer;
        private CompositeDisposable _disposables = new CompositeDisposable();
        private List<EnemyBase> _activeEnemyList = new List<EnemyBase>();

        public EnemiesController(
            InputTouchPresenter inputTouchPresenter,
            MeteorEnemiesPool enemiesPool,
            EnemyMoveModel enemyMoveModel,
            LevelConfig levelConfig,
            LevelHelper level,
            Player player)
        {
            _inputTouchPresenter = inputTouchPresenter;
            _enemiesPool = enemiesPool;
            _enemyMoveModel = enemyMoveModel;
            _levelConfig = levelConfig;
            _level = level;
            _player = player;
        }

        public override void Start()
        {
            SubscribeOnSubjects();
            _enemiesPool.GeneratePool();

            for (int i = 0; i < _levelConfig.EnemiesStartingSpawns; i++) { }
                SpawnNext(_enemiesPool.GetEnemyFromPool());

            Debug.Log($"{nameof(EnemiesController)} Is Subcribed; Disposables count = {_disposables.Count}");
        }

        public override void Dispose()
        {
            _enemiesPool.ResetAll();
            foreach (var enemy in _activeEnemyList)
                GameObject.Destroy(enemy.gameObject);

            _activeEnemyList.Clear();
            _disposables.Clear();
            Debug.Log($"{nameof(EnemiesController)} Is Disposed; Disposables count = {_disposables.Count}");
        }


        private void SpawnNext(EnemyBase enemy)
        {
            if(enemy != null)
            {
                enemy.Position = GetRandomStartPosition(enemy.Scale);
                enemy.gameObject.SetActive(true);
                _activeEnemyList.Add(enemy);
            }
        }

        private void SubscribeOnSubjects()
        {
            _inputTouchPresenter.Enemy.Subscribe(enemy =>
            {
                if (enemy != null)
                    DecreaseHealth(enemy);
            }).AddTo(_disposables);
        }

        private void DecreaseHealth(IEnemy enemy)
        {
            if (enemy.IsDead)
                return;
            
            enemy.CurrentHp -= _player.Damage;
            Debug.Log(enemy.CurrentHp);

            if (enemy.CurrentHp <= 0)
            {
                Score.OnNext(enemy.ScorePoints);
                EnemyToExplose.OnNext(enemy);
                enemy.DeathStateInit();
            }
            else
            {
                EnemyToShoot.OnNext(enemy);
                enemy.TakeDamage();
            }
        }

        public void Tick()
        {
            if (_player.CurrentGameState != GameStates.Game)
                return;

            for (int i = 0; i < _activeEnemyList.Count; i++)
            {
                _activeEnemyList[i].Tick();
            }

            _timer += Time.deltaTime;
            if(_timer>= _levelConfig.TimeBetweenReSpawn)
            {
                _timer = 0;
                SpawnNext(_enemiesPool.GetEnemyFromPool());
            }
        }
        public void FixedTick()
        {
            if (_player.CurrentGameState != GameStates.Game)
                return;

            for (int i = 0; i < _activeEnemyList.Count; i++)
            {
                _activeEnemyList[i].FixedTick();
            }
        }
        /// <summary>
        /// Не используется
        /// </summary>
        private void SpawnEnemies()
        {
            var count = UnityEngine.Random.Range(0, _levelConfig.EnemiesCountAtOnceSpawn);
            if (_activeEnemyList.Count <= count)
                return;

            for (int i = 0; i < count; i++)
            {
                var enemy = _activeEnemyList
                    .Where(enemy => !enemy.gameObject.activeSelf)
                    .FirstOrDefault();

                enemy.MoveType = _enemyMoveModel.GetRandomMoveTypeValue();
                //enemy.SetStartPositioAndDirection(enemy.MoveType, _gameLevelView);
                enemy.GetActive();
                _activeEnemyList.Remove(enemy);
            }
        }

        Vector3 GetRandomStartPosition(float scale)
        {
            var side = (Side)UnityEngine.Random.Range(0, (int)Side.Count);
            var rand = UnityEngine.Random.Range(0.0f, _level.Right * 2);
            var posX = rand;
            var posZ = _level.Right * 2 - rand;


            switch (side)
            {
                case Side.Top:
                    {
                        return new Vector3(posX + scale, 1, posZ + scale);
                    }
                case Side.Bottom:
                    {
                        return new Vector3(-posX + scale, 1, -posZ + scale);
                    }
                case Side.Right:
                    {
                        return new Vector3(posX + scale, 1, -posZ + scale);
                    }
                case Side.Left:
                    {
                        return new Vector3(-posX + scale, 1, posZ + scale);
                    }
            }

            return default;
        }

        enum Side
        {
            Top,
            Bottom,
            Left,
            Right,
            Count
        }
    }
}
