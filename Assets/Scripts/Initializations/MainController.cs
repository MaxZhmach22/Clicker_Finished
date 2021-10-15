using UnityEngine;

namespace Clicker
{
    internal class MainController : BaseController
    {
        private Transform _placeForUi;
        private GameSettingsInstaller _gameData;
        private Camera _mainCamera;
        private PlayerProfile _playerProfile;
        private UiFactories _uiFactories;


        private MainMenuController _mainMenuController;


        public MainController(UiFactories uiFactories /*Transform placeForUi, PlayerProfile playerProfile, ExecuteController controller, GameSettingsInstaller gameData, Camera mainCamera*/)
        {
            _uiFactories = uiFactories;
            //_gameData = gameData;
            //_mainCamera = mainCamera;
            //_placeForUi = placeForUi;
            //_playerProfile = playerProfile;

            //var uiController = new UIController(_gameData);
            //var playerInit = new PlayerInitialization(_gameData);
            //new LevelInitialization(_gameData, _mainCamera);
            //var inputInit = new InputInitialization(controller, playerInit.GetPlayer(), _mainCamera, _gameData);
            //new EnemiesController(_gameData, controller, inputInit);
            //inputInit.TapCatch.OnEnemyTap += uiController.ScoreJson.CurrentScore;

            //OnChangeGameState(_playerProfile.CurrentGameState.Value);
            //_playerProfile.CurrentGameState.SubscribeOnChange(OnChangeGameState);

        }

        protected override void OnDispose()
        {
            DisposeControllers();
            _playerProfile.CurrentGameState.UnSubscribeOnChange(OnChangeGameState);
        }

        private void  OnChangeGameState(GameStates gameState)
        {
            switch (gameState)
            {
                case GameStates.None:
                    DisposeControllers();
                    break;
                case GameStates.Start:
                    break;
                case GameStates.Settings:
                    //_settingsController = new SettingsController();
                    break;
                case GameStates.Credits:
                    break;
                case GameStates.Game:
                    break;
            }
        }

        private void DisposeControllers()
        {
          
        }
    }
}