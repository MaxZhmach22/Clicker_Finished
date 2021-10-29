using System;
using UnityEngine;
using Zenject;

namespace Clicker 
{
    //TODO возможно внендрить интерфейс!
    public sealed class ExplosionForceEffect : MonoBehaviour
    {
        [field: SerializeField] public float Radius { get; private set; } = 5.0F;
        [field: SerializeField] public float PowerOfEnemyExplosion { get; private set; } = 10.0F;
        [field: SerializeField] public float PowerOfEmptyShoot { get; private set; } = 10.0F;

        internal void CreateExplosionEffect(
            Vector3 currentPosition, 
            bool blankShot, 
            float ?explosionForceCoefficient = null, 
            float ?explosionRadiusCoefficient = null)
        {
            Collider[] colliders = Physics.OverlapSphere(currentPosition, Radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if(blankShot)
                        rb.AddExplosionForce(
                            PowerOfEmptyShoot, currentPosition, Radius, 3.0F);
                    else    
                        rb.AddExplosionForce(
                            PowerOfEnemyExplosion * (float)explosionForceCoefficient, 
                            currentPosition, Radius * (float)explosionRadiusCoefficient, 3.0F);

                    Debug.Log($"ExplosionForceCoefficient {explosionForceCoefficient}\n" +
                        $"ExplosionRadiusCoefficient{explosionRadiusCoefficient}");
                }
                    
            }
        }

        public sealed class Factory : PlaceholderFactory<ExplosionForceEffect>
        {
        }
    }
}