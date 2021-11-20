using System;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterClicker
{
    internal sealed class ActiveEnemyChekcer
    {
        #region Fields

        public Action<bool> OnGameOver;
        private List<EnemyBase> _activeEnemies = new List<EnemyBase>();

        #endregion


        #region ClassLifeCycles

        public ActiveEnemyChekcer() =>
            ResetAll();


        #endregion


        #region Methods

        private void ResetAll() =>
           _activeEnemies?.Clear();

        public void AddToActiveEnemyList(EnemyBase enemy)
        {
            _activeEnemies.Add(enemy);
            CheckCountActiveEnemies();
        }

        private void CheckCountActiveEnemies()
        {
            if (_activeEnemies.Count <= 10)
                return;

            Time.timeScale = 0;
            OnGameOver?.Invoke(true);
        }

        public void RemoveFromActiveEnemyList(EnemyBase enemy) =>
            _activeEnemies.Remove(enemy); 

        #endregion
    }
}