using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Clicker
{
    public sealed class ImpactEffectView : MonoBehaviour
    {
        
        [SerializeField] private List<ParticleSystem> _particleSystems;

        public bool IsPlaying => CheckStatus();

        public void DoImpactEffect(Vector3 position)
        {
            gameObject.transform.position = position;
            foreach (var particles in _particleSystems)
                particles.Play();
        }

        private bool CheckStatus()
        {
            foreach (var particles in _particleSystems)
            {
                if (!particles.isPlaying)
                    continue;
                else
                    return true;
            }
            return false;
        }

        public sealed class Factory : PlaceholderFactory<ImpactEffectView>
        {
        }
    }
}