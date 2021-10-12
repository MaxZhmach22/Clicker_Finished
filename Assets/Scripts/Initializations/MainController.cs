using UnityEngine;

namespace Clicker
{
    internal class MainController : BaseController
    {
        private Transform _placeForUi;
        private GameData _gameData;
        private Camera _mainCamera;
        private PlayerProfile _playerProfile;

        private MainMenuController _mainMenuController;

        public MainController(Transform placeForUi, PlayerProfile playerProfile, ExecuteController controller, GameData gameData, Camera mainCamera)
        {
            _gameData = gameData;
            _mainCamera = mainCamera;
            _placeForUi = placeForUi;
            _playerProfile = playerProfile;

            var uiController = new UIController(_gameData);
            var playerInit = new PlayerInitialization(_gameData);
            new LevelInitialization(_gameData, _mainCamera);
            var inputInit = new InputInitialization(controller, playerInit.GetPlayer(), _mainCamera, _gameData);
            new EnemiesController(_gameData, controller, inputInit);
            inputInit.TapCatch.OnEnemyTap += uiController.ScoreJson.CurrentScore;

            OnChangeGameState(_playerProfile.CurrentGameState.Value);
            _playerProfile.CurrentGameState.SubscribeOnChange(OnChangeGameState);

        }

        protected override void OnDispose()
        {
            DisposeControllers();
            _playerProfile.CurrentGameState.UnSubscribeOnChange(OnChangeGameState);
        }

        private void  OnChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.None:
                    DisposeControllers();
                    break;
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUi, _gameData);
                    break;
                case GameState.Settings:
                    //_settingsController = new SettingsController();
                    break;
                case GameState.Credits:
                    break;
                case GameState.Game:
                    break;
            }
        }

        private void DisposeControllers()
        {
          
        }
    }
}