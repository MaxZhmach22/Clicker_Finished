using UnityEngine;
namespace Clicker
{
    [CreateAssetMenu(fileName = nameof(LevelConfig), menuName = "Configs/" + nameof(LevelConfig))]
    internal class LevelConfig : ScriptableObject
    {
        [field: Header("Enemies Configs:")]
        [field: SerializeField] public int EnemiesCountPerLevel { get; private set; }

        [field: SerializeField] public int EnemiesCountAtOnceSpawn { get; private set; }

        [field: SerializeField] public float TimeBetweenReSpawn { get; private set; }
    }
}
