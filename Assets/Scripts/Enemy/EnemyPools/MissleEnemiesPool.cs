using System.Collections.Generic;
using UnityEngine;

namespace Clicker
{
    internal sealed class MissleEnemiesPool : BaseEnemyPool
    {
        #region Fields

        private readonly MissleEnemy _missleEnemy;
        private readonly EnemiesFactory _enemiesFactory; //TODO сделать фабрику единственной
        private readonly List<BaseEnemy> _missleEnemiesList;
        private readonly LevelConfig _levelConfig;
        private Transform _folder;


        #endregion


        #region LifeCycles

        public MissleEnemiesPool(
            EnemiesFactory enemiesFactory,
            MissleEnemy missleEnemy,
            LevelConfig levelConfig
            )
        {
            _enemiesFactory = enemiesFactory;
            _missleEnemy = missleEnemy;
            _levelConfig = levelConfig;
            _missleEnemiesList = new List<BaseEnemy>();
        }

        #endregion


        #region Methods

        public override void GeneratePool()
        {
            _folder = new GameObject("Missles").transform;
            for (int i = 0; i < _levelConfig.MissleEnemyCountPerLevel; i++)
            {
                var enemy = _enemiesFactory.InstantiateEnemy(_missleEnemy, _folder);
                enemy.gameObject.SetActive(false);
                _missleEnemiesList.Add(enemy);
            }
        }

        public BaseEnemy GetEnemyFromPool() =>
            base.GetEnemyFromPool(_missleEnemiesList); 

        #endregion

    }
}
