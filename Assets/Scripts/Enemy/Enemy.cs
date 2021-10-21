using UnityEngine;
using DG.Tweening;

namespace Clicker
{
    internal sealed class Enemy : EnemyBase, IEnemy, IDestroyable, IAnimatable
    {
       
        [field: SerializeField] public GameObject ExplosionPrefab { get; private set; }
        [field: SerializeField] public GameObject MiscPrefab { get; private set; }


        [ContextMenu(nameof(DestroyEffectsInit))]
        public override void DestroyEffectsInit()
        {
            if (IsDead)
                return;
            var go = Instantiate(ExplosionPrefab, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(go, 2f);
        }
        [ContextMenu(nameof(TakeDamageEffectsInit))]
        public override void TakeDamageEffectsInit()
        {
            if (IsDead)
                return;
            var go = Instantiate(MiscPrefab, PointForEffectsInstantiate.position, Quaternion.identity);
            PlayAnimation();
            Destroy(go, 2f);
        }

        [ContextMenu(nameof(PlayAnimation))]
        public override void PlayAnimation()
        {
            if (AnimationInProcess || IsDead)
                return;
            AnimationInProcess = true;
            gameObject.transform.DOShakeScale(Duration, Strenght, Vibrato, Randomness, FadeOut).OnComplete(() => AnimationInProcess = false);
        }

        public override void DeathStateInit()
        {
            //TODO Enemy 1) ƒописать инициализацию смерти врага
            DestroyEffectsInit();
            IsDead = true;

        }
    }
}