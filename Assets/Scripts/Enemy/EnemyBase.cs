using UnityEngine;
using Zenject;

namespace Clicker
{
    internal abstract class EnemyBase : MonoBehaviour, IEnemy, IAnimatable
    {

        [field: Header("Enemy Settings")]
        [field: SerializeField] public float CurrentHp { get; set; }
        [field: SerializeField] public float MaxHp { get; protected set; }
        [field: SerializeField] public int ScorePoints { get; protected set; }

        public abstract Vector3 Position { get; set; }
        public abstract float Scale { get; set; }
        public abstract float Mass { get; set; }
        public abstract Vector3 Velocity { get; set; }


        [SerializeField] protected float _speed;
        public Vector3 CurrentPosition => gameObject.transform.position;
        public bool IsDead { get; set; } = false;
        public abstract void DeathStateInit();


        private EnemyMoveTypes _moveType;
        public EnemyMoveTypes MoveType { get => _moveType; set => _moveType = value; }
        protected Rigidbody _rigidBody;
        public Vector3 MovingDirection { get => _movingDirection; set => _movingDirection = value; }
        protected Vector3 _movingDirection;
        protected Transform _startPosition;

        [Header("Animation Settings")]
        [Range(0, 5)]
        public float Duration;
        [Range(0, 2)]
        public float Strenght;
        [Range(0, 50)]
        public int Vibrato;
        [Range(0, 50)]
        public float Randomness;
        public bool FadeOut;
        public bool AnimationInProcess { get; protected set; }

        [field: Header("Prefabs of Effects")]
        [field: SerializeField] public Transform PointForEffectsInstantiate { get; protected set; }


        public float ExplosionForceCoefficient =>
            _gameObjectMagnitude / _baseCoefficient;
        public float ExplosionRadiusCoefficient =>
            _gameObjectMagnitude / _baseRadiusCoefficient;

        protected float _baseCoefficient = 1;
        protected float _baseRadiusCoefficient = 1;
        protected float _gameObjectMagnitude => 
            transform.localScale.magnitude;

        public abstract void PlayAnimation();
        public abstract void DestroyEffectsInit();
        public abstract void TakeDamageEffectsInit();

        public virtual Vector3 GetRandomDirection()
        {
            var theta = UnityEngine.Random.Range(0, Mathf.PI * 2.0f);
            return new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
        }

        public virtual void SetStartPositioAndDirection(
            EnemyMoveTypes enemyMoveType, 
            GameLevelView gameLevelView)
        {
            switch (enemyMoveType)
            {
                case EnemyMoveTypes.UpToDown:
                    _startPosition = gameLevelView.UpCenterSpawnPoint.transform;
                    _movingDirection = new Vector3(-15, 0, -15);
                    break;
                case EnemyMoveTypes.DownToUp:
                    _startPosition = gameLevelView.DownCenterSpawnPoint.transform;
                    _movingDirection = new Vector3(15, 0, 15);
                    break;
                case EnemyMoveTypes.LeftToRight:
                    _startPosition = gameLevelView.LeftCenterSpawnPoint.transform;
                    _movingDirection = new Vector3(15, 0, -15);
                    break;
                case EnemyMoveTypes.RightToLeft:
                    _startPosition = gameLevelView.RightCenterSpawnPoint.transform;
                    _movingDirection = new Vector3(-15, 0, 15);
                    break; 
            }
        }
        public abstract void GetActive();

        public abstract void Tick();

        public abstract void FixedTick();

        public abstract void Init(LevelHelper level, LevelConfig levelConfig, Player player);
    }
}