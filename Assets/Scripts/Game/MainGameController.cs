using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker
{
    internal sealed class MainGameController : BaseController
    {
        private  GameUiView _gameUiView;
        private  GameLevelView _gameLevelView;
        private readonly GameUiView.Factory _gameUiViewFactory;
        private readonly GameLevelView.Factory _gameLevelViewFactory;
        private Player _player;

        public MainGameController(
            GameUiView.Factory gameUiViewFactory, 
            GameLevelView.Factory gameLevelViewFactory, 
            Player player)
        {
            _gameUiViewFactory = gameUiViewFactory;
            _gameLevelViewFactory = gameLevelViewFactory;
            _player = player;
        }

        public override void Start()
        {
            _gameUiView = _gameUiViewFactory.Create();
            _gameLevelView = _gameLevelViewFactory.Create();
            Debug.Log("Init");
        }

        public sealed class Factory : PlaceholderFactory<MainGameController>
        {
        }
    }
}
