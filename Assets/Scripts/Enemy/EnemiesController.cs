using UniRx;
using UnityEngine;
using Zenject;
using System.Collections.Generic;


namespace Clicker
{
    internal sealed class EnemiesController : BaseController, ITickable, IFixedTickable
    {
        #region Fields

        public Subject<int> Score = new Subject<int>();
        public Subject<IEnemy> EnemyToExplose = new Subject<IEnemy>();
        public Subject<IEnemy> EnemyToShoot = new Subject<IEnemy>();

        private readonly InputTouchPresenter _inputTouchPresenter;
        private readonly MeteorEnemiesPool _meteorEnemiesPool;
        private readonly MissleEnemiesPool _missleEnemiesPool;
        private readonly EnemyMoveModel _enemyMoveModel;
        private readonly LevelConfig _levelConfig;
        private readonly LevelHelper _level;
        private readonly Player _player;

        private float _meteorTimer;
        private float _timeBeforeNextMissle = 3f;
        private float _missleTimer;
        private CompositeDisposable _disposables = new CompositeDisposable();
        private List<BaseEnemy> _activeEnemyList = new List<BaseEnemy>(); 

        #endregion


        #region ClassLifeCycles

        public EnemiesController(
            InputTouchPresenter inputTouchPresenter,
            MeteorEnemiesPool meteorEnemiesPool,
            MissleEnemiesPool missleEnemiesPool,
            EnemyMoveModel enemyMoveModel,
            LevelConfig levelConfig,
            LevelHelper level,
            Player player)
        {
            _inputTouchPresenter = inputTouchPresenter;
            _meteorEnemiesPool = meteorEnemiesPool;
            _missleEnemiesPool = missleEnemiesPool;
            _enemyMoveModel = enemyMoveModel;
            _levelConfig = levelConfig;
            _level = level;
            _player = player;
        }

        public override void Start()
        {
            SubscribeOnSubjects();
            _meteorEnemiesPool.GeneratePool();
            _missleEnemiesPool.GeneratePool();

            for (int i = 0; i < _levelConfig.EnemiesStartingSpawns; i++) { }
            SpawnNext(_meteorEnemiesPool.GetEnemyFromPool());

            Debug.Log($"{nameof(EnemiesController)} Is Subcribed; Disposables count = {_disposables.Count}");
        }

        public override void Dispose()
        {
            _meteorEnemiesPool.ResetAll();
            foreach (var enemy in _activeEnemyList)
                GameObject.Destroy(enemy.gameObject);

            _activeEnemyList.Clear();
            _disposables.Clear();
            Debug.Log($"{nameof(EnemiesController)} Is Disposed; Disposables count = {_disposables.Count}");
        } 
        #endregion


        #region ZenjectUpdateMethods

        public void Tick()
        {
            if (_player.CurrentGameState != GameStates.Game)
                return;
            _meteorTimer += Time.deltaTime;
            _missleTimer += Time.deltaTime;

            for (int i = 0; i < _activeEnemyList.Count; i++)
            {
                _activeEnemyList[i].Tick();
            }

            if (_meteorTimer >= _levelConfig.TimeBetweenReSpawn)
            {
                _meteorTimer = 0;
                SpawnNext(_meteorEnemiesPool.GetEnemyFromPool());
            }
            if (_missleTimer >= _timeBeforeNextMissle)
            {
                _meteorTimer = 0;
                _timeBeforeNextMissle = UnityEngine.Random.Range(35f, 45f);
                SpawnNext(_missleEnemiesPool.GetEnemyFromPool());
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
        #endregion


        #region Methods

        private void SpawnNext(BaseEnemy enemy)
        {
            if (enemy != null)
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

        Vector3 GetRandomStartPosition(float scale)
        {
            var side = (Side)UnityEngine.Random.Range(0, (int)Side.Count);
            switch (side)
            {
                case Side.Top:
                    return new Vector3(UnityEngine.Random.Range(_level.Left, _level.Right), _level.Top + scale, 0);
                case Side.Bottom:
                    return new Vector3(UnityEngine.Random.Range(_level.Left, _level.Right), _level.Bottom - scale, 0);
                case Side.Right:
                    return new Vector3(_level.Right + scale, UnityEngine.Random.Range(_level.Bottom, _level.Top), 0);
                case Side.Left:
                    return new Vector3(_level.Left - scale, UnityEngine.Random.Range(_level.Bottom, _level.Top), 0);
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

    #endregion
}
