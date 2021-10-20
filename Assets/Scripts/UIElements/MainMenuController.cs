using System;
using UnityEngine;
using Zenject;
using UniRx;

namespace Clicker
{
    internal sealed class MainMenuController : BaseUiController
    {

        private readonly MainMenuView.Factory _mainMenuViewFactory;
        private MainMenuView _mainMenuView;
        private Player _player;

        public MainMenuController(MainMenuView.Factory mainMenuViewFactory, Player player)
        {
            _mainMenuViewFactory = mainMenuViewFactory;
            _player = player;
            
        }

        public override void Start()
        {
            _mainMenuView = _mainMenuViewFactory.Create();
            AddGameObject(_mainMenuView.gameObject);
            _mainMenuView.CreditsBtn.OnClickAsObservable().Subscribe(_ => _player.ChangeState(GameStates.Credits));
            _mainMenuView.StartGameBtn.OnClickAsObservable().Subscribe(_ => _player.ChangeState(GameStates.Game));
        }

        public class Factory : PlaceholderFactory<MainMenuController>
        {
        }
    }
}