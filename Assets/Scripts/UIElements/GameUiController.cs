using Zenject;
using UniRx;
using UnityEngine;

namespace Clicker
{
    internal sealed class GameUiController : BaseController
    {
        private readonly GameUiView _gameUiView;
        private readonly EnemiesController _enemiesController; //TODO Interface

        private int _score = 0;
        private CompositeDisposable _disposables = new CompositeDisposable();

        public GameUiController(
            GameUiView gameUiView, 
            EnemiesController enemiesController)
        {
            _gameUiView = gameUiView;
            _enemiesController = enemiesController;
        }

        public override void Start()
        {
            _gameUiView.gameObject.SetActive(true);
            _enemiesController.Score.Subscribe(score =>
            {
                _score += score;
                _gameUiView.SetText($"Score: {_score}");
            }).AddTo(_disposables);
            Debug.Log($"{nameof(GameUiController)} Is Subcribed; Disposables count = {_disposables.Count}");
        }

        public override void Dispose()
        {
            _gameUiView.gameObject.SetActive(false);
            _disposables.Clear();
            Debug.Log($"{nameof(GameUiController)} Is Disposed; Disposables count = {_disposables.Count}");
        }
    }
}