using System;
using System.Collections.Generic;
using UnityEngine;



namespace MonsterClicker
{
    internal sealed class EnemyActivator :  IEnemyActivation
    {
        public Action<EnemyBase> OnEnemyActivation;

        private readonly GameData _gameData;
        private readonly List<EnemyPool> _enemyPools;
        private float _counter;
        private Vector3 _playerPosition;
        private Vector3 _distance;

        public EnemyActivator(
            List<EnemyPool> enemyPools, 
            GameData gameData, 
            Vector3 playerPosition)
        {
            _gameData = gameData;
            _enemyPools = enemyPools;
            _playerPosition = playerPosition;
            //_randomTimeBetweenTap = UnityEngine.Random.Range(_gameData.MinTimerValue, _gameData.MaxTimerValue);
        }

        public void Execute(float deltaTime)
        {
            //_counter += deltaTime;
            //if (_counter < _randomTimeBetweenTap)
            //    return;

            ChooseEnemyTypeToActive();
            _counter = 0;
        }

        private void ChooseEnemyTypeToActive()
        {
            var randomInt = UnityEngine.Random.Range(1, 101);
            if (randomInt % 3 == 0)
            {
                Activation(_enemyPools[1].GetEnemy("MiddleEnemy"));
                return;
            }
            if (randomInt % 2 == 0)
            {
                Activation(_enemyPools[0].GetEnemy("EasyEnemy"));
                return;
            }
            if (randomInt % 1 == 0)
            {
                Activation(_enemyPools[2].GetEnemy("HardEnemy"));
                return;
            }
        }

        public void Activation(EnemyBase enemy)
        {
            enemy.gameObject.SetActive(true);
            do
            {
                enemy.gameObject.transform.position = Utils.RandomPosition(-_gameData.GameBorders.transform.GetChild(0).position.z, _gameData.GameBorders.transform.GetChild(0).position.z);
                _distance = _playerPosition - enemy.gameObject.transform.position;

            } while (_distance.sqrMagnitude <= _gameData.SpawnDistanceBetweenPlayer);
            OnEnemyActivation?.Invoke(enemy); 
        }
    }
}