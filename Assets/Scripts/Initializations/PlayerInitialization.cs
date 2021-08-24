using System;
using UnityEngine;

namespace MonsterClicker
{
    internal sealed class PlayerInitialization
    {
        private GameData _gameData;
        private Transform _player;

        public PlayerInitialization(GameData gameData)
        {
            _gameData = gameData;
            CreatePlayer();

        }

        private void CreatePlayer()
        {
            _player = GameObject.Instantiate(_gameData.Player, new Vector3(0, 1, 0), Quaternion.identity).transform;
            _player.name = "Player";
        }

        public Transform GetPlayer()
        {
            return _player;
        }
    }
}