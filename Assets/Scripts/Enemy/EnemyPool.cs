using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Clicker
{
    internal sealed class EnemyPool
    {
        private readonly Dictionary<string, HashSet<BaseEnemy>> _enemyPool;
        private readonly int _capacityPool;
        private Transform _rootPool;
        private GameSettingsInstaller _gameData;

        public EnemyPool(int capacityPool, GameSettingsInstaller gameData)
        {
            _enemyPool = new Dictionary<string, HashSet<BaseEnemy>>();
            _gameData = gameData;
            _capacityPool = capacityPool;
            if (!_rootPool)
            {
                _rootPool = new GameObject("Enemy pool").transform;
            }
        }

        public BaseEnemy GetEnemy(string type)
        {
            BaseEnemy result;
            switch (type)
            {
                case "EasyEnemy":
                    result = GetEnemy(GetListEnemies(type), 0);
                    break;
                case "MiddleEnemy":
                    result = GetEnemy(GetListEnemies(type), 1);
                    break;
                case "HardEnemy":
                    result = GetEnemy(GetListEnemies(type), 2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Не предусмотрен в программе");
            }

            return result;
        }

        private HashSet<BaseEnemy> GetListEnemies(string type)
        {
            return _enemyPool.ContainsKey(type) ? _enemyPool[type] : _enemyPool[type] = new HashSet<BaseEnemy>();
        }

        private BaseEnemy GetEnemy(HashSet<BaseEnemy> enemies, int indexInEnemyPrefabList)
        {
            var enemy = enemies.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (enemy == null)
            {
                for (var i = 0; i < _capacityPool; i++)
                {
                    var enemyObj = GameObject.Instantiate(_gameData.enemyPrefabList[indexInEnemyPrefabList]);
                    var enemyBaseScript = enemyObj.GetComponent<BaseEnemy>();
                    ReturnToPool(enemyObj.transform);
                    enemies.Add(enemyBaseScript);
                }
                GetEnemy(enemies, 0);
            }
            enemy = enemies.FirstOrDefault(a => !a.gameObject.activeSelf);
           
            return enemy;
        }


        private void ReturnToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.SetParent(_rootPool);
        }

        public void RemovePool()
        {
            Object.Destroy(_rootPool.gameObject);
        }

    }
}
