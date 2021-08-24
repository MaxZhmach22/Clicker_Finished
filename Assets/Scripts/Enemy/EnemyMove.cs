using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Vector3 _playerPos;
    private Vector3 _movement;
    private Vector3 _diretion;
    private Vector3 _randomPos;
    private Rigidbody _rigidbody;
    private GameObject _player;
    private float _blindZone = 4f;
    private float moveSpeed = 4f;
    private float patrolSpeed = 2f;
    private float patrolStopDistance = 2f;
    

    void Start()
    {
        _player = GameObject.Find("Player");
        _rigidbody = GetComponent<Rigidbody>();
        _randomPos = Utils.RandomPosition(-10, 10);
        _randomPos.Normalize();
    }

   
    void Update()
    {
        _playerPos = _player.transform.position;
        _diretion = _playerPos - transform.position;
        if(_diretion.sqrMagnitude < _blindZone * _blindZone)
        {
            float angle = Mathf.Atan2(_diretion.x, _diretion.z) * Mathf.Rad2Deg;
            _rigidbody.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            _diretion.Normalize();
            _movement = _diretion;
            Move(_movement, moveSpeed);
        }
        else
        {
           var directionToRandomPos = _randomPos - transform.position;
           if(directionToRandomPos.sqrMagnitude > patrolStopDistance * patrolStopDistance)
            {
                directionToRandomPos.Normalize();
                Move(directionToRandomPos, patrolSpeed);
            }
           else
            {
                _randomPos = Utils.RandomPosition(-10, 10);
            }
        }

    }

    
    private void Move(Vector3 direction, float speed)
    {
        _rigidbody.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

   
}
