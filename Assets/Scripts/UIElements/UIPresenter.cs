using System;
using UnityEngine;
using UniRx;


namespace MonsterClicker
{
    internal sealed class UIPresenter : MonoBehaviour
    {
        [SerializeField] private UiView _uIView;

        private readonly GameData _gameData;
        private readonly UiModel _uiModel;
        private readonly IScoreSaver _scoreSaver;
        private readonly IScoreChanged _scoreChanged;


        private void Start()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _uIView.Quit.OnClickAsObservable().Subscribe(_ => _uiModel.QuitApp()).AddTo(this);
        }

        private void AddButtonActons()
        {

        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            throw new NotImplementedException();
        }
    }
}
