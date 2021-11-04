using System;
using UnityEngine;
using Zenject;

namespace Clicker
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        Settings _settings = null;

        [SerializeField] private GameSettingsInstaller _gameData;
        [SerializeField] private Transform _placeForUi;
        [SerializeField] private Player _player;
        [SerializeField] private InputTouchPresenter _inputTouchPresenter;
        [SerializeField] private Enemy _enemy; //TODO enemy
        [SerializeField] private LevelConfig _currentLevelConfig;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().WithId("Main").FromInstance(Camera.main);
            Container.Bind<LevelHelper>().AsSingle().NonLazy();
            Container.Bind<LevelConfig>().FromInstance(_currentLevelConfig).AsSingle();
            Container.Bind<InputTouchPresenter>().FromInstance(_inputTouchPresenter).AsSingle();

            InstalGameStateFactories();
            MainGameControllerBindings();
            MainMenuControllerBindings();
            CreditsMenuControllerBindings();
        }
        private void InstalGameStateFactories()
        {
            Container.Bind<GameStateFactory>().AsSingle();

            Container.BindFactory<StartGameState, StartGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainMenuController, MainMenuController.Factory>().WhenInjectedInto<StartGameState>();
         

            Container.BindFactory<CreditsGameState, CreditsGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<CreditsMenuController, CreditsMenuController.Factory>().WhenInjectedInto<CreditsGameState>();
         
            Container.BindFactory<GameGameState, GameGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainGameController, MainGameController.Factory>().WhenInjectedInto<GameGameState>();
           

            Container.Bind<PlayerCollisionController>().AsSingle();
            Container.Bind<Enemy>().FromInstance(_enemy);

            //Èםעונפויס GameLevelView ט GameUiView

           
        }

        private void MainGameControllerBindings()
        {
            GameLevelControllerBindings();
            EnemiesControllerBindings();
            ShootingControllerBindings();
            GameUiControllerBindings();
        }

        private void GameLevelControllerBindings()
        {
            Container.Bind<GameLevelViewController>().AsSingle();
            var levelView = Container.InstantiatePrefabForComponent<GameLevelView>(
                _settings.GameLevelView,
                new GameObject("LevelView").transform);
            Container.Bind<GameLevelView>().FromInstance(levelView).AsSingle();
        }

        private void EnemiesControllerBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemiesController>().AsSingle();
            Container.Bind<EnemiesFactory>().AsSingle();
            Container.Bind<EnemyMoveModel>().AsSingle();
        }

        private void ShootingControllerBindings()
        {
            Container.Bind<ShootingController>().AsSingle();
            Container.BindFactory<ShootingLineRendererView, ShootingLineRendererView.Factory>()
                .FromComponentInNewPrefab(_settings.ShootingLineRendererView)
                .UnderTransform(new GameObject("Shooting").transform);
            Container.BindFactory<ImpactEffectView, ImpactEffectView.Factory>()
                .FromComponentInNewPrefab(_settings.ImpactEffectView)
                .UnderTransform(GameObject.Find("Shooting").transform);
            Container.BindFactory<ExplosionForceEffect, ExplosionForceEffect.Factory>()
                .FromComponentInNewPrefab(_settings.ExplosionForceEffect)
                .UnderTransform(GameObject.Find("Shooting").transform);
        }

        private void GameUiControllerBindings()
        {
            Container.Bind<GameUiController>().AsSingle();
            var gameUiView = Container.InstantiatePrefabForComponent<GameUiView>(
                _settings.GameUiView,
                _placeForUi);
            Container.Bind<GameUiView>().FromInstance(gameUiView).AsSingle();
        }

        private void MainMenuControllerBindings()
        {
            var mainMenuView = Container.InstantiatePrefabForComponent<MainMenuView>(
                _settings.MainMenuView, _placeForUi);
            Container.Bind<MainMenuView>().FromInstance(mainMenuView).AsSingle();
        }

        private void CreditsMenuControllerBindings()
        {
            Container.Bind<CreditsMenuController>().AsSingle();
            var creditsMenuView = Container.InstantiatePrefabForComponent<CreditsMenuView>(
                _settings.CreditsMenuView,
                _placeForUi);
            Container.Bind<CreditsMenuView>().FromInstance(creditsMenuView).AsSingle();
        }


        [Serializable]
        public class Settings
        {
            [Header("Player Prefab")]
            public GameObject Player;

            [Header("Enemy Prefabs")]
            public GameObject Apple;
            public GameObject Apricot;
            public GameObject Banana;
            public GameObject Coconut;
            public GameObject Lemon;
            public GameObject Melon;
            public GameObject Orange;
            public GameObject Peach;
            public GameObject Pear;
            public GameObject Strawberry;

            [Header("UI Prefabs")]
            public MainMenuView MainMenuView;
            public CreditsMenuView CreditsMenuView;
            public GameUiView GameUiView;

            [Header("Level Prefab")]
            public GameLevelView GameLevelView;

            [Header("Shooting LineRenderer")]
            public ShootingLineRendererView ShootingLineRendererView;
            public ImpactEffectView ImpactEffectView;
            public ExplosionForceEffect ExplosionForceEffect;
        } 
    }
}
