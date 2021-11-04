using UniRx;
using UnityEngine;
using Zenject;
 
namespace Clicker
{
    internal sealed class MainGameController : BaseController
    {
        private readonly GameLevelViewController _gameLevelViewController;
        private readonly EnemiesController _enemiesController;
        private readonly ShootingController _shootingController;
        private readonly GameUiController _gameUiController;
        private readonly PlayerCollisionController _playerCollisionController;
        private readonly Player _player;

        public MainGameController(
             GameLevelViewController gameLevelViewController,
             EnemiesController enemiesController,
             ShootingController shootingController,
             GameUiController gameUiController,
             PlayerCollisionController playerCollisionController,
             Player player)
        {
             _gameLevelViewController = gameLevelViewController;
             _enemiesController = enemiesController;
             _shootingController = shootingController;
             _gameUiController = gameUiController;
             _playerCollisionController = playerCollisionController;
             _player = player;
        }

        public override void Start()
        {
            _player.gameObject.SetActive(true);

            _gameLevelViewController.Start();
            _enemiesController.Start();
            _shootingController.Start();
            _gameUiController.Start();
            _playerCollisionController.Start();
        }

        public override void Dispose()
        {
            _gameLevelViewController?.Dispose();
            _shootingController?.Dispose();
            _gameUiController?.Dispose();
            _enemiesController?.Dispose();
            _playerCollisionController?.Dispose();
            Debug.Log(nameof(MainGameController) + " Disposed");
        }

        public sealed class Factory : PlaceholderFactory<MainGameController>
        {
        }
    }
}
