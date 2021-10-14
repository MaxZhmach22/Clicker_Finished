using System;
using UnityEngine;

namespace Clicker
{
    internal sealed class PlayerInitialization
    {
        private GameSettingsInstaller _gameData;
        private Transform _player;

        public PlayerInitialization(GameSettingsInstaller gameData)
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