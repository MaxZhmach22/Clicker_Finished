using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Clicker
{
    [CreateAssetMenu(fileName = nameof(GameSettingsInstaller), menuName = "GameInstaller/" + nameof(GameSettingsInstaller), order = 1)] 
    internal sealed class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _playerSpeed;
        [SerializeField] private Vector3 planeSize;
        [SerializeField] private Vector2 _levelBoundaries;
        [SerializeField] private Material planeMaterial;
        public List<GameObject> enemyPrefabList;
        [SerializeField] private float _maxTimeValue;
        [SerializeField] private float _minTimerValue;
        [SerializeField] private float _timeDecreaseBetweenSpawn;
        [SerializeField] private GameObject _gameBorders;
        [SerializeField] private AudioClip _onCLickSound;
        [SerializeField] private AudioClip _onStartBntSound;
        [SerializeField] private AudioClip _backGroundMusic;
        [SerializeField] private bool _soundsOn = true;
        private bool _isGameOver;
        public Action OnMuteSounds;
        public Action<bool> OnGameOver;

        public AudioHandler.Settings AudioHandler;
        public GameInstaller.Settings GameInstallerSettings;

        public GameObject Player { get => _playerPrefab; }
        public Material PlaneMaterial { get => planeMaterial; }
        public float PlayerSpeed { get => _playerSpeed; }
        public float MaxTimerValue { get => _maxTimeValue; }
        public float MinTimerValue { get => _minTimerValue; }
        public Vector2 LevelBoundaries { get => _levelBoundaries; }
        public Vector3 PlaneSize { get => planeSize; }
        public GameObject GameBorders { get => _gameBorders; }
        public float TimeBetweenSpawn { get => _timeDecreaseBetweenSpawn; }
        public AudioClip OnCLickSound { get => _onCLickSound; }
        public AudioClip OnStartBntSound { get => _onStartBntSound;  }
        public AudioClip BackGroundMusic { get => _backGroundMusic;  }
        public bool SoundsOn { get => _soundsOn; }

        public void SoundsOffOn()
        {
            _soundsOn = !_soundsOn;
            OnMuteSounds?.Invoke();
        }

        public void GameOver(bool gameOver)
        {
            _isGameOver = gameOver;
            OnGameOver?.Invoke(_isGameOver);
        }

        public override void InstallBindings()
        {
            Container.BindInstance(GameInstallerSettings);
        }
    }
}