using UnityEngine;

namespace MonsterClicker
{
    internal sealed class RagdollController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _allRigidBodies;

        private void Awake()
        {
            foreach (var rigidBody in _allRigidBodies)
            {
                rigidBody.isKinematic = true;
            }
        }

        public void MakePhysical()
        {
            _animator.enabled = false;
            foreach (var rigidBody in _allRigidBodies)
            {
                rigidBody.isKinematic = false;
            }
        }

    }
}
