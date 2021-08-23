using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{

    internal sealed class GameHandler : MonoBehaviour
    {
        private Camera _mainCamera;
        private GameData _gameData;
        private ExecuteController _executeController;
       

        void Start()
        {
            _gameData = Resources.Load<GameData>("GameData");
            _mainCamera = Camera.main;
            _executeController = new ExecuteController();
            new GameInitializaton(_executeController,_gameData, _mainCamera);
            
        }

       
        void Update()
        {
            var deltaTime = Time.deltaTime;
            _executeController.Execute(deltaTime);
        }
    }
}
