using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    internal sealed class ActiveEnemyChekcer
    {
        private List<EnemyBase> _activeEnemies = new List<EnemyBase>();
        public Action<bool> OnGameOver;

        public ActiveEnemyChekcer()
        {
            _activeEnemies.Clear();
        }

        public void AddToActiveEnemyList(EnemyBase enemy)
        {
            _activeEnemies.Add(enemy);
            CheckCountActiveEnemies();
        }


        private void CheckCountActiveEnemies()
        {
            if (_activeEnemies.Count > 9)
            {
                Time.timeScale = 0;
                OnGameOver?.Invoke(true);
            }

        }
        public void RemoveFromEnemyList(EnemyBase enemy)
        {
            _activeEnemies.Remove(enemy);
        }
    }
}