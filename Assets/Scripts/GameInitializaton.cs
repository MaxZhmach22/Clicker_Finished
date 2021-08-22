using UnityEngine;

namespace MonsterClicker
{
    internal class GameInitializaton
    {
        private GameData _gameData;
        private Camera _mainCamera;

        public GameInitializaton(GameData gameData, Camera mainCamera)
        {
            _gameData = gameData;
            _mainCamera = mainCamera;
            new LevelInitialization(_gameData, _mainCamera);
            
        }
    }
}