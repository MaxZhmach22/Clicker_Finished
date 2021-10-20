using System;
using Zenject;

namespace Clicker
{
    internal sealed class GameGameState : GameState
    {
        private MainGameController _mainGameController;
        private readonly MainGameController.Factory _mainGameControllerFactory;

        public GameGameState(MainGameController.Factory gameProcessControllerFactory)
        {
            _mainGameControllerFactory = gameProcessControllerFactory;
        }

        public override void Start()
        {
            _mainGameController = _mainGameControllerFactory.Create();
            _mainGameController.Start();
        }

        public override void Dispose()
        {
          
            _mainGameController.Dispose();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
        internal sealed class Factory : PlaceholderFactory<GameGameState>
        {

        }
    }
    
}
