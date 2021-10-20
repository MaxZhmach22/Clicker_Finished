using Zenject;

namespace Clicker
{
    internal sealed class GameUiController : BaseUiController
    {
        private readonly GameUiView.Factory _gameUiViewFactory;
        private GameUiView _gameUiView;
        private Player _player;

        public GameUiController(GameUiView.Factory gameUiViewFactory, Player player)
        {
            _gameUiViewFactory = gameUiViewFactory;
            _player = player;

        }

        public override void Start()
        {
            _gameUiView = _gameUiViewFactory.Create();
            AddGameObject(_gameUiView.gameObject);
        }

        public class Factory : PlaceholderFactory<GameUiController>
        {

        }
    }

}