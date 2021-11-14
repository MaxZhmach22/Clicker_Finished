using UnityEngine;

namespace MonsterClicker
{
    internal sealed class UIController: IDispose
    {
        private readonly GameData _gameData;
        private readonly UIModel _uiModel;
        private readonly UIView _uiView;
        private readonly ScoreJson _scoreJson;
        private AudioSource _auidoSource;

        internal ScoreJson ScoreJson { get => _scoreJson; }

        public UIController(GameData gameData)
        {
            _gameData = gameData;
            _scoreJson = new ScoreJson();
            _uiModel = new UIModel(_gameData);
            _uiView = new UIView(_scoreJson.Score);
            AudioSourceInit();
            AddButtonActons();
            _uiModel.OnGameStart();
            _uiModel.OnClickSound += PlayClickSound;
            _uiModel.OnStartSound += PlayStartSound;
            _uiModel.OnShowBestScore += _uiView.PrintBestScore;
            _scoreJson.OnResetScore += _uiView.PrintBestScore;
            _gameData.OnMuteSounds += StopMusic;
            _gameData.OnGameOver += _scoreJson.IsTheBestScore;
            _gameData.OnGameOver += _uiModel.ShowLooseMenu;
            _scoreJson.OnScoreChange += _uiView.PrintCurrentScore;
            _gameData.OnGameOver += _uiView.PrintCurrentScoreInLooseMenu;
        }

        private void AudioSourceInit()
        {
            var audioSource = new GameObject("BackgroundMusic");
            _auidoSource = audioSource.AddComponent<AudioSource>();
            _auidoSource.loop = true;
            _auidoSource.clip = _gameData.BackGroundMusic;
            _auidoSource.Play();
        }

        private void AddButtonActons()
        {
            _uiView.Quit.onClick.AddListener(_uiModel.QuitApp);
            _uiView.StartGame.onClick.AddListener(_uiModel.StartGame);
            _uiView.ScoreMenuBtn.onClick.AddListener(_uiModel.ShowScoreMenu);
            _uiView.BackCreditsBtn.onClick.AddListener(_uiModel.ShowStartGameMenu);
            _uiView.BackScoreBtn.onClick.AddListener(_uiModel.ShowStartGameMenu);
            _uiView.CreditsMenuBtn.onClick.AddListener(_uiModel.ShowCreditsMenu);
            _uiView.ScoreMenuBtn.onClick.AddListener(_uiModel.ShowScoreMenu);
            _uiView.PauseBtn.onClick.AddListener(_uiModel.PauseGame);
            _uiView.Quit.onClick.AddListener(_uiModel.QuitApp);
            _uiView.MuteBtn.onClick.AddListener(_gameData.SoundsOffOn);
            _uiView.BackLooseMenuBtn.onClick.AddListener(_uiModel.PauseGame);
            _uiView.ClearScoreBtn.onClick.AddListener(_scoreJson.ResetBestScore);

        }
        
        private void PlayClickSound()
        {
            if (_gameData.SoundsOn)
            {
                _auidoSource.PlayOneShot(_gameData.OnCLickSound);
            }
        }

        private void PlayStartSound()
        {
            if (_gameData.SoundsOn)
            {
                _auidoSource.PlayOneShot(_gameData.OnStartBntSound);
            }
        }

        private void StopMusic()
        {
            if (_gameData.SoundsOn)
            {
                _auidoSource.Play();
            }
            else
            {
                _auidoSource.Stop();
            }
           
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
