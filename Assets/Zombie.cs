using UnityEngine;
using DG.Tweening;

namespace MonsterClicker
{
    public sealed class Zombie : MonoBehaviour
    {
        private Camera _camera;
        private Player _player;
        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;
        private Animator _animator;
        [SerializeField] private float _timeAfterKill;
        [SerializeField] private float _attackingDistance;
        private float _timer;
        private bool _isDead;


        private void Awake()
        {
            _camera = Camera.main;
            _player = FindObjectOfType<Player>();
            _colliders = GetComponentsInChildren<Collider>();
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            _animator = GetComponent<Animator>();
            SetRagdoll(false);
            SetMain(true);

        }

        private void SetRagdoll(bool active)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].enabled = active;
            }
            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].isKinematic = !active;
            }
        }

        private void SetMain(bool active)
        {
            _animator.enabled = active;
            _rigidbodies[0].isKinematic = !active;
            _colliders[0].enabled = active;
        }

        void Attack()
        {
            var distance = (gameObject.transform.position - _player.gameObject.transform.position).sqrMagnitude;
            if (distance > _attackingDistance)
                return;

            if (_player.ArmorActive)
            {
                _player.Shield.gameObject.SetActive(true);
                _player.Shield.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f)
                    .OnComplete(() => _player.Shield.gameObject.SetActive(false));
                return;
            }

             _camera.DOShakePosition(0.5f, 0.5f, 10, 90);
             Debug.Log("Attacking");
        }


        private void Update()
        {
            if (!_isDead)
                return;
            
            _timer += Time.deltaTime;
            if(_timer >= _timeAfterKill)
            {
                //ReturnTOPool
                gameObject.SetActive(false);
                _timer = 0;
                _isDead = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
                return;

            if (collision.relativeVelocity.sqrMagnitude > 50)
            {
                SetMain(false);
                SetRagdoll(true);
                _isDead = true;
                Debug.Log(collision.relativeVelocity.sqrMagnitude);
            }
        }
    }
}
