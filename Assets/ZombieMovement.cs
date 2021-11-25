using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterClicker
{
    public class ZombieMovement : MonoBehaviour
    {
        private Player _player;
        private Animator _animator;
        private float _distance;
        private Rigidbody _rigidbody;
        private bool _isAttacking;
        private bool _seePlayer;
        private bool _isMovingInRandomDirection;
        private bool _isWaiting;

        private float _currentWalkingTime;
        private float _maxWalkingTimerValue;
        private float _currentWaitingTime;
        private float _maxWaitingTimerValue;
        private Vector3 _randomDirecton;
        private Vector3 _zombiePosition;
        private Vector3 _playerPosition;

        [SerializeField] private float _maxDistance;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _attackingDistance;
        [SerializeField] private float _speed;

        [field: Header("Zombie Random Move Settings:")]
        [field: Range(3, 9)]
        [field: SerializeField] public float TimeToMoveInRandomDirection { get; private set; }
        [field: Range(-20,0)]   
        [field: SerializeField] public float MinDirectionValue { get; private set; }
        [field: Range(0,20)]    
        [field: SerializeField] public float MaxDirectionValue { get; private set; }
        [field: SerializeField] public float WalkingSpeed { get; private set; }
        [field: Range(1, 6)]
        [field: SerializeField] public float TimeToWaitBeforeNewDirection { get; private set; }


        void Start()
        {
            _player = FindObjectOfType<Player>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            SetRandomDirectionValues();
        }

        void  FixedUpdate()
        {
            _zombiePosition = new Vector3(transform.position.x, 0, transform.position.z);
            _playerPosition = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
            _distance = (_zombiePosition - _playerPosition).sqrMagnitude;
            if (_distance < _maxDistance * _maxDistance || _animator.GetBool("IsNear"))
            {
                _seePlayer = true;
                _animator.SetTrigger("SeePlayer");
                _animator.SetBool("LookAround", false);
                var moveDirection = _playerPosition - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                _rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
                _animator.SetFloat("DistanceToPlayer", 1 - _distance / (_maxDistance * _maxDistance));
                var currentSpeed = _speed * (1 - _distance / (_maxDistance * _maxDistance));
                _currentWaitingTime = 0;

                if (_isAttacking)
                    return;

                if (_distance < _attackingDistance * _attackingDistance)
                {
                    _animator.SetBool("IsNear", true);
                    _isAttacking = true;
                }
                if (_distance > (_attackingDistance * _attackingDistance) * 1.5f && _animator.GetBool("IsNear"))
                {
                    _animator.SetBool("IsNear", false);
                }
                if (!_isAttacking)
                    _rigidbody.MovePosition(_zombiePosition + moveDirection.normalized * currentSpeed * Time.deltaTime);
                _seePlayer = false;
            }
            else
            {
                _animator.ResetTrigger("SeePlayer");
                WaitingForNewDirection();
                WalkInRandomDirection();
            }  
        }

        private float RandomFloat(float minValue, float maxValue) =>
            Random.Range(minValue, maxValue);

        private Vector3 SetRandomDirection() =>
            new Vector3(RandomFloat(MinDirectionValue, MaxDirectionValue), 0, RandomFloat(MinDirectionValue, MaxDirectionValue));

        void WalkInRandomDirection()
        {
            if (_isWaiting)
                return;

            _currentWalkingTime += Time.deltaTime;
            if(_currentWalkingTime >= _maxWalkingTimerValue)
            {
                _currentWaitingTime = 0;
                SetRandomDirectionValues();
                WaitingForNewDirection();
                return;
            }

            Quaternion toRotation = Quaternion.LookRotation(_randomDirecton, Vector3.up);
            _rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            _rigidbody.MovePosition(_zombiePosition + _randomDirecton.normalized * WalkingSpeed * Time.deltaTime);
        }

        private void WaitingForNewDirection()
        {
            _currentWaitingTime += Time.deltaTime;
            if(_currentWaitingTime >= _maxWaitingTimerValue)
            {
                _isWaiting = false;
                _animator.SetBool("LookAround", false);
                return;
            }

            _animator.SetBool("LookAround", true);
            _isWaiting = true;
        }

        private void SetRandomDirectionValues()
        {
            _maxWalkingTimerValue = RandomFloat(3, TimeToMoveInRandomDirection);
            _randomDirecton = SetRandomDirection();
            _maxWaitingTimerValue = RandomFloat(1, TimeToWaitBeforeNewDirection);
            _currentWalkingTime = 0;
        }

        public void FinishedAttack() => 
           _isAttacking = false;

    }
}
