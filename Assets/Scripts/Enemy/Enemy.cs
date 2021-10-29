using UnityEngine;
using DG.Tweening;
using Zenject;

namespace Clicker
{
    internal sealed class Enemy : EnemyBase
    {
        private LevelHelper _level;
        private Vector3 _randomAxisRotateAround;
        private LevelConfig _levelConfig;
        private Player _player;

        public override void Init(LevelHelper level, LevelConfig levelConfig, Player player)
        {
            _player = player;
            _levelConfig = levelConfig;
            _level = level;
            _rigidBody = GetComponent<Rigidbody>();
            _randomAxisRotateAround = new Vector3(Random.value, Random.value, Random.value).normalized;
        }

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

        [field: SerializeField] public GameObject ExplosionPrefab { get; private set; }
        [field: SerializeField] public GameObject MiscPrefab { get; private set; }

        private void Start() =>
            CurrentHp = MaxHp;

        [ContextMenu(nameof(DestroyEffectsInit))]
        public override void DestroyEffectsInit()
        {
            if (IsDead)
                return;
            var explosionEffect = Instantiate(ExplosionPrefab, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(explosionEffect, 2f);
        }
        [ContextMenu(nameof(TakeDamageEffectsInit))]
        public override void TakeDamageEffectsInit()
        {
            if (IsDead)
                return;
            var miscEffect = Instantiate(MiscPrefab, PointForEffectsInstantiate.position, Quaternion.identity);
            PlayAnimation();
            Destroy(miscEffect, 2f);
        }
        [ContextMenu(nameof(PlayAnimation))]
        public override void PlayAnimation()
        {
            if (AnimationInProcess || IsDead)
                return;
            AnimationInProcess = true;
            gameObject.transform.DOShakeScale(
                Duration, 
                Strenght, 
                Vibrato, 
                Randomness, 
                FadeOut).OnComplete(() => AnimationInProcess = false);
        }
        public override void DeathStateInit()
        {
            //TODO Enemy 1) ƒописать инициализацию смерти врага

            DestroyEffectsInit();
            //IsDead = true;
        }

       
        public override void GetActive()
        {
            gameObject.SetActive(true);
            gameObject.transform.position = _startPosition.position;
            _rigidBody.AddForce(_movingDirection * _speed, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                DeathStateInit();
                Debug.Log("Player collision");
            }
        }

        public override void Tick()
        {
            CheckForTeleport();
        }

        public override void FixedTick()
        {
            var speed = _rigidBody.velocity.magnitude;

            if (speed > _levelConfig.MaxSpeed)
            {
                var dir = _rigidBody.velocity / speed;
                _rigidBody.velocity = dir * _levelConfig.MaxSpeed;
            }
            
        }
        void CheckForTeleport()
        {
            //float offset = 30;
            //if(Position.x > 0 && Position.z > 0 && IsMovingInDirection(new Vector3(1,0,1)))
            //{
            //    if(Position.x + Position.z >= offset)
            //    {
            //        SetNewPosition(offset, -Position.x, -Position.z);
            //        Velocity = GetRandomDirection() * 3;
            //    }

            //}
            //else if (Position.x < 0 && Position.z < 0 && IsMovingInDirection(new Vector3(-1, 0, -1)))
            //{
            //    if (Mathf.Abs(Position.x) + Mathf.Abs(Position.z) >= offset)
            //    {
            //        SetNewPosition(offset, Position.x, Position.z);
            //        Velocity = GetRandomDirection() * 3;
            //    }

            //}
            //else if (Position.x > 0 && Position.z < 0 && IsMovingInDirection(new Vector3(1, 0, -1)))
            //{
            //    if (Position.x + Mathf.Abs(Position.z) >= offset)
            //    {

            //        Velocity = GetRandomDirection() * 3 ;
            //    }
            //}
            //else if (Position.x < 0 && Position.z > 0 && IsMovingInDirection(new Vector3(-1, 0, 1)))
            //{
            //    if (Mathf.Abs(Position.x) + Position.z >= offset)
            //    {
            //        SetNewPosition(offset, Position.x, -Position.z);
            //        Velocity = GetRandomDirection() * 3;
            //    }
            //}

            ////Right movement
            //if (Position.x > _level.Right + Scale && Position.z < -_level.Right - Scale && IsMovingInDirection(new Vector3(1, 0, -1)))
            //{
            //    transform.SetX(-_level.Right - Scale / 2);
            //    transform.SetZ(_level.Right + Scale / 2);
            //}
            ////Left movement
            //else if (Position.x < -_level.Right - Scale && Position.z > _level.Right + Scale && IsMovingInDirection(new Vector3(-1, 0, 1)))
            //{
            //    transform.SetX(_level.Right + Scale / 2);
            //    transform.SetZ(-_level.Right - Scale / 2);
            //}

            //else if (Position.x < -_level.Right - Scale && Position.z < -_level.Right - Scale && IsMovingInDirection(new Vector3(-1, 0, -1)))
            //{
            //    transform.SetX(_level.Right + Scale / 2);
            //    transform.SetZ(_level.Right + Scale / 2);
            //}
            //else if (Position.x > _level.Right + Scale && Position.z > _level.Right + Scale && IsMovingInDirection(new Vector3(1, 0, 1)))
            //{
            //    transform.SetX(-_level.Right - Scale / 2);
            //    transform.SetZ(-_level.Right - Scale / 2);
            //}
            //UpTest
            if(Position.x < 0 && Position.z < 0)
            {
                if(Mathf.Abs(Position.x) + Mathf.Abs(Position.z) > _level.Right * 2 + Scale/*+ Scale && IsMovingInDirection(new Vector3(-1, 0, -1))*/)
                {
                    transform.SetX(-Position.x - Scale);
                    transform.SetZ(-Position.z - Scale);
                    //var offset = _level.Right * 2;
                    //var rndPos = Random.Range(0, offset);
                    //var rndPosZ = offset - rndPos;
                    //transform.SetZ(rndPos + Scale / 2);
                    //transform.SetX(rndPosZ + Scale / 2);
                }
            }
            if(Position.x > 0 && Position.z > 0)
            {
                if (Position.x + Position.z > _level.Right * 2 + Scale /*&& IsMovingInDirection(new Vector3(1, 0, 1))*/)
                {
                    transform.SetX(-Position.x + Scale);
                    transform.SetZ(-Position.z + Scale);
                    //var offset = _level.Right * 2;
                    //var rndPos = Random.Range(0, offset);
                    //var rndPosZ = offset - rndPos;
                    //transform.SetZ(-rndPos - Scale / 2);
                    //transform.SetX(-rndPosZ - Scale / 2);
                }
            }
            if(Position.x <0 && Position.z > 0)
            {
                if (Mathf.Abs(Position.x) + Mathf.Abs(Position.z) > _level.Right * 2 + Scale /*&& IsMovingInDirection(new Vector3(-1, 0, 1))*/)
                {
                    transform.SetX(-Position.x - Scale);
                    transform.SetZ(-Position.z + Scale);
                    //var offset = _level.Right * 2;
                    //var rndPos = Random.Range(0, offset);
                    //var rndPosZ = offset - rndPos;
                    //transform.SetZ(rndPos + Scale / 2);
                    //transform.SetX(-rndPosZ + Scale / 2);
                }
            }
            if(Position.x >0 && Position.z < 0)
            {
                 if (Mathf.Abs(Position.x) + Mathf.Abs(Position.z) > _level.Right * 2 + Scale /*&& IsMovingInDirection(new Vector3(1, 0, -1))*/)
                {
                    transform.SetX(-Position.x + Scale);
                    transform.SetZ(-Position.z - Scale);
                    //var offset = _level.Right * 2;
                    //var rndPos = Random.Range(0, offset);
                    //var rndPosZ = offset - rndPos;
                    //transform.SetZ(rndPos + Scale / 2);
                    //transform.SetX(-rndPosZ + Scale / 2);
                }
            }

            transform.RotateAround(transform.position, _randomAxisRotateAround, 30 * Time.deltaTime);
        }
  
        private void SetNewPosition(float offset, float posX, float posZ)
        {
            var rndPosXValue = Random.Range(0, offset);
            var rndPosZValue = offset - rndPosXValue;
            if (posX < 0)
                rndPosXValue *= -1;
            if (posZ < 0)
                rndPosZValue *= -1;
            transform.position = new Vector3(rndPosXValue, 1, rndPosZValue);
            Debug.Log(gameObject.transform.position);
        }

        bool IsMovingInDirection(Vector3 dir)
        {
            return Vector3.Dot(dir, _rigidBody.velocity) > 0;
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.TryGetComponent<LevelBoundary>(out var boundary))
        //        SetNewPositionS(boundary.Type);
        //}

        //private void SetNewPositionS(BoundaryTypes type)
        //{
        //    var offset = 30;
        //    switch (type)
        //    {
        //        case BoundaryTypes.Left:
        //            SetNewPosition(offset, Position.x, -Position.z);
        //            break;
        //        case BoundaryTypes.Right:
        //            SetNewPosition(offset, -Position.x, Position.z);
        //            break;
        //        case BoundaryTypes.Top:
        //            SetNewPosition(offset, -Position.x, -Position.z);
        //            break;
        //        case BoundaryTypes.Bottom:
        //            SetNewPosition(offset, Position.x, Position.z);
        //            break;
        //    }
        //}
    }
}