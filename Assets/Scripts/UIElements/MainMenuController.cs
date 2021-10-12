using UnityEngine;

namespace Clicker
{
    internal class MainMenuController
    {
        private Transform _placeForUi;
        private GameData _gameData;

        public MainMenuController(Transform placeForUi, GameData gameData)
        {
            _placeForUi = placeForUi;
            _gameData = gameData;
        }
    }
}