using UnityEngine;
using Zenject;
using UniRx;

namespace Clicker
{
    internal class Player : MonoBehaviour
    {
        public Subject<Collision> CollisionGameObject = new Subject<Collision>();

        private GameState _state;
        private GameStateFactory _gameStateFactory;
        private InputTouchPresenter _inputTouchPresenter;
        public int Damage = 20;
        private int _lifeCounts = 1;

        public int LifeCounts => _lifeCounts;

        public GameStates CurrentGameState { get; private set; }

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
            CurrentGameState = state;
            _state = _gameStateFactory.CreateState(state);
            _state.Start();
        }

        public class Settings
        {
            public GameObject PlayersPrefab;
        }

        private void OnCollisionEnter(Collision collision) =>
            CollisionGameObject.OnNext(collision);
    }
}
