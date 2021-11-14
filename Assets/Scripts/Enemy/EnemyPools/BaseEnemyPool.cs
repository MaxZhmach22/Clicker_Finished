using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Clicker
{
    internal abstract class BaseEnemyPool : IEnemiesPool
    {

        public void ReturnToPool(BaseEnemy enemy)
        {
            enemy.CurrentHp = enemy.MaxHp;
            enemy.gameObject.SetActive(false);
        }

        public abstract void GeneratePool();

        protected BaseEnemy GetEnemyFromPool(List<BaseEnemy> enemyBases)
        {
            if (enemyBases.Count == 0)
                return null;

            foreach (var enemy in enemyBases)
            {
                if (!enemy.gameObject.activeInHierarchy)
                    return enemy;
            }
            return null;
        }
    }
}
