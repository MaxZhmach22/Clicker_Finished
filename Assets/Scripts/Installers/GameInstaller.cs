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

        public override void InstallBindings()
        {
            //Container.Bind<GameSettingsInstaller>().FromInstance(_gameData);
            //Container.Bind<Transform>().FromInstance(_placeForUi);
            Container.Bind<GameStateFactory>().AsSingle();
            Container.BindFactory<StartGameState, StartGameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MainMenuController, MainMenuController.Factory>().WhenInjectedInto<StartGameState>();
            Container.BindFactory<MainMenuView, MainMenuView.Factory>().
                FromComponentInNewPrefab(_settings.MainMenuView).UnderTransform(_placeForUi);
               
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
        } 
    }
}
