using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    internal sealed class EnemiesController
    {
        private GameData _gameData; 
        private EnemyActivation _enemyActivation;
        private ActiveEnemyChekcer _activeEnemyCheker;
        private EnemyPool _easyEnemyPool;
        private EnemyPool _middleEnemyPool;
        private EnemyPool _hardEnemy;
        private InputInitialization _inputInit;
        private List<EnemyPool> _enemyPools = new List<EnemyPool>();


        public EnemiesController(GameData gameData, ExecuteController controller, InputInitialization inputInit)
        {
            _gameData = gameData;
            _inputInit = inputInit;
            _easyEnemyPool = new EnemyPool(10, _gameData);
            _middleEnemyPool = new EnemyPool(10, _gameData);
            _hardEnemy = new EnemyPool(10, _gameData);
            _enemyPools.Add(_easyEnemyPool);
            _enemyPools.Add(_middleEnemyPool);
            _enemyPools.Add(_hardEnemy);
            _activeEnemyCheker = new ActiveEnemyChekcer();
            _enemyActivation = new EnemyActivation(_enemyPools, _gameData, inputInit.GetPlayerPosition());
            _inputInit.TapCatch.OnEnemyTap += _enemyActivation.ChangeRandomTimeBetweenTaps;
            _enemyActivation.OnEnemyActivation += _activeEnemyCheker.AddToActiveEnemyList;
            controller.Add(_enemyActivation);
        }


        private void RemoveAction()
        {
            _enemyActivation.OnEnemyActivation -= _activeEnemyCheker.AddToActiveEnemyList;
            _inputInit.TapCatch.OnEnemyTap -= _enemyActivation.ChangeRandomTimeBetweenTaps;
        }
        
        
    }
}