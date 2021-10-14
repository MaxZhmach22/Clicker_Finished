using UnityEngine;
using Zenject;

namespace Clicker
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameSettingsInstaller _gameData;
        [SerializeField] private Transform _placeForUi;
        [SerializeField] private GameObject _mainMenuViewPrefab;

        public override void InstallBindings()
        {
            Container.Bind<GameSettingsInstaller>().FromInstance(_gameData);
            Container.Bind<Transform>().FromInstance(_placeForUi);
            Container.Bind<PlayerProfile>().AsSingle().NonLazy();
            Container.Bind<MainMenuView>().FromComponentInNewPrefab(_mainMenuViewPrefab).AsSingle();
        }
    }
}
