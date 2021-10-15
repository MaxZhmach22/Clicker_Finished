using System;
using UnityEngine;
using Zenject;

namespace Clicker
{
    internal class MainMenuController : BaseUiController
    {

        private readonly MainMenuView.Factory _mainMenuViewFactory;
        private MainMenuView _mainMenuView;

        public MainMenuController(MainMenuView.Factory mainMenuViewFactory)
        {
            _mainMenuViewFactory = mainMenuViewFactory;
        }

        public override void Start()
        {
            _mainMenuView = _mainMenuViewFactory.Create();
            AddGameObject(_mainMenuView.gameObject);
        }

        public class Factory : PlaceholderFactory<MainMenuController>
        {
        }
    }
}