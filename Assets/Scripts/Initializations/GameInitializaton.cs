using System;
using UnityEngine;


namespace MonsterClicker
{
    internal sealed class GameInitializaton
    {
        //#region Fields

        private readonly GameData _gameData;
        private readonly Camera _mainCamera;
        private readonly UIPresenter _uIController;
        private readonly IPlayerInitialization _playerInitialization;
        private readonly InputInitialization _inputInitialization;
        private readonly ScoreController _scoreCounter;
        private readonly UiViewsLoader _uiViewsLoader;
        private readonly Transform _placeForUi;
        private StartGameState _startGameState;
        private GameGameState _gameGameState;
        private GameState _state;
        public GameStates CurrentGameState { get; private set; }

        //#endregion


        //#region ClassLifeCycles

        //public GameInitializaton(
        //    GameData gameData,
        //    Camera mainCamera,
        //    Transform placeForUi)
        //{
        //    _gameData = gameData;
        //    _mainCamera = mainCamera;
        //    _placeForUi = placeForUi;
        //    ChangeState(GameStates.Start);

        //    //new LevelInitialization(_gameData, _mainCamera);
        //    //_uiViewsLoader = new UiViewsLoader(_gameData, _placeForUi);           
        //    ////_uIController = new UIController(_gameData);
        //    ////controller.Add(_uIController);
        //    //_playerInitialization = new PlayerInitialization(_gameData);
        //    //_inputInitialization = new InputInitialization(
        //    //     controller,
        //    //    _playerInitialization.GetPlayer(),
        //    //    _mainCamera);
        //    //controller.Add(new EnemiesController(
        //    //  _gameData,
        //    //  _inputInitialization.TapCatch,
        //    //  _playerInitialization.GetPlayer()));
        //}

        //public void Dispose() { }

        //void Update()
        //{
        //    _state.Update(Time.deltaTime);
        //}

        //private void FixedUpdate()
        //{
        //    _state.FixedUpdate(Time.deltaTime);
        //}

        //private void LateUpdate()
        //{
        //    _state.LateUpdate(Time.deltaTime);
        //}

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
        //            return new StartGameState();
        //        case GameStates.Game:
        //            return new GameGameState();
        //        case GameStates.None:
        //            return null;
        //    }
        //    throw new ApplicationException($"Not implemented state");
        //}

        //#endregion
    }
}