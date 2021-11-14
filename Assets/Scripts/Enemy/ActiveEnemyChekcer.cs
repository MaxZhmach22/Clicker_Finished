using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker
{
    internal sealed class ActiveEnemyChekcer
    {
        private List<BaseEnemy> _activeEnemies = new List<BaseEnemy>();
        public Action<bool> OnGameOver;

        public ActiveEnemyChekcer()
        {
            _activeEnemies.Clear();
        }

        public void AddToActiveEnemyList(BaseEnemy enemy)
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
        public void RemoveFromEnemyList(BaseEnemy enemy)
        {
            _activeEnemies.Remove(enemy);
        }
    }
}