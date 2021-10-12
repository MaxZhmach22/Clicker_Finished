using UnityEngine;
using Zenject;

namespace Clicker
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameData _gameData;

        public override void InstallBindings()
        {
            Container.Bind<GameData>().FromInstance(_gameData);
            Container.Bind<PlayerProfile>().AsSingle().NonLazy();
        }
    }
}
