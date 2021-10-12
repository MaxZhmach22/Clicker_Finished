using UnityEngine;

namespace Clicker
{
    internal class InputInitialization
    {
        private TapCatch _tapCatch;
        private Transform _player;
        private PlayerMovement _playerMovement;

        public TapCatch TapCatch { get => _tapCatch; }

        public InputInitialization(ExecuteController controller, Transform player, Camera main, GameData gameData)
        {
            _tapCatch = new TapCatch();
            _player = player;
            _playerMovement = new PlayerMovement(_player, main);
            controller.Add(_playerMovement);
            controller.Add(_tapCatch);
           
        }

        public Vector3 GetPlayerPosition()
        {
            return _playerMovement.PlayerPosition();
        }

        
    }
}