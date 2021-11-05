using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace Clicker
{
    internal sealed class EnemiesFactory
    {
        [Inject] private Enemy _enemy;
        [Inject] private readonly LevelConfig _levelConfigs;
        [Inject] private DiContainer _diContainer;
        private readonly Queue<EnemiesAttributes> _cachedAttributes = new Queue<EnemiesAttributes>();

        public Queue<EnemiesAttributes> CachedAttributes => _cachedAttributes;
        public EnemyBase InstantiateEnemy(Transform parentTransform)
        {
            var enemy = _diContainer.InstantiatePrefabForComponent<EnemyBase>(_enemy, parentTransform);
            enemy.gameObject.SetActive(false);
            return enemy;
        }
                
  
        public void GenerateRandomAttributes(LevelConfig levelConfig, int sizeOfPool)
        {
            var speedTotal = 0.0f;
            var sizeTotal = 0.0f;

            for (int i = 0; i < sizeOfPool; i++)
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