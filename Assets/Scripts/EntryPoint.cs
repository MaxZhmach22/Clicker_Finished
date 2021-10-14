using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Clicker
{
    internal sealed class EntryPoint : MonoBehaviour
    {
        private Camera _mainCamera;
        private GameSettingsInstaller _gameData;
        private ExecuteController _executeController;
        private Transform _placeForUi;
        private PlayerProfile _playerProfile;

        [Inject]
        public void Init(GameSettingsInstaller gameData, Transform placeForUi, PlayerProfile playerProfile)
        {
            _gameData = gameData;
            _placeForUi = placeForUi;
            _playerProfile = playerProfile;
            Debug.Log("Init");
        }

        void Start()
        {
            _mainCamera = Camera.main;
            _executeController = new ExecuteController();
            new MainController(_placeForUi, _playerProfile, _executeController, _gameData, _mainCamera);
        }

        void Update()
        {
            var deltaTime = Time.deltaTime;
            _executeController.Execute(deltaTime);
        }
    }
}
