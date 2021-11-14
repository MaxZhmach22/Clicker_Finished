using UnityEngine;

namespace MonsterClicker
{
    internal sealed class GameHandler : MonoBehaviour
    {
        #region Fields

        private Camera _mainCamera;
        private GameData _gameData;
        private ExecuteController _executeController;

        #endregion


        #region ClassLifeCycles

        void Start()
        {
            _gameData = Resources.Load<GameData>("GameData");
            _mainCamera = Camera.main;
            _executeController = new ExecuteController();
            _executeController.Add(new GameInitializaton(_executeController, _gameData, _mainCamera));
        }

        private void OnDestroy() =>
            _executeController.Dispose();

        #endregion


        #region UnityMethods

        void Update()
        {
            var deltaTime = Time.deltaTime;
            _executeController.Execute(deltaTime);
        } 


        #endregion

    }
}
