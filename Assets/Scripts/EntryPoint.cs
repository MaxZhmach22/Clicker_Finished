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
        private PlayerProfile _playerProfile;
        private UiFactories _uIFactories;

        //[Inject]
        //public void Init(UiFactories uiFactories /*GameSettingsInstaller gameData, Transform placeForUi, PlayerProfile playerProfile*/)
        //{
        //    //_gameData = gameData;
        //    //_placeForUi = placeForUi;
        //    //_playerProfile = playerProfile;
        //    _uIFactories = uiFactories;
        //    Debug.Log("Init");
        //}

        void Start()
        {
            //Debug.Log("Start");
            //_mainCamera = Camera.main;
            //_executeController = new ExecuteController();
        }

        void Update()
        {
        //    var deltaTime = Time.deltaTime;
        //    _executeController.Execute(deltaTime);
        }
    }
}
