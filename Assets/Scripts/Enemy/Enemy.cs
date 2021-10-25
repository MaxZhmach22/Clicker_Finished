using UnityEngine;
using DG.Tweening;

namespace Clicker
{
    internal sealed class Enemy : EnemyBase, IEnemy, IDestroyable, IAnimatable
    {
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
            _rigidbody = GetComponent<Rigidbody>();
            gameObject.transform.position = _startPosition.position;
            _rigidbody.AddForce(_movingDirection * _speed, ForceMode.Impulse);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                DeathStateInit();
                Debug.Log("Player collision");
            }
        }
    }
}