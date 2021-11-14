using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Data", order = 1)]
    internal sealed class GameData : ScriptableObject
    {
        #region CameraSettings

        [field: Header("Camera settings:")]
        [field: SerializeField] public bool IsOrthographic { get; private set; }
        [field: SerializeField] public float OrthographicSize { get; private set; }
        [field: SerializeField] public float NearClipPlaneSize { get; private set; }
        [field: SerializeField] public float FarClipPlaneSize { get; private set; }
        [field: SerializeField] public Vector3 CameraLocalPosition { get; private set; }
        [field: SerializeField] public Vector3 CameraRotation { get; private set; }

        #endregion


        #region SurfaceSettings

        [field: Header("Surface settings:")]
        [field: SerializeField] public PrimitiveType Type { get; private set; }
        [field: SerializeField] public Vector3 Size { get; private set; }
        [field: SerializeField] public Material SurfaceMaterial { get; private set; }
        [field: SerializeField] public GameObject GameBorders { get; private set; }
        [field: SerializeField] public Vector3 BordersRotation { get; private set; }

        #endregion


        [field: Header("Palyer settings:")]
        [field: SerializeField] public FloatingJoystick FloatingJoystick { get; private set; }
        [field: SerializeField] public float PlayerMovementSpeed { get; private set; }

        [field: Header("Enemies settings:")]
        [field: SerializeField] public int EnemiesCountInPool { get; private set; }



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


        public GameObject Player { get => _playerPrefab; }
        public Material PlaneMaterial { get => planeMaterial; }
        public float PlayerSpeed { get => _playerSpeed; }
        public float MaxTimerValue { get => _maxTimeValue; }
        public float MinTimerValue { get => _minTimerValue; }
        public Vector2 LevelBoundaries { get => _levelBoundaries; }
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

     
    }
}