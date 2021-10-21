using UnityEngine;
using Zenject;
using UniRx;

namespace Clicker
{
    internal class Player : MonoBehaviour
    {
        GameState _state;
        GameStateFactory _gameStateFactory;
        private InputTouchPresenter _inputTouchPresenter;
        public int Damage = 20; 

        [Inject]
        public void Init(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
           
        }

        public void Start()
        {
            ChangeState(GameStates.Game);
            //_inputTouchPresenter.Enemy.Subscribe(enemy =>
            //{
            //    if (enemy != null)
            //        ShootAnimation(enemy);
            //});
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

        private void ShootAnimation(IEnemy enemy)
        {
            
        }


    }
}
