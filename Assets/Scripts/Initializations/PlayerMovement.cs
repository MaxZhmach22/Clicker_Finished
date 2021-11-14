using System;
using UnityEngine;


namespace MonsterClicker
{
    internal sealed class PlayerMovement : IExecute
    {
        #region Fields

        private FloatingJoystick _joystick;
        private Vector3 _forward, _right;
        private Transform _player;
        private readonly GameData _gameData;

        #endregion


        #region ClassLifeCycles

        public PlayerMovement(Transform player, Camera mainCamera, GameData gameData)
        {
            _gameData = gameData;
            _player = player;
            _joystick = _gameData.FloatingJoystick;
            SetForwardDirectionVector(mainCamera);
        }

        #endregion


        #region UnityMethods

        public void Execute(float deltaTime)
        {
            if (Math.Abs(_joystick.Vertical + _joystick.Horizontal) > 0)
                Move(deltaTime);
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
