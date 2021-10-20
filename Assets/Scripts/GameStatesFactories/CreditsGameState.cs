using Zenject;

namespace Clicker
{
    internal sealed class CreditsGameState : GameState
    {
        private CreditsMenuController _settingsMenuController;
        private readonly CreditsMenuController.Factory _settingsMenuControllerFactory;

        public CreditsGameState(CreditsMenuController.Factory settingsMenuControllerFactory)
        {
            _settingsMenuControllerFactory = settingsMenuControllerFactory;
        }

        public override void Start()
        {
            _settingsMenuController = _settingsMenuControllerFactory.Create();
            _settingsMenuController.Start();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Dispose()
        {
            _settingsMenuController.Dispose();
        }

        internal class Factory : PlaceholderFactory<CreditsGameState>
        {

        }
    }
}