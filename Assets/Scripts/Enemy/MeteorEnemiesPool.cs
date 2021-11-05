using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Clicker
{
    internal sealed class MeteorEnemiesPool : IEnemiesPool
    {
        #region Fields

        private readonly EnemiesFactory _enemiesFactory;
        private readonly LevelConfig _levelConfig;
        private readonly List<EnemyBase> _enemiesList;
        private readonly int enemiesAtPoolCount = 30;
        private Transform _parentTranform;
        #endregion

        #region LifeCycles
        public MeteorEnemiesPool(
          EnemiesFactory enemiesFactory,
          LevelConfig levelConfig
          )
        {
            _enemiesFactory = enemiesFactory;
            _levelConfig = levelConfig;
            _enemiesList = new List<EnemyBase>();
        } 
        #endregion

        #region Methods
        public void GeneratePool()
        {
            _parentTranform = new GameObject("Enemies").transform;
            _enemiesFactory.GenerateRandomAttributes(_levelConfig, enemiesAtPoolCount);

            for (int i = 0; i < enemiesAtPoolCount; i++)
            {
                var enemy = _enemiesFactory.InstantiateEnemy(_parentTranform);
                var attributes = _enemiesFactory.CachedAttributes.Dequeue();
                enemy.Scale = Mathf.Lerp(_levelConfig.MinScale, _levelConfig.MaxScale, attributes.Size);
                enemy.Mass = Mathf.Lerp(_levelConfig.MinMass, _levelConfig.MaxMass, attributes.Size);
                enemy.Velocity = enemy.GetRandomDirection() * attributes.InitialSpeed;
                enemy.gameObject.SetActive(false);
                _enemiesList.Add(enemy);
            }
        }


        public void ReturnToPool(EnemyBase enemy)
        {
            enemy.CurrentHp = enemy.MaxHp;
            enemy.gameObject.SetActive(false);
        }

        public EnemyBase GetEnemyFromPool()
        {
            if (_enemiesList.Count == 0)
                return null;

            foreach (var enemy in _enemiesList)
            {
                if (!enemy.gameObject.activeInHierarchy)
                    return enemy;
            }
            return null;
        }

        public void ResetAll()
        {
            foreach (var enemy in _enemiesList)
                GameObject.Destroy(enemy.gameObject);

            _enemiesList.Clear();
            _enemiesFactory.CachedAttributes.Clear();
            GameObject.Destroy(_parentTranform.gameObject);
        } 
        #endregion
    }
}