using Zenject;

namespace Clicker
{
    internal sealed class CreditsGameState : GameState
    {
        private CreditsMenuController _creditsMenuController;
        private readonly CreditsMenuController.Factory _creditsMenuControllerFactory;

        public CreditsGameState(CreditsMenuController.Factory settingsMenuControllerFactory)
            => _creditsMenuControllerFactory = settingsMenuControllerFactory;

        public override void Start()
        {
            _creditsMenuController = _creditsMenuControllerFactory.Create();
            _creditsMenuController.Start();
        }

        public override void Update() { }
 
        public override void Dispose() =>
            _creditsMenuController.Dispose();
   
        internal class Factory : PlaceholderFactory<CreditsGameState>
        {
        }
    }
}