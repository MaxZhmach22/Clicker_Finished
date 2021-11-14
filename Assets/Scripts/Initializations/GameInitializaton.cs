using UnityEngine;


namespace MonsterClicker
{
    internal sealed class GameInitializaton : IDispose
    {
        #region Fields

        private readonly GameData _gameData;
        private readonly Camera _mainCamera;
        private readonly UIController _uIController;
        private readonly IPlayerInitialization _playerInitialization;
        private readonly InputInitialization _inputInitialization;

        #endregion


        #region ClassLifeCycles

        public GameInitializaton(
            ExecuteController controller,
            GameData gameData,
            Camera mainCamera)
        {
            _gameData = gameData;
            _mainCamera = mainCamera;
            new LevelInitialization(_gameData, _mainCamera);
            _uIController = new UIController(_gameData);
            controller.Add(_uIController);
            _playerInitialization = new PlayerInitialization(_gameData);
            _inputInitialization = new InputInitialization(
                 controller,
                _playerInitialization.GetPlayer(),
                _mainCamera);
            controller.Add(new EnemiesController(
              _gameData,
              controller,
              _inputInitialization.TapCatch,
              _playerInitialization.GetPlayer()));
        }

        public void Dispose() { } 

        #endregion
    }
}