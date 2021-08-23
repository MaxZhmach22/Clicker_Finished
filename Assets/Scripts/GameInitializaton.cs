using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    internal class GameInitializaton
    {
        private GameData _gameData;
        private Camera _mainCamera;
        private EnemiesController _enemyController;
        private UIModel _uIModel;
            
        public GameInitializaton(ExecuteController controller, GameData gameData, Camera mainCamera)
        {
            _gameData = gameData;
            _mainCamera = mainCamera;
            new UIController(_gameData);
            var playerInit = new PlayerInitialization(_gameData);
            new LevelInitialization(_gameData, _mainCamera);
            var inputInit = new InputInitialization(controller, playerInit.GetPlayer(), mainCamera);
            _enemyController = new EnemiesController(_gameData, controller, inputInit);

        }
    }
}