using UnityEngine;
using Zenject;


namespace MonsterClicker
{
    internal sealed class MainGameController : BaseController
    {
        #region Fields

        private readonly LevelInitialization _levelInitialization;
        private readonly InputInitialization _inputInitialization;
        private readonly ScoreController _scoreCounter;
        private readonly GameUiPresenter _gameUiPresenter;
        private readonly TimeModel _timeModel;
        private readonly Player _player;

        #endregion


        #region ClassLifeCycles

        public MainGameController(
            LevelInitialization levelInitialization,
            InputInitialization inputInitialization,
            GameUiPresenter gameUiPresenter,
            ScoreController scoreCounter,
            TimeModel timeModel,
            Player player)
        {
            _levelInitialization = levelInitialization;
            _inputInitialization = inputInitialization;
            _gameUiPresenter = gameUiPresenter;
            _scoreCounter = scoreCounter;
            _timeModel = timeModel;
            _player = player;
        }

        public override void Start()
        {
            _scoreCounter.Start();
            _player.gameObject.SetActive(true);
        }

        public override void Dispose()
        {
            _gameUiPresenter.Dispose();
            _scoreCounter.Dispose();
            Debug.Log(nameof(MainGameController) + " Disposed");
        }

        #endregion


        public sealed class Factory : PlaceholderFactory<MainGameController>
        {
        }
    }
}
