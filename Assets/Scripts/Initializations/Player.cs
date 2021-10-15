using UnityEngine;
using Zenject;

namespace Clicker
{
    internal class Player : MonoBehaviour
    {
        GameState _state;
        GameStateFactory _gameStateFactory;

        [Inject]
        public void Init(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
        }

        public void Start()
        {
            ChangeState(GameStates.Start);
        }

        public void ChangeState(GameStates state) 
        {
            if (_state != null)
            {
                _state.Dispose();
                _state = null;
            }

            _state = _gameStateFactory.CreateState(state);
            _state.Start();
        }

        public class Settings
        {
            public GameObject PlayersPrefab;
        }
    }
}
