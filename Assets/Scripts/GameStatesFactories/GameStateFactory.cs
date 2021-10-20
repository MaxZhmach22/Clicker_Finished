using ModestTree;

namespace Clicker
{
    internal sealed class GameStateFactory
    {
        readonly StartGameState.Factory _startStateFactory;
        readonly CreditsGameState.Factory  _creditsStateFactory;
        //readonly CreditsGameState.Factory _creditsStateFactory;
        readonly GameGameState.Factory _gameStateFactory;

        public GameStateFactory(
            StartGameState.Factory startStateFactory,
            CreditsGameState.Factory creditssStateFactory, 
            //CreditsGameState.Factory creditsStateFactory, 
            GameGameState.Factory gameStateFactory)
        {
            _startStateFactory = startStateFactory;
            _creditsStateFactory = creditssStateFactory;
            //_settingsStateFactory = settingsStateFactory;
            //_creditsStateFactory = creditsStateFactory;
            _gameStateFactory = gameStateFactory;
        }

        public GameState CreateState(GameStates state)  // TODO FactoryState: 2) Делаем метод принимающий состояния.
        {
            switch (state)
            {
                case GameStates.Start:
                    return _startStateFactory.Create();
                case GameStates.Settings:
                    break;
                case GameStates.Credits:
                    return _creditsStateFactory.Create();
                    break;
                case GameStates.Game:
                    return _gameStateFactory.Create();
                    break;
                case GameStates.None:
                    break;
            }

            throw Assert.CreateException(); //TODO ??
        }
    }
}
