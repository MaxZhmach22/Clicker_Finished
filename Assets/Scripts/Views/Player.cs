using UnityEngine;
using Zenject;


namespace MonsterClicker
{
    internal class Player : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] public float Force { get; private set; }
        [field: SerializeField] public Transform Shield { get; private set; }
        [field: SerializeField] public Transform LeftSword { get; private set; }
        [field: SerializeField] public Transform RigthSword { get; private set; }
        private GameState _state;
        private GameStateFactory _gameStateFactory;
        private Rigidbody _rigidBody;
        private MeshRenderer _sphereMeshRenderer;
        
        


        public GameStates CurrentGameState { get; private set; }
        public Rigidbody RigidBody => _rigidBody;
        public MeshRenderer SphereMeshRenderer => _sphereMeshRenderer;
        public bool ArmorActive { get; set; }
        #endregion


        #region ClassLifeCycles

        [Inject]
        public void Init(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
            _rigidBody = GetComponent<Rigidbody>();
            _sphereMeshRenderer = GetComponent<MeshRenderer>();
        }

        public void Start() =>
            ChangeState(GameStates.Game);


        #endregion


        #region Methods

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

      
        #endregion
    }
}