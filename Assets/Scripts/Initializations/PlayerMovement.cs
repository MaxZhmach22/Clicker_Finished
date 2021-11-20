using System;
using UnityEngine;
using Zenject;


namespace MonsterClicker
{
    internal sealed class PlayerMovement : ITickable
    {
        #region Fields

        private readonly FloatingJoystick _joystick;
        private readonly GameData _gameData;
        private Vector3 _forward, _right;
        private Player _player;

        #endregion


        #region ClassLifeCycles

        public PlayerMovement(
            Player player, 
            Camera mainCamera, 
            GameData gameData)
        {
            _gameData = gameData;
            _player = player;
            _joystick = _gameData.FloatingJoystick;
            SetForwardDirectionVector(mainCamera);
        }

        #endregion


        #region UnityMethods

        public void Tick()
        {
            if (Math.Abs(_joystick.Vertical + _joystick.Horizontal) > 0)
                    Move(Time.deltaTime);
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
