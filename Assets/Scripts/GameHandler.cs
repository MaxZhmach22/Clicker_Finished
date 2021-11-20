using System;
using UnityEngine;

namespace MonsterClicker
{
    internal sealed class GameHandler : MonoBehaviour
    {
        //#region Fields

        //[SerializeField] private GameData _gameData;
        //[SerializeField] private Transform _placeForUi;
        //private Camera _mainCamera;
        //private GameState _state;
        //private ExecuteController _executeController;
        //private GameInitializaton _gameInitialization;
        //public GameStates CurrentGameState { get; private set; }

        //#endregion


        //#region ClassLifeCycles

        //void Start()
        //{
        //    ChangeState(GameStates.Start);
        //    _mainCamera = Camera.main;
        //    _gameInitialization = new GameInitializaton(_gameData, _mainCamera, _placeForUi);
        //    _executeController = new ExecuteController();
        //    _executeController.Add(new GameInitializaton(_executeController, _gameData, _mainCamera, _placeForUi));
        //}

        //private void OnDestroy() =>
        //    _executeController.Dispose();

        //#endregion


        //#region UnityMethods

        //void Update()
        //{
        //    _state.Update(Time.deltaTime);
        //    _executeController.Execute(Time.deltaTime);
        //}

        //private void FixedUpdate()
        //{
        //    _state.FixedUpdate(Time.deltaTime);
        //}

        //private void LateUpdate()
        //{
        //    _state.LateUpdate(Time.deltaTime);
        //}


        //#endregion

        //public void ChangeState(GameStates state)
        //{
        //    if (_state != null)
        //    {
        //        _state.Dispose();
        //        _state = null;
        //    }
        //    CurrentGameState = state;
        //    _state = CreateState(state);
        //    _state.Start();
        //}

        //public GameState CreateState(GameStates state)
        //{
        //    switch (state)
        //    {
        //        case GameStates.Start:
        //            return _startStateFactory.Create();
        //        case GameStates.Game:
        //            return _gameStateFactory.Create();
        //        case GameStates.End:
        //            return _endStateFactory.Create();
        //        case GameStates.None:
        //            break;
        //    }
        //    throw new ApplicationException($"Not implemented state");
        //}

    }
}
