using UnityEngine;
namespace Clicker
{
    [CreateAssetMenu(fileName = nameof(LevelConfig), menuName = "Configs/" + nameof(LevelConfig))]
    internal class LevelConfig : ScriptableObject
    {
        [field: Header("Level")]

        [field: Header("Enemies Configs:")]
        [field: SerializeField] public int EnemiesCountPerLevel { get; private set; }
        [field: SerializeField] public int EnemiesStartingSpawns { get; private set; }
        [field: SerializeField] public int EnemiesCountAtOnceSpawn { get; private set; }
        [field: SerializeField] public float MinScale { get; private set; }
        [field: SerializeField] public float MaxScale { get; private set; }
        [field: SerializeField] public float MinMass { get; private set; }
        [field: SerializeField] public float MaxMass { get; private set; }
        [field: SerializeField] public float MinSpeed { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float TimeBetweenReSpawn { get; private set; }
        [field: SerializeField] public float MissleEnemyCountPerLevel { get; private set; }


    }
}
