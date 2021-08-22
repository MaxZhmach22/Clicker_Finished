using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    internal sealed class EnemiesController
    {
        private List<EnemyBase> _activeEnemies;
        private List<EnemyPool> _enemyPools;
        private EnemyActivation _enemyActivation;
        public Action<bool> OnGameOver;

        public EnemiesController(List<EnemyPool> enemyPools)
        {
            _enemyPools = enemyPools;
            _activeEnemies = new List<EnemyBase>();
            _enemyActivation = new EnemyActivation();
            _enemyActivation.OnEnemyAction += AddToAtiveEnemyList;
        }

        
        private void AddToAtiveEnemyList(EnemyBase enemy)
        {
            _activeEnemies.Add(enemy);
            CheckCountActiveEnemies();
        }


        private void CheckCountActiveEnemies()
        {
            if(_activeEnemies.Count > 9)
            {
                OnGameOver?.Invoke(true);
            }
        }

        private void RemoveAction()
        {
            _enemyActivation.OnEnemyAction -= AddToAtiveEnemyList;
        }
        
        
    }
}