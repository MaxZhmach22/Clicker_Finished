using UnityEngine;


namespace MonsterClicker
{
    internal sealed class InputInitialization
    {
        #region Fields

        private readonly TapCatch _tapCatch;
        private readonly Player _player;
        private readonly PlayerMovement _playerMovement;
        private readonly GameData _gameData;

        #endregion


        #region ClassLifeCycles

        public InputInitialization(
            Player player,
            TapCatch tapCatch,
            PlayerMovement playerMovement,
            Camera main)
        {
            _player = player;
            _tapCatch = tapCatch;
            _playerMovement = playerMovement;
        } 

        #endregion
    }
}