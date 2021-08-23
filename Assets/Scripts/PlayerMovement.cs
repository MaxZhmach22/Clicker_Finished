using System;
using UnityEngine;

namespace MonsterClicker
{
    internal sealed class PlayerMovement : IExecute
    {
        [SerializeField] private float _movementSpeed = 4f;
        private FloatingJoystick _joystick;
        private Vector3 _forward, _right;
        private Transform _player;

        public PlayerMovement(Transform player, Camera main)
        {
            _joystick = GameObject.FindObjectOfType<FloatingJoystick>();
            _player = player;
            _forward = main.transform.forward;
            _forward.y = 0;
            _forward = Vector3.Normalize(_forward);
            _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
        }
        public void Execute(float deltaTime)
        {
            if (Math.Abs(_joystick.Vertical + _joystick.Horizontal) > 0)
            {
                Move(deltaTime);
            }
        }

        private void Move(float deltaTime)
        {

            Vector3 direction = new Vector3((_joystick.Horizontal), 0, (_joystick.Vertical));
            Vector3 rigthMovement = _right * _movementSpeed * Time.deltaTime * _joystick.Horizontal;
            Vector3 upMovement = _forward * _movementSpeed * Time.deltaTime * _joystick.Vertical;
            Vector3 heading = Vector3.Normalize(rigthMovement + upMovement);

            _player.transform.forward = heading;
            _player.transform.position += rigthMovement;
            _player.transform.position += upMovement;
            
        }

        public Vector3 PlayerPosition()
        {
            return _player.transform.position;
        }

    }
}
