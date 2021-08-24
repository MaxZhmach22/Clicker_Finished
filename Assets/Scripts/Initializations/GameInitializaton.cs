using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    internal class GameInitializaton
    {
        private GameData _gameData;
        private Camera _mainCamera;

        public GameInitializaton(ExecuteController controller, GameData gameData, Camera mainCamera)
        {
            _gameData = gameData;
            _mainCamera = mainCamera;
            var uiController = new UIController(_gameData);
            var playerInit = new PlayerInitialization(_gameData);
            new LevelInitialization(_gameData, _mainCamera);
            var inputInit = new InputInitialization(controller, playerInit.GetPlayer(), _mainCamera, _gameData);
            new EnemiesController(_gameData, controller, inputInit);
            inputInit.TapCatch.OnEnemyTap += uiController.ScoreJson.CurrentScore;
        }
    }
}