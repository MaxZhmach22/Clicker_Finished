using UnityEngine;
using Zenject;

namespace MonsterClicker
{
    internal sealed class Installer : MonoInstaller
    {
        [Header("Prefabs")]
        [SerializeField] private Player _player;
        [SerializeField] private GameData _gameData;
        [SerializeField] private FloatingJoystick _floatingJoystick;

        [Header("Ui Views")]
        [SerializeField] private Transform _placeForUi;
        //[SerializeField] private MainMenuView _mainMenuView;
        //[SerializeField] private GameUiView _gameUiView;
        //[SerializeField] private LooseMenuView _looseMenuView;
        [SerializeField] private GameUiPresenter _gameUiPresenter;
            

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Player>().FromInstance(_player).AsSingle();
            Container.Bind<GameData>().FromInstance(_gameData).AsSingle();
            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
         

            InstalGameStateFactories();
            //MainMenuControllerBindings();
            //LooseMenuControllerBindings();
            //GameUiControllerBindings();
            MainGameControllerBindings();
        }

        private void InstalGameStateFactories()
        {
            Container.Bind<GameStateFactory>().AsSingle();
            Container.BindFactory<GameGameState, GameGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainGameController, MainGameController.Factory>().WhenInjectedInto<GameGameState>();
            //Container.BindFactory<StartGameState, StartGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            ////Container.BindFactory<MainMenuController, MainMenuController.Factory>().WhenInjectedInto<StartGameState>();
            //Container.BindFactory<EndGameState, EndGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            //Container.BindFactory<LooseGameController, LooseGameController.Factory>().WhenInjectedInto<EndGameState>();

        }

        //private void MainMenuControllerBindings()
        //{
        //    var mainMenuView = Container.InstantiatePrefabForComponent<MainMenuView>(
        //        _mainMenuView, _placeForUi);
        //    Container.Bind<MainMenuView>().FromInstance(mainMenuView).AsSingle();
        //}

        //private void LooseMenuControllerBindings()
        //{
        //   var looseMenuView = Container.InstantiatePrefabForComponent<LooseMenuView>(
        //       _looseMenuView, _placeForUi);
        //   Container.Bind<LooseMenuView>().FromInstance(looseMenuView).AsSingle();
        //}

        //private void GameUiControllerBindings()
        //{
        //    var gameUiView = Container.InstantiatePrefabForComponent<GameUiView>(
        //        _gameUiView, _placeForUi);
        //    Container.Bind<GameUiView>().FromInstance(gameUiView).AsSingle();
        //}

        private void MainGameControllerBindings()
        {
            //LevelInit
            Container.Bind<LevelInitialization>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();

            //InputInit
            Container.Bind<InputInitialization>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
            Container.Bind<FloatingJoystick>().FromInstance(_floatingJoystick).AsSingle();

            //Score
            Container.Bind<ScoreController>().AsSingle();
            Container.Bind<ScoreListInitialization>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle();

            //Input
            Container.BindInterfacesAndSelfTo<TapCatch>().AsSingle();

            //UiPresenter
            var gameUiPresenter = Container.InstantiatePrefabForComponent<GameUiPresenter>(_gameUiPresenter, _placeForUi);
            Container.Bind<GameUiPresenter>().FromInstance(gameUiPresenter).AsSingle();
        }

    }
}