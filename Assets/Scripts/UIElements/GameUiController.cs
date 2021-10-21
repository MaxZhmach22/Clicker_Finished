using Zenject;
using UniRx;

namespace Clicker
{
    internal sealed class GameUiController : BaseUiController
    {
        private GameUiView _gameUiView;
        private readonly GameUiView.Factory _gameUiViewFactory;
        private readonly Player _player;
        private readonly EnemiesController _enemiesController;

        private int _score = 0;

        public GameUiController(
            GameUiView.Factory gameUiViewFactory, 
            Player player, 
            EnemiesController enemiesController)
        {
            _gameUiViewFactory = gameUiViewFactory;
            _player = player;
            _enemiesController = enemiesController;
        }

        public override void Start()
        {
            _gameUiView = _gameUiViewFactory.Create();
            AddGameObject(_gameUiView.gameObject);

            _enemiesController.Score.Subscribe(score =>
            {
                _score += score;
                _gameUiView.SetText($"Score: {_score}");
            });
        }

        public class Factory : PlaceholderFactory<GameUiController>
        {

        }
    }

}