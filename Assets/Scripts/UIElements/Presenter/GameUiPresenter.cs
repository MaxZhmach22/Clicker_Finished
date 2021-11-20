using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using System;
using Zenject;

namespace MonsterClicker    
{
    internal sealed class GameUiPresenter : BasePresenter
    {
        [SerializeField] private Button _muteButton;
        [SerializeField] private Button _backToMainMenuButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _opanConfermPanel;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private GameObject _confermPanel;

        private CompositeDisposable _disposables;
        private IScoreCounter _scoreCounter;
        private ITimeModel _timeModel;

        [Inject]
        public void Init(
            IScoreCounter scoreCounter,
            ITimeModel timeModel)
        {
            _scoreCounter = scoreCounter;
            _timeModel = timeModel;
            _disposables = new CompositeDisposable();
        }

        public override void Start()
        {
            _opanConfermPanel.OnClickAsObservable().Subscribe(_ =>
            {
                _confermPanel.SetActive(true);
                Time.timeScale = 0;
            }).AddTo(_disposables);

            _resumeButton.OnClickAsObservable().Subscribe(_ =>
            {
                _confermPanel.SetActive(false);
                Time.timeScale = 1;
            }).AddTo(_disposables);

            _backToMainMenuButton.OnClickAsObservable().Subscribe(_ =>
            {
                _confermPanel.SetActive(false);
                Time.timeScale =0;
            }).AddTo(_disposables);

            InitTimer();
            _scoreCounter.CurrentScore.SubscribeToText(_scoreText).AddTo(_disposables);
            _confermPanel.SetActive(false);
        }

        public override void Dispose() =>
            _disposables.Clear();

        private void InitTimer()
        {
            _timeModel.GameTime.Subscribe(seconds =>
            {
                var t = TimeSpan.FromSeconds(seconds);
                _timerText.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            }).AddTo(_disposables);
        }
    }
}
