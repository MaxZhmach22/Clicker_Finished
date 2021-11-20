using System;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterClicker
{
    internal sealed class EnemiesController : IExecute, IDispose
    {
        #region Fields

        private readonly GameData _gameData;
        private readonly EnemyActivator _enemyActivator;
        private readonly ActiveEnemyChekcer _activeEnemyCheker;
        private readonly IDifficultyController _difficultyController;
        private readonly ITapCatch _tapCatch;
        private EnemyPool _easyEnemyPool;
        private EnemyPool _middleEnemyPool;
        private EnemyPool _hardEnemy;
        private List<EnemyPool> _enemyPools;
        private float _timer;

        #endregion


        #region ClassLifeCycles

        public EnemiesController(
            GameData gameData,
            ITapCatch tapCatch,
            Transform player)
        {
            _gameData = gameData;
            _tapCatch = tapCatch;
            EnemiesPoolsInit();
            _activeEnemyCheker = new ActiveEnemyChekcer();
            _enemyActivator = new EnemyActivator(_enemyPools, _gameData, player.position);
            Subscribe();
        }

        public void Dispose() =>
            Unsubscribe(); 

        #endregion

        public void Execute(float deltaTime)
        {
            //_timer += deltaTime;
            //if(_timer < _)
            //_enemyActivator
        }


        private void ListOfPoolsInit()
        {
            _enemyPools ??= new List<EnemyPool>();
            _enemyPools.Add(_easyEnemyPool);
            _enemyPools.Add(_middleEnemyPool);
            _enemyPools.Add(_hardEnemy);
        }

        private void Subscribe()
        {
            _enemyActivator.OnEnemyActivation += _activeEnemyCheker.AddToActiveEnemyList;
            //_tapCatch.OnSelectableTap += _activeEnemyCheker.RemoveFromActiveEnemyList;
            _activeEnemyCheker.OnGameOver += _gameData.GameOver;
        }

        private void EnemiesPoolsInit()
        {
            _easyEnemyPool = new EnemyPool(_gameData.EnemiesCountInPool, _gameData);
            _middleEnemyPool = new EnemyPool(_gameData.EnemiesCountInPool, _gameData);
            _hardEnemy = new EnemyPool(_gameData.EnemiesCountInPool, _gameData);
            ListOfPoolsInit();
        }

        private void Unsubscribe()
        {
            _enemyActivator.OnEnemyActivation -= _activeEnemyCheker.AddToActiveEnemyList;
            //_tapCatch.OnSelectableTap -= _activeEnemyCheker.RemoveFromActiveEnemyList;
            _activeEnemyCheker.OnGameOver -= _gameData.GameOver;
        }

    }
}