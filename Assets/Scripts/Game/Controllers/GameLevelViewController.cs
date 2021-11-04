using UnityEngine;
using Zenject;

namespace Clicker
{
    internal sealed class GameLevelViewController : BaseController
    {
        private readonly GameLevelView _gameLevelView;

        public GameLevelViewController(GameLevelView gameLevelView)
            => _gameLevelView = gameLevelView;

        public override void Start() 
            => _gameLevelView.gameObject.SetActive(true);

        public override void Dispose()
            => _gameLevelView.gameObject.SetActive(false);

    }
}
