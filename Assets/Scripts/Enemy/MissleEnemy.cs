using UnityEngine;
using Zenject;

namespace Clicker
{
    internal sealed class MissleEnemy : BaseEnemy
    {
        private Vector3 _randomAxisRotateAround;
        private LevelConfig _levelConfig;
        private LevelHelper _level;
        private Player _player;
        private IEnemiesPool _enemiesPool;
        private bool isStart = true;
        private Transform _child;
        public override float YAxisOfFset { get; protected set; } = 5;

        [Inject]
        public void Init(
            LevelHelper level,
            LevelConfig levelConfig,
            IEnemiesPool enemiesPool,
            Player player)
        {
            _randomAxisRotateAround = new Vector3(Random.value, Random.value, Random.value).normalized;
            _levelConfig = levelConfig;
            _enemiesPool = enemiesPool;
            _level = level;
            _player = player;
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void Start() =>
           CurrentHp = MaxHp;

        public override Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public override float Scale
        {
            get
            {
                var scale = transform.localScale;
                return scale[0];
            }
            set
            {
                transform.localScale = new Vector3(value, value, value);
                _rigidBody.mass = value;
                MaxHp *= value;
            }
        }

        public override float Mass
        {
            get { return _rigidBody.mass; }
            set { _rigidBody.mass = value; }
        }

        public override Vector3 Velocity
        {
            get { return _rigidBody.velocity; }
            set { _rigidBody.velocity = value; }
        }

        public override void DeathStateInit()
        {
            DestroyEffectsInit();
            _enemiesPool.ReturnToPool(this);
        }

        public override void DestroyEffectsInit()
        {
            
        }

        public override void FixedTick()
        {
           
        }

        public override void GetActive()
        {
           
        }

        public override void PlayAnimation()
        {
            
        }

        public override void TakeDamage()
        {
            TakeDamageEffectsInit();
            PlayAnimation();
        }

        public override void TakeDamageEffectsInit()
        {
           
        }

        public override void Tick()
        {
            if (isStart)
            {
                AddForce();
                isStart = false;
            }
            //_rigidBody.MovePosition(_player.gameObject.transform.position.normalized  * Time.deltaTime);
        }
        
        public void AddForce()
        {
            var vectorForce =_player.transform.position - transform.position;
            transform.LookAt(vectorForce);
            _rigidBody.AddForce(vectorForce * _speed * Time.deltaTime, ForceMode.Impulse);
        }
        

        

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
            Debug.Log(gameObject.transform.position.y);
            //_enemiesPool.ReturnToPool(this);

        }
    }
}
