using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{

    internal sealed class GameHandler : MonoBehaviour
    {
        private Camera _mainCamera;
        private PlayerMovement _playerMovement;
        private GameData _gameData;
        private EnemyPool _easyEnemyPool;
        private EnemiesController _enemyController;
        private List<EnemyPool> _enemyPools = new List<EnemyPool>();

        void Start()
        {
            _gameData = Resources.Load<GameData>("GameData");
            _mainCamera = Camera.main;
            new GameInitializaton(_gameData, _mainCamera);
            _easyEnemyPool = new EnemyPool(15, _gameData);
            _enemyPools.Add(_easyEnemyPool);
            _enemyController = new EnemiesController(_enemyPools);
            var enemy = _easyEnemyPool.GetEnemy("EasyEnemy");
            enemy.gameObject.SetActive(true);
            enemy.transform.position = new Vector3(5, 1, 5);
            _playerMovement = FindObjectOfType<PlayerMovement>();

        }

       
        void Update()
        {

        }
    }
}
