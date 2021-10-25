﻿using UniRx;
using UnityEngine;
using Zenject;

namespace Clicker
{
    internal sealed class MainGameController : BaseController
    {
        private ShootingController _shootingController;
        private GameUiController _gameUiController;
        private readonly EnemiesController _enemiesController;
        private readonly GameUiView.Factory _gameUiViewFactory;
        //private readonly GameLevelView.Factory _gameLevelViewFactory;
        private readonly GameLevelView _gameLevelView;
        private readonly ShootingController.Factory _shootingControllerFactory;
        private readonly GameUiController.Factory _gameUiControllerFactory;
        private Player _player;

        public MainGameController(
            //GameLevelView.Factory gameLevelViewFactory,
            ShootingController.Factory shootingControllerFactory,
            GameUiController.Factory gameUiControllerFactory,
            EnemiesController enemiesController,
            GameLevelView gameLevelView,
            Player player)
        {
            //_gameLevelViewFactory = gameLevelViewFactory;
            _gameLevelView = gameLevelView;
            _shootingControllerFactory = shootingControllerFactory;
            _gameUiControllerFactory = gameUiControllerFactory;
            _enemiesController = enemiesController;
            _player = player;
        }

        public override void Start()
        {
            //_gameLevelView = _gameLevelViewFactory.Create();
            _shootingController = _shootingControllerFactory.Create();
            _shootingController.Start();
            _gameUiController = _gameUiControllerFactory.Create();
            _gameUiController.Start();
            _enemiesController.Start();
        }

        public sealed class Factory : PlaceholderFactory<MainGameController>
        {
        }
    }
}
