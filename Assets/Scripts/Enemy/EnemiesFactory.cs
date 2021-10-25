using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace Clicker
{
    internal sealed class EnemiesFactory
    {
        [Inject] private Enemy _enemy;
        private readonly LevelConfig _levelConfigs;
        
        public EnemiesFactory(LevelConfig levelConfig)
        {
            _levelConfigs = levelConfig;
        }

        public List<EnemyBase> InstantiateEnemies()
        {
            var listOfEnenmiew = new List<EnemyBase>();

            for (int i = 0; i < _levelConfigs.EnemiesCountPerLevel; i++)
            {
                var enemy = GameObject.Instantiate(_enemy);
                enemy.gameObject.SetActive(false);
                listOfEnenmiew.Add(enemy);
            }

            return listOfEnenmiew;
        }
    }
}