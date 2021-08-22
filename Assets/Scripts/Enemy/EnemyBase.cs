using UnityEngine;

namespace MonsterClicker
{
    internal abstract class EnemyBase : MonoBehaviour
    {
        public TapCount TapCounts { get; private set; }

        public static EnemyBase CreateEnemy (TapCount tapCount, GameData data, int enemyIndex, TapCount count)
        {

            var gameObject = Instantiate(data.enemyPrefabList[enemyIndex], Vector3.zero, Quaternion.identity);

            var enemy = gameObject.GetComponent<EnemyBase>();
            enemy.TapCounts = count;

            return enemy;
        }

    }
}
