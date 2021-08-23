using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    internal sealed class EnemyActivation :  IEnemyActivation, IExecute
    {
        private GameData _gameData;
        private List<EnemyPool> _enemyPools;
        private float _randomTimeBetweenTap;
        private float _decreaseTimeBetweenSpawn;
        private float _counter;
        private Vector3 _playerPosition;
        private Vector3 _direction;

        public Action<EnemyBase> OnEnemyActivation;

        public EnemyActivation(List<EnemyPool> enemyPools, GameData gameData, Vector3 playerPosition)
        {
            _gameData = gameData;
            _enemyPools = enemyPools;
            _playerPosition = playerPosition;
            _decreaseTimeBetweenSpawn = _gameData.TimeBetweenSpawn;
            _randomTimeBetweenTap = UnityEngine.Random.Range(_gameData.MinTimerValue, _gameData.MaxTimerValue);
        }


        public void Execute(float deltaTime)
        {
            _counter += deltaTime;
            if (_counter >= _randomTimeBetweenTap)
            {
                
                var randomInt = UnityEngine.Random.Range(1, 101);
                if(randomInt % 3 == 0)
                {
                    Activation(_enemyPools[1].GetEnemy("MiddleEnemy"));
                    _counter = 0;
                    return;
                } 
                if(randomInt % 2 == 0)
                {
                    Activation(_enemyPools[0].GetEnemy("EasyEnemy"));
                    _counter = 0;
                    return;
                }
                if (randomInt % 1 == 0)
                {
                    Activation(_enemyPools[2].GetEnemy("HardEnemy"));
                    _counter = 0;
                    return;
                }
            }
        }
        public void ChangeRandomTimeBetweenTaps(bool isTaped)
        {
            if (isTaped)
            {
 
                _decreaseTimeBetweenSpawn += _gameData.TimeBetweenSpawn;
                _randomTimeBetweenTap = UnityEngine.Random.Range(_gameData.MinTimerValue, _gameData.MaxTimerValue);
                _randomTimeBetweenTap -= _decreaseTimeBetweenSpawn;
            }
        }

        public void Activation(EnemyBase enemy)
        {
            enemy.gameObject.SetActive(true);
            do
            {
                enemy.gameObject.transform.position = Utils.RandomPosition(-_gameData.GameBorders.transform.GetChild(0).position.z, _gameData.GameBorders.transform.GetChild(0).position.z);
                _direction = enemy.gameObject.transform.position - _playerPosition;

            } while (_direction.sqrMagnitude <= 2);
            OnEnemyActivation?.Invoke(enemy); 
        }

    }
}