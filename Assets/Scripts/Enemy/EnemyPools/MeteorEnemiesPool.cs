using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Clicker
{
    internal sealed class MeteorEnemiesPool : BaseEnemyPool
    {
        #region Fields
        [Inject] private MeteorEnemy _meteorEnemy;

        private readonly EnemiesFactory _enemiesFactory;
        private readonly LevelConfig _levelConfig;
        private readonly List<BaseEnemy> _enemiesList;
        private readonly int enemiesAtPoolCount = 30;
        private Transform _folder;
        #endregion

        #region LifeCycles
        public MeteorEnemiesPool(
          EnemiesFactory enemiesFactory,
          LevelConfig levelConfig
          )
        {
            _enemiesFactory = enemiesFactory;
            _levelConfig = levelConfig;
            _enemiesList = new List<BaseEnemy>();
        } 
        #endregion

        #region Methods
        public override void GeneratePool()
        {
            _folder = new GameObject("Meteors").transform;
            _enemiesFactory.GenerateRandomAttributes(_levelConfig, enemiesAtPoolCount);

            for (int i = 0; i < enemiesAtPoolCount; i++)
            {
                var enemy = _enemiesFactory.InstantiateEnemy(_meteorEnemy, _folder);
                var attributes = _enemiesFactory.CachedAttributes.Dequeue();
                enemy.Scale = Mathf.Lerp(_levelConfig.MinScale, _levelConfig.MaxScale, attributes.Size);
                enemy.Mass = Mathf.Lerp(_levelConfig.MinMass, _levelConfig.MaxMass, attributes.Size);
                enemy.Velocity = enemy.GetRandomDirection() * attributes.InitialSpeed;
                enemy.gameObject.SetActive(false);
                _enemiesList.Add(enemy);
            }
        }

        public BaseEnemy GetEnemyFromPool() => base.GetEnemyFromPool(_enemiesList);
    
        public void ResetAll()
        {
            foreach (var enemy in _enemiesList)
                GameObject.Destroy(enemy.gameObject);

            _enemiesList.Clear();
            _enemiesFactory.CachedAttributes.Clear();
            GameObject.Destroy(_folder.gameObject);
        } 
        #endregion
    }
}