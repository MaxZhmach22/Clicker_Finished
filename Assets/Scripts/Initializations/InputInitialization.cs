using UnityEngine;


namespace MonsterClicker
{
    internal sealed class InputInitialization
    {
        #region Fields

        private readonly TapCatch _tapCatch;
        private readonly Transform _player;
        private readonly PlayerMovement _playerMovement;
        private readonly GameData _gameData;

        public ITapCatch TapCatch => _tapCatch;

        #endregion


        #region ClassLifeCycles

        public InputInitialization(
            ExecuteController controller,
            Transform player,
            Camera main)
        {
            _player = player;
            _tapCatch = new TapCatch();
            _playerMovement = new PlayerMovement(_player, main, _gameData);
            controller.Add(_playerMovement);
            controller.Add(_tapCatch);
        } 

        #endregion
    }
}