using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using DG.Tweening;


namespace MonsterClicker
{
    internal sealed class PlayerMovement : ITickable, IForcePowerLevel
    {
        #region Fields

        private ReactiveProperty<float> _moveForcePower = new ReactiveProperty<float>();
        private readonly FloatingJoystick _joystick;
        private float _maxTimer = 3f;
        private float _increaseLevel;
        private float _timer;
        private readonly GameData _gameData;
        private Vector3 _forward, _right;
        private Player _player;


        public IReadOnlyReactiveProperty<float> MoveForcePower => _moveForcePower;

        #endregion


        #region ClassLifeCycles

        public PlayerMovement(
            Player player, 
            Camera mainCamera, 
            GameData gameData,
            FloatingJoystick joystick)
        {
            _gameData = gameData;
            _player = player;
            _joystick = joystick;
            SetForwardDirectionVector(mainCamera);
            _joystick.DirectionToMove.Subscribe(direction => Move(direction));
            
        }

        #endregion


        #region UnityMethods

        public void Tick()
        {
            if (_joystick.StartIncrease)
            {
                _timer += Time.deltaTime;
                _increaseLevel = Mathf.Lerp(0, _maxTimer, _timer);
                _moveForcePower.Value = _increaseLevel / _maxTimer;
            }
            float velocity = _player.RigidBody.velocity.sqrMagnitude;
            //_player.SphereMeshRenderer.material.color = Color.Lerp(Color.red, Color.green, velocity/10);
            
            //Debug.Log((int)velocity);
            if (velocity < 5)
            {
                _player.ArmorActive = false;
            }
            else
                _player.ArmorActive = true;

        }

        private void Move(Vector3 direction)
        {
            Vector3 rigthMovement = _right * -direction.x;
            Vector3 upMovement = _forward * -direction.y;
            Vector3 heading = Vector3.Normalize(rigthMovement + upMovement);
            _player.RigidBody.AddForce(heading * _player.Force * _increaseLevel, ForceMode.Impulse);
            ResetValues();
        }

        private void ResetValues()
        {
            _timer = 0;
            _increaseLevel = 0;
            _moveForcePower.Value = 0;
        }

        #endregion


        #region Methods

        private void SetForwardDirectionVector(Camera mainCamera)
        {
            if (!mainCamera.orthographic)
                return;

            _forward = mainCamera.transform.forward;
            _forward.y = 0;
            _forward = Vector3.Normalize(_forward);
            _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
        }

        private void Move(float deltaTime)
        {
            Vector3 rigthMovement = _right * _gameData.PlayerMovementSpeed * deltaTime * _joystick.Horizontal;
            Vector3 upMovement = _forward * _gameData.PlayerMovementSpeed * deltaTime * _joystick.Vertical;
            Vector3 heading = Vector3.Normalize(rigthMovement + upMovement);

            _player.transform.forward = heading;
            _player.transform.position += rigthMovement;
            _player.transform.position += upMovement;
        }

        #endregion
    }
}
