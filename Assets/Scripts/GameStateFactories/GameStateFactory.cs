using ModestTree;
using UnityEngine;

namespace MonsterClicker
{
    internal sealed class GameStateFactory
    {
        #region Fields

        readonly GameGameState.Factory _gameStateFactory;
        //readonly StartGameState.Factory _startStateFactory;
        //readonly EndGameState.Factory _endStateFactory; 

        #endregion

        #region ClassLifeCycles

        public GameStateFactory(
          GameGameState.Factory gameStateFactory
          //StartGameState.Factory startStateFactory,
          //EndGameState.Factory endStateFactory
          )
        {
            _gameStateFactory = gameStateFactory;
            //_endStateFactory = endStateFactory;
            //_startStateFactory = startStateFactory;
        } 

        #endregion


        public GameState CreateState(GameStates state) 
        {
            switch (state)
            {
                case GameStates.Game:
                    return _gameStateFactory.Create();
                //case GameStates.Start:
                //    return _startStateFactory.Create();
                //case GameStates.End:
                //    return _endStateFactory.Create();
                //case GameStates.None:
                //    break;
            }
            throw Assert.CreateException(); 
        }
    }
}
