using UnityEngine;


namespace MonsterClicker
{
    internal sealed class PlayerInitialization : IPlayerInitialization
    {
        #region Fields

        private readonly GameData _gameData;
        private Transform _player;

        #endregion


        #region ClassLifeCycles

        public PlayerInitialization(GameData gameData)
        {
            _gameData = gameData;
            CreatePlayer();
        }

        #endregion


        #region IPlayerInitialization

        private void CreatePlayer()
        {
            _player = GameObject.Instantiate(
                _gameData.Player,
                new Vector3(0, 1, 0),
                Quaternion.identity).transform;
            _player.name = "Player";
        }

        public Transform GetPlayer() =>
            _player; 

        #endregion
    }
}