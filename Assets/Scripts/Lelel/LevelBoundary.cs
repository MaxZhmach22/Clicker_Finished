using UnityEngine;

namespace Clicker
{
    internal enum BoundaryTypes
    {
        Left,
        Right,
        Top,
        Bottom
    }
    internal sealed class LevelBoundary : MonoBehaviour
    {
        [field: SerializeField] public BoundaryTypes Type { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger Collision");
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision");
        }
    }

    
}
