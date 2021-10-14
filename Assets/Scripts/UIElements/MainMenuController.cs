using UnityEngine;
using Zenject;

namespace Clicker
{
    internal class MainMenuController
    {
        private readonly Transform _placeForUi;
        private readonly GameSettingsInstaller _gameData;
        private readonly MainMenuView _mainMenuView;
        

        public MainMenuController(Transform placeForUi, GameSettingsInstaller gameData, MainMenuView mainMenuView)
        {
            _placeForUi = placeForUi;
            _gameData = gameData;
            _mainMenuView = mainMenuView;
            GameObject.Instantiate<MainMenuView>(_mainMenuView, placeForUi.position, Quaternion.identity);
        }
    }
}