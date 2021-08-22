using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    [CreateAssetMenu (fileName = "Game Data", menuName = "Data", order = 1)]
    public sealed class GameData : ScriptableObject
    {
        [SerializeField] private Vector3 planeSize;
        [SerializeField] private Material planeMaterial;
        public List<GameObject> enemyPrefabList;
        
        
        
        public Vector3 PlaneSize { get => planeSize; }
        public Material PlaneMaterial { get => planeMaterial; }
    }
}