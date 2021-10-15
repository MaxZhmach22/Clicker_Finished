using ModestTree;

namespace Clicker
{
    internal sealed class GameStateFactory
    {
        readonly StartGameState.Factory _startStateFactory;
        //readonly SettingsGameState.Factory _settingsStateFactory;
        //readonly CreditsGameState.Factory _creditsStateFactory;
        //readonly GameGameState.Factory _gameStateFactory;

        public GameStateFactory(
            StartGameState.Factory startStateFactory)
            //SettingsGameState.Factory settingsStateFactory, 
            //CreditsGameState.Factory creditsStateFactory, 
            //GameGameState.Factory gameStateFactory)
        {
            _startStateFactory = startStateFactory;
            //_settingsStateFactory = settingsStateFactory;
            //_creditsStateFactory = creditsStateFactory;
            //_gameStateFactory = gameStateFactory;
        }

        public GameState CreateState(GameStates state)  // TODO FactoryState: 2) Делаем метод принимающий состояния.
        {
            switch (state)
            {
                case GameStates.Start:
                    {
                        return _startStateFactory.Create();
                    }

                case GameStates.None:
                    break;
                case GameStates.Settings:
                    break;
                case GameStates.Credits:
                    break;
                case GameStates.Game:
                    break;
            }

            throw Assert.CreateException(); //TODO ??
        }
    }
}
