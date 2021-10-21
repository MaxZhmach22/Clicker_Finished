using UnityEngine;

namespace Clicker
{
    internal abstract class EnemyBase : MonoBehaviour, IEnemy, IDestroyable, IAnimatable
    {
        [field: Header("Enemy Settings")]
        [field: SerializeField] public float CurrentHp { get; set; }
        [field: SerializeField] public float MaxHp { get; protected set; }
        [field: SerializeField] public int ScorePoints { get; protected set; }
        public Vector3 CurrentPosition => gameObject.transform.position;
        public bool IsDead { get; set; } = false;
        public abstract void DeathStateInit();

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
    }
}
