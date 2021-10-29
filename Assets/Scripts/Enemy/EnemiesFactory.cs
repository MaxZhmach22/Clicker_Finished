using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace Clicker
{
    internal sealed class EnemiesFactory
    {
        [Inject] private Enemy _enemy;
        [Inject] private readonly LevelConfig _levelConfigs;
        private readonly List<EnemyBase> _listOfEnemies = new List<EnemyBase>();
        private readonly Queue<EnemiesAttributes> _cachedAttributes = new Queue<EnemiesAttributes>();

        public List<EnemyBase> ListOfEnemies => _listOfEnemies;
        public Queue<EnemiesAttributes> CachedAttributes => _cachedAttributes;
        public EnemyBase InstantiateEnemy(LevelHelper level, LevelConfig levelConfig, Player player)
        {
            var enemy = GameObject.Instantiate(_enemy);
            enemy.gameObject.SetActive(false);
            enemy.Init(level, levelConfig, player);
            return enemy;
        }
                
  
        public void GenerateRandomAttributes(LevelConfig levelConfig)
        {
            var speedTotal = 0.0f;
            var sizeTotal = 0.0f;

            for (int i = 0; i < levelConfig.EnemiesCountPerLevel; i++)
            {
                var sizePx = Random.Range(0.0f, 1.0f);
                var speed = Random.Range(_levelConfigs.MinSpeed, levelConfig.MinSpeed);

                _cachedAttributes.Enqueue(new EnemiesAttributes
                {
                    Size = sizePx,
                    InitialSpeed = speed
                });

                speedTotal += speed;
                sizeTotal += sizePx;
            }

            var desiredAverageSpeed = (_levelConfigs.MinSpeed + _levelConfigs.MaxSpeed) * 0.5f;
            var desiredAverageSize = 0.5f;

            var averageSize = sizeTotal / levelConfig.EnemiesCountPerLevel;
            var averageSpeed = speedTotal / levelConfig.EnemiesCountPerLevel;

            var speedScaleFactor = desiredAverageSpeed / averageSpeed;
            var sizeScaleFactor = desiredAverageSize / averageSize;

            foreach (var attributes in _cachedAttributes)
            {
                attributes.Size *= sizeScaleFactor;
                attributes.InitialSpeed *= speedScaleFactor;
            }
        }

        internal class EnemiesAttributes
        {
            public float Size;
            public float InitialSpeed;
        }
    }
}