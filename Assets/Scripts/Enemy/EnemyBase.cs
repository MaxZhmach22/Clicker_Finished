using UnityEngine;

namespace Clicker
{
    internal abstract class EnemyBase : MonoBehaviour, IEnemy, IDestroyable, IAnimatable
    {
        [field: Header("Enemy Settings")]
        [field: SerializeField] public float CurrentHp { get; set; }
        [field: SerializeField] public float MaxHp { get; protected set; }
        [field: SerializeField] public int ScorePoints { get; protected set; }

        [SerializeField] protected float _speed;
        public Vector3 CurrentPosition => gameObject.transform.position;
        public bool IsDead { get; set; } = false;
        public abstract void DeathStateInit();


        private EnemyMoveTypes _moveType;
        public EnemyMoveTypes MoveType { get => _moveType; set => _moveType = value; }
        protected Rigidbody _rigidbody;
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

        public abstract void PlayAnimation();
        public abstract void DestroyEffectsInit();
        public abstract void TakeDamageEffectsInit();

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
       
    }
}