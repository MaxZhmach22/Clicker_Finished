using System;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterClicker
{
    internal sealed class EnemiesController : IDispose
    {
        #region Fields

        private readonly GameData _gameData;
        private readonly EnemyActivation _enemyActivation;
        private readonly ActiveEnemyChekcer _activeEnemyCheker;
        private readonly ITapCatch _tapCatch;
        private EnemyPool _easyEnemyPool;
        private EnemyPool _middleEnemyPool;
        private EnemyPool _hardEnemy;
        private List<EnemyPool> _enemyPools;

        #endregion


        #region ClassLifeCycles

        public EnemiesController(
            GameData gameData,
            ExecuteController controller,
            ITapCatch tapCatch,
            Transform player)
        {
            _gameData = gameData;
            _tapCatch = tapCatch;
            EnemiesPoolsInit();
            _activeEnemyCheker = new ActiveEnemyChekcer();
            _enemyActivation = new EnemyActivation(_enemyPools, _gameData, player.position);
            Subscribe();
            controller.Add(_enemyActivation);
        }

        public void Dispose() =>
            Unsubscribe(); 

        #endregion


        private void ListOfPoolsInit()
        {
            _enemyPools ??= new List<EnemyPool>();
            _enemyPools.Add(_easyEnemyPool);
            _enemyPools.Add(_middleEnemyPool);
            _enemyPools.Add(_hardEnemy);
        }

        private void Subscribe()
        {
            _enemyActivation.OnEnemyActivation += _activeEnemyCheker.AddToActiveEnemyList;
            _tapCatch.OnEnemyTouch += _activeEnemyCheker.RemoveFromEnemyList;
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
            _enemyActivation.OnEnemyActivation -= _activeEnemyCheker.AddToActiveEnemyList;
            _tapCatch.OnEnemyTouch -= _activeEnemyCheker.RemoveFromEnemyList;
            _activeEnemyCheker.OnGameOver -= _gameData.GameOver;
        }
    }
}