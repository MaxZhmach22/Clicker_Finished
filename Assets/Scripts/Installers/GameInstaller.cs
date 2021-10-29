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
            Container.Bind<InputTouchPresenter>().FromInstance(_inputTouchPresenter).WhenInjectedInto<EnemiesController>();
            //Container.Bind<GameLevelView>().AsSingle().WhenInjectedInto<EnemiesController>();
            Container.Bind<InputTouchPresenter>().FromInstance(_inputTouchPresenter).WhenInjectedInto<ShootingController>();
            InstalGameStateFactories();

        }

        private void InstalGameStateFactories()
        {
            Container.Bind<GameStateFactory>().AsSingle();

            Container.BindFactory<StartGameState, StartGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainMenuController, MainMenuController.Factory>().WhenInjectedInto<StartGameState>();
            Container.BindFactory<MainMenuView, MainMenuView.Factory>().
                FromComponentInNewPrefab(_settings.MainMenuView).UnderTransform(_placeForUi);

            Container.BindFactory<CreditsGameState, CreditsGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<CreditsMenuController, CreditsMenuController.Factory>().WhenInjectedInto<CreditsGameState>();
            Container.BindFactory<CreditsMenuView, CreditsMenuView.Factory>().
                FromComponentInNewPrefab(_settings.CreditsMenuView).UnderTransform(_placeForUi);

            Container.BindFactory<GameGameState, GameGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainGameController, MainGameController.Factory>().WhenInjectedInto<GameGameState>();
            Container.Bind<EnemiesFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesController>().AsSingle();
            Container.Bind<Enemy>().FromInstance(_enemy);
            Container.Bind<EnemyMoveModel>().AsSingle();
            Container.BindFactory<GameUiController, GameUiController.Factory>().WhenInjectedInto<MainGameController>();

            Container.BindFactory<GameUiView, GameUiView.Factory>().FromComponentInNewPrefab(_settings.GameUiView).UnderTransform(_placeForUi);
            var levelView = Container.InstantiatePrefabForComponent<GameLevelView>(_settings.GameLevelView);
            Container.Bind<GameLevelView>().FromInstance(levelView).AsSingle();
            //var levelView = Container.BindFactory<GameLevelView, GameLevelView.Factory>().
            //   FromComponentInNewPrefab(_settings.GameLevelView).UnderTransform(new GameObject("Level").transform).AsSingle();

            SHootingControllerBindings();
        }

        private void SHootingControllerBindings()
        {
            Container.BindFactory<ShootingController, ShootingController.Factory>().WhenInjectedInto<MainGameController>();
            Container.BindFactory<ShootingLineRendererView, ShootingLineRendererView.Factory>().
               FromComponentInNewPrefab(_settings.ShootingLineRendererView).UnderTransform(new GameObject("Shooting").transform);
            Container.BindFactory<ImpactEffectView, ImpactEffectView.Factory>().
               FromComponentInNewPrefab(_settings.ImpactEffectView).UnderTransform(GameObject.Find("Shooting").transform);
            Container.BindFactory<ExplosionForceEffect, ExplosionForceEffect.Factory>().
                FromComponentInNewPrefab(_settings.ExplosionForceEffect).UnderTransform(GameObject.Find("Shooting").transform);
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
