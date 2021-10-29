using System;
using UniRx;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Clicker
{
    internal sealed class EnemiesController : BaseController, ITickable, IFixedTickable
    {
        public Subject<int> Score = new Subject<int>();

        private readonly InputTouchPresenter _inputTouchPresenter;
        private readonly GameLevelView _gameLevelView;
        private readonly EnemiesFactory _enemiesFactroy;
        private readonly EnemyMoveModel _enemyMoveModel;
        private readonly LevelConfig _levelConfig;
        private readonly LevelHelper _level;
        private readonly Player _player;

        private float _timer;

        public EnemiesController(
            InputTouchPresenter inputTouchPresenter,
            GameLevelView gameLevelView,
            EnemiesFactory enemiesFactroy,
            EnemyMoveModel enemyMoveModel,
            LevelConfig levelConfig,
            LevelHelper level,
            Player player)
        {
            _inputTouchPresenter = inputTouchPresenter;
            _gameLevelView = gameLevelView;
            _enemiesFactroy = enemiesFactroy;
            _enemyMoveModel = enemyMoveModel;
            _levelConfig = levelConfig;
            _level = level;
            _player = player;
        }

        public override void Start()
        {
            SubscribeOnSubjects();
            ResetAll();
            _enemiesFactroy.GenerateRandomAttributes(_levelConfig);

            for (int i = 0; i < _levelConfig.EnemiesStartingSpawns; i++)
            {
                SpawnNext(_level, _levelConfig, _player);
            }
        }

        private void SpawnNext(LevelHelper level, LevelConfig levelConfig, Player player)
        {
            var enemy = _enemiesFactroy.InstantiateEnemy(level, levelConfig, player);
            var attributes = _enemiesFactroy.CachedAttributes.Dequeue();

            enemy.Scale = Mathf.Lerp(_levelConfig.MinScale, _levelConfig.MaxScale, attributes.Size);
            enemy.Mass = Mathf.Lerp(_levelConfig.MinMass, _levelConfig.MaxMass, attributes.Size);
            enemy.Position = GetRandomStartPosition(enemy.Scale);
            enemy.Velocity = enemy.GetRandomDirection() * attributes.InitialSpeed;

            _enemiesFactroy.ListOfEnemies.Add(enemy);
            enemy.gameObject.SetActive(true);
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

        public void Tick()
        {
            for (int i = 0; i < _enemiesFactroy.ListOfEnemies.Count; i++)
            {
                _enemiesFactroy.ListOfEnemies[i].Tick();
            }

            _timer += Time.deltaTime;

            if(_timer>= _levelConfig.TimeBetweenReSpawn && _enemiesFactroy.ListOfEnemies.Count < _levelConfig.EnemiesCountPerLevel)
            {
                SpawnNext(_level, _levelConfig, _player);
                _timer = 0;
            }
        }
        public void FixedTick()
        {
            for (int i = 0; i < _enemiesFactroy.ListOfEnemies.Count; i++)
            {
                _enemiesFactroy.ListOfEnemies[i].FixedTick();
            }
        }

        private void SpawnEnemies()
        {
            var count = UnityEngine.Random.Range(0, _levelConfig.EnemiesCountAtOnceSpawn);
            if (_enemiesFactroy.ListOfEnemies.Count <= count)
                return;

            for (int i = 0; i < count; i++)
            {
                var enemy = _enemiesFactroy.ListOfEnemies
                    .Where(enemy => !enemy.gameObject.activeSelf)
                    .FirstOrDefault();

                enemy.MoveType = _enemyMoveModel.GetRandomMoveTypeValue();
                enemy.SetStartPositioAndDirection(enemy.MoveType, _gameLevelView);
                enemy.GetActive();
                _enemiesFactroy.ListOfEnemies.Remove(enemy);
            }
        }

        void ResetAll()
        {
            foreach (var enemy in _enemiesFactroy.ListOfEnemies)
            {
                GameObject.Destroy(enemy.gameObject);
            }
            _enemiesFactroy.ListOfEnemies.Clear();
            _enemiesFactroy.CachedAttributes.Clear();
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
